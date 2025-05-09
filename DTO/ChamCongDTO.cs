using System;

public class ChamCongDTO
{
    public string MaCC { get; set; } //nchar(10) trong csdl
    public string MaNV { get; set; } //nchar(10)
    public DateTime NgayCC { get; set; }//datetime
    public TimeSpan? TGVao { get; set; }//time(7)
    public TimeSpan? TGRa { get; set; }//time(7)

    // Thêm thuộc tính Vang
    public int Vang { get; set; } // 0: không vắng, 1: vắng, 2: vắng có phép
}
