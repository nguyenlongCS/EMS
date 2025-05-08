using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BTL_LTCSDL.DTO;

namespace BTL_LTCSDL.DAL
{
    public class ChamCongDAL
    {
        private string connectionString = "Server=LONG_ACER\\SQLEXPRESS;Database=QL_NhanVien;Integrated Security=True;";

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
                    list.Add(new ChamCongDTO
                    {
                        MaCC = reader["MaCC"].ToString(),
                        MaNV = reader["MaNV"].ToString(),
                        NgayCC = Convert.ToDateTime(reader["NgayCC"]),
                        TGVao = reader["TGVao"] != DBNull.Value ? (TimeSpan?)reader["TGVao"] : null,
                        TGRa = reader["TGRa"] != DBNull.Value ? (TimeSpan?)reader["TGRa"] : null,
                        TGVaoTangCa = reader["TGVaoTangCa"] != DBNull.Value ? (TimeSpan?)reader["TGVaoTangCa"] : null,
                        TGRaTangCa = reader["TGRaTangCa"] != DBNull.Value ? (TimeSpan?)reader["TGRaTangCa"] : null,
                        TrangThai = reader["TrangThai"].ToString(),
                        VangCoPhep = reader["VangCoPhep"] != DBNull.Value ? Convert.ToInt32(reader["VangCoPhep"]) : 0
                    });
                }
            }

            return list;
        }

        // Các hàm khác như: InsertChamCong, UpdateChamCong, DeleteChamCong nếu cần thêm
    }
}
