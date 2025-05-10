using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO;

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
            string query = "SELECT MaCC, MaNV, NgayCC, TGVao, TGRa, Vang FROM ChamCong";

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

    // Lấy MaCC cao nhất trong bảng ChamCong
    public string GetLastMaCC()
    {
        try
        {
            string query = "SELECT TOP 1 MaCC FROM ChamCong ORDER BY MaCC DESC";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    var result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        return result.ToString();  // Trả về MaCC cao nhất
                    }
                    else
                    {
                        return "CC000";  // Trả về giá trị mặc định nếu không có dữ liệu
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Xử lý lỗi
            return "CC000";  // Trả về giá trị mặc định khi có lỗi
        }
    }

    // Thêm chấm công mới
    public bool InsertChamCong(ChamCongDTO chamCong)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "INSERT INTO ChamCong (MaCC, MaNV, NgayCC, TGVao, TGRa, Vang) VALUES (@MaCC, @MaNV, @NgayCC, @TGVao, @TGRa, @Vang)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaCC", chamCong.MaCC);
                cmd.Parameters.AddWithValue("@MaNV", chamCong.MaNV);
                cmd.Parameters.AddWithValue("@NgayCC", chamCong.NgayCC);
                cmd.Parameters.AddWithValue("@TGVao", chamCong.TGVao.HasValue ? (object)chamCong.TGVao.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@TGRa", chamCong.TGRa.HasValue ? (object)chamCong.TGRa.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Vang", chamCong.Vang);

                return cmd.ExecuteNonQuery() > 0; // Trả về true nếu thêm thành công
            }
        }
    }
    public bool UpdateChamCong(ChamCongDTO chamCong)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = @"
            UPDATE ChamCong
            SET TGVao = @TGVao, TGRa = @TGRa, Vang = @Vang
            WHERE MaCC = @MaCC AND MaNV = @MaNV AND NgayCC = @NgayCC";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // Handle null for TGVao and TGRa
                cmd.Parameters.AddWithValue("@TGVao", chamCong.TGVao.HasValue ? (object)chamCong.TGVao.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@TGRa", chamCong.TGRa.HasValue ? (object)chamCong.TGRa.Value : DBNull.Value);

                // Include Vang (if needed)
                cmd.Parameters.AddWithValue("@Vang", chamCong.Vang);
                cmd.Parameters.AddWithValue("@MaCC", chamCong.MaCC);
                cmd.Parameters.AddWithValue("@MaNV", chamCong.MaNV);
                cmd.Parameters.AddWithValue("@NgayCC", chamCong.NgayCC);

                return cmd.ExecuteNonQuery() > 0; // Return true if update is successful
            }
        }
    }

}
