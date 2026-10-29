namespace HotelManagementApp.DTO
{
    public class KhachHangDTO
    {
        public int MaKH { get; set; }
        public string HoTen { get; set; }
        public string CCCD { get; set; }
        public string SoDienThoai { get; set; }

        public KhachHangDTO() { }

        public KhachHangDTO(int maKH, string hoTen, string cccd, string soDienThoai)
        {
            MaKH = maKH;
            HoTen = hoTen;
            CCCD = cccd;
            SoDienThoai = soDienThoai;
        }
    }
}