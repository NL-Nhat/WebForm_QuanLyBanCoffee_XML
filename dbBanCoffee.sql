-- Kiem tra xem database da ton tai hay chua, ton tai thi xoa
IF EXISTS (SELECT * FROM sys.databases WHERE name = N'QuanLyBanCaPhe')
BEGIN
    -- Dong tat ca cac ket noi den co so du lieu
    EXECUTE sp_MSforeachdb 'IF ''?'' = ''QuanLyBanCaPhe''
    BEGIN
        DECLARE @sql AS NVARCHAR(MAX) = ''USE [?]; ALTER DATABASE [?] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;''
        EXEC (@sql)
    END'
    -- Xoa tat ca cac ket noi toi co so du lieu (thuc hien qua he thong master)
    USE MASTER
    -- Xoa co so du lieu neu ton tai
    DROP DATABASE QuanLyBanCaPhe
END

CREATE DATABASE QuanLyBanCaPhe;
GO

USE QuanLyBanCaPhe;
GO

CREATE TABLE TAIKHOAN (
    MaTaiKhoan INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
    MatKhau NVARCHAR(100) NOT NULL,
    LoaiTaiKhoan NVARCHAR(20) CHECK (LoaiTaiKhoan IN (N'Admin', N'Nhân viên')) NOT NULL default N'Nhân viên',
    TrangThai BIT DEFAULT 1 CHECK (TrangThai IN (0, 1))
);
CREATE TABLE NHANVIEN (
    MaNhanVien INT IDENTITY(1,1) PRIMARY KEY,
    TenNhanVien NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(10),
    NgaySinh DATE,
    SDT NVARCHAR(15),
    DiaChi NVARCHAR(200),
    MaTaiKhoan INT UNIQUE,
    FOREIGN KEY (MaTaiKhoan) REFERENCES TAIKHOAN(MaTaiKhoan) on delete cascade on update cascade
);
CREATE TABLE DANHMUC (
    MaDanhMuc INT IDENTITY(1,1) PRIMARY KEY,
    TenDanhMuc NVARCHAR(100) NOT NULL
);
CREATE TABLE SANPHAM (
    MaSanPham INT IDENTITY(1,1) PRIMARY KEY,
    TenSanPham NVARCHAR(100) NOT NULL,
    DonGia DECIMAL(18,2) CHECK (DonGia >= 0) default 0 not null,
    MaDanhMuc INT NOT NULL,
    HinhAnhURL NVARCHAR(255), -- đường dẫn ảnh sản phẩm
    FOREIGN KEY (MaDanhMuc) REFERENCES DANHMUC(MaDanhMuc) on delete cascade on update cascade
);
CREATE TABLE TANG (
    MaTang INT IDENTITY(1,1) PRIMARY KEY,
    TenTang NVARCHAR(50) NOT NULL
);
CREATE TABLE BAN (
    MaBan INT IDENTITY(1,1) PRIMARY KEY,
    TenBan NVARCHAR(50) NOT NULL,
    MaTang INT NOT NULL,
    TrangThai NVARCHAR(50) CHECK (TrangThai IN (N'Trống', N'Có khách', N'Đang dọn')) DEFAULT N'Trống',
    FOREIGN KEY (MaTang) REFERENCES TANG(MaTang) on delete cascade on update cascade
);
CREATE TABLE ODER (
    MaOder INT IDENTITY(1,1) PRIMARY KEY,
    MaBan INT NOT NULL,
    MaNhanVien INT NOT NULL,
    ThoiGianBatDau DATETIME DEFAULT GETDATE(),
    ThoiGianThanhToan DATETIME,
    ChietKhau float default 0 check (ChietKhau in (0, 0.1, 0.2, 0.5, 1)),
    TongTien DECIMAL(18,2) DEFAULT 0,
    TrangThai NVARCHAR(50) CHECK (TrangThai IN (N'Chưa thanh toán', N'Đã thanh toán', N'Đã huỷ')) DEFAULT N'Chưa thanh toán',
    FOREIGN KEY (MaBan) REFERENCES BAN(MaBan) on delete cascade on update cascade,
    FOREIGN KEY (MaNhanVien) REFERENCES NHANVIEN(MaNhanVien) on delete cascade on update cascade
);
CREATE TABLE CHITIETODER (
    MaCTOder INT IDENTITY(1,1) PRIMARY KEY,
    MaOder INT NOT NULL,
    MaSanPham INT NOT NULL,
    SoLuong INT CHECK (SoLuong > 0) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL,
    TrangThai NVARCHAR(50) CHECK (TrangThai IN (N'Chưa thanh toán', N'Đã thanh toán', N'Đã huỷ')) DEFAULT N'Chưa thanh toán',
    FOREIGN KEY (MaOder) REFERENCES ODER(MaOder) on delete cascade on update cascade,
    FOREIGN KEY (MaSanPham) REFERENCES SANPHAM(MaSanPham) on delete cascade on update cascade
);
CREATE TABLE HUYMON (
    MaHuy INT IDENTITY(1,1) PRIMARY KEY,
    MaOder INT NOT NULL,
    MaSanPham INT NOT NULL,
    SoLuong int not null default 1 check(SoLuong >0),
    LyDo NVARCHAR(200),
    ThoiGianHuy DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MaOder) REFERENCES ODER(MaOder) on delete cascade on update cascade,
    FOREIGN KEY (MaSanPham) REFERENCES SANPHAM(MaSanPham) on delete cascade on update cascade
);
-- ==============================
-- DỮ LIỆU MẪU
-- ==============================

