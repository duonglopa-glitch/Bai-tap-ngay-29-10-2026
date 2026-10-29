IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'InventoryDB')
BEGIN
    CREATE DATABASE InventoryDB;
END
GO

USE InventoryDB;
GO

-- 1. Bảng Nhà cung cấp (NhaCungCap)
IF OBJECT_ID('dbo.NhaCungCap', 'U') IS NULL
BEGIN
    CREATE TABLE NhaCungCap (
        MaNCC INT IDENTITY(1,1) PRIMARY KEY,
        TenNCC NVARCHAR(150) NOT NULL,
        DienThoai VARCHAR(15) UNIQUE NULL,
        DiaChi NVARCHAR(255) NULL
    );
END
GO

-- 2. Bảng Vật tư (VatTu)
IF OBJECT_ID('dbo.VatTu', 'U') IS NULL
BEGIN
    CREATE TABLE VatTu (
        MaVatTu VARCHAR(20) PRIMARY KEY,
        TenVatTu NVARCHAR(150) NOT NULL,
        DVT NVARCHAR(20) NOT NULL,
        SoLuongTon DECIMAL(18,2) NOT NULL DEFAULT 0 CHECK (SoLuongTon >= 0)
    );
END
GO

-- 3. Bảng Phiếu kho (PhieuKho)
IF OBJECT_ID('dbo.PhieuKho', 'U') IS NULL
BEGIN
    CREATE TABLE PhieuKho (
        MaPhieu INT IDENTITY(1,1) PRIMARY KEY,
        LoaiPhieu NVARCHAR(10) NOT NULL CHECK (LoaiPhieu IN ('NHAP', 'XUAT')),
        NgayLap DATETIME NOT NULL DEFAULT GETDATE(),
        MaNCC INT NULL,
        TongTien DECIMAL(18,2) NOT NULL DEFAULT 0,
        CONSTRAINT FK_PhieuKho_NhaCungCap FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC)
    );
END
GO

-- 4. Bảng Chi tiết phiếu kho (ChiTietPhieuKho)
IF OBJECT_ID('dbo.ChiTietPhieuKho', 'U') IS NULL
BEGIN
    CREATE TABLE ChiTietPhieuKho (
        MaPhieu INT NOT NULL,
        MaVatTu VARCHAR(20) NOT NULL,
        SoLuong DECIMAL(18,2) NOT NULL CHECK (SoLuong > 0),
        DonGia DECIMAL(18,2) NOT NULL CHECK (DonGia >= 0),
        PRIMARY KEY (MaPhieu, MaVatTu),
        CONSTRAINT FK_CTPK_PhieuKho FOREIGN KEY (MaPhieu) REFERENCES PhieuKho(MaPhieu),
        CONSTRAINT FK_CTPK_VatTu FOREIGN KEY (MaVatTu) REFERENCES VatTu(MaVatTu)
    );
END
GO

-- CHÈN DỮ LIỆU MẪU INITIAL
IF NOT EXISTS (SELECT 1 FROM NhaCungCap)
BEGIN
    INSERT INTO NhaCungCap (TenNCC, DienThoai, DiaChi) VALUES
    (N'Công ty TNHH Thép Việt Nhật', '02438889999', N'KCN Hà Nội'),
    (N'Tổng Công ty Xi măng Hà Tiên', '02839998888', N'TP. Hồ Chí Minh'),
    (N'Nhà máy Sơn Hòa Bình', '02367776666', N'Đà Nẵng');
END

IF NOT EXISTS (SELECT 1 FROM VatTu)
BEGIN
    INSERT INTO VatTu (MaVatTu, TenVatTu, DVT, SoLuongTon) VALUES
    ('VT01', N'Thép Phi 12 Hòa Phát', N'Tấn', 50.00),
    ('VT02', N'Xi măng Hà Tiên PCB40', N'Bao', 200.00),
    ('VT03', N'Sơn Lót Chống Thấm', N'Thùng', 15.00),
    ('VT04', N'Gạch Nung 2 Lỗ', N'Viên', 5000.00),
    ('VT05', N'Cát Vàng Xây Dựng', N'M3', 8.00); -- Đang ở mức tồn kho thấp
END
GO