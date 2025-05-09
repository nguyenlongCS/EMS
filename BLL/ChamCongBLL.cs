using System;
using System.Collections.Generic;
using System.Data;
using BTL_LTCSDL.DAL;
using BTL_LTCSDL.DTO;

public class ChamCongBLL
{
    private ChamCongDAL chamCongDAL = new ChamCongDAL();

    // Lấy danh sách chấm công
    public List<ChamCongDTO> GetDanhSachChamCong()
    {
        return chamCongDAL.GetDanhSachChamCong();
    }

    // Thêm chấm công mới
    public bool InsertChamCong(ChamCongDTO chamCong)
    {
        // Kiểm tra logic trước khi thêm
        if (string.IsNullOrWhiteSpace(chamCong.MaCC) || string.IsNullOrWhiteSpace(chamCong.MaNV))
            return false;

        return chamCongDAL.InsertChamCong(chamCong);
    }
}
