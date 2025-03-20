using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTL_LTCSDL.DAL;
using BTL_LTCSDL.DTO;

namespace BTL_LTCSDL.BLL
{
    public class NhanVienBLL
    {
        private NhanVienDAL dal = new NhanVienDAL();

        public List<NhanVienDTO> GetAllNhanVien()
        {
            return dal.GetAllNhanVien();
        }

        public bool InsertNhanVien(NhanVienDTO nv)
        {
            // Kiểm tra logic trước khi thêm
            if (string.IsNullOrWhiteSpace(nv.MaNV) || string.IsNullOrWhiteSpace(nv.TenNV))
                return false;

            return dal.InsertNhanVien(nv);
        }
    }
}
