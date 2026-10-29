using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using InventoryManagementApp.BUS;
using InventoryManagementApp.DTO;

namespace frmInventoryManager
{
    public partial class frmInventoryManager : Form
    {
        private readonly InventoryBUS _bus = new InventoryBUS();
        private List<ChiTietPhieuKhoDTO> _voucherDetailsList = new List<ChiTietPhieuKhoDTO>();
        public frmInventoryManager()
        {
            InitializeComponent();
        }

        private void frmInventoryManager_Load(object sender, EventArgs e)
        {
            InitVoucherTypeComboBox();
            LoadSupplierComboBox();
            ConfigureGridCatalog();
            ConfigureGridVoucherDetails();
            LoadVatTuCatalog();
        }
        private void InitVoucherTypeComboBox()
        {
            cboVoucherType.Items.Clear();
            cboVoucherType.Items.Add("NHAP");
            cboVoucherType.Items.Add("XUAT");
            cboVoucherType.SelectedIndex = 0;
        }

        private void LoadSupplierComboBox()
        {
            List<NhaCungCapDTO> suppliers = _bus.GetSuppliers();
            cboSupplier.DataSource = suppliers;
            cboSupplier.DisplayMember = "TenNCC";
            cboSupplier.ValueMember = "MaNCC";
        }

        private void ConfigureGridCatalog()
        {
            dgvVatTuCatalog.AutoGenerateColumns = false;
            dgvVatTuCatalog.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVatTuCatalog.ReadOnly = true;
            dgvVatTuCatalog.MultiSelect = false;
            dgvVatTuCatalog.Columns.Clear();

            dgvVatTuCatalog.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaVatTu", HeaderText = "Mã VT", Width = 80 });
            dgvVatTuCatalog.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenVatTu", HeaderText = "Tên Vật Tư", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvVatTuCatalog.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DVT", HeaderText = "ĐVT", Width = 70 });
            dgvVatTuCatalog.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoLuongTon", HeaderText = "Tồn Kho", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } });
        }

        private void ConfigureGridVoucherDetails()
        {
            dgvVoucherDetails.AutoGenerateColumns = false;
            dgvVoucherDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVoucherDetails.ReadOnly = true;
            dgvVoucherDetails.Columns.Clear();

            dgvVoucherDetails.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaVatTu", HeaderText = "Mã VT", Width = 80 });
            dgvVoucherDetails.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenVatTu", HeaderText = "Tên Vật Tư", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvVoucherDetails.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoLuong", HeaderText = "Số Lượng", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvVoucherDetails.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DonGia", HeaderText = "Đơn Giá", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvVoucherDetails.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ThanhTien", HeaderText = "Thành Tiền", Width = 130, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
        }

        private void LoadVatTuCatalog()
        {
            dgvVatTuCatalog.DataSource = _bus.SearchVatTu(txtSearchVatTu.Text.Trim());
        }

        private void txtSearchVatTu_TextChanged(object sender, EventArgs e)
        {
            LoadVatTuCatalog();
        }

        private void cboVoucherType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isNhap = cboVoucherType.SelectedItem.ToString() == "NHAP";
            cboSupplier.Enabled = isNhap;
        }

        private void txtSearchVatTu_TextChanged_1(object sender, EventArgs e)
        {
            LoadVatTuCatalog();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (dgvVatTuCatalog.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một vật tư từ bảng danh mục!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtUnitPrice.Text.Trim(), out decimal unitPrice) || unitPrice < 0)
            {
                MessageBox.Show("Đơn giá vật tư phải là số nguyên dương!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView row = dgvVatTuCatalog.CurrentRow.DataBoundItem as DataRowView;
            string maVatTu = row["MaVatTu"].ToString();
            string tenVatTu = row["TenVatTu"].ToString();
            decimal quantity = nudQuantity.Value;

            // Kiểm tra xem vật tư đã có trong danh sách tạm chưa
            var existingItem = _voucherDetailsList.Find(x => x.MaVatTu == maVatTu);
            if (existingItem != null)
            {
                existingItem.SoLuong += quantity;
                existingItem.DonGia = unitPrice;
            }
            else
            {
                _voucherDetailsList.Add(new ChiTietPhieuKhoDTO
                {
                    MaVatTu = maVatTu,
                    TenVatTu = tenVatTu,
                    SoLuong = quantity,
                    DonGia = unitPrice
                });
            }

            RefreshVoucherGrid();
        }
        private void RefreshVoucherGrid()
        {
            dgvVoucherDetails.DataSource = null;
            dgvVoucherDetails.DataSource = _voucherDetailsList;

            decimal total = 0;
            foreach (var item in _voucherDetailsList) total += item.ThanhTien;
            lblTotalVoucherAmount.Text = string.Format("Tổng tiền phiếu: {0:N0} VNĐ", total);
        }

        private void btnSaveVoucher_Click(object sender, EventArgs e)
        {
            string voucherType = cboVoucherType.SelectedItem.ToString();
            int? supplierId = voucherType == "NHAP" ? (int?)cboSupplier.SelectedValue : null;

            PhieuKhoDTO phieu = new PhieuKhoDTO
            {
                LoaiPhieu = voucherType,
                MaNCC = supplierId,
                NgayLap = DateTime.Now
            };

            if (_bus.CreateVoucher(phieu, _voucherDetailsList, out string error))
            {
                MessageBox.Show(string.Format("Lưu {0} KHO thành công! CSDL và tồn kho đã cập nhật đồng bộ.",
                                              voucherType == "NHAP" ? "PHIẾU NHẬP" : "PHIẾU XUẤT"),
                                "Thông báo Giao dịch", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _voucherDetailsList.Clear();
                RefreshVoucherGrid();
                LoadVatTuCatalog(); // Tải lại bảng danh mục để cập nhật SoLuongTon mới
            }
            else
            {
                MessageBox.Show("Giao dịch thất bại! " + error, "Lỗi SqlTransaction", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFilterLowStock_Click(object sender, EventArgs e)
        {
            decimal threshold = nudSafetyThreshold.Value;
            DataTable dtLowStock = _bus.GetLowStockVatTu(threshold);

            dgvVatTuCatalog.DataSource = dtLowStock;
            MessageBox.Show(string.Format("Tìm thấy {0} vật tư có số lượng tồn kho <= {1}!", dtLowStock.Rows.Count, threshold),
                            "Cảnh báo tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnTotalValuation_Click(object sender, EventArgs e)
        {
            decimal totalQty = _bus.GetTotalInventoryQuantity();
            MessageBox.Show(string.Format("📊 TỔNG SỐ LƯỢNG TỒN KHO TOÀN HỆ THỐNG: {0:N2}", totalQty),
                            "Báo cáo Tồn Kho (ExecuteScalar)", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
