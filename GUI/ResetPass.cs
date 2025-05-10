using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace GUI
{
    public partial class ResetPass : Form
    {
        private readonly LoginBLL loginBLL = new LoginBLL();
        public ResetPass()
        {
            InitializeComponent();
        }

        private void ResetPass_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đóng?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void lkExit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btXacnhan_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isUpdated = loginBLL.ResetPassword(username, newPassword);
            if (isUpdated)
            {
                MessageBox.Show("Mật khẩu đã được cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Đóng form ResetPass sau khi thành công
            }
            else
            {
                MessageBox.Show("Tên đăng nhập không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            txtUsername.Clear();
        }

        private void txtNewPassword_Enter(object sender, EventArgs e)
        {
            txtNewPassword.Clear();
        }
    }
}
