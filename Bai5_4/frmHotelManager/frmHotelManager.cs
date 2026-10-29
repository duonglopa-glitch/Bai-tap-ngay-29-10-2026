using System;
using System.Data;
using System.Windows.Forms;
using HotelManagementApp.BUS;
using HotelManagementApp.DTO;
using MySqlConnector;

namespace frmHotelManager
{
    public partial class frmHotelManager : Form
    {
        private readonly HotelBUS _bus = new HotelBUS();
        private DataTable _dtRooms;
        private DataView _dvRooms;

        private string _selectedRoomNo = string.Empty;
        private decimal _selectedRoomRate = 0;
        private string _selectedRoomStatus = string.Empty;
        public frmHotelManager()
        {
            InitializeComponent();
        }

        private void frmHotelManager_Load(object sender, EventArgs e)
        {
            ConfigureRoomGrid();
            ConfigureServiceGrid();
            InitFilterComboBoxes();
            LoadRoomData();
            LoadServiceData();
        }
        private void ConfigureRoomGrid()
        {
            dgvRooms.AutoGenerateColumns = false;
            dgvRooms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRooms.ReadOnly = true;
            dgvRooms.MultiSelect = false;
            dgvRooms.Columns.Clear();

            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoPhong", HeaderText = "Số Phòng", Width = 90 });
            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "LoaiPhong", HeaderText = "Loại Phòng", Width = 110 });
            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "GiaTheoGio", HeaderText = "Giá/Giờ", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TrangThai", HeaderText = "Trạng Thái", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        private void ConfigureServiceGrid()
        {
            dgvServicesUsed.AutoGenerateColumns = false;
            dgvServicesUsed.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvServicesUsed.ReadOnly = true;
            dgvServicesUsed.Columns.Clear();

            dgvServicesUsed.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenDV", HeaderText = "Tên Dịch Vụ", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvServicesUsed.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoLuong", HeaderText = "SL", Width = 60, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvServicesUsed.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DonGia", HeaderText = "Đơn Giá", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvServicesUsed.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ThanhTien", HeaderText = "Thành Tiền", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
        }

        private void InitFilterComboBoxes()
        {
            cboFilterType.Items.Clear();
            cboFilterType.Items.AddRange(new object[] { "Tất cả", "Đơn", "Đôi", "VIP" });
            cboFilterType.SelectedIndex = 0;

            cboFilterStatus.Items.Clear();
            cboFilterStatus.Items.AddRange(new object[] { "Tất cả", "Trống", "Đang ở", "Đang dọn dẹp" });
            cboFilterStatus.SelectedIndex = 0;
        }

        private void LoadRoomData()
        {
            _dtRooms = _bus.GetAllRooms();
            _dvRooms = new DataView(_dtRooms);
            dgvRooms.DataSource = _dvRooms;
        }

        private void LoadServiceData()
        {
            DataTable dtServices = _bus.GetAllServices();
            cboServices.DataSource = dtServices;
            cboServices.DisplayMember = "TenDV";
            cboServices.ValueMember = "MaDV";
        }
        private void ApplyDisconnectedFilter(object sender, EventArgs e)
        {
            if (_dvRooms == null) return;

            string filterType = cboFilterType.SelectedItem.ToString();
            string filterStatus = cboFilterStatus.SelectedItem.ToString();

            string expression = "1=1";

            if (filterType != "Tất cả")
                expression += string.Format(" AND LoaiPhong = '{0}'", filterType);

            if (filterStatus != "Tất cả")
                expression += string.Format(" AND TrangThai = '{0}'", filterStatus);

            _dvRooms.RowFilter = expression;
        }

