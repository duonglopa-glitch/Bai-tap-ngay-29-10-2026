using System;
using System.Data;
using System.Data.SqlClient;
using HotelManagementApp.DTO;
using MySqlConnector;

namespace HotelManagementApp.DAL
{
    public class HotelDAL
    {
        // 1. Tải danh sách phòng lên DataTable (Disconnected Model)
        public DataTable GetAllRooms()
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = "SELECT SoPhong, LoaiPhong, GiaTheoGio, TrangThai FROM Phong";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 2. Tra cứu khách hàng theo CCCD (Sử dụng SqlParameter)
        public KhachHangDTO GetCustomerByCCCD(string cccd)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = "SELECT MaKH, HoTen, CCCD, SoDienThoai FROM KhachHang WHERE CCCD = @CCCD";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@CCCD", SqlDbType.VarChar) { Value = cccd });
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new KhachHangDTO
                            {
                                MaKH = Convert.ToInt32(reader["MaKH"]),
                                HoTen = reader["HoTen"].ToString(),
                                CCCD = reader["CCCD"].ToString(),
                                SoDienThoai = reader["SoDienThoai"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        // 3. Thêm mới khách hàng
        public int InsertCustomer(KhachHangDTO kh)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = @"INSERT INTO KhachHang (HoTen, CCCD, SoDienThoai) 
                                VALUES (@HoTen, @CCCD, @SoDienThoai); 
                                SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@HoTen", SqlDbType.NVarChar) { Value = kh.HoTen });
                    cmd.Parameters.Add(new SqlParameter("@CCCD", SqlDbType.VarChar) { Value = kh.CCCD });
                    cmd.Parameters.Add(new SqlParameter("@SoDienThoai", SqlDbType.VarChar) { Value = (object)kh.SoDienThoai ?? DBNull.Value });
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // 4. Lập Phiếu nhận phòng & Chuyển trạng thái phòng sang "Đang ở"
        public bool CreateBooking(string soPhong, int maKH)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // Chèn phiếu đặt phòng
                    string insertQuery = "INSERT INTO PhieuDatPhong (SoPhong, MaKH, NgayNhanPhong) VALUES (@SoPhong, @MaKH, GETDATE())";
                    using (SqlCommand cmd1 = new SqlCommand(insertQuery, conn, trans))
                    {
                        cmd1.Parameters.AddWithValue("@SoPhong", soPhong);
                        cmd1.Parameters.AddWithValue("@MaKH", maKH);
                        cmd1.ExecuteNonQuery();
                    }

                    // Cập nhật phòng
                    string updateQuery = "UPDATE Phong SET TrangThai = N'Đang ở' WHERE SoPhong = @SoPhong";
                    using (SqlCommand cmd2 = new SqlCommand(updateQuery, conn, trans))
                    {
                        cmd2.Parameters.AddWithValue("@SoPhong", soPhong);
                        cmd2.ExecuteNonQuery();
                    }

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }

        // 5. Lấy Phiếu Đặt Phong đang hoạt động của Phòng
        public PhieuDatPhongDTO GetActiveBookingByRoom(string soPhong)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = "SELECT MaPhieu, SoPhong, MaKH, NgayNhanPhong FROM PhieuDatPhong WHERE SoPhong = @SoPhong AND NgayTraPhong IS NULL";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SoPhong", soPhong);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PhieuDatPhongDTO
                            {
                                MaPhieu = Convert.ToInt32(reader["MaPhieu"]),
                                SoPhong = reader["SoPhong"].ToString(),
                                MaKH = Convert.ToInt32(reader["MaKH"]),
                                NgayNhanPhong = Convert.ToDateTime(reader["NgayNhanPhong"])
                            };
                        }
                    }
                }
            }
            return null;
        }

        // 6. Lấy danh sách Dịch vụ
        public DataTable GetAllServices()
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = "SELECT MaDV, TenDV, DonGia FROM DichVu";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 7. Thêm Dịch vụ sử dụng
        public bool OrderService(int maPhieu, int maDV, int soLuong)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = "INSERT INTO SuDungDichVu (MaPhieu, MaDV, SoLuong, ThoiGian) VALUES (@MaPhieu, @MaDV, @SoLuong, GETDATE())";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                    cmd.Parameters.AddWithValue("@MaDV", maDV);
                    cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // 8. Lấy tổng tiền dịch vụ đã dùng
        public decimal GetTotalServiceCost(int maPhieu)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = @"SELECT ISNULL(SUM(sd.SoLuong * dv.DonGia), 0)
                                FROM SuDungDichVu sd
                                INNER JOIN DichVu dv ON sd.MaDV = dv.MaDV
                                WHERE sd.MaPhieu = @MaPhieu";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                    conn.Open();
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
        }

        // 9. Lấy chi tiết dịch vụ đã dùng dạng DataTable
        public DataTable GetServiceUsageDetails(int maPhieu)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = @"SELECT dv.TenDV, sd.SoLuong, dv.DonGia, (sd.SoLuong * dv.DonGia) AS ThanhTien, sd.ThoiGian
                                FROM SuDungDichVu sd
                                INNER JOIN DichVu dv ON sd.MaDV = dv.MaDV
                                WHERE sd.MaPhieu = @MaPhieu";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@MaPhieu", maPhieu);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 10. Check-out & Xuất hóa đơn (Thực thi SqlTransaction)
        public bool ProcessCheckout(int maPhieu, string soPhong, decimal tongTien)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // Cập nhật phiếu đặt phòng
                    string updatePDP = "UPDATE PhieuDatPhong SET NgayTraPhong = GETDATE(), TongTienThanhToan = @TongTien WHERE MaPhieu = @MaPhieu";
                    using (SqlCommand cmd1 = new SqlCommand(updatePDP, conn, trans))
                    {
                        cmd1.Parameters.AddWithValue("@TongTien", tongTien);
                        cmd1.Parameters.AddWithValue("@MaPhieu", maPhieu);
                        cmd1.ExecuteNonQuery();
                    }

                    // Chuyển trạng thái phòng sang "Đang dọn dẹp"
                    string updatePhong = "UPDATE Phong SET TrangThai = N'Đang dọn dẹp' WHERE SoPhong = @SoPhong";
                    using (SqlCommand cmd2 = new SqlCommand(updatePhong, conn, trans))
                    {
                        cmd2.Parameters.AddWithValue("@SoPhong", soPhong);
                        cmd2.ExecuteNonQuery();
                    }

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }

        // 11. Đổi trạng thái phòng về "Trống" sau khi dọn dẹp xong
        public bool SetRoomCleaned(string soPhong)
        {
            using (SqlConnection conn = DatabaseConnect.GetConnection())
            {
                string query = "UPDATE Phong SET TrangThai = N'Trống' WHERE SoPhong = @SoPhong";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SoPhong", soPhong);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}