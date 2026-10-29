namespace InventoryManagementApp.DTO
{
    public class NhaCungCapDTO
    {
        public int MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }

        public NhaCungCapDTO() { }
        public NhaCungCapDTO(int maNCC, string tenNCC, string dienThoai, string diaChi)
        {
            MaNCC = maNCC;
            TenNCC = tenNCC;
            DienThoai = dienThoai;
            DiaChi = diaChi;
        }
    }
}