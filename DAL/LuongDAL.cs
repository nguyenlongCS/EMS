using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO;

public class LuongDAL
{
    private string connectionString = "Server=LONG_ACER\\SQLEXPRESS;Database=QL_NhanVien;Integrated Security=True;";

    public DataRow GetLuongChiTiet(string maNV)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = @"
        SELECT 
            l.TongNgayLam, 
            l.TongNgayNghi, 
            l.TongNgayNghiCoPhep, 
            l.TongNgayDiTre, 
            l.TongNgayVeSom, 
            l.TroCap, 
            l.TamUng
        FROM NhanVien nv
        JOIN Luong l ON nv.MaNV = l.MaNV
        WHERE nv.MaNV = @MaNV";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                }
            }
        }
    }
    public List<LuongDTO> GetDanhSachLuong()
{
    List<LuongDTO> danhSach = new List<LuongDTO>();
    using (SqlConnection conn = new SqlConnection(connectionString))
    {
        conn.Open();
        string query = @"
        SELECT 
            L.MaNV, NV.TenNV, L.MaLuong, L.NgayXetLuong, 
            L.TongNgayLam, L.TongNgaynghi, 
            L.TongNgayNghiCoPhep, L.TongNgayDiTre, L.TongNgayVeSom,
            L.TroCap, L.TamUng
        FROM Luong L
        JOIN NhanVien NV ON L.MaNV = NV.MaNV";
        
        using (SqlCommand cmd = new SqlCommand(query, conn))
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                danhSach.Add(new LuongDTO
                {
                    MaNV = reader["MaNV"].ToString(),
                    TenNV = reader["TenNV"].ToString(),
                    MaLuong = reader["MaLuong"].ToString(),
                    NgayXetLuong = Convert.ToDateTime(reader["NgayXetLuong"]),
                    TongNgayLam = Convert.ToInt32(reader["TongNgayLam"]),
                    TongNgaynghi = Convert.ToInt32(reader["TongNgaynghi"]),
                    TongNgayNghiCoPhep = Convert.ToInt32(reader["TongNgayNghiCoPhep"]),
                    TongNgayDiTre = Convert.ToInt32(reader["TongNgayDiTre"]),
                    TongNgayVeSom = Convert.ToInt32(reader["TongNgayVeSom"]),
                    TroCap = Convert.ToDouble(reader["TroCap"]),
                    TamUng = Convert.ToDouble(reader["TamUng"])
                });
            }
        }
    }
    return danhSach;
}
}

