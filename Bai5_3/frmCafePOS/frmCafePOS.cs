using System;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using MySqlConnector;

namespace frmCafePOS
{
    public partial class frmCafePOS : Form
    {
        private readonly string _connectionString = @"Server=LAPTOP-F4JF2AS7\SQLEXPRESS;Database=CafePOSDB;Trusted_Connection=True;TrustServerCertificate=True;";
        private int _selectedTableId = -1;
        private string _selectedTableName = string.Empty;
        public frmCafePOS()
        {
            InitializeComponent();
        }

        private void frmCafePOS_Load(object sender, EventArgs e)
        {
            ConfigureBillGrid();
            LoadCategories();
            RenderTableButtons();
        }
        private void ConfigureBillGrid()
        {
            dgvBillDetails.AutoGenerateColumns = false;
            dgvBillDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBillDetails.ReadOnly = true;
            dgvBillDetails.MultiSelect = false;
            dgvBillDetails.Columns.Clear();

            dgvBillDetails.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenMon", HeaderText = "Tên Món", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvBillDetails.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoLuong", HeaderText = "SL", Width = 60, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvBillDetails.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DonGia", HeaderText = "Đơn Giá", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvBillDetails.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ThanhTien", HeaderText = "Thành Tiền", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
        }

        // Tạo danh sách nút Bàn ăn động trong FlowLayoutPanel
        private void RenderTableButtons()
        {
            flpTableLayout.Controls.Clear();
            string query = "SELECT MaBan, TenBan, TrangThai FROM BanAn";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int tableId = Convert.ToInt32(reader["MaBan"]);
                                string tableName = reader["TenBan"].ToString();
                                string status = reader["TrangThai"].ToString();

                                Button btn = new Button
                                {
                                    Width = 110,
                                    Height = 85,
                                    Text = string.Format("{0}\n({1})", tableName, status),
                                    Tag = new { TableId = tableId, TableName = tableName, Status = status },
                                    Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                                    Margin = new Padding(6)
                                };

                                // Đổi màu sắc nút bàn theo trạng thái
                                if (status == "Có khách")
                                {
                                    btn.BackColor = Color.Salmon;
                                    btn.ForeColor = Color.White;
                                }
                                else
                                {
                                    btn.BackColor = Color.LightGreen;
                                    btn.ForeColor = Color.DarkGreen;
                                }

                                btn.Click += TableButton_Click;
                                flpTableLayout.Controls.Add(btn);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách bàn: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TableButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            dynamic tableInfo = btn.Tag;

            _selectedTableId = tableInfo.TableId;
            _selectedTableName = tableInfo.TableName;

            lblCurrentTable.Text = string.Format("Bàn đang chọn: {0} [{1}]", _selectedTableName, tableInfo.Status);
            LoadActiveBillDetails(_selectedTableId);
        }
        private void LoadCategories()
        {
            string query = "SELECT MaDanhMuc, TenDanhMuc FROM DanhMuc";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboCategory.DataSource = dt;
                    cboCategory.DisplayMember = "TenDanhMuc";
                    cboCategory.ValueMember = "MaDanhMuc";

                    if (cboCategory.Items.Count > 0)
                        cboCategory.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategory.SelectedValue == null || !(cboCategory.SelectedValue is int || int.TryParse(cboCategory.SelectedValue.ToString(), out _)))
                return;

            int categoryId = Convert.ToInt32(cboCategory.SelectedValue);
            string query = "SELECT MaMon, TenMon, DonGia FROM MonAn WHERE MaDanhMuc = @CategoryId";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        cboFood.DataSource = dt;
                        cboFood.DisplayMember = "TenMon";
                        cboFood.ValueMember = "MaMon";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp thực đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadActiveBillDetails(int tableId)
        {
            string query = @"SELECT m.TenMon, ct.SoLuong, ct.DonGia, (ct.SoLuong * ct.DonGia) AS ThanhTien
                             FROM HoaDon h
                             INNER JOIN ChiTietHoaDon ct ON h.MaHoaDon = ct.MaHoaDon
                             INNER JOIN MonAn m ON ct.MaMon = m.MaMon
                             WHERE h.MaBan = @TableId AND h.TrangThai = 0";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TableId", tableId);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dtBill = new DataTable();
                        da.Fill(dtBill);

