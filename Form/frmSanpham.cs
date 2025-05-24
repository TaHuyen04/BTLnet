using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QLCHBanXeMay.Class;

namespace QLCHBanXeMay.form
{
    public partial class frmSanpham : Form
    {
       

        public frmSanpham()
        {
            InitializeComponent();
        }
        private string imageFolder = "Images"; // Thư mục lưu ảnh, nằm trong thư mục gốc của ứng dụng

        // Biến trạng thái kiểm tra chế độ
        private bool isAdding = false; // Đang thêm mới sản phẩm
        private bool isEditing = false; // Đang sửa sản phẩm
        private DataTable tblHang; // Lưu dữ liệu sản phẩm

        // Biến lưu trạng thái ban đầu khi sửa
        private string initialMaSP, initialTenSP, initialAnh;
        private string initialSoLuong, initialDonGiaNhap, initialDonGiaBan, initialThoiGianBH;
        private string initialMaLoai, initialMaNhaSX, initialMaMau, initialMaPhanh, initialMaDongCo, initialMaTinhTrang, initialMaNuocSX;

        private void frmSanpham_Load(object sender, EventArgs e)
        {
            // Đặt ReadOnly cho các TextBox
            txtMaSP.ReadOnly = false; // Cho phép nhập để tìm kiếm
            txtTenSP.ReadOnly = false; // Cho phép nhập để tìm kiếm
            txtSoLuong.ReadOnly = false; // Cho phép nhập để tìm kiếm
            txtDonGiaNhap.ReadOnly = false; // Cho phép nhập để tìm kiếm
            txtDonGiaBan.ReadOnly = false; // Cho phép nhập để tìm kiếm
            txtThoiGianBH.ReadOnly = false; // Cho phép nhập để tìm kiếm
            txtAnh.ReadOnly = false; // Cho phép nhập để tìm kiếm

            // Vô hiệu hóa các nút thao tác
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            // Tải dữ liệu vào DataGridView
            Load_dgvSanPham();

            // Nạp dữ liệu vào các ComboBox
            Functions.FillCombo("SELECT MaMau, TenMau FROM tblMauSac", cboMauSac, "MaMau", "TenMau");
            cboMauSac.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMauSac.SelectedIndex = -1;

            Functions.FillCombo("SELECT MaLoai, TenLoai FROM tblTheLoai", cboLoai, "MaLoai", "TenLoai");
            cboLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoai.SelectedIndex = -1;

            Functions.FillCombo("SELECT MaHangSX, TenHangSX FROM tblHangSX", cboNhaSX, "MaHangSX", "TenHangSX");
            cboNhaSX.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNhaSX.SelectedIndex = -1;

            Functions.FillCombo("SELECT MaPhanh, TenPhanh FROM tblPhanhXe", cboPhanh, "MaPhanh", "TenPhanh");
            cboPhanh.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPhanh.SelectedIndex = -1;

            Functions.FillCombo("SELECT MaDongCo, TenDongCo FROM tblDongCo", cboDongCo, "MaDongCo", "TenDongCo");
            cboDongCo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDongCo.SelectedIndex = -1;

            Functions.FillCombo("SELECT MaTinhTrang, TenTinhTrang FROM tblTinhTrang", cboTinhTrang, "MaTinhTrang", "TenTinhTrang");
            cboTinhTrang.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTinhTrang.SelectedIndex = -1;

            Functions.FillCombo("SELECT MaNuocSX, TenNuocSX FROM tblNuocSX", cboNuocSX, "MaNuocSX", "TenNuocSX");
            cboNuocSX.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNuocSX.SelectedIndex = -1;

            // Xóa dữ liệu đầu vào
            ResetValues();

            // Tạo thư mục Images nếu chưa tồn tại
            string imagePath = Path.Combine(Application.StartupPath, imageFolder);
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }
        }

