using System;
using System.Data;
using HotelManagementApp.DAL;
using HotelManagementApp.DTO;

namespace HotelManagementApp.BUS
{
    public class HotelBUS
    {
        private readonly HotelDAL _dal = new HotelDAL();

        public DataTable GetAllRooms() => _dal.GetAllRooms();
        public DataTable GetAllServices() => _dal.GetAllServices();
        public KhachHangDTO GetCustomerByCCCD(string cccd) => _dal.GetCustomerByCCCD(cccd);
        public PhieuDatPhongDTO GetActiveBookingByRoom(string soPhong) => _dal.GetActiveBookingByRoom(soPhong);
        public DataTable GetServiceUsageDetails(int maPhieu) => _dal.GetServiceUsageDetails(maPhieu);
        public bool SetRoomCleaned(string soPhong) => _dal.SetRoomCleaned(soPhong);

        // Nghiệp vụ Check-in
        public bool CheckIn(string soPhong, string hoTen, string cccd, string sdt, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(cccd))
            {
                error = "Họ tên và CCCD không được để trống!";
                return false;
            }

            // Tra cứu hoặc tạo mới khách hàng
            KhachHangDTO kh = _dal.GetCustomerByCCCD(cccd);
            int maKH;

            if (kh == null)
            {
                maKH = _dal.InsertCustomer(new KhachHangDTO(0, hoTen.Trim(), cccd.Trim(), sdt?.Trim()));
            }
            else
            {
                maKH = kh.MaKH;
            }

            return _dal.CreateBooking(soPhong, maKH);
        }

        // Nghiệp vụ Gọi Dịch Vụ
        public bool OrderService(int maPhieu, int maDV, int soLuong, out string error)
        {
            error = string.Empty;
            if (soLuong <= 0)
            {
                error = "Số lượng dịch vụ phải lớn hơn 0!";
                return false;
            }
            return _dal.OrderService(maPhieu, maDV, soLuong);
        }

        // Nghiệp vụ Tính Toán Chi Phí Check-out
        public decimal CalculateCheckoutTotal(PhieuDatPhongDTO pdp, decimal giaTheoGio, out int hoursStayed, out decimal roomCost, out decimal serviceCost)
        {
            TimeSpan timeStayed = DateTime.Now - pdp.NgayNhanPhong;
            // Làm tròn số giờ ở tối thiểu 1 giờ, làm tròn lên theo mốc giờ
            hoursStayed = Math.Max(1, (int)Math.Ceiling(timeStayed.TotalHours));

            roomCost = hoursStayed * giaTheoGio;
            serviceCost = _dal.GetTotalServiceCost(pdp.MaPhieu);

            return roomCost + serviceCost;
        }

        // Nghiệp vụ Check-out
        public bool ProcessCheckout(int maPhieu, string soPhong, decimal tongTien)
        {
            return _dal.ProcessCheckout(maPhieu, soPhong, tongTien);
        }
    }
}