        private void dgvRooms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRooms.CurrentRow != null && dgvRooms.CurrentRow.DataBoundItem is DataRowView drv)
            {
                _selectedRoomNo = drv["SoPhong"].ToString();
                _selectedRoomRate = Convert.ToDecimal(drv["GiaTheoGio"]);
                _selectedRoomStatus = drv["TrangThai"].ToString();

                lblCurrentRoomInfo.Text = string.Format("Phòng đang chọn: {0} ({1}) - [{2}]", _selectedRoomNo, drv["LoaiPhong"], _selectedRoomStatus);

                // Cập nhật giao diện phụ thuộc trạng thái phòng
                btnCheckIn.Enabled = (_selectedRoomStatus == "Trống");
                btnOrderService.Enabled = (_selectedRoomStatus == "Đang ở");
                btnCheckOut.Enabled = (_selectedRoomStatus == "Đang ở");
                btnCleanDone.Enabled = (_selectedRoomStatus == "Đang dọn dẹp");

                LoadRoomServiceDetails();
            }
        }
        private void txtCCCD_Leave(object sender, EventArgs e)
        {
            string cccd = txtCCCD.Text.Trim();
            if (!string.IsNullOrEmpty(cccd))
            {
                KhachHangDTO kh = _bus.GetCustomerByCCCD(cccd);
                if (kh != null)
                {
                    txtCustomerName.Text = kh.HoTen;
                    txtPhone.Text = kh.SoDienThoai;
                }
            }
        }

        private void LoadRoomServiceDetails()
        {
            if (_selectedRoomStatus == "Đang ở")
            {
                PhieuDatPhongDTO pdp = _bus.GetActiveBookingByRoom(_selectedRoomNo);
                if (pdp != null)
                {
                    dgvServicesUsed.DataSource = _bus.GetServiceUsageDetails(pdp.MaPhieu);
                    return;
                }
            }
            dgvServicesUsed.DataSource = null;
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedRoomNo) || _selectedRoomStatus != "Trống")
            {
                MessageBox.Show("Vui lòng chọn một phòng đang TRỐNG!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_bus.CheckIn(_selectedRoomNo, txtCustomerName.Text, txtCCCD.Text, txtPhone.Text, out string err))
            {
                MessageBox.Show(string.Format("Check-in thành công phòng [{0}]!", _selectedRoomNo), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearCustomerInputs();
                LoadRoomData();
            }
            else
            {
                MessageBox.Show("Lỗi Check-in: " + err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOrderService_Click(object sender, EventArgs e)
        {
            if (_selectedRoomStatus != "Đang ở") return;

            PhieuDatPhongDTO pdp = _bus.GetActiveBookingByRoom(_selectedRoomNo);
            if (pdp == null) return;

            int serviceId = Convert.ToInt32(cboServices.SelectedValue);
            int qty = (int)nudServiceQty.Value;

            if (_bus.OrderService(pdp.MaPhieu, serviceId, qty, out string err))
            {
                MessageBox.Show("Thêm dịch vụ cho phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRoomServiceDetails();
            }
            else
            {
                MessageBox.Show("Lỗi thêm dịch vụ: " + err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (_selectedRoomStatus != "Đang ở") return;

            PhieuDatPhongDTO pdp = _bus.GetActiveBookingByRoom(_selectedRoomNo);
            if (pdp == null) return;

            decimal totalMoney = _bus.CalculateCheckoutTotal(pdp, _selectedRoomRate, out int hours, out decimal roomCost, out decimal serviceCost);

            string invoiceDetail = string.Format(
                "🧾 HÓA ĐƠN THANH TOÁN PHÒNG [{0}]\n\n" +
                "⏱ Thời gian nhận: {1:dd/MM/yyyy HH:mm}\n" +
                "⏱ Thời gian ở: {2} giờ\n" +
                "💵 Tiền phòng: {3:N0} VNĐ\n" +
                "🥤 Tiền dịch vụ: {4:N0} VNĐ\n" +
                "─────────────────────────\n" +
                "💰 TỔNG THANH TOÁN: {5:N0} VNĐ\n\n" +
                "Xác nhận thanh toán và Check-out?",
                _selectedRoomNo, pdp.NgayNhanPhong, hours, roomCost, serviceCost, totalMoney);

            DialogResult dr = MessageBox.Show(invoiceDetail, "Xác nhận Check-out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                if (_bus.ProcessCheckout(pdp.MaPhieu, _selectedRoomNo, totalMoney))
                {
                    MessageBox.Show(string.Format("Check-out phòng [{0}] thành công! Trạng thái phòng đã chuyển sang 'Đang dọn dẹp'.", _selectedRoomNo),
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRoomData();
                }
                else
                {
                    MessageBox.Show("Lỗi trong quá trình thanh toán!", "Lỗi Transaction", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCleanDone_Click(object sender, EventArgs e)
        {
            if (_selectedRoomStatus != "Đang dọn dẹp") return;

            if (_bus.SetRoomCleaned(_selectedRoomNo))
            {
                MessageBox.Show(string.Format("Phòng [{0}] đã dọn dẹp xong và sẵn sàng đón khách!", _selectedRoomNo), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRoomData();
            }
        }
        private void ClearCustomerInputs()
        {
            txtCCCD.Clear();
            txtCustomerName.Clear();
            txtPhone.Clear();
        }
    }
}
