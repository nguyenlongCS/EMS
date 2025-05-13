CREATE DATABASE EMS;
go
USE EMS;
go
CREATE TABLE NhanVien (
    MaNV NVARCHAR(10) PRIMARY KEY,    -- Mã nhân viên
    HoNV NVARCHAR(50),
    TenNV NVARCHAR(50),
    DiaChi NVARCHAR(255),
    SoDT NVARCHAR(10),
    Email NVARCHAR(100),
    NgaySinh DATE,
    GioiTinh BIT,
    CCCD NVARCHAR(12),
    ChucVu NVARCHAR(50),
    MaPB NVARCHAR(10) -- Mã phòng ban (FK)
);
CREATE TABLE Luong (
    MaLuong INT IDENTITY(1,1) PRIMARY KEY,           -- Mã lương
    MaNV NVARCHAR(10),                               -- Mã nhân viên (FK)
    NgayXetLuong DATETIME,
    TroCap INT,
    TamUng DECIMAL(18,2),
    TongNgayLam INT,
    TongNgayNghi INT,
    TongNgayNghiCoPhep INT,
    TongNgayDiTre INT,
    TongNgayVeSom INT,
    MaBacLuong INT,                    -- Mã bậc lương (FK)
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV) ON DELETE CASCADE
);

CREATE TABLE ChamCong (
    MaCC NCHAR(10) PRIMARY KEY,        -- Mã chấm công
    MaNV NVARCHAR(10),                 -- Mã nhân viên (FK)
    NgayCC DATETIME,
    TGVao TIME,
    TGRa TIME,
    Vang INT,
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV) ON DELETE CASCADE
);
CREATE TABLE LuongTheoBac (
	MaBacLuong INT PRIMARY KEY,
	BacLuong int,
	LuongCanBanTheoBac decimal (18,2)
);
ALTER TABLE Luong
ADD CONSTRAINT FK_Luong_MaBacLuong
FOREIGN KEY (MaBacLuong)
REFERENCES LuongTheoBac(MaBacLuong);

CREATE TABLE PhongBan (
    MaPB NVARCHAR(10) PRIMARY KEY,      
    TenPB NVARCHAR(100),                
);
ALTER TABLE NhanVien
ADD CONSTRAINT FK_NhanVien_PhongBan
FOREIGN KEY (MaPB)
REFERENCES PhongBan(MaPB);

CREATE TABLE Login (
    MaLogin NCHAR(10) PRIMARY KEY,     
    Username NCHAR(30),                
    Password NVARCHAR(20)              
);

INSERT INTO Login (MaLogin, Username, Password)
VALUES (N'LI01', N'admin', N'admin123');

INSERT INTO PhongBan (MaPB, TenPB)
VALUES
('PB001', N'Phòng Kinh Doanh'),
('PB002', N'Phòng Nhân Sự'),
('PB003', N'Phòng Kế Toán'),
('PB004', N'Phòng Kỹ Thuật'),
('PB005', N'Phòng Marketing');

INSERT INTO NhanVien (MaNV, HoNV, TenNV, DiaChi, SoDT, Email, NgaySinh, GioiTinh, CCCD, ChucVu, MaPB)
VALUES
('NV001', N'Nguyen', N'An', N'123 ABC', '0123456789', 'an@example.com', '1995-01-01', 1, '012345678901', N'Nhân viên', 'PB001'),
('NV002', N'Tran', N'Binh', N'234 DEF', '0987654321', 'binh@example.com', '1994-02-02', 1, '012345678902', N'Quản Lý', 'PB001'),
('NV003', N'Le', N'Chi', N'345 GHI', '0912345678', 'chi@example.com', '1993-03-03', 0, '012345678903', N'Nhân viên', 'PB002'),
('NV004', N'Pham', N'Duc', N'456 JKL', '0923456789', 'duc@example.com', '1992-04-04', 1, '012345678904', N'Nhân viên', 'PB002'),
('NV005', N'Hoang', N'Em', N'567 MNO', '0934567890', 'em@example.com', '1991-05-05', 0, '012345678905', N'Nhân viên', 'PB003');



