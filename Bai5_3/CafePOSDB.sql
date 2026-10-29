IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'CafePOSDB')
BEGIN
    CREATE DATABASE CafePOSDB;
END
GO

USE CafePOSDB;
GO

IF OBJECT_ID('dbo.BanAn', 'U') IS NULL
BEGIN
    CREATE TABLE BanAn (
        MaBan INT IDENTITY(1,1) PRIMARY KEY,
        TenBan NVARCHAR(50) NOT NULL,
        TrangThai NVARCHAR(20) DEFAULT N'Trống' -- N'Trống' hoặc N'Có khách'
    );
END
GO

IF OBJECT_ID('dbo.DanhMuc', 'U') IS NULL
BEGIN
    CREATE TABLE DanhMuc (
        MaDanhMuc INT IDENTITY(1,1) PRIMARY KEY,
        TenDanhMuc NVARCHAR(100) NOT NULL
    );
END
GO

IF OBJECT_ID('dbo.MonAn', 'U') IS NULL
BEGIN
    CREATE TABLE MonAn (
        MaMon INT IDENTITY(1,1) PRIMARY KEY,
        TenMon NVARCHAR(100) NOT NULL,
        MaDanhMuc INT NOT NULL,
        DonGia DECIMAL(18,2) NOT NULL DEFAULT 0,
        CONSTRAINT FK_MonAn_DanhMuc FOREIGN KEY (MaDanhMuc) REFERENCES DanhMuc(MaDanhMuc)
    );
END
GO

IF OBJECT_ID('dbo.HoaDon', 'U') IS NULL
BEGIN
    CREATE TABLE HoaDon (
        MaHoaDon INT IDENTITY(1,1) PRIMARY KEY,
        MaBan INT NOT NULL,
        GioVao DATETIME NOT NULL DEFAULT GETDATE(),
        GioRa DATETIME NULL,
        TongTien DECIMAL(18,2) DEFAULT 0,
        TrangThai BIT DEFAULT 0, -- 0: Chưa thanh toán, 1: Đã thanh toán
        CONSTRAINT FK_HoaDon_BanAn FOREIGN KEY (MaBan) REFERENCES BanAn(MaBan)
    );
END
GO

IF OBJECT_ID('dbo.ChiTietHoaDon', 'U') IS NULL
BEGIN
    CREATE TABLE ChiTietHoaDon (
        MaHoaDon INT NOT NULL,
        MaMon INT NOT NULL,
        SoLuong INT NOT NULL DEFAULT 1,
        DonGia DECIMAL(18,2) NOT NULL,
        PRIMARY KEY (MaHoaDon, MaMon),
        CONSTRAINT FK_CTHD_HoaDon FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon),
        CONSTRAINT FK_CTHD_MonAn FOREIGN KEY (MaMon) REFERENCES MonAn(MaMon)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM BanAn)
BEGIN
    INSERT INTO BanAn (TenBan, TrangThai) VALUES 
    (N'Bàn 01', N'Trống'), (N'Bàn 02', N'Trống'), (N'Bàn 03', N'Trống'), (N'Bàn 04', N'Trống'),
    (N'Bàn 05', N'Trống'), (N'Bàn 06', N'Trống'), (N'Bàn 07', N'Trống'), (N'Bàn 08', N'Trống'),
    (N'Bàn 09', N'Trống'), (N'Bàn 10', N'Trống');
END

IF NOT EXISTS (SELECT 1 FROM DanhMuc)
BEGIN
    INSERT INTO DanhMuc (TenDanhMuc) VALUES (N'Cà Phê'), (N'Trà Sữa & Trà Trái Cây'), (N'Đồ Ăn Nhẹ');
END

IF NOT EXISTS (SELECT 1 FROM MonAn)
BEGIN
    INSERT INTO MonAn (TenMon, MaDanhMuc, DonGia) VALUES 
    (N'Cà Phê Đen Đá', 1, 25000),
    (N'Cà Phê Sữa Đá', 1, 29000),
    (N'Bạc Xỉu', 1, 32000),
    (N'Trà Sữa Trân Châu Đường Đen', 2, 45000),
    (N'Trà Đào Cam Sả', 2, 39000),
    (N'Bánh Mỳ Tỏi Bơ Pháp', 3, 35000),
    (N'Hướng Dương Vị Dừa', 3, 20000);
END
GO