-- Tầng
INSERT INTO TANG (TenTang) VALUES (N'Tầng 1'), (N'Tầng 2');

-- Bàn: mỗi tầng 10 bàn
DECLARE @i INT = 1;
WHILE @i <= 10
BEGIN
    INSERT INTO BAN (TenBan, MaTang) VALUES (N'Bàn ' + CAST(@i AS NVARCHAR(10)), 1);
    SET @i += 1;
END;

SET @i = 1;
WHILE @i <= 10
BEGIN
    INSERT INTO BAN (TenBan, MaTang) VALUES (N'Bàn ' + CAST(@i AS NVARCHAR(10)), 2);
    SET @i += 1;
END;

-- Danh mục
INSERT INTO DANHMUC (TenDanhMuc)
VALUES (N'Cà phê'), (N'Sinh tố'), (N'Matcha'), (N'Khác');

-- 2. Chèn lại dữ liệu sản phẩm với tên file ảnh
-- Lưu ý: Giá trị HinhAnhURL chỉ là tên file ảnh (ví dụ: DM1_SP1.jpg)

-- Danh mục 1: Cà phê (MaDanhMuc = 1)
INSERT INTO SANPHAM (TenSanPham, DonGia, MaDanhMuc, HinhAnhURL) VALUES
(N'Cà phê đen đá', 25000.00, 1, N'denda.jpg'),
(N'Cà phê sữa đá', 28000.00, 1, N'suada.jpg'),
(N'Americano nóng', 35000.00, 1, N'ame.jpg'),
(N'Latte', 40000.00, 1, N'latte.jpg'),
(N'Cappuccino', 40000.00, 1, N'cappu.jpg'),
(N'Espresso', 30000.00, 1, N'espres.jpg'),
(N'Bạc xỉu', 32000.00, 1, N'bacsiu.jpg'),
(N'Mocha', 45000.00, 1, N'mocha.jpg'),
(N'Cold Brew', 42000.00, 1, N'cold.jpg'),
(N'Cà phê trứng', 48000.00, 1, N'trung.jpg');

