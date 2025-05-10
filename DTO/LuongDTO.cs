using System;
public class LuongDTO
{
    public string MaNV { get; set; }
    public string TenNV { get; set; }
    public string MaLuong { get; set; }
    public DateTime NgayXetLuong { get; set; }
    public int TongNgayLam { get; set; }
    public int TongNgaynghi { get; set; }
    public int TongNgayNghiCoPhep { get; set; } // Added field for days off with permission
    public int TongNgayDiTre { get; set; }      // Added field for late arrivals
    public int TongNgayVeSom { get; set; }      // Added field for early departures
    public double TroCap { get; set; }          // Trợ cấp
    public double TamUng { get; set; }          // Tạm ứng
}