using System.Collections.Generic;
using DAL;
using DTO;

namespace BLL
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
        //Phan nay them vao de ComBoBox hiện danh sách mã nhân viên (Long)
        public List<string> GetAllMaNhanVien()
        {
            return dal.GetAllMaNhanVien();  // Gọi DAL để lấy Mã Nhân Viên
        }
    }
}
