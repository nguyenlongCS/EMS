use QL_NhanVien


go
CREATE PROCEDURE UpdateLuong
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


DELETE FROM Luong;

-- Dữ liệu lương ngày xét lương 01/05/2025
INSERT INTO Luong (MaNV, NgayXetLuong, MaBacLuong, TroCap, TamUng)
VALUES 
('NV001', '2025-05-01', 2, 500000, 1000000),
('NV002', '2025-05-01', 3, 600000, 900000),
('NV003', '2025-05-01', 4, 700000, 800000),
('NV004', '2025-05-01', 5, 800000, 700000),
('NV005', '2025-05-01', 1, 900000, 600000);

EXEC UpdateLuong;