                        dgvBillDetails.DataSource = dtBill;

                        // Tính tổng tiền hóa đơn tạm
                        decimal totalMoney = 0;
                        foreach (DataRow row in dtBill.Rows)
                        {
                            totalMoney += Convert.ToDecimal(row["ThanhTien"]);
                        }

                        lblTotalMoney.Text = string.Format("Tổng tiền: {0:N0} VNĐ", totalMoney);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp hóa đơn bàn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            if (_selectedTableId <= 0)
            {
                MessageBox.Show("Vui lòng chọn một bàn trên sơ đồ trước khi gọi món!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboFood.SelectedValue == null) return;

            int foodId = Convert.ToInt32(cboFood.SelectedValue);
            int quantity = (int)nudQuantity.Value;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    // 1. Kiểm tra xem bàn đã có hóa đơn chưa thanh toán (TrangThai = 0) hay chưa
                    int unpaidBillId = GetUnpaidBillId(conn, _selectedTableId);

                    // 2. Nếu chưa có hóa đơn -> Khởi tạo Hóa đơn mới và cập nhật trạng thái Bàn = "Có khách"
                    if (unpaidBillId == -1)
                    {
                        string insertBillQuery = "INSERT INTO HoaDon (MaBan, GioVao, TrangThai) VALUES (@TableId, GETDATE(), 0); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmdInsertBill = new SqlCommand(insertBillQuery, conn))
                        {
                            cmdInsertBill.Parameters.AddWithValue("@TableId", _selectedTableId);
                            unpaidBillId = Convert.ToInt32(cmdInsertBill.ExecuteScalar());
                        }

                        // Cập nhật trạng thái Bàn
                        string updateTableQuery = "UPDATE BanAn SET TrangThai = N'Có khách' WHERE MaBan = @TableId";
                        using (SqlCommand cmdUpdateTable = new SqlCommand(updateTableQuery, conn))
                        {
                            cmdUpdateTable.Parameters.AddWithValue("@TableId", _selectedTableId);
                            cmdUpdateTable.ExecuteNonQuery();
                        }
                    }

                    // 3. Lấy đơn giá món hiện tại
                    decimal unitPrice = 0;
                    string getPriceQuery = "SELECT DonGia FROM MonAn WHERE MaMon = @FoodId";
                    using (SqlCommand cmdPrice = new SqlCommand(getPriceQuery, conn))
                    {
                        cmdPrice.Parameters.AddWithValue("@FoodId", foodId);
                        unitPrice = Convert.ToDecimal(cmdPrice.ExecuteScalar());
                    }

                    // 4. Kiểm tra món đã tồn tại trong ChiTietHoaDon chưa: nếu có -> cộng dồn, nếu chưa -> chèn mới
                    string checkDetailQuery = "SELECT SoLuong FROM ChiTietHoaDon WHERE MaHoaDon = @BillId AND MaMon = @FoodId";
                    using (SqlCommand cmdCheckDetail = new SqlCommand(checkDetailQuery, conn))
                    {
                        cmdCheckDetail.Parameters.AddWithValue("@BillId", unpaidBillId);
                        cmdCheckDetail.Parameters.AddWithValue("@FoodId", foodId);
                        object result = cmdCheckDetail.ExecuteScalar();

                        if (result != null)
                        {
                            int currentQty = Convert.ToInt32(result);
                            string updateDetailQuery = "UPDATE ChiTietHoaDon SET SoLuong = @NewQty WHERE MaHoaDon = @BillId AND MaMon = @FoodId";
                            using (SqlCommand cmdUpdateDetail = new SqlCommand(updateDetailQuery, conn))
                            {
                                cmdUpdateDetail.Parameters.AddWithValue("@NewQty", currentQty + quantity);
                                cmdUpdateDetail.Parameters.AddWithValue("@BillId", unpaidBillId);
                                cmdUpdateDetail.Parameters.AddWithValue("@FoodId", foodId);
                                cmdUpdateDetail.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string insertDetailQuery = "INSERT INTO ChiTietHoaDon (MaHoaDon, MaMon, SoLuong, DonGia) VALUES (@BillId, @FoodId, @Qty, @Price)";
                            using (SqlCommand cmdInsertDetail = new SqlCommand(insertDetailQuery, conn))
                            {
                                cmdInsertDetail.Parameters.AddWithValue("@BillId", unpaidBillId);
                                cmdInsertDetail.Parameters.AddWithValue("@FoodId", foodId);
                                cmdInsertDetail.Parameters.AddWithValue("@Qty", quantity);
                                cmdInsertDetail.Parameters.AddWithValue("@Price", unitPrice);
                                cmdInsertDetail.ExecuteNonQuery();
                            }
                        }
                    }
                }

                // Nạp lại giao diện
                RenderTableButtons();
                LoadActiveBillDetails(_selectedTableId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm món: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int GetUnpaidBillId(SqlConnection conn, int tableId)
        {
            string query = "SELECT MaHoaDon FROM HoaDon WHERE MaBan = @TableId AND TrangThai = 0";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TableId", tableId);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (_selectedTableId <= 0)
            {
                MessageBox.Show("Vui lòng chọn bàn cần thanh toán!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                int unpaidBillId = GetUnpaidBillId(conn, _selectedTableId);

                if (unpaidBillId == -1)
                {
                    MessageBox.Show("Bàn này hiện tại đang trống hoặc chưa gọi món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Tính tổng tiền cuối cùng của hóa đơn
                string sumQuery = "SELECT ISNULL(SUM(SoLuong * DonGia), 0) FROM ChiTietHoaDon WHERE MaHoaDon = @BillId";
                decimal totalMoney = 0;
                using (SqlCommand cmdSum = new SqlCommand(sumQuery, conn))
                {
                    cmdSum.Parameters.AddWithValue("@BillId", unpaidBillId);
                    totalMoney = Convert.ToDecimal(cmdSum.ExecuteScalar());
                }

                DialogResult dr = MessageBox.Show(
                    string.Format("Xác nhận thanh toán cho [{0}]?\nTổng tiền thanh toán: {1:N0} VNĐ", _selectedTableName, totalMoney),
                    "Xác nhận thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr != DialogResult.Yes) return;

                // THỰC THI GIAO DỊCH ATOMIC BẰNG SQLTRANSACTION
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    // 1. Cập nhật Hóa đơn: GioRa, TongTien, TrangThai = 1 (Đã thanh toán)
                    string updateBillQuery = @"UPDATE HoaDon 
                                               SET GioRa = GETDATE(), TongTien = @TotalMoney, TrangThai = 1 
                                               WHERE MaHoaDon = @BillId";
                    using (SqlCommand cmdBill = new SqlCommand(updateBillQuery, conn, trans))
                    {
                        cmdBill.Parameters.AddWithValue("@TotalMoney", totalMoney);
                        cmdBill.Parameters.AddWithValue("@BillId", unpaidBillId);
                        cmdBill.ExecuteNonQuery();
                    }

                    // 2. Cập nhật Trạng thái Bàn ăn về N'Trống'
                    string updateTableQuery = "UPDATE BanAn SET TrangThai = N'Trống' WHERE MaBan = @TableId";
                    using (SqlCommand cmdTable = new SqlCommand(updateTableQuery, conn, trans))
                    {
                        cmdTable.Parameters.AddWithValue("@TableId", _selectedTableId);
                        cmdTable.ExecuteNonQuery();
                    }

                    // XÁC NHẬN HOÀN TẤT GIAO DỊCH
                    trans.Commit();

                    MessageBox.Show(string.Format("Thanh toán thành công cho [{0}]!\nTổng tiền: {1:N0} VNĐ", _selectedTableName, totalMoney),
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Làm mới sơ đồ bàn và danh sách món
                    RenderTableButtons();
                    LoadActiveBillDetails(_selectedTableId);
                }
                catch (Exception ex)
                {
                    // NẾU XẢY RA LỖI -> ROLLBACK TOÀN BỘ GIAO DỊCH
                    trans.Rollback();
                    MessageBox.Show("Giao dịch thanh toán thất bại! Đã khôi phục dữ liệu.\nLỗi: " + ex.Message, "Lỗi Transaction", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
