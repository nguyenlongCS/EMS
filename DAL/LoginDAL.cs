using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class LoginDAL
    {
        private readonly string connectionString = "Server = LONG_ACER\\SQLEXPRESS;Database=QL_NhanVien;Integrated Security = True;";

        public LoginDTO GetUser(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Username, Password FROM Login WHERE Username=@Username AND Password=@Password";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new LoginDTO(reader["Username"].ToString(), reader["Password"].ToString());
                        }
                        return null;
                    }
                }
            }
        }

        public bool UpdatePassword(string username, string newPassword)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Login SET Password=@NewPassword WHERE Username=@Username";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@NewPassword", newPassword);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
