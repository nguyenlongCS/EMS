using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class LuongDAL
{
    private string connectionString = "Server=LONG_ACER\\SQLEXPRESS;Database=QL_NhanVien;Integrated Security=True;";

    // Lấy danh sách lương nhân viên
    public List<LuongDTO> GetDanhSachLuong()
    {
        List<LuongDTO> danhSach = new List<LuongDTO>();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = @"
            SELECT L.MaNV, NV.TenNV, L.MaLuong, L.NgayXetLuong, TL.LuongNhanDuoc
            FROM Luong L
            JOIN NhanVien NV ON L.MaNV = NV.MaNV
            JOIN TinhLuong TL ON L.MaLuong = TL.MaLuong";

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
                        LuongNhanDuoc = Convert.ToDouble(reader["LuongNhanDuoc"])
                    });
                }
            }
        }
        return danhSach;
    }

    // Lấy thông tin chi tiết lương của nhân viên
    public DataRow GetLuongChiTiet(string maNV)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = @"
            SELECT l.SoGioLam, l.SoNgayNghi, l.SoGioTre, lt.LuongCanBanTheoBac, 
                   lt.BacLuong, l.SoGioTangCa, l.TroCap, l.TamUng, COALESCE(l.LuongNhanDuoc, 0) AS LuongNhanDuoc
            FROM NhanVien nv
            JOIN Luong l ON nv.MaNV = l.MaNV
            JOIN LuongTheoBac lt ON l.MaLuong = lt.MaLuong
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
}
