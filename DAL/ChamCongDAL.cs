using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BTL_LTCSDL.DTO;

namespace BTL_LTCSDL.DAL
{
    public class ChamCongDAL
    {
        private string connectionString = "Server=LONG_ACER\\SQLEXPRESS;Database=QL_NhanVien;Integrated Security=True;";

        // Lấy tất cả danh sách chấm công
        public List<ChamCongDTO> GetAllChamCong()
        {
            var list = new List<ChamCongDTO>();
            string query = "SELECT * FROM ChamCong";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    bool vang = reader["Vang"] != DBNull.Value && Convert.ToBoolean(reader["Vang"]);

                    var chamCong = new ChamCongDTO
                    {
                        MaCC = reader["MaCC"].ToString(),
                        MaNV = reader["MaNV"].ToString(),
                        NgayCC = Convert.ToDateTime(reader["NgayCC"]),
                        TGVao = reader["TGVao"] != DBNull.Value ? (TimeSpan?)reader["TGVao"] : null,
                        TGRa = reader["TGRa"] != DBNull.Value ? (TimeSpan?)reader["TGRa"] : null,
                        TGVaoTangCa = reader["TGVaoTangCa"] != DBNull.Value ? (TimeSpan?)reader["TGVaoTangCa"] : null,
                        TGRaTangCa = reader["TGRaTangCa"] != DBNull.Value ? (TimeSpan?)reader["TGRaTangCa"] : null,
                        TrangThai = reader["TrangThai"].ToString(),
                        Vang = vang,
                        VangCoPhep = vang
                            ? (reader["VangCoPhep"] != DBNull.Value ? (bool?)Convert.ToBoolean(reader["VangCoPhep"]) : null)
                            : null
                    };

                    list.Add(chamCong);
                }
            }

            return list;
        }

        // Lấy thông tin chấm công theo ngày và mã nhân viên
        public ChamCongDTO GetChamCongTheoNgay(string maNV, DateTime ngayCC)
        {
            ChamCongDTO chamCong = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM ChamCong WHERE MaNV = @MaNV AND NgayCC = @NgayCC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                cmd.Parameters.AddWithValue("@NgayCC", ngayCC);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    chamCong = new ChamCongDTO
                    {
                        MaCC = reader["MaCC"].ToString(),
                        MaNV = reader["MaNV"].ToString(),
                        NgayCC = Convert.ToDateTime(reader["NgayCC"]),
                        TGVao = reader["TGVao"] != DBNull.Value ? (TimeSpan?)reader["TGVao"] : null,
                        TGRa = reader["TGRa"] != DBNull.Value ? (TimeSpan?)reader["TGRa"] : null,
                        Vang = Convert.ToBoolean(reader["Vang"]),

                        // Sửa phần VangCoPhep để chuyển đổi đúng kiểu
                        VangCoPhep = reader["VangCoPhep"] != DBNull.Value ?
                                     (reader["VangCoPhep"].ToString() == "1" ? (bool?)true :
                                     (reader["VangCoPhep"].ToString() == "0" ? (bool?)false : null)) :
                                     null,

                        TrangThai = reader["TrangThai"].ToString()
                    };
                }
            }
            return chamCong;
        }


        // Thêm mới chấm công
        public bool InsertChamCong(ChamCongDTO chamCong)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Gán mã mới
                string maCC = GenerateNewMaCC();

                string query = "INSERT INTO ChamCong (MaCC, MaNV, NgayCC, TGVao, Vang, VangCoPhep, TrangThai) " +
                               "VALUES (@MaCC, @MaNV, @NgayCC, @TGVao, @Vang, @VangCoPhep, @TrangThai)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaCC", maCC);
                cmd.Parameters.AddWithValue("@MaNV", chamCong.MaNV);
                cmd.Parameters.AddWithValue("@NgayCC", chamCong.NgayCC);
                cmd.Parameters.AddWithValue("@TGVao", chamCong.TGVao ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Vang", chamCong.Vang);
                cmd.Parameters.AddWithValue("@VangCoPhep", chamCong.VangCoPhep ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TrangThai", chamCong.TrangThai);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }


        // Cập nhật thông tin chấm công (giờ vào/ra, trạng thái)
        public bool UpdateChamCong(ChamCongDTO chamCong)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE ChamCong SET TGVao = @TGVao, TGRa = @TGRa, TrangThai = @TrangThai " +
                               "WHERE MaNV = @MaNV AND NgayCC = @NgayCC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNV", chamCong.MaNV);
                cmd.Parameters.AddWithValue("@NgayCC", chamCong.NgayCC);
                cmd.Parameters.AddWithValue("@TGVao", chamCong.TGVao ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TGRa", chamCong.TGRa ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TrangThai", chamCong.TrangThai);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        private string GenerateNewMaCC()
        {
            string newMaCC = "CC001"; // default nếu chưa có dữ liệu

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT TOP 1 MaCC FROM ChamCong ORDER BY MaCC DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                var result = cmd.ExecuteScalar();

                if (result != null)
                {
                    string lastMaCC = result.ToString(); // VD: "CC015"
                    int number = int.Parse(lastMaCC.Substring(2)) + 1;
                    newMaCC = "CC" + number.ToString("D3");
                }
            }

            return newMaCC;
        }

    }
}
