using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BTL_LTCSDL.DTO;

public class ChamCongDAL
{
    private string connectionString = "Server=LONG_ACER\\SQLEXPRESS;Database=QL_NhanVien;Integrated Security=True;";

    // Lấy danh sách chấm công của nhân viên
    public List<ChamCongDTO> GetDanhSachChamCong()
    {
        List<ChamCongDTO> danhSach = new List<ChamCongDTO>();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = @"
        SELECT MaCC, MaNV, NgayCC, TGVao, TGRa, Vang
        FROM ChamCong";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    danhSach.Add(new ChamCongDTO
                    {
                        MaCC = reader["MaCC"].ToString(),
                        MaNV = reader["MaNV"].ToString(),
                        NgayCC = Convert.ToDateTime(reader["NgayCC"]),
                        TGVao = reader["TGVao"] as TimeSpan?,
                        TGRa = reader["TGRa"] as TimeSpan?,
                        Vang = Convert.ToInt32(reader["Vang"])
                    });
                }
            }
        }
        return danhSach;
    }


    // Thêm chấm công mới
    public bool InsertChamCong(ChamCongDTO chamCong)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = @"
            INSERT INTO ChamCong (MaCC, MaNV, NgayCC, TGVao, TGRa)
            VALUES (@MaCC, @MaNV, @NgayCC, @TGVao, @TGRa)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaCC", chamCong.MaCC);
                cmd.Parameters.AddWithValue("@MaNV", chamCong.MaNV);
                cmd.Parameters.AddWithValue("@NgayCC", chamCong.NgayCC);
                cmd.Parameters.AddWithValue("@TGVao", chamCong.TGVao.HasValue ? (object)chamCong.TGVao.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@TGRa", chamCong.TGRa.HasValue ? (object)chamCong.TGRa.Value : DBNull.Value);

                return cmd.ExecuteNonQuery() > 0; // Trả về true nếu thêm thành công
            }
        }
    }
}