-- Danh mục 2: Sinh tố (MaDanhMuc = 2)
INSERT INTO SANPHAM (TenSanPham, DonGia, MaDanhMuc, HinhAnhURL) VALUES
(N'Sinh tố bơ', 45000.00, 2, N'bo.jpg'),
(N'Sinh tố xoài', 42000.00, 2, N'xoai.jpg'),
(N'Sinh tố dâu', 48000.00, 2, N'dau.jpg'),
(N'Sinh tố mãng cầu', 40000.00, 2, N'mangcau.jpg'),
(N'Sinh tố chuối yến mạch', 50000.00, 2, N'chuoi.jpg'),
(N'Sinh tố cam cà rốt', 42000.00, 2, N'carot.jpg'),
(N'Sinh tố việt quất', 52000.00, 2, N'vietquat.jpg'),
(N'Sinh tố rau má', 38000.00, 2, N'rauma.jpg'),
(N'Sinh tố sapoche', 45000.00, 2, N'sapoche.jpg'),
(N'Sinh tố thập cẩm', 55000.00, 2, N'thapcam.jpg');

-- Danh mục 3: Matcha (MaDanhMuc = 3)
INSERT INTO SANPHAM (TenSanPham, DonGia, MaDanhMuc, HinhAnhURL) VALUES
(N'Trà sữa Matcha', 40000.00, 3, N'matcha.jpg'),
(N'Matcha đá xay', 48000.00, 3, N'daxay.jpg'),
(N'Latte Matcha nóng', 45000.00, 3, N'matchanong.jpg'),
(N'Matcha nguyên chất', 38000.00, 3, N'nguyenchat.jpg'),
(N'Matcha dâu', 50000.00, 3, N'matchadau.jpg'),
(N'Matcha kem phô mai', 55000.00, 3, N'phomai.jpg'),
(N'Matcha đậu đỏ', 42000.00, 3, N'daudo.jpg'),
(N'Matcha cốt dừa', 48000.00, 3, N'cotdua.jpg'),
(N'Matcha trà xanh', 35000.00, 3, N'xanh.jpg'),
(N'Matcha kem tươi', 50000.00, 3, N'kemtuoi.jpg');

-- Danh mục 4: Khác (MaDanhMuc = 4)
INSERT INTO SANPHAM (TenSanPham, DonGia, MaDanhMuc, HinhAnhURL) VALUES
(N'Trà đào cam sả', 38000.00, 4, N'camsa.jpg'),
(N'Trà vải', 35000.00, 4, N'vai.jpg'),
(N'Sô cô la nóng', 45000.00, 4, N'socola.jpg'),
(N'Nước ép cam', 40000.00, 4, N'cam.jpg'),
(N'Sữa tươi trân châu đường đen', 45000.00, 4, N'tranchau.jpg'),
(N'Soda việt quất', 42000.00, 4, N'sodavq.jpg'),
(N'Bánh Tiramisu', 55000.00, 4, N'tira.jpg'),
(N'Bánh Red Velvet', 50000.00, 4, N'red.jpg'),
(N'Bia thủ công', 60000.00, 4, N'bia.jpg'),
(N'Nước suối', 15000.00, 4, N'suoi.jpg');

-- Tài khoản + Nhân viên
INSERT INTO TAIKHOAN (TenDangNhap, MatKhau, LoaiTaiKhoan)
VALUES
(N'admin', N'123', N'Admin'),
(N'nv1', N'123', N'Nhân viên'),
(N'nv2', N'123', N'Nhân viên'),
(N'nv3', N'123', N'Nhân viên'),
(N'nv4', N'123', N'Nhân viên'),
(N'nv5', N'123', N'Nhân viên');

INSERT INTO TAIKHOAN (TenDangNhap, MatKhau, LoaiTaiKhoan, TrangThai)
VALUES
(N'nv6', N'123', N'Nhân viên', 0);

