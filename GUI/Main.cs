using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTL_LTCSDL.BLL;
using BTL_LTCSDL.DTO;

namespace GUI
{
    public partial class Main : Form
    {
        private NhanVienBLL bll = new NhanVienBLL();
        private LuongBLL luongBLL = new LuongBLL();
        private ChamCongBLL chamCongBLL = new ChamCongBLL();

        public Main()
        {
            InitializeComponent();
            SetupDataGridView();
            LoadData();
            LoadData_TinhLuong();
        }

        private void LoadData()
        {
            dataGridView1.Rows.Clear();
            dataGridView_DSNV.Rows.Clear();
            foreach (var nv in bll.GetAllNhanVien())
            {
                dataGridView1.Rows.Add(nv.MaNV, nv.HoNV + " " + nv.TenNV, nv.DiaChi, nv.SoDT, nv.Email,
                                       nv.NgaySinh.ToString("dd/MM/yyyy"), nv.GioiTinh ? "Nam" : "Nữ",
                                       nv.CCCD, nv.ChucVu, nv.MaPB);
                dataGridView_DSNV.Rows.Add(nv.MaNV, nv.HoNV + " " + nv.TenNV, nv.DiaChi, nv.SoDT, nv.Email,
                                       nv.NgaySinh.ToString("dd/MM/yyyy"), nv.GioiTinh ? "Nam" : "Nữ",
                                       nv.CCCD, nv.ChucVu, nv.MaPB);
            }
        }
        private void LoadData_TinhLuong()
        {
            List<LuongDTO> danhSach = luongBLL.GetDanhSachLuong();

            if (DataGridView_DSNV_Luong.Columns.Count == 0) // Kiểm tra nếu chưa có cột
            {
                SetupDataGridView();
            }

            DataGridView_DSNV_Luong.Rows.Clear(); // Xóa dữ liệu cũ

            foreach (var luong in danhSach)
            {
                DataGridView_DSNV_Luong.Rows.Add(
                    luong.MaNV,
                    luong.TenNV,
                    luong.MaLuong,
                    luong.NgayXetLuong.ToString("dd/MM/yyyy"),
                    luong.LuongNhanDuoc.ToString("N0") + " VNĐ"
                );
            }
        }

        private void DataGridView_DSNV_Luong_SelectionChanged_1(object sender, EventArgs e)
        {
            if (DataGridView_DSNV_Luong.SelectedRows.Count > 0)
            {
                string maNV = DataGridView_DSNV_Luong.SelectedRows[0].Cells["MaNV"].Value.ToString();
                LoadLuongChiTiet(maNV);
            }

        }

        private void LoadLuongChiTiet(string maNV)
        {
            DataRow row = luongBLL.GetLuongChiTiet(maNV);
            if (row != null)
            {
                label_SoGioLam.Text = row["SoGioLam"].ToString();
                label_SoNgayNghi.Text = row["SoNgayNghi"].ToString();
                label_SoGioDiTre.Text = row["SoGioTre"].ToString();
                label_LuongCanban.Text = double.Parse(row["LuongCanBanTheoBac"].ToString()).ToString("N0");
                label_BacLuong.Text = row["BacLuong"].ToString();
                label_SoGioTangCa.Text = row["SoGioTangCa"].ToString();
                label_TroCap.Text = double.Parse(row["TroCap"].ToString()).ToString("N0");
                label_TamUng.Text = double.Parse(row["TamUng"].ToString()).ToString("N0");
                label_LuongNhanDuoc.Text = $"{double.Parse(row["LuongNhanDuoc"].ToString()):N0} VNĐ";
            }
        }

        private void SetupDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false; // Không cho phép thêm dòng trống
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("MaNV", "Mã NV");
            dataGridView1.Columns.Add("TenNV", "Tên Nhân Viên");
            dataGridView1.Columns.Add("DiaChi", "Địa Chỉ");
            dataGridView1.Columns.Add("SoDT", "Số ĐT");
            dataGridView1.Columns.Add("Email", "Email");
            dataGridView1.Columns.Add("NgaySinh", "Ngày Sinh");
            dataGridView1.Columns.Add("GioiTinh", "Giới Tính");
            dataGridView1.Columns.Add("CCCD", "CCCD");
            dataGridView1.Columns.Add("ChucVu", "Chức Vụ");
            dataGridView1.Columns.Add("MaPB", "Mã PB");

            dataGridView_DSNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_DSNV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_DSNV.AllowUserToAddRows = false;
            dataGridView_DSNV.Columns.Clear();
            dataGridView_DSNV.Columns.Add("MaNV", "Mã NV");
            dataGridView_DSNV.Columns.Add("TenNV", "Tên Nhân Viên");
            dataGridView_DSNV.Columns.Add("DiaChi", "Địa Chỉ");
            dataGridView_DSNV.Columns.Add("SoDT", "Số ĐT");
            dataGridView_DSNV.Columns.Add("Email", "Email");
            dataGridView_DSNV.Columns.Add("NgaySinh", "Ngày Sinh");
            dataGridView_DSNV.Columns.Add("GioiTinh", "Giới Tính");
            dataGridView_DSNV.Columns.Add("CCCD", "CCCD");
            dataGridView_DSNV.Columns.Add("ChucVu", "Chức Vụ");
            dataGridView_DSNV.Columns.Add("MaPB", "Mã PB");

