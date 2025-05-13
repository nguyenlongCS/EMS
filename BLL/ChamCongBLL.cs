using System;
using System.Collections.Generic;
using DTO;

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
    // Xóa chấm công theo mã nhân viên
    public bool DeleteChamCongByMaNV(string maNV)
    {
        return chamCongDAL.DeleteChamCongByMaNV(maNV); // Gọi DAL để xóa bản ghi chấm công của nhân viên
    }
}