INSERT INTO NHANVIEN (TenNhanVien, GioiTinh, NgaySinh, SDT, DiaChi, MaTaiKhoan)
VALUES
(N'Nguyễn Long Nhật', N'Nam', '2005-11-01', N'0909000002', N'Quảng Trị', 1),
(N'Lê Văn A', N'Nam', '1995-05-01', N'0909000002', N'Hà Nội', 2),
(N'Trần Thị B', N'Nữ', '1998-03-10', N'0909000003', N'Hải Phòng', 3),
(N'Phạm Văn C', N'Nam', '1999-07-15', N'0909000004', N'Hà Nam', 4),
(N'Hoàng Thị D', N'Nữ', '2000-11-25', N'0909000005', N'Ninh Bình', 5),
(N'Nguyễn Văn E', N'Nam', '2001-09-20', N'0909000006', N'Nam Định', 6),
(N'Nguyễn Văn F', N'Nam', '2001-09-20', N'0909000006', N'Nam Định', 7);

-- Oder mẫu: 10 dòng, trạng thái khác nhau
INSERT INTO ODER (MaBan, MaNhanVien, ThoiGianBatDau, ThoiGianThanhToan, TongTien, TrangThai)
VALUES
(1, 2, GETDATE(), DATEADD(MINUTE, 30, GETDATE()), 120000, N'Đã thanh toán'),
(2, 3, GETDATE(), DATEADD(MINUTE, 25, GETDATE()), 95000, N'Đã thanh toán'),
(3, 4, GETDATE(), NULL, 0, N'Chưa thanh toán'),
(4, 5, GETDATE(), DATEADD(MINUTE, 15, GETDATE()), 80000, N'Đã thanh toán'),
(5, 1, GETDATE(), NULL, 0, N'Chưa thanh toán'),
(6, 2, GETDATE(), DATEADD(MINUTE, 20, GETDATE()), 65000, N'Đã thanh toán'),
(7, 3, GETDATE(), NULL, 0, N'Đã huỷ'),
(8, 4, GETDATE(), NULL, 0, N'Đã huỷ'),
(9, 5, GETDATE(), DATEADD(MINUTE, 40, GETDATE()), 105000, N'Đã thanh toán'),
(10, 1, GETDATE(), NULL, 0, N'Chưa thanh toán'),
(1, 2, GETDATE(), null, 110000, N'Chưa thanh toán');

-- Chi tiết order mẫu
INSERT INTO CHITIETODER (MaOder, MaSanPham, SoLuong, DonGia)
VALUES
(1, 1, 2, 30000),
(1, 2, 1, 60000),
(2, 5, 1, 45000),
(2, 6, 1, 50000),
(4, 8, 2, 40000),
(6, 9, 1, 65000),
(9, 12, 3, 35000),
(3, 10, 1, 30000),
(5, 13, 2, 40000),
(10, 15, 1, 55000),
(11, 15, 2, 55000)

INSERT INTO HUYMON (MaOder, MaSanPham, LyDo)
VALUES
(7, 5, N'Khách đổi món'),
(8, 9, N'Món pha nhầm');


update ban
set TrangThai = N'Có khách'
where MaBan in (1,3,5,10)

