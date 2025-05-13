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

        public List<NhanVienDTO> GetNV_SX_TenNV_DAL()
        {
            var danhSach = GetAllNhanVien();
            return danhSach.OrderBy(nv => nv.TenNV).ThenBy(nv => nv.MaNV).ToList();
        }

        public List<NhanVienDTO> GetNV_SX_MaNV_DAL()
        {
            var danhSach = GetAllNhanVien();
            return danhSach.OrderBy(nv => nv.MaNV).ToList();
        }

        public bool InsertNhanVienDAL(NhanVienDTO nv)
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
        public bool DeleteNhanVienDAL(string maNV)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    maNV = maNV.Trim(); // loại bỏ khoảng trắng thừa nếu có

                    bool coTrongChamCong = false;
                    bool coTrongLuong = false;

                    // Kiểm tra bảng ChamCong
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM ChamCong WHERE MaNV = @MaNV", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        coTrongChamCong = (int)cmd.ExecuteScalar() > 0;
                    }

                    // Kiểm tra bảng Luong
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Luong WHERE MaNV = @MaNV", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        coTrongLuong = (int)cmd.ExecuteScalar() > 0;
                    }

                    // Nếu không có trong ChamCong hoặc Luong thì không xóa
                    if (!coTrongChamCong && !coTrongLuong)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    // Xóa dữ liệu liên quan trước
                    if (coTrongChamCong)
                    {
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM ChamCong WHERE MaNV = @MaNV", conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@MaNV", maNV);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (coTrongLuong)
                    {
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Luong WHERE MaNV = @MaNV", conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@MaNV", maNV);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Cuối cùng xóa nhân viên
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM NhanVien WHERE MaNV = @MaNV", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        int affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows == 0)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Lỗi khi xóa nhân viên: {ex.Message}");
                    return false;
                }
            }
        }

        public NhanVienDTO GetNhanVienByMaNV(string maNV)
        {
            NhanVienDTO nhanVien = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM NhanVien WHERE MaNV = @MaNV";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nhanVien = new NhanVienDTO
                                {
                                    MaNV = reader["MaNV"].ToString(),
                                    HoNV = reader["HoNV"].ToString(),
                                    TenNV = reader["TenNV"].ToString(),
                                    DiaChi = reader["DiaChi"].ToString(),
                                    SoDT = reader["SoDT"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    NgaySinh = reader["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(reader["NgaySinh"]) : DateTime.MinValue,
                                    GioiTinh = reader["GioiTinh"] != DBNull.Value ? Convert.ToBoolean(reader["GioiTinh"]) : false,
                                    CCCD = reader["CCCD"].ToString(),
                                    ChucVu = reader["ChucVu"].ToString(),
                                    MaPB = reader["MaPB"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi hoặc xử lý ngoại lệ theo nhu cầu
                Console.WriteLine($"Lỗi khi lấy thông tin nhân viên: {ex.Message}");
            }

            return nhanVien;
        }
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

        public bool UpdateNhanVienDAL(NhanVienDTO nv)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE NhanVien SET ";
                    bool updateRequired = false;
                    List<SqlParameter> parameters = new List<SqlParameter>();

                    // Kiểm tra và chỉ cập nhật các trường có giá trị hợp lệ (không null và không rỗng)
                    if (!string.IsNullOrEmpty(nv.HoNV))
                    {
                        query += "HoNV = @HoNV, ";
                        parameters.Add(new SqlParameter("@HoNV", nv.HoNV));
                        updateRequired = true;
                    }

                    if (!string.IsNullOrEmpty(nv.TenNV))
                    {
                        query += "TenNV = @TenNV, ";
                        parameters.Add(new SqlParameter("@TenNV", nv.TenNV));
                        updateRequired = true;
                    }

                    if (!string.IsNullOrEmpty(nv.DiaChi))
                    {
                        query += "DiaChi = @DiaChi, ";
                        parameters.Add(new SqlParameter("@DiaChi", nv.DiaChi));
                        updateRequired = true;
                    }

                    if (!string.IsNullOrEmpty(nv.SoDT))
                    {
                        query += "SoDT = @SoDT, ";
                        parameters.Add(new SqlParameter("@SoDT", nv.SoDT));
                        updateRequired = true;
                    }

                    if (!string.IsNullOrEmpty(nv.Email))
                    {
                        query += "Email = @Email, ";
                        parameters.Add(new SqlParameter("@Email", nv.Email));
                        updateRequired = true;
                    }

                    if (nv.NgaySinh != default(DateTime))
                    {
                        query += "NgaySinh = @NgaySinh, ";
                        parameters.Add(new SqlParameter("@NgaySinh", nv.NgaySinh));
                        updateRequired = true;
                    }

                    if (nv.GioiTinh != null)
                    {
                        query += "GioiTinh = @GioiTinh, ";
                        parameters.Add(new SqlParameter("@GioiTinh", nv.GioiTinh));
                        updateRequired = true;
                    }

                    if (!string.IsNullOrEmpty(nv.CCCD))
                    {
                        query += "CCCD = @CCCD, ";
                        parameters.Add(new SqlParameter("@CCCD", nv.CCCD));
                        updateRequired = true;
                    }

                    if (!string.IsNullOrEmpty(nv.ChucVu))
                    {
                        query += "ChucVu = @ChucVu, ";
                        parameters.Add(new SqlParameter("@ChucVu", nv.ChucVu));
                        updateRequired = true;
                    }

                    if (!string.IsNullOrEmpty(nv.MaPB))
                    {
                        query += "MaPB = @MaPB, ";
                        parameters.Add(new SqlParameter("@MaPB", nv.MaPB));
                        updateRequired = true;
                    }

                    if (!updateRequired)
                    {
                        return false; // Không có trường nào cần cập nhật
                    }

                    // Xóa dấu phẩy cuối cùng
                    query = query.TrimEnd(',', ' ') + " WHERE MaNV = @MaNV";
                    parameters.Add(new SqlParameter("@MaNV", nv.MaNV));

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Nếu có dòng bị ảnh hưởng, cập nhật thành công
                        return rowsAffected > 0;
                    }
                }
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
