using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class LoginBLL
    {
        private readonly LoginDAL dbHelper = new LoginDAL();

        public bool ValidateUser(LoginDTO user)
        {
            LoginDTO foundUser = dbHelper.GetUser(user.Username, user.Password);
            return foundUser != null;
        }

        public bool ResetPassword(string username, string newPassword)
        {
            return dbHelper.UpdatePassword(username, newPassword);
        }
    }
}
