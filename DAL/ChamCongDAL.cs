using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BTL_LTCSDL.DTO;

namespace BTL_LTCSDL.DAL
{
    public class ChamCongDAL
    {
        private string connectionString = "Server=LONG_ACER\\SQLEXPRESS;Database=QL_NhanVien;Integrated Security=True;";  // Kết nối cơ sở dữ liệu

        // Phương thức lấy chi tiết chấm công của một nhân viên
        public ChamCongDTO GetChamCongChiTiet(string maNV, DateTime ngayCC)
        {
            ChamCongDTO chamCong = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaCC, MaNV, NgayCC, TGVao, TGRa, TGVaoTangCa, TGRaTangCa " +
                               "FROM ChamCong WHERE MaNV = @MaNV AND CONVERT(date, NgayCC) = @NgayCC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                cmd.Parameters.AddWithValue("@NgayCC", ngayCC.Date);  // Ensure we only compare the date part

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        chamCong = new ChamCongDTO
                        {
                            MaCC = reader["MaCC"].ToString(),
                            MaNV = reader["MaNV"].ToString(),
                            NgayCC = Convert.ToDateTime(reader["NgayCC"]),
                            TGVao = reader["TGVao"] as TimeSpan?,
                            TGRa = reader["TGRa"] as TimeSpan?,
                            TGVaoTangCa = reader["TGVaoTangCa"] as TimeSpan?,
                            TGRaTangCa = reader["TGRaTangCa"] as TimeSpan?,
                        };
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi truy vấn dữ liệu chấm công: {ex.Message}");
                }
            }

            return chamCong;
        }





        // Phương thức lấy danh sách chấm công của tất cả nhân viên
        public List<ChamCongDTO> GetDanhSachChamCong()
        {
            List<ChamCongDTO> danhSachChamCong = new List<ChamCongDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaCC, MaNV, NgayCC, TGVao, TGRa, TGVaoTangCa, TGRaTangCa FROM ChamCong";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ChamCongDTO chamCong = new ChamCongDTO
                        {
                            MaCC = reader["MaCC"].ToString(),
                            MaNV = reader["MaNV"].ToString(),
                            NgayCC = Convert.ToDateTime(reader["NgayCC"]),
                            TGVao = reader["TGVao"] as TimeSpan?,
                            TGRa = reader["TGRa"] as TimeSpan?,
                            TGVaoTangCa = reader["TGVaoTangCa"] as TimeSpan?,
                            TGRaTangCa = reader["TGRaTangCa"] as TimeSpan?,
                        };

                        danhSachChamCong.Add(chamCong);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi truy vấn danh sách chấm công: {ex.Message}");
                }
            }

            return danhSachChamCong;
        }


        // Phương thức thêm mới một bản ghi chấm công
        public bool InsertChamCong(ChamCongDTO chamCong)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO ChamCong (MaCC, MaNV, NgayCC, TGVao, TGRa, TGVaoTangCa, TGRaTangCa) " +
                               "VALUES (@MaCC, @MaNV, @NgayCC, @TGVao, @TGRa, @TGVaoTangCa, @TGRaTangCa)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaCC", chamCong.MaCC);
                cmd.Parameters.AddWithValue("@MaNV", chamCong.MaNV);
                cmd.Parameters.AddWithValue("@NgayCC", chamCong.NgayCC);
                cmd.Parameters.AddWithValue("@TGVao", chamCong.TGVao ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TGRa", chamCong.TGRa ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TGVaoTangCa", chamCong.TGVaoTangCa ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TGRaTangCa", chamCong.TGRaTangCa ?? (object)DBNull.Value);
                

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi thêm dữ liệu chấm công: {ex.Message}");
                }
            }
        }

        // Phương thức cập nhật chấm công
        public bool UpdateChamCong(ChamCongDTO chamCong)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE ChamCong SET MaNV = @MaNV, NgayCC = @NgayCC, TGVao = @TGVao, TGRa = @TGRa, " +
                               "TGVaoTangCa = @TGVaoTangCa, TGRaTangCa = @TGRaTangCa" +
                               "WHERE MaCC = @MaCC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaCC", chamCong.MaCC);
                cmd.Parameters.AddWithValue("@MaNV", chamCong.MaNV);
                cmd.Parameters.AddWithValue("@NgayCC", chamCong.NgayCC);
                cmd.Parameters.AddWithValue("@TGVao", chamCong.TGVao ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TGRa", chamCong.TGRa ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TGVaoTangCa", chamCong.TGVaoTangCa ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TGRaTangCa", chamCong.TGRaTangCa ?? (object)DBNull.Value);
                

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi cập nhật dữ liệu chấm công: {ex.Message}");
                }
            }
        }

        // Phương thức xóa chấm công
        public bool DeleteChamCong(string maCC)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM ChamCong WHERE MaCC = @MaCC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaCC", maCC);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi xóa dữ liệu chấm công: {ex.Message}");
                }
            }
        }
        public int GetNextMaCC()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MAX(CAST(SUBSTRING(MaCC, 3, 3) AS INT)) + 1 FROM ChamCong";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToInt32(result) : 1;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi lấy mã chấm công tiếp theo: {ex.Message}");
                }
            }
        }

    }
}
