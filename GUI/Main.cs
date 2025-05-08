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
        }

        private void Main_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadNhanVienData();
            LoadTinhLuongData();
            LoadChamCongData();
        }

        #region Thiết lập điều khiển

        private void SetupDataGridView()
        {
            // Thiết lập DataGridView Nhân viên
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
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

            // Thiết lập DataGridView DSNV
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

            // Thiết lập DataGridView Danh sách nhân viên lương
            DataGridView_DSNV_Luong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridView_DSNV_Luong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_DSNV_Luong.AllowUserToAddRows = false;
            DataGridView_DSNV_Luong.Columns.Clear();
            DataGridView_DSNV_Luong.Columns.Add("MaNV", "Mã NV");
            DataGridView_DSNV_Luong.Columns.Add("TenNV", "Tên Nhân Viên");
            DataGridView_DSNV_Luong.Columns.Add("MaLuong", "Mã Lương");
            DataGridView_DSNV_Luong.Columns.Add("NgayXetLuong", "Ngày Xét Lương");
            DataGridView_DSNV_Luong.Columns.Add("LuongNhanDuoc", "Lương Nhận Được");

            // Thiết lập DataGridView Chấm công
            dataGridView_ChamCong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_ChamCong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_ChamCong.AllowUserToAddRows = false;
            dataGridView_ChamCong.Columns.Clear();
            dataGridView_ChamCong.Columns.Add("MaCC", "Mã CC");
            dataGridView_ChamCong.Columns.Add("MaNV", "Mã Nhân Viên");
            dataGridView_ChamCong.Columns.Add("NgayCC", "Ngày Chấm Công");
            dataGridView_ChamCong.Columns.Add("TGVao", "Thời Gian Vào");
            dataGridView_ChamCong.Columns.Add("TGRa", "Thời Gian Ra");
            dataGridView_ChamCong.Columns.Add("TGVaoTangCa", "TG Vào Tăng Ca");
            dataGridView_ChamCong.Columns.Add("TGRaTangCa", "TG Ra Tăng Ca");
            dataGridView_ChamCong.Columns.Add("TrangThai", "Trạng Thái");
            dataGridView_ChamCong.Columns.Add("VangCoPhep", "Vắng Có Phép");
        }




        #endregion

        #region Quản lý Nhân viên

        private void LoadNhanVienData()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu Nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_Show_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = tabPage_DanhSachNV;
        }

        private void button_Add_Click_1(object sender, EventArgs e)
        {
            try
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
                    LoadNhanVienData();
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Quản lý Lương

        private void LoadTinhLuongData()
        {
            try
            {
                List<LuongDTO> danhSach = luongBLL.GetDanhSachLuong();
                if (DataGridView_DSNV_Luong.Columns.Count == 0)
                {
                    SetupDataGridView();
                }
                DataGridView_DSNV_Luong.Rows.Clear();
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
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu Lương: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView_DSNV_Luong_SelectionChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (DataGridView_DSNV_Luong.SelectedRows.Count > 0)
                {
                    string maNV = DataGridView_DSNV_Luong.SelectedRows[0].Cells["MaNV"].Value.ToString();
                    LoadLuongChiTiet(maNV);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn dòng Lương: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLuongChiTiet(string maNV)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết Lương: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Quản lý Chấm công
        private void LoadChamCongData()
        {
            try
            {
                var danhSachChamCong = chamCongBLL.GetDanhSachChamCong();
                dataGridView_ChamCong.Rows.Clear();
                foreach (var cc in danhSachChamCong)
                {
                    dataGridView_ChamCong.Rows.Add(
                        cc.MaCC,
                        cc.MaNV,
                        cc.NgayCC.ToString("dd/MM/yyyy"),
                        cc.TGVao?.ToString(@"hh\:mm") ?? "",
                        cc.TGRa?.ToString(@"hh\:mm") ?? "",
                        cc.TGVaoTangCa?.ToString(@"hh\:mm") ?? "",
                        cc.TGRaTangCa?.ToString(@"hh\:mm") ?? "",
                        cc.TrangThai,
                        cc.VangCoPhep?.ToString() ?? "0"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu chấm công: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView_ChamCong_SelectionChanged(object sender, EventArgs e)
        {
            //Hàm này tượng tự DataGridView_DSNV_Luong_SelectionChanged_1
        }
        private void LoadChamCongChiTiet(ChamCongDTO chamCong)
        {
            //Hàm này tương tự LoadLuongChiTiet
        }
        #endregion
    }
}