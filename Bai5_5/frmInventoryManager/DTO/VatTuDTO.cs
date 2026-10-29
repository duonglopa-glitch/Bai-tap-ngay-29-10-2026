namespace InventoryManagementApp.DTO
{
    public class VatTuDTO
    {
        public string MaVatTu { get; set; }
        public string TenVatTu { get; set; }
        public string DVT { get; set; }
        public decimal SoLuongTon { get; set; }

        public VatTuDTO() { }
        public VatTuDTO(string maVatTu, string tenVatTu, string dvt, decimal soLuongTon)
        {
            MaVatTu = maVatTu;
            TenVatTu = tenVatTu;
            DVT = dvt;
            SoLuongTon = soLuongTon;
        }
    }
}