INSERT INTO ODER (MaBan, MaNhanVien, ThoiGianBatDau, ThoiGianThanhToan, TongTien, TrangThai)
VALUES
(11, 2, '2025-11-20 10:00:00', '2025-3-20 10:30:00', 200000, N'Đã thanh toán'),
(12, 2, '2025-11-19 14:15:00', '2025-3-19 14:45:00', 150000, N'Đã thanh toán'), 
(13, 2, '2025-11-18 09:30:00', '2025-4-18 10:05:00', 185000, N'Đã thanh toán'), 
(14, 2, '2025-11-20 16:00:00', '2025-9-20 16:25:00', 90000, N'Đã thanh toán'), 
(15, 2, '2025-11-17 11:00:00', '2025-1-17 11:40:00', 110000, N'Đã thanh toán'),
(16, 2, '2025-11-16 19:00:00', '2025-1-16 19:20:00', 135000, N'Đã thanh toán'),
(17, 3, '2025-11-15 08:45:00', '2025-1-15 09:15:00', 88000, N'Đã thanh toán'),
(18, 3, '2025-11-14 12:30:00', '2025-3-14 13:10:00', 210000, N'Đã thanh toán'),
(19, 3, '2025-11-13 20:00:00', '2025-3-13 20:45:00', 145000, N'Đã thanh toán'),
(20, 4, '2025-11-19 07:00:00', '2025-4-19 07:35:00', 75000, N'Đã thanh toán'), 
(1, 4, '2025-11-20 06:10:00', '2025-4-20 06:40:00', 160000, N'Đã thanh toán'), 
(2, 5, '2025-11-12 15:00:00', '2025-10-12 15:25:00', 125000, N'Đã thanh toán'),
(3, 5, '2025-11-11 17:30:00', '2025-10-11 18:00:00', 100000, N'Đã thanh toán'),
(4, 5, '2025-11-10 10:00:00', '2025-10-10 10:30:00', 195000, N'Đã thanh toán'),
(6, 5, '2025-11-10 14:00:00', '2025-10-10 14:35:00', 130000, N'Đã thanh toán'),
(5, 6, '2025-11-18 21:00:00', '2025-5-18 21:30:00', 170000, N'Đã thanh toán'),
(7, 6, '2025-11-17 18:00:00', '2025-11-17 18:40:00', 99000, N'Đã thanh toán'), 
(8, 6, '2025-2-15 11:30:00', NULL, 0, N'Đã huỷ'),
(9, 2, '2025-2-14 09:00:00', NULL, 0, N'Đã huỷ'), 
(10, 3, '2025-2-13 13:00:00', NULL, 0, N'Đã huỷ');              

INSERT INTO CHITIETODER (MaOder, MaSanPham, SoLuong, DonGia)
VALUES
(12, 1, 3, 30000),
(12, 3, 1, 110000),
(13, 4, 2, 75000), 
(14, 5, 1, 45000),
(14, 7, 2, 70000),
(15, 8, 1, 40000),
(15, 10, 1, 50000),
(16, 11, 1, 110000),
(17, 12, 1, 35000),
(17, 13, 2, 50000), 
(18, 14, 1, 88000), 
(19, 15, 3, 70000),
(20, 1, 2, 72500),
(21, 2, 1, 30000),
(21, 3, 1, 45000), 
(22, 4, 2, 80000), 
(23, 5, 2, 62500), 
(24, 6, 1, 100000), 
(25, 7, 1, 195000),
(26, 8, 2, 65000),
(27, 9, 2, 85000),
(27, 10, 1, 0), 
(28, 11, 1, 99000),
(29, 1, 1, 30000), 
(30, 2, 2, 60000),
(31, 3, 1, 110000);

INSERT INTO HUYMON (MaOder, MaSanPham, LyDo, ThoiGianHuy)
VALUES
(28, 11, N'Khách đổi món', '2025-3-13 13:00:00'),
(29, 1, N'Khách đổi món', '2025-3-13 13:00:00'),
(30, 2, N'Khách đổi món', '2025-6-13 13:00:00');

-- Cập nhật trạng thái trong CHITIETODER dựa trên trạng thái của ODER

UPDATE CTO
SET CTO.TrangThai = O.TrangThai  -- Lấy trạng thái từ bảng ODER
FROM CHITIETODER CTO
INNER JOIN ODER O ON CTO.MaOder = O.MaOder
WHERE O.TrangThai = N'Đã huỷ' or O.TrangThai = N'Đã thanh toán' ;


select *from ODER
select *from CHITIETODER
select *from SANPHAM
select *from BAN
select *from DANHMUC    
select *from TAIKHOAN
select *from NHANVIEN
select *from HUYMON