using System;

namespace DTO
{
    public class LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginDTO() { }

        public LoginDTO(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
