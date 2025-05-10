using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public bool InsertNhanVienBLL(NhanVienDTO nv)
        {
            // Kiểm tra logic trước khi thêm
            if (string.IsNullOrWhiteSpace(nv.MaNV) || string.IsNullOrWhiteSpace(nv.TenNV))
                return false;

            return dal.InsertNhanVienDAL(nv);
        }


        public bool DeleteNhanVienBLL(string maNV)
        {

            try
            {
                // Kiểm tra sự tồn tại của nhân viên
                var nhanVien = dal.GetNhanVienByMaNV(maNV);
                if (nhanVien == null)
                {
                    throw new Exception("Nhân viên không tồn tại.");
                }

                // Thực hiện xóa nhân viên
                return dal.DeleteNhanVienDAL(maNV);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi hoặc xử lý ngoại lệ theo nhu cầu
                Console.WriteLine($"Lỗi khi xóa nhân viên: {ex.Message}");
                return false;
            }
        }

        public List<NhanVienDTO> GetNV_SX_TenNV_BLL()
        {
            return dal.GetNV_SX_TenNV_DAL();
        }

        public List<NhanVienDTO> GetNV_SX_MaNV_BLL()
        {
            return dal.GetNV_SX_MaNV_DAL();
        }
        public List<string> GetAllMaNhanVien()
        {
            return dal.GetAllMaNhanVien();  // Gọi DAL để lấy Mã Nhân Viên
        }

        public bool UpdateNhanVien(NhanVienDTO nv)
        {
            try
            {
                return dal.UpdateNhanVienDAL(nv);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                Console.WriteLine($"Lỗi khi cập nhật nhân viên: {ex.Message}");
                return false;
            }
        }
    }
}
