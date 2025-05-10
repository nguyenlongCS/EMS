using System;
using System.Windows.Forms;
using BLL;
using DTO;

namespace GUI
{
    public partial class Login : Form
    {
        private readonly LoginBLL loginBLL = new LoginBLL();
        public Login()
        {
            InitializeComponent();
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            LoginDTO user = new LoginDTO(txtUsername.Text.Trim(), txtPassword.Text.Trim());

            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (loginBLL.ValidateUser(user))
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide(); // Ẩn form Login

                Main mainForm = new Main(); // Mở form Main
                mainForm.FormClosed += (s, args) => this.Close(); // Đảm bảo form Login đóng khi form Main đóng
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            txtUsername.Clear();
        }

        //private void txtPassword_TextChanged(object sender, EventArgs e)
        //{
        //    //txtPassword.Clear();
        //}

        //private void lkExit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    this.Close();
        //}

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đóng?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void lkFogot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ResetPass frmReset = new ResetPass();
            frmReset.ShowDialog(); // Mở form ResetPass
        }
    }
}
