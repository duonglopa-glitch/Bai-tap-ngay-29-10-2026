using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using InventoryManagementApp.DTO;

namespace InventoryManagementApp.DAL
{
    public class InventoryDAL
    {
        // 1. Tải danh sách Nhà cung cấp
        public List<NhaCungCapDTO> GetSuppliers()
        {
            List<NhaCungCapDTO> list = new List<NhaCungCapDTO>();
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = "SELECT MaNCC, TenNCC, DienThoai, DiaChi FROM NhaCungCap";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new NhaCungCapDTO(
                                Convert.ToInt32(reader["MaNCC"]),
                                reader["TenNCC"].ToString(),
                                reader["DienThoai"]?.ToString(),
                                reader["DiaChi"]?.ToString()
                            ));
                        }
                    }
                }
            }
            return list;
        }

        // 2. Tra cứu Vật tư bằng SqlParameter (Chống SQL Injection 100%)
        public DataTable SearchVatTu(string keyword)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = @"SELECT MaVatTu, TenVatTu, DVT, SoLuongTon 
                                 FROM VatTu 
                                 WHERE MaVatTu LIKE @Keyword OR TenVatTu LIKE @Keyword";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Keyword", SqlDbType.NVarChar) { Value = "%" + keyword + "%" });
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        // 3. THỰC THI GIAO DỊCH LẬP PHIẾU NHẬP/XUẤT KHO (SqlTransaction)
        public bool ExecuteInventoryVoucherTransaction(PhieuKhoDTO phieu, List<ChiTietPhieuKhoDTO> details, out string errorMessage)
        {
            errorMessage = string.Empty;

            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    // BƯỚC 1: Chèn bản ghi vào bảng PhieuKho
                    string insertPhieuQuery = @"INSERT INTO PhieuKho (LoaiPhieu, NgayLap, MaNCC, TongTien) 
                                               VALUES (@LoaiPhieu, GETDATE(), @MaNCC, @TongTien); 
                                               SELECT SCOPE_IDENTITY();";

                    int maPhieuGenerated = 0;
                    using (SqlCommand cmdPhieu = new SqlCommand(insertPhieuQuery, conn, trans))
                    {
                        cmdPhieu.Parameters.Add(new SqlParameter("@LoaiPhieu", SqlDbType.NVarChar) { Value = phieu.LoaiPhieu });
                        cmdPhieu.Parameters.Add(new SqlParameter("@MaNCC", SqlDbType.Int) { Value = (object)phieu.MaNCC ?? DBNull.Value });
                        cmdPhieu.Parameters.Add(new SqlParameter("@TongTien", SqlDbType.Decimal) { Value = phieu.TongTien });

                        maPhieuGenerated = Convert.ToInt32(cmdPhieu.ExecuteScalar());
                    }

                    // BƯỚC 2 & 3: Lặp qua từng vật tư -> Kiểm tra kho & Cập nhật SoLuongTon & Chèn ChiTietPhieuKho
                    foreach (var item in details)
                    {
                        // Kiểm tra nếu là PHIẾU XUẤT -> Kiểm tra tồn kho thực tế
                        if (phieu.LoaiPhieu == "XUAT")
                        {
                            string checkStockQuery = "SELECT SoLuongTon FROM VatTu WHERE MaVatTu = @MaVatTu";
                            decimal currentStock = 0;

                            using (SqlCommand cmdCheck = new SqlCommand(checkStockQuery, conn, trans))
                            {
                                cmdCheck.Parameters.Add(new SqlParameter("@MaVatTu", SqlDbType.VarChar) { Value = item.MaVatTu });
                                object stockObj = cmdCheck.ExecuteScalar();
                                currentStock = stockObj != null ? Convert.ToDecimal(stockObj) : 0;
                            }

                            if (currentStock < item.SoLuong)
                            {
                                // HỦY GIAO DỊCH KHI TỒN KHO KHÔNG ĐỦ
                                trans.Rollback();
                                errorMessage = string.Format("Không đủ hàng trong kho cho vật tư [{0} - {1}]! (Tồn hiện tại: {2}, Yêu cầu xuất: {3})",
                                                            item.MaVatTu, item.TenVatTu, currentStock, item.SoLuong);
                                return false;
                            }
                        }

                        // Chèn bản ghi ChiTietPhieuKho
                        string insertDetailQuery = @"INSERT INTO ChiTietPhieuKho (MaPhieu, MaVatTu, SoLuong, DonGia) 
                                                     VALUES (@MaPhieu, @MaVatTu, @SoLuong, @DonGia)";
                        using (SqlCommand cmdDetail = new SqlCommand(insertDetailQuery, conn, trans))
                        {
                            cmdDetail.Parameters.Add(new SqlParameter("@MaPhieu", SqlDbType.Int) { Value = maPhieuGenerated });
                            cmdDetail.Parameters.Add(new SqlParameter("@MaVatTu", SqlDbType.VarChar) { Value = item.MaVatTu });
                            cmdDetail.Parameters.Add(new SqlParameter("@SoLuong", SqlDbType.Decimal) { Value = item.SoLuong });
                            cmdDetail.Parameters.Add(new SqlParameter("@DonGia", SqlDbType.Decimal) { Value = item.DonGia });

                            cmdDetail.ExecuteNonQuery();
                        }

                        // Cập nhật số lượng tồn kho trong bảng VatTu
                        string updateStockQuery = phieu.LoaiPhieu == "NHAP"
                            ? "UPDATE VatTu SET SoLuongTon = SoLuongTon + @SoLuong WHERE MaVatTu = @MaVatTu"
                            : "UPDATE VatTu SET SoLuongTon = SoLuongTon - @SoLuong WHERE MaVatTu = @MaVatTu";

                        using (SqlCommand cmdUpdate = new SqlCommand(updateStockQuery, conn, trans))
                        {
                            cmdUpdate.Parameters.Add(new SqlParameter("@SoLuong", SqlDbType.Decimal) { Value = item.SoLuong });
                            cmdUpdate.Parameters.Add(new SqlParameter("@MaVatTu", SqlDbType.VarChar) { Value = item.MaVatTu });

                            cmdUpdate.ExecuteNonQuery();
                        }
                    }

                    // BƯỚC 4: Xác nhận hoàn tất giao dịch
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    errorMessage = "Lỗi trong quá trình thực thi giao dịch: " + ex.Message;
                    return false;
                }
            }
        }

        // 4. Cảnh báo Tồn kho dưới ngưỡng an toàn
        public DataTable GetLowStockVatTu(decimal safetyThreshold)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = "SELECT MaVatTu, TenVatTu, DVT, SoLuongTon FROM VatTu WHERE SoLuongTon <= @Threshold";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Threshold", SqlDbType.Decimal) { Value = safetyThreshold });
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        // 5. Tính Tổng Số Lượng Tồn Kho Toàn Hệ Thống (Sử dụng ExecuteScalar)
        public decimal GetTotalInventoryQuantity()
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = "SELECT ISNULL(SUM(SoLuongTon), 0) FROM VatTu";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return Convert.ToDecimal(result);
                }
            }
        }
    }
}