        private void Load_dgvSanPham()
        {
            // Tải toàn bộ dữ liệu sản phẩm
            string sql = @"
                SELECT 
                    H.MaHang AS [Mã xe],
                    H.TenHang AS [Tên xe],
                    L.TenLoai AS [Loại],
                    SX.TenHangSX AS [Hãng SX],
                    MS.TenMau AS [Màu],
                    P.TenPhanh AS [Phanh],
                    D.TenDongCo AS [Động cơ],
                    NSX.TenNuocSX AS [Nước sản xuất],
                    H.SoLuong AS [Số lượng],
                    H.DonGiaNhap AS [Đơn giá nhập],
                    H.DonGiaBan AS [Đơn giá bán],
                    TTT.TenTinhTrang AS [Tình trạng],
                    H.ThoiGianBaoHanh AS [Bảo hành (tháng)],
                    H.Anh AS [Ảnh]
                FROM tblDMHang H
                LEFT JOIN tblTheLoai L ON H.MaLoai = L.MaLoai
                LEFT JOIN tblHangSX SX ON H.MaHangSX = SX.MaHangSX
                LEFT JOIN tblMauSac MS ON H.MaMau = MS.MaMau
                LEFT JOIN tblPhanhXe P ON H.MaPhanh = P.MaPhanh
                LEFT JOIN tblDongCo D ON H.MaDongCo = D.MaDongCo
                LEFT JOIN tblTinhTrang TTT ON H.MaTinhTrang = TTT.MaTinhTrang
                LEFT JOIN tblNuocSX NSX ON H.MaNuocSX = NSX.MaNuocSX";
            tblHang = Functions.getdatatotable(sql);
            dgvSanPham.DataSource = tblHang;

            // Ngăn chỉnh sửa thủ công
            dgvSanPham.AllowUserToAddRows = false;
            dgvSanPham.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ResetValues()
        {
            // Xóa dữ liệu đầu vào
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            txtSoLuong.Text = "";
            txtDonGiaNhap.Text = "";
            txtDonGiaBan.Text = "";
            txtThoiGianBH.Text = "";
            txtAnh.Text = "";
            picAnh.Image = null;

            cboLoai.SelectedIndex = -1;
            cboNhaSX.SelectedIndex = -1;
            cboMauSac.SelectedIndex = -1;
            cboPhanh.SelectedIndex = -1;
            cboDongCo.SelectedIndex = -1;
            cboTinhTrang.SelectedIndex = -1;
            cboNuocSX.SelectedIndex = -1;

            // Đặt lại trạng thái
            isAdding = false;
            isEditing = false;
            initialMaSP = initialTenSP = initialAnh = initialSoLuong = initialDonGiaNhap = initialDonGiaBan = initialThoiGianBH = "";
            initialMaLoai = initialMaNhaSX = initialMaMau = initialMaPhanh = initialMaDongCo = initialMaTinhTrang = initialMaNuocSX = "";
        }

        private void SaveInitialValues()
        {
            // Lưu trạng thái ban đầu khi sửa
            initialMaSP = txtMaSP.Text;
            initialTenSP = txtTenSP.Text;
            initialSoLuong = txtSoLuong.Text;
            initialDonGiaNhap = txtDonGiaNhap.Text;
            initialDonGiaBan = txtDonGiaBan.Text;
            initialThoiGianBH = txtThoiGianBH.Text;
            initialAnh = txtAnh.Text;
            initialMaLoai = cboLoai.SelectedValue?.ToString() ?? "";
            initialMaNhaSX = cboNhaSX.SelectedValue?.ToString() ?? "";
            initialMaMau = cboMauSac.SelectedValue?.ToString() ?? "";
            initialMaPhanh = cboPhanh.SelectedValue?.ToString() ?? "";
            initialMaDongCo = cboDongCo.SelectedValue?.ToString() ?? "";
            initialMaTinhTrang = cboTinhTrang.SelectedValue?.ToString() ?? "";
            initialMaNuocSX = cboNuocSX.SelectedValue?.ToString() ?? "";
        }

        private bool HasChanges()
        {
            // Kiểm tra thay đổi dữ liệu
            return txtMaSP.Text != initialMaSP ||
                   txtTenSP.Text != initialTenSP ||
                   txtSoLuong.Text != initialSoLuong ||
                   txtDonGiaNhap.Text != initialDonGiaNhap ||
                   txtDonGiaBan.Text != initialDonGiaBan ||
                   txtThoiGianBH.Text != initialThoiGianBH ||
                   txtAnh.Text != initialAnh ||
                   (cboLoai.SelectedValue?.ToString() ?? "") != initialMaLoai ||
                   (cboNhaSX.SelectedValue?.ToString() ?? "") != initialMaNhaSX ||
                   (cboMauSac.SelectedValue?.ToString() ?? "") != initialMaMau ||
                   (cboPhanh.SelectedValue?.ToString() ?? "") != initialMaPhanh ||
                   (cboDongCo.SelectedValue?.ToString() ?? "") != initialMaDongCo ||
                   (cboTinhTrang.SelectedValue?.ToString() ?? "") != initialMaTinhTrang ||
                   (cboNuocSX.SelectedValue?.ToString() ?? "") != initialMaNuocSX;
        }

        private string GenerateNewMaSP()
        {
            // Tạo mã sản phẩm tự động (H01, H02, ...)
            string sql = "SELECT MAX(MaHang) FROM tblDMHang";
            string lastMaSP = Functions.GetFieldValues(sql);
            if (string.IsNullOrEmpty(lastMaSP))
                return "H01";
            int number = int.Parse(lastMaSP.Substring(1)) + 1;
            return "H" + number.ToString("D2");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Cho phép nhập liệu cho thêm
            txtMaSP.ReadOnly = false;
            txtTenSP.ReadOnly = false;
            txtSoLuong.ReadOnly = false;
            txtDonGiaNhap.ReadOnly = false;
            txtDonGiaBan.ReadOnly = true; // Giá bán tự động tính
            txtThoiGianBH.ReadOnly = false;
            txtAnh.ReadOnly = false;

            // Điều chỉnh trạng thái nút
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            btnThem.Enabled = false;

            // Reset và sinh mã sản phẩm mới
            ResetValues();
            txtMaSP.Text = GenerateNewMaSP();
            isAdding = true;
            txtMaSP.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSP.Text))
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                string sql = "DELETE FROM tblDMHang WHERE MaHang = '" + txtMaSP.Text + "'";
                Functions.Runsql(sql);
                Load_dgvSanPham();
                ResetValues();
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                MessageBox.Show("Đã xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể xóa sản phẩm do lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (tblHang.Rows.Count == 0)
            {
                MessageBox.Show("Không có sản phẩm để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtMaSP.Text))
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cho phép nhập liệu cho sửa
            txtMaSP.ReadOnly = false;
            txtTenSP.ReadOnly = false;
            txtSoLuong.ReadOnly = false;
            txtDonGiaNhap.ReadOnly = false;
            txtDonGiaBan.ReadOnly = true; // Giá bán tự động tính
            txtThoiGianBH.ReadOnly = false;
            txtAnh.ReadOnly = false;

            // Lưu trạng thái ban đầu
            SaveInitialValues();
            isEditing = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            btnSua.Enabled = false;
            txtTenSP.Focus();
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (isAdding || isEditing)
            {
                MessageBox.Show("Vui lòng hoàn tất hoặc hủy thao tác thêm/sửa trước khi tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem có nhập ít nhất một trường
            if (string.IsNullOrEmpty(txtMaSP.Text) &&
                string.IsNullOrEmpty(txtTenSP.Text) &&
                string.IsNullOrEmpty(txtSoLuong.Text) &&
                string.IsNullOrEmpty(txtDonGiaNhap.Text) &&
                string.IsNullOrEmpty(txtDonGiaBan.Text) &&
                string.IsNullOrEmpty(txtThoiGianBH.Text) &&
                string.IsNullOrEmpty(txtAnh.Text) &&
                cboLoai.SelectedIndex == -1 &&
                cboNhaSX.SelectedIndex == -1 &&
                cboMauSac.SelectedIndex == -1 &&
                cboPhanh.SelectedIndex == -1 &&
                cboDongCo.SelectedIndex == -1 &&
                cboTinhTrang.SelectedIndex == -1 &&
                cboNuocSX.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập hoặc chọn ít nhất một trường để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xây dựng câu lệnh SQL tìm kiếm
            string sql = @"
                SELECT 
                    H.MaHang AS [Mã xe],
                    H.TenHang AS [Tên xe],
                    L.TenLoai AS [Loại],
                    SX.TenHangSX AS [Hãng SX],
                    MS.TenMau AS [Màu],
                    P.TenPhanh AS [Phanh],
                    D.TenDongCo AS [Động cơ],
                    NSX.TenNuocSX AS [Nước sản xuất],
                    H.SoLuong AS [Số lượng],
                    H.DonGiaNhap AS [Đơn giá nhập],
                    H.DonGiaBan AS [Đơn giá bán],
                    TTT.TenTinhTrang AS [Tình trạng],
                    H.ThoiGianBaoHanh AS [Bảo hành (tháng)],
                    H.Anh AS [Ảnh]
                FROM tblDMHang H
                LEFT JOIN tblTheLoai L ON H.MaLoai = L.MaLoai
                LEFT JOIN tblHangSX SX ON H.MaHangSX = SX.MaHangSX
                LEFT JOIN tblMauSac MS ON H.MaMau = MS.MaMau
                LEFT JOIN tblPhanhXe P ON H.MaPhanh = P.MaPhanh
                LEFT JOIN tblDongCo D ON H.MaDongCo = D.MaDongCo
                LEFT JOIN tblTinhTrang TTT ON H.MaTinhTrang = TTT.MaTinhTrang
                LEFT JOIN tblNuocSX NSX ON H.MaNuocSX = NSX.MaNuocSX
                WHERE 1=1";

            // Thêm điều kiện tìm kiếm
            if (!string.IsNullOrEmpty(txtMaSP.Text))
                sql += " AND H.MaHang LIKE '%" + txtMaSP.Text + "%'";
            if (!string.IsNullOrEmpty(txtTenSP.Text))
                sql += " AND H.TenHang LIKE '%" + txtTenSP.Text + "%'";
            if (!string.IsNullOrEmpty(txtSoLuong.Text) && int.TryParse(txtSoLuong.Text, out int soLuong))
                sql += " AND H.SoLuong = " + soLuong;
            if (!string.IsNullOrEmpty(txtDonGiaNhap.Text) && decimal.TryParse(txtDonGiaNhap.Text, out decimal donGiaNhap))
                sql += " AND H.DonGiaNhap = " + donGiaNhap;
            if (!string.IsNullOrEmpty(txtDonGiaBan.Text) && decimal.TryParse(txtDonGiaBan.Text, out decimal donGiaBan))
                sql += " AND H.DonGiaBan = " + donGiaBan;
            if (!string.IsNullOrEmpty(txtThoiGianBH.Text) && int.TryParse(txtThoiGianBH.Text, out int thoiGianBH))
                sql += " AND H.ThoiGianBaoHanh = " + thoiGianBH;
            if (!string.IsNullOrEmpty(txtAnh.Text))
                sql += " AND H.Anh LIKE '%" + txtAnh.Text + "%'";
            if (cboLoai.SelectedIndex != -1)
                sql += " AND H.MaLoai = '" + cboLoai.SelectedValue + "'";
            if (cboNhaSX.SelectedIndex != -1)
                sql += " AND H.MaHangSX = '" + cboNhaSX.SelectedValue + "'";
            if (cboMauSac.SelectedIndex != -1)
                sql += " AND H.MaMau = '" + cboMauSac.SelectedValue + "'";
            if (cboPhanh.SelectedIndex != -1)
                sql += " AND H.MaPhanh = '" + cboPhanh.SelectedValue + "'";
            if (cboDongCo.SelectedIndex != -1)
                sql += " AND H.MaDongCo = '" + cboDongCo.SelectedValue + "'";
            if (cboTinhTrang.SelectedIndex != -1)
                sql += " AND H.MaTinhTrang = '" + cboTinhTrang.SelectedValue + "'";
            if (cboNuocSX.SelectedIndex != -1)
                sql += " AND H.MaNuocSX = '" + cboNuocSX.SelectedValue + "'";

            tblHang = Functions.getdatatotable(sql);
            dgvSanPham.DataSource = tblHang;

            if (tblHang.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy sản phẩm nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnHienthiDS_Click(object sender, EventArgs e)
        {
            Load_dgvSanPham();
            ResetValues();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            // Khôi phục trạng thái cho phép tìm kiếm
            txtMaSP.ReadOnly = false;
            txtTenSP.ReadOnly = false;
            txtSoLuong.ReadOnly = false;
            txtDonGiaNhap.ReadOnly = false;
            txtDonGiaBan.ReadOnly = false;
            txtThoiGianBH.ReadOnly = false;
            txtAnh.ReadOnly = false;
        }


        private void btnBoQua_Click(object sender, EventArgs e)
        {
            // Khôi phục trạng thái ban đầu nếu đang sửa
            if (isEditing)
            {
                txtMaSP.Text = initialMaSP;
                txtTenSP.Text = initialTenSP;
                txtSoLuong.Text = initialSoLuong;
                txtDonGiaNhap.Text = initialDonGiaNhap;
                txtDonGiaBan.Text = initialDonGiaBan;
                txtThoiGianBH.Text = initialThoiGianBH;
                txtAnh.Text = initialAnh;
                cboLoai.SelectedValue = initialMaLoai != "" ? initialMaLoai : null;
                cboNhaSX.SelectedValue = initialMaNhaSX != "" ? initialMaNhaSX : null;
                cboMauSac.SelectedValue = initialMaMau != "" ? initialMaMau : null;
                cboPhanh.SelectedValue = initialMaPhanh != "" ? initialMaPhanh : null;
                cboDongCo.SelectedValue = initialMaDongCo != "" ? initialMaDongCo : null;
                cboTinhTrang.SelectedValue = initialMaTinhTrang != "" ? initialMaTinhTrang : null;
                cboNuocSX.SelectedValue = initialMaNuocSX != "" ? initialMaNuocSX : null;
                DisplayImageFromPath(txtAnh.Text);
            }
            else
            {
                ResetValues();
            }

            // Đặt lại trạng thái giao diện
            txtMaSP.ReadOnly = false;
            txtTenSP.ReadOnly = false;
            txtSoLuong.ReadOnly = false;
            txtDonGiaNhap.ReadOnly = false;
            txtDonGiaBan.ReadOnly = false;
            txtThoiGianBH.ReadOnly = false;
            txtAnh.ReadOnly = false;

            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            isAdding = false;
            isEditing = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (File.Exists(dlg.FileName))
                    {
                        // Lấy đường dẫn gốc của ứng dụng
                        string appPath = Application.StartupPath;
                        string imagePath = Path.Combine(appPath, imageFolder);

                        // Sao chép file ảnh vào thư mục Images của dự án
                        string fileName = Path.GetFileName(dlg.FileName);
                        string destPath = Path.Combine(imagePath, fileName);

                        // Nếu file đã tồn tại, thêm số thứ tự để tránh ghi đè
                        int counter = 1;
                        string newFileName = fileName;
                        while (File.Exists(destPath))
                        {
                            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                            string fileExt = Path.GetExtension(fileName);
                            newFileName = $"{fileNameWithoutExt}_{counter}{fileExt}";
                            destPath = Path.Combine(imagePath, newFileName);
                            counter++;
                        }

                        File.Copy(dlg.FileName, destPath, false);

                        // Lưu đường dẫn tương đối vào txtAnh
                        txtAnh.Text = Path.Combine(imageFolder, newFileName);
                        DisplayImageFromPath(txtAnh.Text);
                    }
                    else
                    {
                        MessageBox.Show("Đường dẫn ảnh không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tải ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu
            if (string.IsNullOrEmpty(txtTenSP.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSP.Focus();
                return;
            }
            if (cboLoai.SelectedIndex == -1 || cboNhaSX.SelectedIndex == -1 || cboMauSac.SelectedIndex == -1 ||
                cboPhanh.SelectedIndex == -1 || cboDongCo.SelectedIndex == -1 || cboTinhTrang.SelectedIndex == -1 ||
                cboNuocSX.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin (Loại, Hãng SX, Màu, Phanh, Động cơ, Tình trạng, Nước SX)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong < 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên không âm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }
            if (!decimal.TryParse(txtDonGiaNhap.Text, out decimal donGiaNhap) || donGiaNhap < 0)
            {
                MessageBox.Show("Đơn giá nhập phải là số không âm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGiaNhap.Focus();
                return;
            }
            if (!decimal.TryParse(txtDonGiaBan.Text, out decimal donGiaBan) || donGiaBan < 0)
            {
                MessageBox.Show("Đơn giá bán phải là số không âm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGiaBan.Focus();
                return;
            }
            if (!int.TryParse(txtThoiGianBH.Text, out int thoiGianBH) || thoiGianBH < 0)
            {
                MessageBox.Show("Thời gian bảo hành phải là số nguyên không âm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtThoiGianBH.Focus();
                return;
            }

            // Kiểm tra ký tự đặc biệt trong mã và tên để giảm rủi ro SQL Injection
            if (txtMaSP.Text.Contains("'") || txtTenSP.Text.Contains("'"))
            {
                MessageBox.Show("Mã hoặc tên sản phẩm không được chứa ký tự nháy đơn (')!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (isEditing)
                {
                    // Kiểm tra thay đổi
                    if (!HasChanges())
                    {
                        MessageBox.Show("Không có thay đổi nào để lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Kiểm tra trùng mã sản phẩm
                    string sqlCheck = "SELECT MaHang FROM tblDMHang WHERE MaHang = '" + txtMaSP.Text + "' AND MaHang != '" + initialMaSP + "'";
                    if (Functions.Checkkey(sqlCheck))
                    {
                        MessageBox.Show("Mã sản phẩm này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaSP.Focus();
                        return;
                    }

                    // Cập nhật sản phẩm
                    string sql = "UPDATE tblDMHang SET " +
                                 "TenHang = N'" + txtTenSP.Text + "', " +
                                 "MaLoai = '" + cboLoai.SelectedValue + "', " +
                                 "MaHangSX = '" + cboNhaSX.SelectedValue + "', " +
                                 "MaMau = '" + cboMauSac.SelectedValue + "', " +
                                 "MaPhanh = '" + cboPhanh.SelectedValue + "', " +
                                 "MaDongCo = '" + cboDongCo.SelectedValue + "', " +
                                 "MaNuocSX = '" + cboNuocSX.SelectedValue + "', " +
                                 "MaTinhTrang = '" + cboTinhTrang.SelectedValue + "', " +
                                 "SoLuong = " + soLuong + ", " +
                                 "DonGiaNhap = " + donGiaNhap + ", " +
                                 "DonGiaBan = " + donGiaBan + ", " +
                                 "ThoiGianBaoHanh = " + thoiGianBH + ", " +
                                 "Anh = N'" + (string.IsNullOrEmpty(txtAnh.Text) ? "" : txtAnh.Text) + "' " +
                                 "WHERE MaHang = '" + initialMaSP + "'";
                    Functions.Runsql(sql);
                    MessageBox.Show("Đã cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (isAdding)
                {
                    if (string.IsNullOrEmpty(txtMaSP.Text.Trim()))
                    {
                        MessageBox.Show("Vui lòng nhập mã sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaSP.Focus();
                        return;
                    }

                    // Kiểm tra trùng mã sản phẩm
                    string sqlCheck = "SELECT MaHang FROM tblDMHang WHERE MaHang = '" + txtMaSP.Text + "'";
                    if (Functions.Checkkey(sqlCheck))
                    {
                        MessageBox.Show("Mã sản phẩm này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaSP.Focus();
                        return;
                    }

                    // Thêm sản phẩm mới
                    string sql = "INSERT INTO tblDMHang(MaHang, TenHang, MaLoai, MaHangSX, MaMau, MaPhanh, MaDongCo, MaNuocSX, MaTinhTrang, SoLuong, DonGiaNhap, DonGiaBan, ThoiGianBaoHanh, Anh) " +
                                 "VALUES('" + txtMaSP.Text + "', N'" + txtTenSP.Text + "', '" + cboLoai.SelectedValue + "', '" + cboNhaSX.SelectedValue + "', '" + cboMauSac.SelectedValue + "', '" +
                                 cboPhanh.SelectedValue + "', '" + cboDongCo.SelectedValue + "', '" + cboNuocSX.SelectedValue + "', '" + cboTinhTrang.SelectedValue + "', " +
                                 soLuong + ", " + donGiaNhap + ", " + donGiaBan + ", " + thoiGianBH + ", N'" + (string.IsNullOrEmpty(txtAnh.Text) ? "" : txtAnh.Text) + "')";
                    Functions.Runsql(sql);
                    MessageBox.Show("Đã thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Cập nhật giao diện
                Load_dgvSanPham();
                ResetValues();
                btnLuu.Enabled = false;
                btnBoQua.Enabled = false;
                btnThem.Enabled = true;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;

                // Khôi phục trạng thái cho phép tìm kiếm
                txtMaSP.ReadOnly = false;
                txtTenSP.ReadOnly = false;
                txtSoLuong.ReadOnly = false;
                txtDonGiaNhap.ReadOnly = false;
                txtDonGiaBan.ReadOnly = false;
                txtThoiGianBH.ReadOnly = false;
                txtAnh.ReadOnly = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (isAdding || isEditing)
            {
                DialogResult result = MessageBox.Show("Bạn đang thêm hoặc sửa sản phẩm. Bạn có muốn lưu trước khi đóng?", "Cảnh báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    btnLuu_Click(null, null);
                    if (isAdding || isEditing) // Nếu lưu thất bại, không đóng
                        return;
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            this.Close();
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (isAdding || isEditing)
                {
                    MessageBox.Show("Vui lòng hoàn tất hoặc hủy thao tác thêm/sửa trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];
                txtMaSP.Text = row.Cells["Mã xe"].Value?.ToString() ?? "";
                txtTenSP.Text = row.Cells["Tên xe"].Value?.ToString() ?? "";
                txtSoLuong.Text = row.Cells["Số lượng"].Value?.ToString() ?? "";
                txtDonGiaNhap.Text = row.Cells["Đơn giá nhập"].Value?.ToString() ?? "";
                txtDonGiaBan.Text = row.Cells["Đơn giá bán"].Value?.ToString() ?? "";
                txtThoiGianBH.Text = row.Cells["Bảo hành (tháng)"].Value?.ToString() ?? "";
                txtAnh.Text = row.Cells["Ảnh"].Value?.ToString() ?? "";

                string sqlLoai = "SELECT MaLoai FROM tblTheLoai WHERE TenLoai = N'" + (row.Cells["Loại"].Value?.ToString() ?? "") + "'";
                cboLoai.SelectedValue = Functions.GetFieldValues(sqlLoai);

                string sqlNhaSX = "SELECT MaHangSX FROM tblHangSX WHERE TenHangSX = N'" + (row.Cells["Hãng SX"].Value?.ToString() ?? "") + "'";
                cboNhaSX.SelectedValue = Functions.GetFieldValues(sqlNhaSX);

                string sqlMau = "SELECT MaMau FROM tblMauSac WHERE TenMau = N'" + (row.Cells["Màu"].Value?.ToString() ?? "") + "'";
                cboMauSac.SelectedValue = Functions.GetFieldValues(sqlMau);

                string sqlPhanh = "SELECT MaPhanh FROM tblPhanhXe WHERE TenPhanh = N'" + (row.Cells["Phanh"].Value?.ToString() ?? "") + "'";
                cboPhanh.SelectedValue = Functions.GetFieldValues(sqlPhanh);

                string sqlDongCo = "SELECT MaDongCo FROM tblDongCo WHERE TenDongCo = N'" + (row.Cells["Động cơ"].Value?.ToString() ?? "") + "'";
                cboDongCo.SelectedValue = Functions.GetFieldValues(sqlDongCo);

                string sqlNuocSX = "SELECT MaNuocSX FROM tblNuocSX WHERE TenNuocSX = N'" + (row.Cells["Nước sản xuất"].Value?.ToString() ?? "") + "'";
                cboNuocSX.SelectedValue = Functions.GetFieldValues(sqlNuocSX);

                string sqlTinhTrang = "SELECT MaTinhTrang FROM tblTinhTrang WHERE TenTinhTrang = N'" + (row.Cells["Tình trạng"].Value?.ToString() ?? "") + "'";
                cboTinhTrang.SelectedValue = Functions.GetFieldValues(sqlTinhTrang);

                DisplayImageFromPath(txtAnh.Text);
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void DisplayImageFromPath(string relativePath)
        {
            picAnh.Image = null;
            try
            {
                if (!string.IsNullOrEmpty(relativePath))
                {
                    // Chuyển đường dẫn tương đối thành đường dẫn tuyệt đối
                    string absolutePath = Path.Combine(Application.StartupPath, relativePath);
                    if (File.Exists(absolutePath))
                    {
                        picAnh.Image = Image.FromFile(absolutePath);
                        picAnh.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy file ảnh: " + relativePath, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDonGiaNhap_TextChanged(object sender, EventArgs e)
        {
            // Tự động tính giá bán = 110% giá nhập khi đang thêm hoặc sửa
            if (isAdding || isEditing)
            {
                if (decimal.TryParse(txtDonGiaNhap.Text, out decimal donGiaNhap) && donGiaNhap >= 0)
                {
                    decimal donGiaBan = donGiaNhap * 1.1m;
                    txtDonGiaBan.Text = donGiaBan.ToString("F2"); // Định dạng 2 chữ số thập phân
                }
                else
                {
                    txtDonGiaBan.Text = "";
                }
            }
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
                e.Handled = true;
        }

        private void txtDonGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != (char)8)
                e.Handled = true;
        }

        private void txtDonGiaBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != (char)8)
                e.Handled = true;
        }

        private void txtThoiGianBH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
                e.Handled = true;
        }
    }
}