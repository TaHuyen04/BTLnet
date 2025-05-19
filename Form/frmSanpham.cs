using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        // Biến lưu trạng thái ban đầu của các trường khi nhấn Sửa
        private Dictionary<string, string> initialValues = new Dictionary<string, string>();

        private void frmSanpham_Load(object sender, EventArgs e)
        {
            txtMaSP.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            Load_DataGridView();
            Functions.FillCombo("SELECT MaMau, TenMau FROM tblMauSac", cboMauSac, "MaMau", "TenMau");
            Functions.FillCombo("SELECT MaLoai, TenLoai FROM tblTheLoai", cboLoai, "MaLoai", "TenLoai");
            Functions.FillCombo("SELECT MaHangSX, TenHangSX FROM tblHangSX", cboNhaSX, "MaHangSX", "TenHangSX");
            Functions.FillCombo("SELECT MaPhanh, TenPhanh FROM tblPhanhXe", cboPhanh, "MaPhanh", "TenPhanh");
            Functions.FillCombo("SELECT MaDongCo, TenDongCo FROM tblDongCo", cboDongCo, "MaDongCo", "TenDongCo");
            Functions.FillCombo("SELECT MaTinhTrang, TenTinhTrang FROM tblTinhTrang", cboTinhTrang, "MaTinhTrang", "TenTinhTrang");
            try
            {
                txtMaSP.Enabled = false;
                btnLuu.Enabled = false;
                btnBoQua.Enabled = false;
                Load_DataGridView();
                Functions.FillCombo("SELECT MaMau, TenMau FROM tblMauSac", cboMauSac, "MaMau", "TenMau");
                Functions.FillCombo("SELECT MaLoai, TenLoai FROM tblTheLoai", cboLoai, "MaLoai", "TenLoai");
                Functions.FillCombo("SELECT MaHangSX, TenHangSX FROM tblHangSX", cboNhaSX, "MaHangSX", "TenHangSX");
                Functions.FillCombo("SELECT MaPhanh, TenPhanh FROM tblPhanhXe", cboPhanh, "MaPhanh", "TenPhanh");
                Functions.FillCombo("SELECT MaDongCo, TenDongCo FROM tblDongCo", cboDongCo, "MaDongCo", "TenDongCo");
                Functions.FillCombo("SELECT MaTinhTrang, TenTinhTrang FROM tblTinhTrang", cboTinhTrang, "MaTinhTrang", "TenTinhTrang");
                Functions.FillCombo("SELECT MaNuocSX, TenNuocSX FROM tblNuocSX", cboNuocSX, "MaNuocSX", "TenNuocSX");
                cboNuocSX.SelectedIndex = -1;

                ResetValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        DataTable tblHang;

        private void Load_DataGridView()
        {
            try
            {
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
        ";

                tblHang = Functions.getdatatotable(sql);
                dgvSanPham.DataSource = tblHang;

                dgvSanPham.AllowUserToAddRows = false;
                dgvSanPham.EditMode = DataGridViewEditMode.EditProgrammatically;

                // Chọn lại dòng hiện tại nếu có (sau khi thêm/sửa)
                if (!string.IsNullOrEmpty(txtMaSP.Text) && tblHang.Rows.Count > 0)
                {
                    foreach (DataRow row in tblHang.Rows)
                    {
                        if (row["Mã xe"].ToString() == txtMaSP.Text)
                        {
                            int rowIndex = tblHang.Rows.IndexOf(row);
                            dgvSanPham.ClearSelection();
                            dgvSanPham.Rows[rowIndex].Selected = true;
                            dgvSanPham.CurrentCell = dgvSanPham.Rows[rowIndex].Cells[0];
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải DataGridView: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetValues()
        {
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            txtSoLuong.Text = "0";
            txtDonGiaNhap.Text = "0";
            txtDonGiaBan.Text = "0";
            txtThoiGianBH.Text = "0";
            txtAnh.Text = "";
            picAnh.Image = null;
            cboLoai.SelectedIndex = -1;
            cboMauSac.SelectedIndex = -1;
            cboPhanh.SelectedIndex = -1;
            cboDongCo.SelectedIndex = -1;
            cboTinhTrang.SelectedIndex = -1;
            cboNhaSX.SelectedIndex = -1;
            cboNuocSX.SelectedIndex = -1;

            // Xóa trạng thái ban đầu khi reset
            initialValues.Clear();
        }

        private void SaveInitialValues()
        {
            initialValues.Clear();
            initialValues["txtMaSP"] = txtMaSP.Text;
            initialValues["txtTenSP"] = txtTenSP.Text;
            initialValues["txtSoLuong"] = txtSoLuong.Text;
            initialValues["txtDonGiaNhap"] = txtDonGiaNhap.Text;
            initialValues["txtDonGiaBan"] = txtDonGiaBan.Text;
            initialValues["txtThoiGianBH"] = txtThoiGianBH.Text;
            initialValues["txtAnh"] = txtAnh.Text;
            initialValues["cboLoai"] = cboLoai.SelectedValue?.ToString() ?? "";
            initialValues["cboNhaSX"] = cboNhaSX.SelectedValue?.ToString() ?? "";
            initialValues["cboMauSac"] = cboMauSac.SelectedValue?.ToString() ?? "";
            initialValues["cboPhanh"] = cboPhanh.SelectedValue?.ToString() ?? "";
            initialValues["cboDongCo"] = cboDongCo.SelectedValue?.ToString() ?? "";
            initialValues["cboTinhTrang"] = cboTinhTrang.SelectedValue?.ToString() ?? "";
            initialValues["cboNuocSX"] = cboNuocSX.SelectedValue?.ToString() ?? "";
        }

        private bool HasChanges()
        {
            return txtMaSP.Text != initialValues["txtMaSP"] ||
                   txtTenSP.Text != initialValues["txtTenSP"] ||
                   txtSoLuong.Text != initialValues["txtSoLuong"] ||
                   txtDonGiaNhap.Text != initialValues["txtDonGiaNhap"] ||
                   txtDonGiaBan.Text != initialValues["txtDonGiaBan"] ||
                   txtThoiGianBH.Text != initialValues["txtThoiGianBH"] ||
                   txtAnh.Text != initialValues["txtAnh"] ||
                   (cboLoai.SelectedValue?.ToString() ?? "") != initialValues["cboLoai"] ||
                   (cboNhaSX.SelectedValue?.ToString() ?? "") != initialValues["cboNhaSX"] ||
                   (cboMauSac.SelectedValue?.ToString() ?? "") != initialValues["cboMauSac"] ||
                   (cboPhanh.SelectedValue?.ToString() ?? "") != initialValues["cboPhanh"] ||
                   (cboDongCo.SelectedValue?.ToString() ?? "") != initialValues["cboDongCo"] ||
                   (cboTinhTrang.SelectedValue?.ToString() ?? "") != initialValues["cboTinhTrang"] ||
                   (cboNuocSX.SelectedValue?.ToString() ?? "") != initialValues["cboNuocSX"];
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;

            ResetValues();
            txtMaSP.Enabled = true;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            txtMaSP.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaSP.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo");
                    return;
                }
                string sql = "DELETE tblDMHang WHERE MaHang='" + txtMaSP.Text + "'";
                Functions.Runsqldel(sql);
                Load_DataGridView();
                ResetValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (tblHang.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để sửa!", "Thông báo");
                    return;
                }

                if (txtMaSP.Text.Trim() == "")
                {
                    MessageBox.Show("Bạn chưa chọn sản phẩm cần sửa!", "Thông báo");
                    return;
                }

                // Lưu trạng thái ban đầu của các trường
                SaveInitialValues();

                // Bật các nút điều khiển
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnBoQua.Enabled = true;
                btnSua.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chuẩn bị sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM tblDMHang WHERE 1=1";
                if (txtMaSP.Text != "")
                    sql += " AND MaHang LIKE N'%" + txtMaSP.Text + "%'";
                if (txtTenSP.Text != "")
                    sql += " AND TenHang LIKE N'%" + txtTenSP.Text + "%'";
                tblHang = Functions.getdatatotable(sql);
                dgvSanPham.DataSource = tblHang;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Load_DataGridView();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            // Khôi phục trạng thái ban đầu nếu đang sửa
            if (initialValues.Count > 0)
            {
                txtMaSP.Text = initialValues["txtMaSP"];
                txtTenSP.Text = initialValues["txtTenSP"];
                txtSoLuong.Text = initialValues["txtSoLuong"];
                txtDonGiaNhap.Text = initialValues["txtDonGiaNhap"];
                txtDonGiaBan.Text = initialValues["txtDonGiaBan"];
                txtThoiGianBH.Text = initialValues["txtThoiGianBH"];
                txtAnh.Text = initialValues["txtAnh"];
                cboLoai.SelectedValue = initialValues["cboLoai"] != "" ? initialValues["cboLoai"] : null;
                cboNhaSX.SelectedValue = initialValues["cboNhaSX"] != "" ? initialValues["cboNhaSX"] : null;
                cboMauSac.SelectedValue = initialValues["cboMauSac"] != "" ? initialValues["cboMauSac"] : null;
                cboPhanh.SelectedValue = initialValues["cboPhanh"] != "" ? initialValues["cboPhanh"] : null;
                cboDongCo.SelectedValue = initialValues["cboDongCo"] != "" ? initialValues["cboDongCo"] : null;
                cboTinhTrang.SelectedValue = initialValues["cboTinhTrang"] != "" ? initialValues["cboTinhTrang"] : null;
                cboNuocSX.SelectedValue = initialValues["cboNuocSX"] != "" ? initialValues["cboNuocSX"] : null;

                // Khôi phục ảnh
                try
                {
                    if (!string.IsNullOrEmpty(txtAnh.Text) && System.IO.File.Exists(txtAnh.Text))
                    {
                        picAnh.Image = Image.FromFile(txtAnh.Text);
                        picAnh.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else
                    {
                        picAnh.Image = null;
                    }
                }
                catch
                {
                    picAnh.Image = null;
                }
            }
            else
            {
                ResetValues();
            }

            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            txtMaSP.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (System.IO.File.Exists(dlg.FileName))
                    {
                        txtAnh.Text = dlg.FileName;
                        picAnh.Image = Image.FromFile(dlg.FileName);
                        picAnh.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else
                    {
                        MessageBox.Show("Đường dẫn ảnh không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem đang thêm mới hay sửa
                bool isEditMode = initialValues.Count > 0;

                if (isEditMode)
                {
                    // Kiểm tra có thay đổi nào không
                    if (!HasChanges())
                    {
                        MessageBox.Show("Bạn chưa chỉnh sửa thông tin nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Kiểm tra dữ liệu bắt buộc khi sửa
                    if (txtTenSP.Text.Trim() == "")
                    {
                        MessageBox.Show("Bạn phải nhập TÊN SẢN PHẨM", "Thông báo");
                        txtTenSP.Focus();
                        return;
                    }
                    if (cboLoai.SelectedIndex == -1 || cboNhaSX.SelectedIndex == -1 || cboMauSac.SelectedIndex == -1 ||
                        cboPhanh.SelectedIndex == -1 || cboDongCo.SelectedIndex == -1 || cboTinhTrang.SelectedIndex == -1 ||
                        cboNuocSX.SelectedIndex == -1)
                    {
                        MessageBox.Show("Bạn phải chọn đầy đủ thông tin (Loại, Hãng SX, Màu, Phanh, Động cơ, Tình trạng, Nước SX)", "Thông báo");
                        return;
                    }
                    if (!int.TryParse(txtSoLuong.Text, out _) || !decimal.TryParse(txtDonGiaNhap.Text, out _) ||
                        !decimal.TryParse(txtDonGiaBan.Text, out _) || !int.TryParse(txtThoiGianBH.Text, out _))
                    {
                        MessageBox.Show("Số lượng, đơn giá nhập, đơn giá bán và thời gian bảo hành phải là số!", "Thông báo");
                        return;
                    }

                    // Kiểm tra trùng mã sản phẩm
                    if (Functions.Checkkey("SELECT MaHang FROM tblDMHang WHERE MaHang = '" + txtMaSP.Text.Trim() + "' AND MaHang != '" + initialValues["txtMaSP"] + "'"))
                    {
                        MessageBox.Show("Mã sản phẩm này đã tồn tại. Hãy nhập mã khác!", "Thông báo");
                        txtMaSP.Focus();
                        return;
                    }

                    // Thực hiện sửa
                    string sql = "UPDATE tblDMHang SET " +
                                 "TenHang = N'" + txtTenSP.Text + "', " +
                                 "MaLoai = '" + cboLoai.SelectedValue + "', " +
                                 "MaHangSX = '" + cboNhaSX.SelectedValue + "', " +
                                 "MaMau = '" + cboMauSac.SelectedValue + "', " +
                                 "MaPhanh = '" + cboPhanh.SelectedValue + "', " +
                                 "MaDongCo = '" + cboDongCo.SelectedValue + "', " +
                                 "MaNuocSX = '" + cboNuocSX.SelectedValue + "', " +
                                 "MaTinhTrang = '" + cboTinhTrang.SelectedValue + "', " +
                                 "SoLuong = " + txtSoLuong.Text + ", " +
                                 "DonGiaNhap = " + txtDonGiaNhap.Text + ", " +
                                 "DonGiaBan = " + txtDonGiaBan.Text + ", " +
                                 "ThoiGianBaoHanh = " + txtThoiGianBH.Text + ", " +
                                 "Anh = '" + txtAnh.Text + "' " +
                                 "WHERE MaHang = '" + initialValues["txtMaSP"] + "'";

                    Functions.Runsql(sql);
                    Load_DataGridView();
                    ResetValues();
                    btnBoQua.Enabled = false;
                    btnLuu.Enabled = false;
                    btnThem.Enabled = true;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    txtMaSP.Enabled = false;
                }
                else
                {
                    // Chế độ thêm mới
                    if (txtMaSP.Text.Trim() == "")
                    {
                        MessageBox.Show("Bạn phải nhập MÃ SẢN PHẨM", "Thông báo");
                        txtMaSP.Focus();
                        return;
                    }
                    if (txtTenSP.Text.Trim() == "")
                    {
                        MessageBox.Show("Bạn phải nhập TÊN SẢN PHẨM", "Thông báo");
                        txtTenSP.Focus();
                        return;
                    }
                    if (txtSoLuong.Text.Trim() == "")
                    {
                        MessageBox.Show("Bạn phải nhập SỐ LƯỢNG", "Thông báo");
                        txtSoLuong.Focus();
                        return;
                    }
                    if (!int.TryParse(txtSoLuong.Text, out _))
                    {
                        MessageBox.Show("Số lượng phải là số nguyên!");
                        txtSoLuong.Focus();
                        return;
                    }
                    if (txtDonGiaNhap.Text.Trim() == "")
                    {
                        MessageBox.Show("Bạn phải nhập ĐƠN GIÁ NHẬP", "Thông báo");
                        txtDonGiaNhap.Focus();
                        return;
                    }
                    if (txtDonGiaBan.Text.Trim() == "")
                    {
                        MessageBox.Show("Bạn phải nhập ĐƠN GIÁ BÁN", "Thông báo");
                        txtDonGiaBan.Focus();
                        return;
                    }
                    if (txtThoiGianBH.Text.Trim() == "")
                    {
                        MessageBox.Show("Bạn phải nhập THỜI GIAN BẢO HÀNH", "Thông báo");
                        txtThoiGianBH.Focus();
                        return;
                    }
                    if (!decimal.TryParse(txtDonGiaNhap.Text, out _) || !decimal.TryParse(txtDonGiaBan.Text, out _) || !int.TryParse(txtThoiGianBH.Text, out _))
                    {
                        MessageBox.Show("Đơn giá nhập, đơn giá bán và thời gian bảo hành phải là số!", "Thông báo");
                        return;
                    }

                    // Kiểm tra các ComboBox bắt buộc chọn
                    if (cboLoai.SelectedIndex == -1)
                    {
                        MessageBox.Show("Bạn phải chọn LOẠI", "Thông báo");
                        cboLoai.Focus();
                        return;
                    }
                    if (cboNhaSX.SelectedIndex == -1)
                    {
                        MessageBox.Show("Bạn phải chọn NHÀ SẢN XUẤT", "Thông báo");
                        cboNhaSX.Focus();
                        return;
                    }
                    if (cboMauSac.SelectedIndex == -1)
                    {
                        MessageBox.Show("Bạn phải chọn MÀU SẮC", "Thông báo");
                        cboMauSac.Focus();
                        return;
                    }
                    if (cboPhanh.SelectedIndex == -1)
                    {
                        MessageBox.Show("Bạn phải chọn PHANH", "Thông báo");
                        cboPhanh.Focus();
                        return;
                    }
                    if (cboDongCo.SelectedIndex == -1)
                    {
                        MessageBox.Show("Bạn phải chọn ĐỘNG CƠ", "Thông báo");
                        cboDongCo.Focus();
                        return;
                    }
                    if (cboTinhTrang.SelectedIndex == -1)
                    {
                        MessageBox.Show("Bạn phải chọn TÌNH TRẠNG", "Thông báo");
                        cboTinhTrang.Focus();
                        return;
                    }
                    if (cboNuocSX.SelectedIndex == -1)
                    {
                        MessageBox.Show("Bạn phải chọn NƯỚC SẢN XUẤT", "Thông báo");
                        cboNuocSX.Focus();
                        return;
                    }

                    // Kiểm tra trùng khóa
                    if (Functions.Checkkey("SELECT MaHang FROM tblDMHang WHERE MaHang = '" + txtMaSP.Text.Trim() + "'"))
                    {
                        MessageBox.Show("Mã sản phẩm này đã tồn tại. Hãy nhập mã khác!", "Thông báo");
                        txtMaSP.Focus();
                        return;
                    }

                    // Nếu hợp lệ → thực hiện thêm mới
                    string sql = "INSERT INTO tblDMHang(MaHang, TenHang, MaLoai, MaHangSX, MaMau, MaPhanh, MaDongCo, MaNuocSX, MaTinhTrang, SoLuong, DonGiaNhap, DonGiaBan, ThoiGianBaoHanh, Anh) " +
                      "VALUES('" + txtMaSP.Text + "', N'" + txtTenSP.Text + "', '" + cboLoai.SelectedValue +
                      "', '" + cboNhaSX.SelectedValue + "', '" + cboMauSac.SelectedValue + "', '" + cboPhanh.SelectedValue +
                      "', '" + cboDongCo.SelectedValue + "', '" + cboNuocSX.SelectedValue + "', '" + cboTinhTrang.SelectedValue + "', " +
                      txtSoLuong.Text + ", " + txtDonGiaNhap.Text + ", " + txtDonGiaBan.Text + ", " + txtThoiGianBH.Text + ", '" + txtAnh.Text + "')";

                    Functions.Runsql(sql);
                    Load_DataGridView();
                    ResetValues();
                    btnBoQua.Enabled = false;
                    btnLuu.Enabled = false;
                    btnThem.Enabled = true;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    txtMaSP.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];

                    txtMaSP.Text = row.Cells["Mã xe"].Value?.ToString() ?? "";
                    txtTenSP.Text = row.Cells["Tên xe"].Value?.ToString() ?? "";
                    txtSoLuong.Text = row.Cells["Số lượng"].Value?.ToString() ?? "0";
                    txtDonGiaNhap.Text = row.Cells["Đơn giá nhập"].Value?.ToString() ?? "0";
                    txtDonGiaBan.Text = row.Cells["Đơn giá bán"].Value?.ToString() ?? "0";
                    txtThoiGianBH.Text = row.Cells["Bảo hành (tháng)"].Value?.ToString() ?? "0";
                    txtAnh.Text = row.Cells["Ảnh"].Value?.ToString() ?? "";


                    // Lấy mã dựa vào tên hiển thị
                    cboLoai.SelectedValue = Functions.GetFieldValues("SELECT MaLoai FROM tblTheLoai WHERE TenLoai = N'" + (row.Cells["Loại"].Value?.ToString() ?? "") + "'");
                    cboNhaSX.SelectedValue = Functions.GetFieldValues("SELECT MaHangSX FROM tblHangSX WHERE TenHangSX = N'" + (row.Cells["Hãng SX"].Value?.ToString() ?? "") + "'");
                    cboMauSac.SelectedValue = Functions.GetFieldValues("SELECT MaMau FROM tblMauSac WHERE TenMau = N'" + (row.Cells["Màu"].Value?.ToString() ?? "") + "'");
                    cboPhanh.SelectedValue = Functions.GetFieldValues("SELECT MaPhanh FROM tblPhanhXe WHERE TenPhanh = N'" + (row.Cells["Phanh"].Value?.ToString() ?? "") + "'");
                    cboDongCo.SelectedValue = Functions.GetFieldValues("SELECT MaDongCo FROM tblDongCo WHERE TenDongCo = N'" + (row.Cells["Động cơ"].Value?.ToString() ?? "") + "'");
                    cboNuocSX.SelectedValue = Functions.GetFieldValues("SELECT MaNuocSX FROM tblNuocSX WHERE TenNuocSX = N'" + (row.Cells["Nước sản xuất"].Value?.ToString() ?? "") + "'");
                    cboTinhTrang.SelectedValue = Functions.GetFieldValues("SELECT MaTinhTrang FROM tblTinhTrang WHERE TenTinhTrang = N'" + (row.Cells["Tình trạng"].Value?.ToString() ?? "") + "'");

                    // Hiển thị ảnh từ cột "Ảnh"
                    DisplayImageFromPath(txtAnh.Text);

                    // Bật các nút thao tác
                    txtMaSP.Enabled = false;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    btnBoQua.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn dòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Phương thức hiển thị ảnh từ đường dẫn
        private void DisplayImageFromPath(string imagePath)
        {
            try
            {
                picAnh.Image = null;

                if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                {
                    using (var fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        Image img = Image.FromStream(fs);
                        picAnh.Image = new Bitmap(img);
                    }

                    picAnh.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    MessageBox.Show("File ảnh không tồn tại tại:\n" + imagePath, "Ảnh không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("File không phải là ảnh hợp lệ hoặc bị lỗi:\n" + imagePath, "Lỗi ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải ảnh:\n" + ex.Message + "\nĐường dẫn: " + imagePath, "Lỗi ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}