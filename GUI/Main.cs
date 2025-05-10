using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BLL;
using DTO;

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
            // Set kích thước cố định
            this.Width = 1150;
            this.Height = 600;

            // Tắt resize
            this.FormBorderStyle = FormBorderStyle.FixedSingle;//ép form không được kéo giãn.

            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void Main_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadNhanVienData();
            LoadTinhLuongData();
            LoadChamCongData();
            LoadMaNV();// Phan nay them vao de ComBoBox hiện danh sách mã nhân viên(Long)
            // Khởi tạo timer để cập nhật thời gian hiện tại
            timer1.Interval = 1000; // 1 giây
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        #region Thiết lập điều khiển
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Update the label with current system time
            lbl_ThoiGianHienTai.Text = DateTime.Now.ToString("HH:mm:ss");
        }
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
            DataGridView_DSNV_Luong.Columns.Add("MaLuong", "Mã Lương");
            DataGridView_DSNV_Luong.Columns.Add("NgayXetLuong", "Ngày Xét Lương");
            DataGridView_DSNV_Luong.Columns.Add("TongNgayLam", "Tổng Ngày Làm");  // Đảm bảo tên này khớp
            DataGridView_DSNV_Luong.Columns.Add("TongNgaynghi", "Tổng Ngày Nghỉ");  // Đảm bảo tên này khớp
            DataGridView_DSNV_Luong.Columns.Add("TroCap", "Trợ Cấp");
            DataGridView_DSNV_Luong.Columns.Add("TamUng", "Tạm Ứng");



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
            dataGridView_ChamCong.Columns.Add("Vang", "Vắng");

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
        private void LoadMaNV()
        {
            try
            {
                // Gọi NhanVienBLL để lấy danh sách Mã Nhân Viên
                List<string> maNhanVienList = bll.GetAllMaNhanVien();  // bll là đối tượng của NhanVienBLL
                cbb_MaNV.Items.Clear();  // Xóa hết các mục trong ComboBox trước khi thêm

                // Kiểm tra xem có mã nhân viên nào không
                if (maNhanVienList.Count == 0)
                {
                    MessageBox.Show("Không có mã nhân viên nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Thêm tất cả mã nhân viên vào ComboBox
                foreach (var maNV in maNhanVienList)
                {
                    cbb_MaNV.Items.Add(maNV);  // Thêm Mã Nhân Viên vào ComboBox
                }

                // Chọn mã nhân viên đầu tiên nếu có
                if (cbb_MaNV.Items.Count > 0)
                {
                    cbb_MaNV.SelectedIndex = 0;  // Chọn item đầu tiên
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải mã nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    SetupDataGridView();  // Thiết lập cột nếu chưa thiết lập
                }

                DataGridView_DSNV_Luong.Rows.Clear();

                foreach (var luong in danhSach)
                {
                    DataGridView_DSNV_Luong.Rows.Add(
                        luong.MaNV,                // Mã nhân viên
                        luong.MaLuong,             // Mã lương
                        luong.NgayXetLuong.ToString("dd/MM/yyyy"),  // Ngày xét lương
                        luong.TongNgayLam,       // Tổng số ngày làm
                        luong.TongNgaynghi,      // Tổng số ngày nghỉ
                        luong.TroCap,              // Trợ cấp
                        luong.TamUng               // Tạm ứng
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
            
        }


        #endregion

        #region Quản lý Chấm công
        private void LoadChamCongData()
        {
            try
            {
                dataGridView_ChamCong.Rows.Clear();
                List<ChamCongDTO> chamCongList = chamCongBLL.GetDanhSachChamCong();

                if (chamCongList.Count == 0)
                {
                    MessageBox.Show("No data found in ChamCong table.");
                }

                foreach (var chamCong in chamCongList)
                {
                    // Populate the grid with attendance data
                    dataGridView_ChamCong.Rows.Add(
                        chamCong.MaCC,
                        chamCong.MaNV,
                        chamCong.NgayCC.ToString("dd/MM/yyyy"),
                        chamCong.TGVao.HasValue ? chamCong.TGVao.Value.ToString(@"hh\:mm") : "-",   // Handling NULL TGVao
                        chamCong.TGRa.HasValue ? chamCong.TGRa.Value.ToString(@"hh\:mm") : "-",      // Handling NULL TGRa
                        chamCong.Vang // Vang can be 0, 1, or 2; handle accordingly
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu Chấm Công: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //private void LoadChamCongChiTiet(string maNV, DateTime ngayCC)
        //{
            
        //}



        private void dataGridView_ChamCong_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView_ChamCong.SelectedRows.Count > 0)
                {
                    var row = dataGridView_ChamCong.SelectedRows[0];

                    // Lấy dữ liệu cơ bản
                    string maNV = row.Cells["MaNV"].Value?.ToString();
                    string maCC = row.Cells["MaCC"].Value?.ToString();
                    DateTime ngayCC = DateTime.ParseExact(row.Cells["NgayCC"].Value.ToString(), "dd/MM/yyyy", null);

                    lbl_MaNV.Text = maNV;
                    lbl_MaCC.Text = maCC;
                    lbl_NgayCC.Text = ngayCC.ToString("dd/MM/yyyy");

                    // Chuẩn bị thời gian chuẩn
                    TimeSpan timeStart = new TimeSpan(8, 0, 0);   // 08:00
                    TimeSpan timeEnd = new TimeSpan(17, 0, 0);    // 17:00

                    // Lấy và parse thời gian vào/ra
                    TimeSpan tgVao = TimeSpan.Zero;
                    TimeSpan tgRa = TimeSpan.Zero;
                    bool isVang = int.TryParse(row.Cells["Vang"].Value.ToString(), out int vang);

                    // Kiểm tra thời gian vào (TGVao) có hợp lệ không (không phải TimeSpan.Zero)
                    if (row.Cells["TGVao"].Value != null && TimeSpan.TryParse(row.Cells["TGVao"].Value.ToString(), out tgVao) && tgVao != TimeSpan.Zero)
                    {
                        // Tính thời gian đi trễ nếu thời gian vào sau 08:00
                        if (tgVao > timeStart)
                        {
                            TimeSpan tre = tgVao - timeStart;
                            lbl_ThoiGianDiTre.Text = tre.ToString(@"hh\:mm");
                        }
                        else
                        {
                            lbl_ThoiGianDiTre.Text = "-"; // Không trễ nếu vào trước hoặc đúng 08:00
                        }
                    }
                    else
                    {
                        lbl_ThoiGianDiTre.Text = "-";  // Nếu không có thời gian vào hợp lệ
                    }

                    // Kiểm tra thời gian ra (TGRa) có hợp lệ không (không phải TimeSpan.Zero)
                    if (row.Cells["TGRa"].Value != null && TimeSpan.TryParse(row.Cells["TGRa"].Value.ToString(), out tgRa) && tgRa != TimeSpan.Zero)
                    {
                        // Tính thời gian về sớm nếu thời gian ra trước 17:00
                        if (tgRa < timeEnd)
                        {
                            TimeSpan veSom = timeEnd - tgRa;
                            lbl_ThoiGianVeSom.Text = veSom.ToString(@"hh\:mm");
                        }
                        else
                        {
                            lbl_ThoiGianVeSom.Text = "-"; // Không về sớm nếu ra đúng hoặc sau 17:00
                        }
                    }
                    else
                    {
                        lbl_ThoiGianVeSom.Text = "-";  // Nếu không có thời gian ra hợp lệ
                    }

                    // Xử lý logic vắng (vắng có phép, vắng không phép, hoặc không vắng)
                    if (isVang)
                    {
                        switch (vang)
                        {
                            case 1:
                                lbl_Vang.Text = "Vắng có phép";
                                break;
                            case 2:
                                lbl_Vang.Text = "Vắng không phép";
                                break;
                            default:
                                lbl_Vang.Text = "Không vắng";
                                break;
                        }
                    }

                    // Kiểm tra trạng thái check-in/check-out
                    if (tgVao == TimeSpan.Zero && tgRa == TimeSpan.Zero)
                    {
                        lbl_TrangThai.Text = "Vắng";
                    }
                    else if (tgVao != TimeSpan.Zero && tgRa == TimeSpan.Zero)
                    {
                        lbl_TrangThai.Text = "Chưa check-out";
                    }
                    else if (tgVao == TimeSpan.Zero && tgRa != TimeSpan.Zero)
                    {
                        lbl_TrangThai.Text = "Chưa check-in";
                    }
                    else
                    {
                        lbl_TrangThai.Text = "Đã check-out";
                    }

                    
                    
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xử lý dữ liệu chấm công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void button_CheckIn_Click(object sender, EventArgs e)
        {
            try
            {
                // Get selected MaNV from ComboBox
                string maNV = cbb_MaNV.SelectedItem.ToString();
                DateTime ngayHienTai = DateTime.Now.Date; // Get today's date
                TimeSpan tgVao = DateTime.Now.TimeOfDay;  // Get current time for TGVao

                // Check if the employee has already checked in today
                var chamCongList = chamCongBLL.GetDanhSachChamCong();
                var existingRecord = chamCongList.FirstOrDefault(cc => cc.MaNV == maNV && cc.NgayCC.Date == ngayHienTai);

                if (existingRecord != null)
                {
                    // If the employee has already checked in, we show the message and skip further processing
                    if (existingRecord.TGVao != null)
                    {
                        MessageBox.Show("Bạn đã check-in hôm nay rồi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;  // Exit if the employee has already checked in
                    }
                    else
                    {
                        // If the employee hasn't checked in yet, update the check-in time (TGVao)
                        existingRecord.TGVao = tgVao;
                        existingRecord.Vang = 0;  // Set Vang to 0 (present)
                        bool success = chamCongBLL.UpdateChamCong(existingRecord);

                        if (success)
                        {
                            MessageBox.Show("Check-in thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadChamCongData();  // Reload the attendance data
                        }
                        else
                        {
                            MessageBox.Show("Lỗi khi check-in!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    // If no attendance record exists, show error message
                    MessageBox.Show("Không tìm thấy bản ghi chấm công cho hôm nay.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thực hiện Check-in: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_CheckOut_Click(object sender, EventArgs e)
        {
            try
            {
                // Get selected MaNV from ComboBox
                string maNV = cbb_MaNV.SelectedItem.ToString();
                DateTime ngayHienTai = DateTime.Now.Date; // Get today's date
                TimeSpan tgRa = DateTime.Now.TimeOfDay;  // Get current time for TGRa

                // Check if the employee has already checked in today
                var chamCongList = chamCongBLL.GetDanhSachChamCong();
                var existingRecord = chamCongList.FirstOrDefault(cc => cc.MaNV == maNV && cc.NgayCC.Date == ngayHienTai && cc.TGVao.HasValue && !cc.TGRa.HasValue);

                if (existingRecord != null)
                {
                    // If the employee has checked in but hasn't checked out yet
                    TimeSpan tgVao = existingRecord.TGVao.Value;
                    if (tgRa - tgVao < TimeSpan.FromMinutes(30))
                    {
                        var dialogResult = MessageBox.Show(
                            "Thời gian check-out quá sớm. Bạn có muốn tiếp tục checkout không?",
                            "Cảnh báo",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                        );

                        if (dialogResult == DialogResult.No)
                        {
                            return; // Cancel checkout if user presses No
                        }
                    }

                    // Update the TGRa if the employee hasn't checked out yet
                    existingRecord.TGRa = tgRa;
                    existingRecord.Vang = 0;  // Set Vang to 0 (present)
                    bool success = chamCongBLL.UpdateChamCong(existingRecord);

                    if (success)
                    {
                        MessageBox.Show("Check-out thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadChamCongData();  // Reload the attendance data
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi check-out!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa check-in hoặc đã checkout hôm nay.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thực hiện Check-out: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void cbx_PhepVang_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu Mã NV được chọn trong ComboBox
                string maNV = cbb_MaNV.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(maNV))
                {
                    MessageBox.Show("Vui lòng chọn Mã Nhân Viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;  // Nếu chưa chọn mã nhân viên thì không làm gì
                }

                // Lấy thông tin chấm công của nhân viên từ CSDL hoặc danh sách
                var chamCongList = chamCongBLL.GetDanhSachChamCong();
                var chamCong = chamCongList.FirstOrDefault(cc => cc.MaNV == maNV && cc.NgayCC.Date == DateTime.Now.Date);

                if (chamCong == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu chấm công cho nhân viên này trong ngày hôm nay.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra nếu checkbox được tích vào và giá trị Vang là 2 (vắng không phép), cho phép đổi thành vắng có phép (Vang = 1)
                if (cbx_PhepVang.Checked)
                {
                    if (chamCong.Vang == 0 || chamCong.Vang == 1)
                    {
                        // Nếu Vang = 0 (không vắng) hoặc Vang = 1 (vắng có phép), không thể thay đổi
                        MessageBox.Show("Không thể thay đổi tình trạng vắng khi nhân viên đã check-in hoặc đã vắng có phép.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (chamCong.Vang == 2) // Nếu Vang = 2 (vắng không phép), cho phép thay đổi thành vắng có phép
                    {
                        var dialogResult = MessageBox.Show(
                            "Bạn muốn thay đổi tình trạng vắng không phép thành vắng có phép?",
                            "Thông báo",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (dialogResult == DialogResult.Yes)
                        {
                            // Thay đổi giá trị Vang thành 1 (vắng có phép)
                            chamCong.Vang = 1;

                            // Cập nhật vào CSDL
                            bool success = chamCongBLL.UpdateChamCong(chamCong);
                            if (success)
                            {
                                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Cập nhật không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            cbx_PhepVang.Checked = false; // Hủy bỏ checkbox nếu người dùng chọn No
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xử lý dữ liệu chấm công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button_TaoLichCC_Click(object sender, EventArgs e)
        {
            try
            {
                // Get today's date
                DateTime ngayHienTai = DateTime.Now.Date;  // Only the date part, without time

                // Get all employee IDs from LoadMaNV()
                List<string> maNVList = bll.GetAllMaNhanVien();  // Assuming bll.GetAllMaNhanVien() gets all employee IDs

                // Get the highest MaCC currently available in the database through ChamCongBLL
                string lastMaCC = chamCongBLL.GetLastMaCC(); // Calling ChamCongBLL to fetch the last MaCC
                int recordNumber = 1;

                if (!string.IsNullOrEmpty(lastMaCC))
                {
                    // Extract the last number from MaCC (CC001 -> 1, CC002 -> 2, ...)
                    recordNumber = int.Parse(lastMaCC.Substring(2)) + 1;
                }

                // Loop through each employee to create attendance records
                foreach (var maNV in maNVList)
                {
                    // Check if attendance already exists for this employee for today
                    var existingRecord = chamCongBLL.GetDanhSachChamCong().FirstOrDefault(cc => cc.MaNV == maNV && cc.NgayCC.Date == ngayHienTai);

                    // If record exists, skip creating new one
                    if (existingRecord != null)
                    {
                        MessageBox.Show($"Lịch chấm công đã tồn tại cho {maNV} hôm nay.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        continue;  // Skip to the next employee if record exists
                    }

                    // If no existing record, create a new one
                    string maCC = $"CC{recordNumber:D3}";  // Generate MaCC in format CC001, CC002, ...

                    // Create a new ChamCongDTO object
                    ChamCongDTO chamCongDTO = new ChamCongDTO
                    {
                        MaCC = maCC,
                        MaNV = maNV,
                        NgayCC = ngayHienTai,
                        TGVao = null,  // No time in yet
                        TGRa = null,   // No time out yet
                        Vang = 2       // Mark as absent by default
                    };

                    // Insert the new attendance record into the database (InsertChamCong method)
                    bool success = chamCongBLL.InsertChamCong(chamCongDTO);

                    if (success)
                    {
                        MessageBox.Show($"Tạo lịch chấm công thành công cho {maNV}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Tạo lịch chấm công thất bại cho {maNV}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    recordNumber++;  // Increment record number for MaCC
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo lịch chấm công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button_RefreshDGV_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear existing rows in the DataGridView
                dataGridView_ChamCong.Rows.Clear();

                // Reload the data from the database or any data source
                LoadChamCongData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while restarting the DataGridView: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


    }
}