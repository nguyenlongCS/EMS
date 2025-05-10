using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class NhanVienDAL
    {
        private string connectionString = "Server=LONG_ACER\\SQLEXPRESS;Database=QL_NhanVien;Integrated Security=True;";

        public List<NhanVienDTO> GetAllNhanVien()
        {
            List<NhanVienDTO> danhSach = new List<NhanVienDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM NhanVien";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        danhSach.Add(new NhanVienDTO
                        {
                            MaNV = reader["MaNV"].ToString(),
                            HoNV = reader["HoNV"].ToString(),
                            TenNV = reader["TenNV"].ToString(),
                            DiaChi = reader["DiaChi"].ToString(),
                            SoDT = reader["SoDT"].ToString(),
                            Email = reader["Email"].ToString(),
                            NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                            GioiTinh = Convert.ToBoolean(reader["GioiTinh"]),
                            CCCD = reader["CCCD"].ToString(),
                            ChucVu = reader["ChucVu"].ToString(),
                            MaPB = reader["MaPB"].ToString()
                        });
                    }
                }
            }
            return danhSach;
        }

        public bool InsertNhanVien(NhanVienDTO nv)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO NhanVien (MaNV, HoNV, TenNV, DiaChi, SoDT, Email, NgaySinh, GioiTinh, CCCD, ChucVu, MaPB) 
                                 VALUES (@MaNV, @HoNV, @TenNV, @DiaChi, @SoDT, @Email, @NgaySinh, @GioiTinh, @CCCD, @ChucVu, @MaPB)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNV", nv.MaNV);
                    cmd.Parameters.AddWithValue("@HoNV", nv.HoNV);
                    cmd.Parameters.AddWithValue("@TenNV", nv.TenNV);
                    cmd.Parameters.AddWithValue("@DiaChi", nv.DiaChi);
                    cmd.Parameters.AddWithValue("@SoDT", nv.SoDT);
                    cmd.Parameters.AddWithValue("@Email", nv.Email);
                    cmd.Parameters.AddWithValue("@NgaySinh", nv.NgaySinh);
                    cmd.Parameters.AddWithValue("@GioiTinh", nv.GioiTinh);
                    cmd.Parameters.AddWithValue("@CCCD", nv.CCCD);
                    cmd.Parameters.AddWithValue("@ChucVu", nv.ChucVu);
                    cmd.Parameters.AddWithValue("@MaPB", nv.MaPB);

                    return cmd.ExecuteNonQuery() > 0; // Trả về true nếu thêm thành công
                }
            }
        }
        //Phan nay them vao de ComBoBox hiện danh sách mã nhân viên (Long)
        public List<string> GetAllMaNhanVien()
        {
            List<string> maNhanVienList = new List<string>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT MaNV FROM NhanVien";  // Lấy chỉ Mã Nhân Viên
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        maNhanVienList.Add(reader["MaNV"].ToString());  // Chỉ lấy Mã Nhân Viên
                    }
                }
            }
            return maNhanVienList;
        }

    }
}
