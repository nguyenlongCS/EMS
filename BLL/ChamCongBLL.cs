using System;
using System.Collections.Generic;
using BTL_LTCSDL.DTO;
using BTL_LTCSDL.DAL;

namespace BTL_LTCSDL.BLL
{
    public class ChamCongBLL
    {
        private ChamCongDAL dal = new ChamCongDAL();

        // Lấy danh sách tất cả chấm công
        public List<ChamCongDTO> GetDanhSachChamCong()
        {
            return dal.GetAllChamCong();
        }

        // Lấy chấm công theo ngày của một nhân viên
        public ChamCongDTO GetChamCongTheoNgay(string maNV, DateTime ngayCC)
        {
            return dal.GetChamCongTheoNgay(maNV, ngayCC);
        }

        // Thêm chấm công mới
        public bool InsertChamCong(ChamCongDTO chamCong)
        {
            return dal.InsertChamCong(chamCong);
        }

        // Cập nhật chấm công (giờ vào, giờ ra, trạng thái)
        public bool UpdateChamCong(ChamCongDTO chamCong)
        {
            return dal.UpdateChamCong(chamCong);
        }
    }
}
