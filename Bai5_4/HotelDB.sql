IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'HotelDB')
BEGIN
    CREATE DATABASE HotelDB;
END
GO

USE HotelDB;
GO

-- 1. Bảng Phòng (Phong)
IF OBJECT_ID('dbo.Phong', 'U') IS NULL
BEGIN
    CREATE TABLE Phong (
        SoPhong VARCHAR(10) PRIMARY KEY,
        LoaiPhong NVARCHAR(50) NOT NULL, -- "Đơn", "Đôi", "VIP"
        GiaTheoGio DECIMAL(18,2) NOT NULL DEFAULT 0,
        TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Trống' -- "Trống", "Đang ở", "Đang dọn dẹp"
    );
END
GO

-- 2. Bảng Khách hàng (KhachHang)
IF OBJECT_ID('dbo.KhachHang', 'U') IS NULL
BEGIN
    CREATE TABLE KhachHang (
        MaKH INT IDENTITY(1,1) PRIMARY KEY,
        HoTen NVARCHAR(100) NOT NULL,
        CCCD VARCHAR(20) UNIQUE NOT NULL,
        SoDienThoai VARCHAR(15) NULL
    );
END
GO

-- 3. Bảng Dịch vụ (DichVu)
IF OBJECT_ID('dbo.DichVu', 'U') IS NULL
BEGIN
    CREATE TABLE DichVu (
        MaDV INT IDENTITY(1,1) PRIMARY KEY,
        TenDV NVARCHAR(100) NOT NULL,
        DonGia DECIMAL(18,2) NOT NULL DEFAULT 0
    );
END
GO

-- 4. Bảng Phiếu đặt phòng (PhieuDatPhong)
IF OBJECT_ID('dbo.PhieuDatPhong', 'U') IS NULL
BEGIN
    CREATE TABLE PhieuDatPhong (
        MaPhieu INT IDENTITY(1,1) PRIMARY KEY,
        SoPhong VARCHAR(10) NOT NULL,
        MaKH INT NOT NULL,
        NgayNhanPhong DATETIME NOT NULL DEFAULT GETDATE(),
        NgayTraPhong DATETIME NULL,
        TongTienThanhToan DECIMAL(18,2) DEFAULT 0,
        CONSTRAINT FK_PDP_Phong FOREIGN KEY (SoPhong) REFERENCES Phong(SoPhong),
        CONSTRAINT FK_PDP_KhachHang FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
    );
END
GO

-- 5. Bảng Sử dụng dịch vụ (SuDungDichVu)
IF OBJECT_ID('dbo.SuDungDichVu', 'U') IS NULL
BEGIN
    CREATE TABLE SuDungDichVu (
        MaPhieu INT NOT NULL,
        MaDV INT NOT NULL,
        SoLuong INT NOT NULL DEFAULT 1,
        ThoiGian DATETIME NOT NULL DEFAULT GETDATE(),
        PRIMARY KEY (MaPhieu, MaDV, ThoiGian),
        CONSTRAINT FK_SDDV_Phieu FOREIGN KEY (MaPhieu) REFERENCES PhieuDatPhong(MaPhieu),
        CONSTRAINT FK_SDDV_DichVu FOREIGN KEY (MaDV) REFERENCES DichVu(MaDV)
    );
END
GO

-- CHÈN DỮ LIỆU MẪU
IF NOT EXISTS (SELECT 1 FROM Phong)
BEGIN
    INSERT INTO Phong (SoPhong, LoaiPhong, GiaTheoGio, TrangThai) VALUES
    ('P101', N'Đơn', 100000, N'Trống'),
    ('P102', N'Đơn', 100000, N'Trống'),
    ('P201', N'Đôi', 150000, N'Trống'),
    ('P202', N'Đôi', 150000, N'Trống'),
    ('P301', N'VIP', 300000, N'Trống');
END

IF NOT EXISTS (SELECT 1 FROM DichVu)
BEGIN
    INSERT INTO DichVu (TenDV, DonGia) VALUES
    (N'Nước suối Lavie 500ml', 15000),
    (N'Cà phê lon Nescafé', 20000),
    (N'Mì cốc Omachi', 25000),
    (N'Giặt ủi quần áo (Bộ)', 50000);
END
GO