INSERT INTO ChamCong (MaCC, MaNV, NgayCC, TGVao, TGRa, Vang) VALUES
('CC001', 'NV001', '2025-05-01', '07:30', '17:11', 0),
('CC002', 'NV002', '2025-05-01', '09:10', '16:46', 0),
('CC003', 'NV003', '2025-05-01', '07:30', '16:43', 0),
('CC004', 'NV004', '2025-05-01', NULL, NULL, 2),
('CC005', 'NV005', '2025-05-01', NULL, NULL, 2),
('CC006', 'NV001', '2025-05-02', '08:11', '17:10', 0),
('CC007', 'NV002', '2025-05-02', '09:58', '17:33', 0),
('CC008', 'NV003', '2025-05-02', NULL, '17:03', 0),
('CC009', 'NV004', '2025-05-02', '07:36', '17:26', 0),
('CC010', 'NV005', '2025-05-02', '09:03', NULL, 0),
('CC011', 'NV001', '2025-05-03', '07:26', '16:25', 0),
('CC012', 'NV002', '2025-05-03', NULL, '17:32', 0),
('CC013', 'NV003', '2025-05-03', NULL, NULL, 2),
('CC014', 'NV004', '2025-05-03', '07:02', '16:45', 0),
('CC015', 'NV005', '2025-05-03', '08:35', NULL, 0),
('CC016', 'NV001', '2025-05-04', '07:26', '16:55', 0),
('CC017', 'NV002', '2025-05-04', NULL, '16:03', 0),
('CC018', 'NV003', '2025-05-04', '08:48', '17:50', 0),
('CC019', 'NV004', '2025-05-04', '08:45', '17:59', 0),
('CC020', 'NV005', '2025-05-04', NULL, NULL, 1),
('CC021', 'NV001', '2025-05-05', '08:31', '17:10', 0),
('CC022', 'NV002', '2025-05-05', '07:48', '16:32', 0),
('CC023', 'NV003', '2025-05-05', '07:10', '17:21', 0),
('CC024', 'NV004', '2025-05-05', '08:00', '16:50', 0),
('CC025', 'NV005', '2025-05-05', '08:09', '16:47', 0),
('CC026', 'NV001', '2025-05-06', '07:47', '17:40', 0),
('CC027', 'NV002', '2025-05-06', NULL, NULL, 2),
('CC028', 'NV003', '2025-05-06', '08:00', '16:00', 0),
('CC029', 'NV004', '2025-05-06', '08:41', '17:33', 0),
('CC030', 'NV005', '2025-05-06', '08:34', '16:01', 0),
('CC031', 'NV001', '2025-05-07', '08:53', '17:47', 0),
('CC032', 'NV002', '2025-05-07', '09:00', '16:56', 0),
('CC033', 'NV003', '2025-05-07', '08:10', '17:20', 0),
('CC034', 'NV004', '2025-05-07', '07:54', '17:27', 0),
('CC035', 'NV005', '2025-05-07', '08:23', '17:15', 0),
('CC036', 'NV001', '2025-05-08', '07:34', '16:57', 0),
('CC037', 'NV002', '2025-05-08', '09:04', '17:45', 0),
('CC038', 'NV003', '2025-05-08', '07:13', '16:11', 0),
('CC039', 'NV004', '2025-05-08', NULL, NULL, 2),
('CC040', 'NV005', '2025-05-08', '07:58', '16:18', 0),
('CC041', 'NV001', '2025-05-09', '08:07', '17:19', 0),
('CC042', 'NV002', '2025-05-09', '07:33', '17:48', 0),
('CC043', 'NV003', '2025-05-09', '09:00', '17:34', 0),
('CC044', 'NV004', '2025-05-09', '07:29', '16:12', 0),
('CC045', 'NV005', '2025-05-09', '08:55', '17:05', 0),
('CC046', 'NV001', '2025-05-10', '08:21', '16:22', 0),
('CC047', 'NV002', '2025-05-10', '08:04', '17:14', 0),
('CC048', 'NV003', '2025-05-10', '07:52', '16:38', 0),
('CC049', 'NV004', '2025-05-10', '07:56', '17:42', 0),
('CC050', 'NV005', '2025-05-10', NULL, NULL, 1),
('CC051', 'NV001', '2025-05-11', '08:46', '17:29', 0),
('CC052', 'NV002', '2025-05-11', '07:44', '16:27', 0),
('CC053', 'NV003', '2025-05-11', '07:41', '16:05', 0),
('CC054', 'NV004', '2025-05-11', '07:58', '17:00', 0),
('CC055', 'NV005', '2025-05-11', '08:28', '16:40', 0),
('CC056', 'NV001', '2025-05-12', NULL, NULL, 2),
('CC057', 'NV002', '2025-05-12', '08:31', '16:30', 0),
('CC058', 'NV003', '2025-05-12', '08:50', '17:12', 0),
('CC059', 'NV004', '2025-05-12', '09:00', '17:23', 0),
('CC060', 'NV005', '2025-05-12', '07:55', '16:34', 0);


