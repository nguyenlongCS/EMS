using System;
using System.Collections.Generic;

public class ChamCongBLL
{
    private ChamCongDAL chamCongDAL = new ChamCongDAL();

    public bool CheckIn(ChamCongDTO chamCong)
    {
        // Kiểm tra xem nhân viên đã check-in trong ngày chưa
        List<ChamCongDTO> danhSach = chamCongDAL.GetChamCong();
        foreach (var cc in danhSach)
        {
            if (cc.MaNV == chamCong.MaNV && cc.NgayCC == chamCong.NgayCC)
            {
                return false; // Đã check-in rồi
            }
        }
        return chamCongDAL.CheckIn(chamCong);
    }

    public bool CheckOut(string maNV, DateTime ngayCC, TimeSpan tgRa)
    {
        return chamCongDAL.CheckOut(maNV, ngayCC, tgRa);
    }

    public List<ChamCongDTO> GetChamCong()
    {
        return chamCongDAL.GetChamCong();
    }
}
