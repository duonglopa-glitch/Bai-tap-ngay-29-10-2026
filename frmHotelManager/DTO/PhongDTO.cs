namespace HotelManagementApp.DTO
{
    public class PhongDTO
    {
        public string SoPhong { get; set; }
        public string LoaiPhong { get; set; }
        public decimal GiaTheoGio { get; set; }
        public string TrangThai { get; set; }

        public PhongDTO() { }

        public PhongDTO(string soPhong, string loaiPhong, decimal giaTheoGio, string trangThai)
        {
            SoPhong = soPhong;
            LoaiPhong = loaiPhong;
            GiaTheoGio = giaTheoGio;
            TrangThai = trangThai;
        }
    }
}