            DataGridView_DSNV_Luong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridView_DSNV_Luong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_DSNV_Luong.AllowUserToAddRows = false;
            DataGridView_DSNV_Luong.Columns.Clear();

            DataGridView_DSNV_Luong.Columns.Add("MaNV", "Mã NV"); // Lấy từ bảng Luong
            DataGridView_DSNV_Luong.Columns.Add("TenNV", "Tên Nhân Viên"); // Lấy từ bảng NhanVien
            DataGridView_DSNV_Luong.Columns.Add("MaLuong", "Mã Lương"); // Lấy từ bảng Luong
            DataGridView_DSNV_Luong.Columns.Add("NgayXetLuong", "Ngày Xét Lương"); // Lấy từ bảng Luong
            DataGridView_DSNV_Luong.Columns.Add("LuongNhanDuoc", "Lương Nhận Được"); // Lấy từ bảng TinhLuong
        }


        private void button_Show_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = tabPage_DanhSachNV;
        }

        private void button_Add_Click_1(object sender, EventArgs e)
        {
            var nv = new NhanVienDTO
            {
                MaNV = textBox_Employeed.Text,
                HoNV = textBox_Surname.Text,
                TenNV = textBox_Name.Text,
                DiaChi = textBox_DiaChi.Text,
                SoDT = textBox_SDT.Text,
                Email = textBox_email.Text,
                NgaySinh = dateTime_DateOfBirth.Value,
                GioiTinh = radioBut_Male.Checked,
                CCCD = textBox_CCCD.Text,
                ChucVu = textBox_ChucVu.Text,
                MaPB = textBox_MaPhongBan.Text
            };

            if (bll.InsertNhanVien(nv))
            {
                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            else
            {
                MessageBox.Show("Thêm nhân viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label_RealTime.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
        }
        
        private void button_CheckIn_Click(object sender, EventArgs e)
        {
            if (dataGridView_ChamCong.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maNV = dataGridView_ChamCong.SelectedRows[0].Cells["MaNV"].Value.ToString();
            string tenNV = dataGridView_ChamCong.SelectedRows[0].Cells["TenNV"].Value.ToString();
            DateTime ngayCC = DateTime.Now.Date;
            TimeSpan tgVao = radioButton_RealTime.Checked ? DateTime.Now.TimeOfDay : dateTimePicker1.Value.TimeOfDay;

            var chamCong = new ChamCongDTO
            {
                MaCC = Guid.NewGuid().ToString(), // Sinh mã chấm công tự động
                MaNV = maNV,
                TenNV = tenNV,
                NgayCC = ngayCC,
                TGVao = tgVao,
                TrangThai = "Chưa check-out"
            };

            if (chamCongBLL.CheckIn(chamCong))
            {
                MessageBox.Show($"Check-In thành công lúc {tgVao}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            else
            {
                MessageBox.Show("Nhân viên đã Check-In hôm nay!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_CheckOut_Click(object sender, EventArgs e)
        {
            if (dataGridView_ChamCong.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maNV = dataGridView_ChamCong.SelectedRows[0].Cells["MaNV"].Value.ToString();
            DateTime ngayCC = DateTime.Now.Date;
            TimeSpan tgRa = radioButton_RealTime2.Checked ? DateTime.Now.TimeOfDay : dateTimePicker2.Value.TimeOfDay;

            if (chamCongBLL.CheckOut(maNV, ngayCC, tgRa))
            {
                MessageBox.Show($"Check-Out thành công lúc {tgRa}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            else
            {
                MessageBox.Show("Check-Out thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000; // Cập nhật mỗi giây
            timer1.Tick += timer1_Tick;
            timer1.Start();
            // Mặc định chọn thời gian hiện tại
            radioButton_RealTime.Checked = true;
            radioButton_RealTime2.Checked = true;
            dateTimePicker1.Enabled = false; // Ẩn chọn tay
            dateTimePicker2.Enabled = false; // Ẩn chọn tay
        }

        private void radioButton_RealTime_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = !radioButton_RealTime.Checked; // Tắt nếu chọn "Thời gian hiện tại"
        }

        private void radioButton_RealTime2_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Enabled = !radioButton_RealTime2.Checked; // Tắt nếu chọn "Thời gian hiện tại"
        }

        private void button_CheckIn_Click_1(object sender, EventArgs e)
        {
            string checkInTime;
            if (radioButton_RealTime.Checked)
            {
                checkInTime = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"); // Lấy giờ hiện tại
            }
            else
            {
                checkInTime = dateTimePicker1.Value.ToString("HH:mm:ss dd/MM/yyyy"); // Lấy giờ từ DateTimePicker
            }

            MessageBox.Show("Mã nhân viên: \n" + "Tên Nhân Viên: \n" + "Đã check-in lúc: " + checkInTime, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button_CheckOut_Click_1(object sender, EventArgs e)
        {
            string checkInTime;
            if (radioButton_RealTime2.Checked)
            {
                checkInTime = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"); // Lấy giờ hiện tại
            }
            else
            {
                checkInTime = dateTimePicker2.Value.ToString("HH:mm:ss dd/MM/yyyy"); // Lấy giờ từ DateTimePicker
            }

            MessageBox.Show("Mã nhân viên: \n" + "Tên Nhân Viên: \n" + "Đã check-out lúc: " + checkInTime, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
