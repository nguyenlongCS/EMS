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
            LoadMaNV();
            timer1.Interval = 1000; // 1 giây
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        #region Thiết lập điều khiển
        private void timer1_Tick(object sender, EventArgs e)
        {
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
            //dataGridView1.Columns.Add("DiaChi", "Địa Chỉ");
            //dataGridView1.Columns.Add("SoDT", "Số ĐT");
            //dataGridView1.Columns.Add("Email", "Email");
            //dataGridView1.Columns.Add("NgaySinh", "Ngày Sinh");
            //dataGridView1.Columns.Add("GioiTinh", "Giới Tính");
            //dataGridView1.Columns.Add("CCCD", "CCCD");
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
            DataGridView_DSNV_Luong.Columns.Add("NgayXetLuong", "Ngày Xét Lương");
            DataGridView_DSNV_Luong.Columns.Add("TongNgayLam", "Tổng Ngày Làm");  
            DataGridView_DSNV_Luong.Columns.Add("TongNgaynghi", "Tổng Ngày Nghỉ");  
            DataGridView_DSNV_Luong.Columns.Add("TroCap", "Trợ Cấp");
            DataGridView_DSNV_Luong.Columns.Add("TamUng", "Tạm Ứng");
            DataGridView_DSNV_Luong.Columns.Add("LuongNhanDuoc", "Lương nhận được");



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
                    dataGridView1.Rows.Add(nv.MaNV, nv.HoNV + " " + nv.TenNV, nv.ChucVu, nv.MaPB);
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

                if (bll.InsertNhanVienBLL(nv))
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
                        luong.NgayXetLuong.ToString("dd/MM/yyyy"),  // Ngày xét lương
                        luong.TongNgayLam,       // Tổng số ngày làm
                        luong.TongNgaynghi,      // Tổng số ngày nghỉ
                        luong.TroCap,              // Trợ cấp
                        luong.TamUng,               // Tạm ứng
                        luong.LuongNhanDuoc
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu Lương: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DataGridView_DSNV_Luong_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có chọn được dòng không
                if (DataGridView_DSNV_Luong.SelectedRows.Count > 0)
                {
                    // Lấy mã nhân viên từ cột MaNV trong dòng được chọn
                    string maNV = DataGridView_DSNV_Luong.SelectedRows[0].Cells["MaNV"].Value.ToString();

                    // Gọi hàm LoadLuongChiTiet để lấy thông tin lương chi tiết và lương cơ bản
                    LoadLuongChiTiet(maNV);
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có sự cố
                MessageBox.Show($"Lỗi khi chọn dòng Lương: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLuongChiTiet(string maNV)
        {
            try
            {
                // Lấy dữ liệu lương chi tiết cho nhân viên từ BLL
                DataRow luongChiTiet = luongBLL.GetLuongChiTiet(maNV);

                if (luongChiTiet != null)
                {
                    // Cập nhật các label với dữ liệu lương chi tiết
                    lbl_SoNgayLamViec.Text = luongChiTiet["TongNgayLam"].ToString();
                    lbl_SoNgayNghi.Text = luongChiTiet["TongNgayNghi"].ToString();
                    lbl_SoNNghiCoPhep.Text = luongChiTiet["TongNgayNghiCoPhep"].ToString();
                    lbl_TongNgayDiTre.Text = luongChiTiet["TongNgayDiTre"].ToString();
                    lbl_TongNgayVeSom.Text = luongChiTiet["TongNgayVeSom"].ToString();
                    lbl_LuongNhanDuoc.Text = string.Format("{0:N0} VND", Convert.ToDouble(luongChiTiet["LuongNhanDuoc"]));  
                    lbl_ThueVat.Text = string.Format("{0:N0} VND", Convert.ToDouble(luongChiTiet["ThueVat"]));  

                    // Định dạng và hiển thị các giá trị trợ cấp và tạm ứng
                    lbl_TroCap.Text = string.Format("{0:N0} VND", Convert.ToDouble(luongChiTiet["TroCap"]));
                    lbl_TamUng.Text = string.Format("{0:N0} VND", Convert.ToDouble(luongChiTiet["TamUng"]));

                    // Lấy thông tin lương theo bậc từ bảng LuongTheoBac
                    string maBacLuong = luongChiTiet["MaBacLuong"].ToString(); 
                    DataRow salaryData = luongBLL.GetLuongTheoBac(maBacLuong); 
                    if (salaryData != null)
                    {
                        // Cập nhật các label với dữ liệu lương cơ bản và bậc lương
                        lbl_LuongCanban.Text = string.Format("{0:N2} VND", Convert.ToDecimal(salaryData["LuongCanBanTheoBac"]));
                        lbl_BacLuong.Text = $"Bậc {salaryData["BacLuong"]}";
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin lương theo bậc cho nhân viên này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Nếu không tìm thấy dữ liệu lương chi tiết, xóa các label
                    lbl_SoNgayLamViec.Text = "0";
                    lbl_SoNgayNghi.Text = "0";
                    lbl_SoNNghiCoPhep.Text = "0";
                    lbl_TongNgayDiTre.Text = "0";
                    lbl_TongNgayVeSom.Text = "0";
                    lbl_TroCap.Text = "0 VND";
                    lbl_TamUng.Text = "0 VND";
                    lbl_LuongCanban.Text = "0 VND";
                    lbl_BacLuong.Text = "Bậc 0";
                    lbl_LuongNhanDuoc.Text = "0 VND";
                    lbl_ThueVat.Text = "0 VND";


                    MessageBox.Show($"Không tìm thấy thông tin lương chi tiết cho nhân viên {maNV}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu lương chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                // Lấy Mã Nhân Viên từ ComboBox (hoặc từ bảng chấm công đã chọn)
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

                // Nếu checkbox được tích vào, thay đổi trạng thái Vang thành "vắng có phép" (Vang = 1)
                if (cbx_PhepVang.Checked)
                {
                    if (chamCong.Vang == 0 || chamCong.Vang == 1)
                    {
                        MessageBox.Show("Không thể thay đổi tình trạng vắng khi nhân viên đã check-in hoặc đã vắng có phép.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (chamCong.Vang == 2) // Nếu Vang = 2 (vắng không phép), cho phép thay đổi thành vắng có phép
                    {
                        chamCong.Vang = 1;  // Đánh dấu vắng có phép

                        // Cập nhật lại vào CSDL
                        bool success = chamCongBLL.UpdateChamCong(chamCong);
                        if (success)
                        {
                            MessageBox.Show("Cập nhật tình trạng vắng có phép thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Cập nhật lại bảng chấm công và các label hiển thị
                            LoadChamCongData();
                            LoadLuongChiTiet(maNV);  // Làm mới thông tin lương chi tiết
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else // Nếu checkbox không được tích vào (vắng không phép)
                {
                    if (chamCong.Vang == 1)
                    {
                        chamCong.Vang = 2;  // Đánh dấu vắng không phép

                        // Cập nhật lại vào CSDL
                        bool success = chamCongBLL.UpdateChamCong(chamCong);
                        if (success)
                        {
                            MessageBox.Show("Cập nhật tình trạng vắng không phép thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Cập nhật lại bảng chấm công và các label hiển thị
                            LoadChamCongData();
                            LoadLuongChiTiet(maNV);  // Làm mới thông tin lương chi tiết
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                LoadChamCongData();  // Làm mới bảng chấm công
                LoadTinhLuongData();  // Làm mới bảng lương
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo lịch chấm công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button_Delete_Click(object sender, EventArgs e)
        {
            string maNV = textBox_Employeed.Text.Trim();

            if (string.IsNullOrEmpty(maNV))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên có mã \"{maNV}\" không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (bll.DeleteNhanVienBLL(maNV))
                {
                    MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNhanVienData(); // Cập nhật lại danh sách nhân viên
                }
                else
                {
                    MessageBox.Show("Xóa nhân viên thất bại hoặc mã nhân viên không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bt_SX_TheoTen_Click(object sender, EventArgs e)
        {
            var danhSach = bll.GetNV_SX_TenNV_BLL();
            dataGridView1.DataSource = danhSach;
            dataGridView_DSNV.DataSource = danhSach;
        }

        private void bt_SX_TheoMa_Click(object sender, EventArgs e)
        {
            var danhSach = bll.GetNV_SX_MaNV_BLL();
            dataGridView1.DataSource = danhSach;
            dataGridView_DSNV.DataSource = danhSach;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Kiểm tra xem người dùng có click vào hàng hợp lệ không (RowIndex >= 0)
                if (e.RowIndex >= 0)
                {
                    // Lấy thông tin hàng vừa click
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    textBox_Employeed.Text = row.Cells["MaNV"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xử lý click vào hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox_Employeed.Text))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var nv = new NhanVienDTO
                {
                    MaNV = textBox_Employeed.Text,  
                    HoNV = string.IsNullOrEmpty(textBox_Surname.Text) ? null : textBox_Surname.Text, 
                    TenNV = string.IsNullOrEmpty(textBox_Name.Text) ? null : textBox_Name.Text, 
                    DiaChi = string.IsNullOrEmpty(textBox_DiaChi.Text) ? null : textBox_DiaChi.Text,
                    SoDT = string.IsNullOrEmpty(textBox_SDT.Text) ? null : textBox_SDT.Text, 
                    Email = string.IsNullOrEmpty(textBox_email.Text) ? null : textBox_email.Text, 
                    NgaySinh = dateTime_DateOfBirth.Value, 
                    GioiTinh = radioBut_Male.Checked, 
                    CCCD = string.IsNullOrEmpty(textBox_CCCD.Text) ? null : textBox_CCCD.Text, 
                    ChucVu = string.IsNullOrEmpty(textBox_ChucVu.Text) ? null : textBox_ChucVu.Text, 
                    MaPB = string.IsNullOrEmpty(textBox_MaPhongBan.Text) ? null : textBox_MaPhongBan.Text 
                };

                // Gọi BLL để cập nhật dữ liệu
                bool isUpdated = bll.UpdateNhanVien(nv);

                if (isUpdated)
                {
                    MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNhanVienData();  // Reload lại dữ liệu
                }
                else
                {
                    MessageBox.Show("Cập nhật nhân viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Bt_ClearnTxtBox_Click(object sender, EventArgs e)
        {
            textBox_Employeed.Clear();
            textBox_Surname.Clear();
            textBox_Name.Clear();
            textBox_DiaChi.Clear();
            textBox_SDT.Clear();
            textBox_email.Clear();
            textBox_CCCD.Clear();
            textBox_ChucVu.Clear();
            textBox_MaPhongBan.Clear();
            dateTime_DateOfBirth.Value = DateTime.Now;  
            radioBut_Male.Checked = false; 
        }
        #endregion

        
    }
}