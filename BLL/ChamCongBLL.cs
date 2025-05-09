using System.Collections.Generic;

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
        return chamCongDAL.InsertChamCong(chamCong);
    }
}
