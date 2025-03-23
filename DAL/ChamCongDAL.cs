using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class ChamCongDAL
{
    private string connectionString = "Server=LONG_ACER\\SQLEXPRESS;Database=QL_NhanVien;Integrated Security=True;";

    // Check-In
    public bool CheckIn(ChamCongDTO chamCong)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = @"
                INSERT INTO ChamCong (MaCC, MaNV, TenNV, NgayCC, TGVao, TrangThai) 
                VALUES (@MaCC, @MaNV, @TenNV, @NgayCC, @TGVao, 'Chưa check-out')";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaCC", chamCong.MaCC);
                cmd.Parameters.AddWithValue("@MaNV", chamCong.MaNV);
                cmd.Parameters.AddWithValue("@TenNV", chamCong.TenNV);
                cmd.Parameters.AddWithValue("@NgayCC", chamCong.NgayCC);
                cmd.Parameters.AddWithValue("@TGVao", chamCong.TGVao ?? (object)DBNull.Value);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    // Check-Out
    public bool CheckOut(string maNV, DateTime ngayCC, TimeSpan tgRa)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = @"
                UPDATE ChamCong SET TGRa = @TGRa, TrangThai = 'Đầy đủ'
                WHERE MaNV = @MaNV AND NgayCC = @NgayCC";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                cmd.Parameters.AddWithValue("@NgayCC", ngayCC);
                cmd.Parameters.AddWithValue("@TGRa", tgRa);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    // Lấy danh sách chấm công
    public List<ChamCongDTO> GetChamCong()
    {
        List<ChamCongDTO> danhSachChamCong = new List<ChamCongDTO>();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM ChamCong";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    danhSachChamCong.Add(new ChamCongDTO
                    {
                        MaCC = reader["MaCC"].ToString(),
                        MaNV = reader["MaNV"].ToString(),
                        TenNV = reader["TenNV"].ToString(),
                        NgayCC = Convert.ToDateTime(reader["NgayCC"]),
                        TGVao = reader["TGVao"] as TimeSpan?,
                        TGRa = reader["TGRa"] as TimeSpan?,
                        TrangThai = reader["TrangThai"].ToString()
                    });
                }
            }
        }
        return danhSachChamCong;
    }
}
