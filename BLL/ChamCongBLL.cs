using System;
using System.Collections.Generic;
using BTL_LTCSDL.DTO;

public class ChamCongBLL
{
    private ChamCongDAL chamCongDAL = new ChamCongDAL();

    // Lấy danh sách chấm công của nhân viên
    public List<ChamCongDTO> GetDanhSachChamCong()
    {
        return chamCongDAL.GetDanhSachChamCong();
    }

    // Thêm chấm công mới
    public bool InsertChamCong(ChamCongDTO chamCong)
    {
        return chamCongDAL.InsertChamCong(chamCong);
    }

    // Cập nhật chấm công
    public bool UpdateChamCong(ChamCongDTO chamCong)
    {
        return chamCongDAL.UpdateChamCong(chamCong);
    }

    // Lấy mã chấm công cao nhất
    public string GetLastMaCC()
    {
        return chamCongDAL.GetLastMaCC();
    }
}