INSERT INTO LuongTheoBac (MaBacLuong, BacLuong, LuongCanBanTheoBac)
VALUES 
(1, 1, 200000),
(2, 2, 240000),
(3, 3, 280000),
(4, 4, 320000),
(5, 5, 360000);

INSERT INTO Luong (MaNV, NgayXetLuong, MaBacLuong, TroCap, TamUng)
VALUES 
('NV001', '2025-05-01', 1, 500000, 0),
('NV002', '2025-05-01', 2, 600000, 0),
('NV003', '2025-05-01', 3, 700000, 0),
('NV004', '2025-05-01', 4, 800000, 700000),
('NV005', '2025-05-01', 5, 900000, 600000);

ALTER TABLE Luong
ADD LuongNhanDuoc FLOAT, 
    ThueVAT FLOAT;

CREATE PROCEDURE UpdateLuong2
AS
BEGIN
    -- Cập nhật Tổng Ngày Làm (Vang = 0)
    UPDATE Luong
    SET TongNgayLam = (
        SELECT COUNT(*)
        FROM ChamCong
        WHERE ChamCong.MaNV = Luong.MaNV 
          AND Vang = 0
          AND NgayCC >= Luong.NgayXetLuong
          AND NgayCC < DATEADD(MONTH, 1, Luong.NgayXetLuong)
    );

    -- Cập nhật Tổng Ngày Nghỉ (Vang = 1, 2)
    UPDATE Luong
    SET TongNgayNghi = (
        SELECT COUNT(*)
        FROM ChamCong
        WHERE ChamCong.MaNV = Luong.MaNV 
          AND Vang IN (1, 2)
          AND NgayCC >= Luong.NgayXetLuong
          AND NgayCC < DATEADD(MONTH, 1, Luong.NgayXetLuong)
    );

    -- Cập nhật Tổng Ngày Nghỉ Có Phép (giá trị cố định là 1)
    UPDATE Luong
    SET TongNgayNghiCoPhep = 1;

    -- Cập nhật Tổng Ngày Đi Trễ và Tổng Ngày Về Sớm
    UPDATE Luong
    SET 
        TongNgayDiTre = (
            SELECT COUNT(*)
            FROM ChamCong
            WHERE ChamCong.MaNV = Luong.MaNV
              AND TGVao > '08:00:00'
              AND NgayCC >= Luong.NgayXetLuong
              AND NgayCC < DATEADD(MONTH, 1, Luong.NgayXetLuong)
        ),
        TongNgayVeSom = (
            SELECT COUNT(*)
            FROM ChamCong
            WHERE ChamCong.MaNV = Luong.MaNV
              AND TGRa < '17:00:00'
              AND TGRa IS NOT NULL
              AND NgayCC >= Luong.NgayXetLuong
              AND NgayCC < DATEADD(MONTH, 1, Luong.NgayXetLuong)
        );

    -- Cập nhật Lương Nhận Được và Thuế VAT
    UPDATE L
    SET 
        L.LuongNhanDuoc = 
            (L.TongNgayLam * LT.LuongCanBanTheoBac)
            - ((L.TongNgayDiTre + L.TongNgayVeSom) * LT.LuongCanBanTheoBac * 0.8)
            - L.TamUng
            + L.TroCap,
        L.ThueVAT = 
            ((L.TongNgayLam * LT.LuongCanBanTheoBac)
            - ((L.TongNgayDiTre + L.TongNgayVeSom) * LT.LuongCanBanTheoBac * 0.8)
            - L.TamUng
            + L.TroCap) * 0.1
    FROM Luong L
    JOIN LuongTheoBac LT ON L.MaBacLuong = LT.MaBacLuong;
END;
GO

EXEC UpdateLuong2;
