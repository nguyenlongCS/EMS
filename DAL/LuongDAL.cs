using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO;

public class LuongDAL
{
    private string connectionString = "Server=LONG_ACER\\SQLEXPRESS;Database=EMS;Integrated Security=True;";

    // Lấy thông tin chi tiết lương cho một nhân viên
    public DataRow GetLuongChiTiet(string maNV)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = @"
            SELECT 
                l.MaBacLuong,  
                l.TongNgayLam, 
                l.TongNgayNghi, 
                l.TongNgayNghiCoPhep, 
                l.TongNgayDiTre, 
                l.TongNgayVeSom, 
                l.TroCap, 
                l.TamUng,
                l.LuongNhanDuoc,  -- Added field
                l.ThueVat          -- Added field
            FROM Luong l
            JOIN NhanVien nv ON l.MaNV = nv.MaNV
            WHERE nv.MaNV = @MaNV";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@MaNV", SqlDbType.NChar, 10).Value = maNV;

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                }
            }
        }
    }

    // Lấy danh sách lương của tất cả nhân viên
    public List<LuongDTO> GetDanhSachLuong()
    {
        List<LuongDTO> danhSach = new List<LuongDTO>();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = @"
            SELECT 
                L.MaNV, NV.TenNV, L.NgayXetLuong, 
                L.TongNgayLam, L.TongNgaynghi, 
                L.TongNgayNghiCoPhep, L.TongNgayDiTre, L.TongNgayVeSom,
                L.TroCap, L.TamUng, L.LuongNhanDuoc
            FROM Luong L
            JOIN NhanVien NV ON L.MaNV = NV.MaNV";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var luongNhanDuoc = reader["LuongNhanDuoc"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["LuongNhanDuoc"]);

                    // Add the DTO to the list
                    danhSach.Add(new LuongDTO
                    {
                        MaNV = reader["MaNV"].ToString(),
                        TenNV = reader["TenNV"].ToString(),
                        NgayXetLuong = Convert.ToDateTime(reader["NgayXetLuong"]),
                        TongNgayLam = Convert.ToInt32(reader["TongNgayLam"]),
                        TongNgaynghi = Convert.ToInt32(reader["TongNgaynghi"]),
                        TongNgayNghiCoPhep = Convert.ToInt32(reader["TongNgayNghiCoPhep"]),
                        TongNgayDiTre = Convert.ToInt32(reader["TongNgayDiTre"]),
                        TongNgayVeSom = Convert.ToInt32(reader["TongNgayVeSom"]),
                        TroCap = Convert.ToDouble(reader["TroCap"]),
                        TamUng = Convert.ToDouble(reader["TamUng"]),
                        LuongNhanDuoc = luongNhanDuoc // Truyền đúng giá trị
                    });
                }
            }
        }
        return danhSach;
    }



    // Lấy thông tin lương theo bậc
    public DataRow GetLuongTheoBac(string maBacLuong)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = @"
                SELECT 
                    MaBacLuong, 
                    BacLuong, 
                    LuongCanBanTheoBac
                FROM LuongTheoBac
                WHERE MaBacLuong = @MaBacLuong";  

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@MaBacLuong", SqlDbType.NChar, 10).Value = maBacLuong;  

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                }
            }
        }
    }
    public bool ExecuteUpdateLuong()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("UpdateLuong", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return cmd.ExecuteNonQuery() >= 0; 
            }
        }
    }
    public bool DeleteLuongByMaNV(string maNV)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "DELETE FROM Luong WHERE MaNV = @MaNV";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                return cmd.ExecuteNonQuery() > 0; // Trả về true nếu xóa thành công
            }
        }
    }
    public LuongDTO GetLuongByMaNV(string maNV)
    {
        LuongDTO luong = null;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM Luong WHERE MaNV = @MaNV";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        luong = new LuongDTO
                        {
                            MaNV = reader["MaNV"].ToString(),
                            NgayXetLuong = Convert.ToDateTime(reader["NgayXetLuong"]),
                            TongNgayLam = Convert.ToInt32(reader["TongNgayLam"]),
                            TongNgaynghi = Convert.ToInt32(reader["TongNgaynghi"]),
                            TroCap = Convert.ToDouble(reader["TroCap"]),
                            TamUng = Convert.ToDouble(reader["TamUng"]),
                            LuongNhanDuoc = Convert.ToDouble(reader["LuongNhanDuoc"])
                        };
                    }
                }
            }
        }
        return luong;
    }


}
