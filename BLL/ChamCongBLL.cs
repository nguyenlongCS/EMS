using System;
using System.Collections.Generic;
using BTL_LTCSDL.DTO;
using BTL_LTCSDL.DAL;

namespace BTL_LTCSDL.BLL
{
    public class ChamCongBLL
    {
        private ChamCongDAL chamCongDAL = new ChamCongDAL();  // Khởi tạo đối tượng DAL

        // Phương thức lấy chi tiết chấm công của một nhân viên
        public ChamCongDTO GetChamCongChiTiet(string maNV, DateTime ngayCC)
        {
            try
            {
                return chamCongDAL.GetChamCongChiTiet(maNV, ngayCC.Date);  // Pass only the date part
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy chi tiết chấm công: {ex.Message}");
            }
        }



        // Phương thức lấy danh sách chấm công của tất cả nhân viên
        public List<ChamCongDTO> GetDanhSachChamCong()
        {
            try
            {
                return chamCongDAL.GetDanhSachChamCong();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách chấm công: {ex.Message}");
            }
        }

        // Phương thức thêm mới một bản ghi chấm công
        public bool InsertChamCong(ChamCongDTO chamCong)
        {
            try
            {
                return chamCongDAL.InsertChamCong(chamCong);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm chấm công: {ex.Message}");
            }
        }

        // Phương thức cập nhật chấm công
        public bool UpdateChamCong(ChamCongDTO chamCong)
        {
            try
            {
                return chamCongDAL.UpdateChamCong(chamCong);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật chấm công: {ex.Message}");
            }
        }

        // Phương thức xóa chấm công
        public bool DeleteChamCong(string maCC)
        {
            try
            {
                return chamCongDAL.DeleteChamCong(maCC);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa chấm công: {ex.Message}");
            }
        }
        public int GetNextMaCC()
        {
            try
            {
                return chamCongDAL.GetNextMaCC();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy mã chấm công tiếp theo: {ex.Message}");
            }
        }

    }
}
