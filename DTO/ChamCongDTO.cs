using System;

public class ChamCongDTO
{
    public string MaCC { get; set; }
    public string MaNV { get; set; }
    public string TenNV { get; set; }
    public DateTime NgayCC { get; set; }
    public TimeSpan? TGVao { get; set; }
    public TimeSpan? TGRa { get; set; }
    public TimeSpan? TGVaoTangCa { get; set; }
    public TimeSpan? TGRaTangCa { get; set; }
    public string TrangThai { get; set; }
}
