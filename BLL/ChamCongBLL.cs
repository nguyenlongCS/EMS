using System.Collections.Generic;
using BTL_LTCSDL.DTO;
using BTL_LTCSDL.DAL;

namespace BTL_LTCSDL.BLL
{
    public class ChamCongBLL
    {
        private ChamCongDAL dal = new ChamCongDAL();

        public List<ChamCongDTO> GetDanhSachChamCong()
        {
            return dal.GetAllChamCong();
        }

        // Các hàm xử lý logic nâng cao có thể thêm ở đây
    }
}
