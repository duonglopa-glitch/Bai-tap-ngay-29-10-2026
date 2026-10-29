using System;
using System.Collections.Generic;
using System.Data;
using InventoryManagementApp.DAL;
using InventoryManagementApp.DTO;

namespace InventoryManagementApp.BUS
{
    public class InventoryBUS
    {
        private readonly InventoryDAL _dal = new InventoryDAL();

        public List<NhaCungCapDTO> GetSuppliers() => _dal.GetSuppliers();

        public DataTable SearchVatTu(string keyword) => _dal.SearchVatTu(keyword ?? string.Empty);

        public DataTable GetLowStockVatTu(decimal threshold) => _dal.GetLowStockVatTu(threshold);

        public decimal GetTotalInventoryQuantity() => _dal.GetTotalInventoryQuantity();

        // Xử lý lập phiếu kho
        public bool CreateVoucher(PhieuKhoDTO phieu, List<ChiTietPhieuKhoDTO> details, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (details == null || details.Count == 0)
            {
                errorMessage = "Danh sách vật tư trong phiếu kho không được để trống!";
                return false;
            }

            if (phieu.LoaiPhieu == "NHAP" && (!phieu.MaNCC.HasValue || phieu.MaNCC.Value <= 0))
            {
                errorMessage = "Phiếu nhập kho bắt buộc phải chọn Nhà cung cấp!";
                return false;
            }

            // Tính toán lại tổng tiền phiếu
            decimal calculatedTotal = 0;
            foreach (var item in details)
            {
                if (item.SoLuong <= 0 || item.DonGia < 0)
                {
                    errorMessage = string.Format("Số lượng hoặc đơn giá của vật tư [{0}] không hợp lệ!", item.MaVatTu);
                    return false;
                }
                calculatedTotal += item.ThanhTien;
            }
            phieu.TongTien = calculatedTotal;

            return _dal.ExecuteInventoryVoucherTransaction(phieu, details, out errorMessage);
        }
    }
}