using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLCHBanXeMay.Class;

namespace QLCHBanXeMay.form
{
    public partial class frmTaodonnhap : Form
    {
        public frmTaodonnhap()
        {
            InitializeComponent();
            // Đặt ReadOnly cho các TextBox để không cho nhập thủ công
            txtTenSP.ReadOnly = true;
            txtDonGiaNhap.ReadOnly = true;
            txtTenNV.ReadOnly = true;
            txtTenNCC.ReadOnly = true;
            txtSDT.ReadOnly = true;
            txtDiachi.ReadOnly = true;
            txtThanhtien.ReadOnly = true;
            txtMaHDN.ReadOnly = true; // Ngăn thay đổi mã hóa đơn sau khi tạo
        }

        DataTable tblDSSP;


        private void frmTaodonnhap_Load(object sender, EventArgs e)
        {
            // Load ComboBox Mã nhân viên (hiển thị TenNV, chọn MaNV)
            string sqlNhanVien = "SELECT MaNV, TenNV FROM tblNhanVien";
            Functions.FillCombo(sqlNhanVien, cboMaNV, "MaNV", "TenNV");
            cboMaNV.DisplayMember = "TenNV"; // Hiển thị tên nhân viên
            cboMaNV.ValueMember = "MaNV";   // Giá trị chọn là mã nhân viên

            // Load ComboBox Mã nhà cung cấp (hiển thị TenNCC, chọn MaNCC)
            string sqlNCC = "SELECT MaNCC, TenNCC FROM tblNhaCungCap";
            Functions.FillCombo(sqlNCC, cboMaNCC, "MaNCC", "TenNCC");
            cboMaNCC.DisplayMember = "TenNCC"; // Hiển thị tên nhà cung cấp
            cboMaNCC.ValueMember = "MaNCC";    // Giá trị chọn là mã nhà cung cấp

            // Load ComboBox Mã sản phẩm (hiển thị TenHang, chọn MaHang)
            string sqlSP = "SELECT MaHang, TenHang, DonGiaNhap FROM tblDMHang";
            Functions.FillCombo(sqlSP, cboMaSP, "MaHang", "TenHang");
            cboMaSP.DisplayMember = "TenHang"; // Hiển thị tên sản phẩm
            cboMaSP.ValueMember = "MaHang";    // Giá trị chọn là mã sản phẩm

            cboMaNV.SelectedIndex = -1;
            cboMaNCC.SelectedIndex = -1;
            cboMaSP.SelectedIndex = -1;

            // Tự sinh mã hóa đơn
            txtMaHDN.Text = GenerateNewInvoiceCode();

            Load_dgvDSSP();
            btnThem.Enabled = false;
            btnBoqua.Enabled = false;
            btnInHD.Enabled = false;
            btnXoaHD.Enabled = false;
            btnLuuHD.Enabled = false;
        }

        // Hàm tạo mã hóa đơn tự động
        private string GenerateNewInvoiceCode()
        {
            string newCode = "HDN001"; // Mã mặc định nếu không có hóa đơn nào
            string sql = "SELECT TOP 1 SoHDN FROM tblHoaDonNhap ORDER BY SoHDN DESC";
            string lastCode = Functions.GetFieldValues(sql);

            if (!string.IsNullOrEmpty(lastCode))
            {
                // Tách phần số từ mã (VD: "HDN001" -> "001")
                string numberPart = lastCode.Substring(3); // Bỏ "HDN"
                int number = int.Parse(numberPart) + 1; // Tăng số lên 1
                newCode = "HDN" + number.ToString("D3"); // Định dạng lại (VD: "HDN002")
            }

            return newCode;
        }

        private void cboMaNCC_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            if (cboMaNCC.SelectedIndex != -1)
            {
                string sql = "SELECT TenNCC, DienThoai, Diachi FROM tblNhaCungCap WHERE MaNCC = '" + cboMaNCC.SelectedValue + "'";
                DataTable dt = Functions.getdatatotable(sql);
                if (dt.Rows.Count > 0)
                {
                    txtTenNCC.Text = dt.Rows[0]["TenNCC"].ToString();
                    txtSDT.Text = dt.Rows[0]["DienThoai"].ToString();
                    txtDiachi.Text = dt.Rows[0]["Diachi"].ToString();
                }
                else
                {
                    txtTenNCC.Text = "";
                    txtSDT.Text = "";
                    txtDiachi.Text = "";
                }
            }
            else
            {
                txtTenNCC.Text = "";
                txtSDT.Text = "";
                txtDiachi.Text = "";
            }
        }

        private void cboMaNV_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cboMaNV.SelectedIndex != -1)
            {
                string sql = "SELECT TenNV FROM tblNhanVien WHERE MaNV = '" + cboMaNV.SelectedValue + "'";
                DataTable dt = Functions.getdatatotable(sql);
                if (dt.Rows.Count > 0)
                {
                    txtTenNV.Text = dt.Rows[0]["TenNV"].ToString();
                }
                else
                {
                    txtTenNV.Text = "";
                }
            }
            else
            {
                txtTenNV.Text = "";
            }
        }

        private void cboMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaSP.SelectedIndex != -1)
            {
                string sql = "SELECT TenHang, DonGiaNhap FROM tblDMHang WHERE MaHang = '" + cboMaSP.SelectedValue + "'";
                DataTable dt = Functions.getdatatotable(sql);
                if (dt.Rows.Count > 0)
                {
                    txtTenSP.Text = dt.Rows[0]["TenHang"].ToString();
                    txtDonGiaNhap.Text = dt.Rows[0]["DonGiaNhap"].ToString();
                    TinhThanhTien(); // Tự động tính thành tiền khi chọn sản phẩm
                }
            }
            else
            {
                txtTenSP.Text = "";
                txtDonGiaNhap.Text = "";
                txtThanhtien.Text = "";
            }
        }


        private void Load_dgvDSSP()
        {
            // Chỉ tải chi tiết hóa đơn của hóa đơn hiện tại
            // Sử dụng JOIN để lấy TenHang từ tblDMHang
            string sql = "SELECT ct.MaHang, dm.TenHang, ct.DonGia, ct.SoLuong, ct.ThanhTien " +
                         "FROM tblChiTietHoaDonNhap ct " +
                         "INNER JOIN tblDMHang dm ON ct.MaHang = dm.MaHang " +
                         "WHERE ct.SoHDN = '" + txtMaHDN.Text + "'";
            tblDSSP = Functions.getdatatotable(sql);
            dgvDSSP.DataSource = tblDSSP;

            // Cập nhật header và độ rộng cột
            if (dgvDSSP.Columns.Count > 0)
            {
                dgvDSSP.Columns[0].HeaderText = "Mã hàng";
                dgvDSSP.Columns[1].HeaderText = "Tên hàng";
                dgvDSSP.Columns[2].HeaderText = "Đơn giá nhập";
                dgvDSSP.Columns[3].HeaderText = "Số lượng";
                dgvDSSP.Columns[4].HeaderText = "Thành tiền";

                dgvDSSP.Columns[0].Width = 100;
                dgvDSSP.Columns[1].Width = 200;
                dgvDSSP.Columns[2].Width = 120;
                dgvDSSP.Columns[3].Width = 80;
                dgvDSSP.Columns[4].Width = 120;
            }

            // Không cho chỉnh sửa trực tiếp trên lưới
            dgvDSSP.AllowUserToAddRows = false;
            dgvDSSP.EditMode = DataGridViewEditMode.EditProgrammatically;

            TinhTongTien(); // Cập nhật tổng tiền sau khi tải dữ liệu
        }

        private void dgvDSSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaSP.Focus();
                return;
            }
            if (dgvDSSP.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            cboMaSP.Text = dgvDSSP.CurrentRow.Cells["TenHang"].Value.ToString(); // Hiển thị tên để chọn
            string maSP = dgvDSSP.CurrentRow.Cells["MaHang"].Value.ToString();
            string sql = "SELECT TenHang, DonGiaNhap FROM tblDMHang WHERE MaHang = '" + maSP + "'";
            DataTable dt = Functions.getdatatotable(sql);
            if (dt.Rows.Count > 0)
            {
                txtTenSP.Text = dt.Rows[0]["TenHang"].ToString();
                txtDonGiaNhap.Text = dt.Rows[0]["DonGiaNhap"].ToString();
            }
            nudSoluong.Text = dgvDSSP.CurrentRow.Cells["SoLuong"].Value.ToString();
            txtThanhtien.Text = dgvDSSP.CurrentRow.Cells["ThanhTien"].ToString();
            TinhThanhTien(); // Cập nhật thành tiền

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoqua.Enabled = true;
        }

        private void TinhThanhTien()
        {
            if (!string.IsNullOrEmpty(txtDonGiaNhap.Text) && nudSoluong.Value > 0)
            {
                double donGia = Convert.ToDouble(txtDonGiaNhap.Text);
                int soLuong = Convert.ToInt32(nudSoluong.Value);
                double giamGia = Convert.ToDouble(nudGiamgia.Value);
                double thanhTien = soLuong * donGia * (1 - giamGia / 100);
                txtThanhtien.Text = thanhTien.ToString("N0");
            }
            else
            {
                txtThanhtien.Text = "0";
            }
        }

        private void nudSoluong_ValueChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void nudGiamgia_ValueChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }


        private void ResetValuesSP()
        {
            cboMaSP.SelectedIndex = -1;
            txtTenSP.Text = "";
            txtDonGiaNhap.Text = "";
            nudSoluong.Value = 1;
            nudGiamgia.Value = 0;
            txtThanhtien.Text = "";
        }

        private void ResetForm()
        {
            txtMaHDN.Text = GenerateNewInvoiceCode();
            cboMaNV.SelectedIndex = -1;
            cboMaNCC.SelectedIndex = -1;
            txtTenNV.Text = "";
            txtTenNCC.Text = "";
            txtSDT.Text = "";
            txtDiachi.Text = "";
            dgvDSSP.DataSource = null;
            lblTongtien.Text = "0";
            lblSoluongSP.Text = "0";
            lblSoSP.Text = "0";
            lblTongtienChu.Text = "";

            ResetValuesSP();
            isHoaDonCreated = false;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            btnLuuHD.Enabled = false;
            btnInHD.Enabled = false;
            btnXoaHD.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnThem.Enabled = false;
            btnLuu.Enabled = true;
            ResetValuesSP();
            cboMaSP.Focus();
        }

        private bool isHoaDonCreated = false; // Biến trạng thái kiểm tra hóa đơn đã tạo chưa

        private void btnTaomoi_Click(object sender, EventArgs e)
        {

            if (cboMaNV.SelectedIndex == -1 || cboMaNCC.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn mã nhân viên và mã nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maHDN = txtMaHDN.Text.Trim();

            string sqlInsert = "INSERT INTO tblHoaDonNhap (SoHDN, MaNV, MaNCC, NgayNhap) " +
                               "VALUES ('" + maHDN + "', '" + cboMaNV.SelectedValue + "', '" + cboMaNCC.SelectedValue + "', '" + dtpNgaynhap.Value.ToString("yyyy-MM-dd") + "')";
            Functions.Runsql(sqlInsert);

            isHoaDonCreated = true;
            btnThem.Enabled = true;
            btnLuuHD.Enabled = true;
            btnInHD.Enabled = true;
            btnXoaHD.Enabled = true;
            MessageBox.Show("Đã tạo hóa đơn. Tiếp tục thêm sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Load_dgvDSSP();
        }

        private void TinhTongTien()
        {
            double tong = 0;
            int tongSL = 0;
            for (int i = 0; i < dgvDSSP.Rows.Count; i++)
            {
                try
                {
                    tong += Convert.ToDouble(dgvDSSP.Rows[i].Cells["ThanhTien"].Value);
                    tongSL += Convert.ToInt32(dgvDSSP.Rows[i].Cells["SoLuong"].Value);
                }
                catch
                {
                    // Bỏ qua nếu có lỗi chuyển đổi
                }
            }
            lblTongtien.Text = tong.ToString("N0");
            lblSoluongSP.Text = tongSL.ToString();
            lblSoSP.Text = dgvDSSP.Rows.Count.ToString();

            lblTongtienChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChu(tong.ToString());
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDSSP.CurrentRow == null) return;

            string maHDN = txtMaHDN.Text;
            string maSP = dgvDSSP.CurrentRow.Cells["MaHang"].Value.ToString();

            if (MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này khỏi hóa đơn?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string sql = "DELETE FROM tblChiTietHoaDonNhap WHERE SoHDN = '" + maHDN + "' AND MaHang = '" + maSP + "'";
                Functions.Runsql(sql);
                Load_dgvDSSP();
                TinhTongTien();
            }
        }

        private void btnXoaHD_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa toàn bộ hóa đơn và chi tiết?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string sql1 = "DELETE FROM tblChiTietHoaDonNhap WHERE SoHDN = '" + txtMaHDN.Text + "'";
                Functions.Runsql(sql1);
                string sql2 = "DELETE FROM tblHoaDonNhap WHERE SoHDN = '" + txtMaHDN.Text + "'";
                Functions.Runsql(sql2);
                MessageBox.Show("Đã xóa hóa đơn nhập!", "Thông báo");
                ResetForm();
            }
        }

        private void btnLuuHD_Click(object sender, EventArgs e)
        {
            double tong = 0;
            try
            {
                tong = dgvDSSP.Rows.Cast<DataGridViewRow>().Sum(r => Convert.ToDouble(r.Cells["ThanhTien"].Value));
            }
            catch
            {
                MessageBox.Show("Có lỗi khi tính tổng tiền. Vui lòng kiểm tra dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = "UPDATE tblHoaDonNhap SET TongTien = " + tong + " WHERE SoHDN = '" + txtMaHDN.Text + "'";
            Functions.Runsql(sql);
            MessageBox.Show("Hóa đơn đã được cập nhật tổng tiền!", "Thông báo");
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboMaSP.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải chọn mã sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaSP.Focus();
                return;
            }

            if (nudSoluong.Value <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maHDN = txtMaHDN.Text.Trim();
            if (!isHoaDonCreated)
            {
                if (cboMaNV.SelectedIndex == -1 || cboMaNCC.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn mã nhân viên và mã nhà cung cấp để tạo hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sqlInsert = "INSERT INTO tblHoaDonNhap (SoHDN, MaNV, MaNCC, NgayNhap) " +
                                   "VALUES ('" + maHDN + "', '" + cboMaNV.SelectedValue + "', '" + cboMaNCC.SelectedValue + "', '" + dtpNgaynhap.Value.ToString("yyyy-MM-dd") + "')";
                Functions.Runsql(sqlInsert);
                isHoaDonCreated = true;
                btnThem.Enabled = true;
                btnLuuHD.Enabled = true;
                btnInHD.Enabled = true;
                btnXoaHD.Enabled = true;
                MessageBox.Show("Đã tự động tạo hóa đơn. Tiếp tục thêm sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            string maSP = cboMaSP.SelectedValue.ToString();
            double donGia = Convert.ToDouble(txtDonGiaNhap.Text);
            int soLuong = Convert.ToInt32(nudSoluong.Value);
            double giamGia = Convert.ToDouble(nudGiamgia.Value);
            double thanhTien = Convert.ToDouble(txtThanhtien.Text);

            // Kiểm tra xem sản phẩm đã tồn tại trong hóa đơn chưa
            string sqlCheck = "SELECT * FROM tblChiTietHoaDonNhap WHERE SoHDN = '" + maHDN + "' AND MaHang = '" + maSP + "'";
            if (Functions.Checkkey(sqlCheck))
            {
                MessageBox.Show("Sản phẩm này đã có trong hóa đơn. Vui lòng chỉnh sửa thay vì thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sqlInsertCT = "INSERT INTO tblChiTietHoaDonNhap(SoHDN, MaHang, SoLuong, DonGia, GiamGia, ThanhTien) " +
                                 "VALUES('" + maHDN + "', '" + maSP + "', " + soLuong + ", " + donGia + ", " + giamGia + ", " + thanhTien + ")";
            Functions.Runsql(sqlInsertCT);

            Load_dgvDSSP();
            ResetValuesSP();
            btnBoqua.Enabled = false;
            btnThem.Enabled = true;
        }


        private void btnBoqua_Click_1(object sender, EventArgs e)
        {
            ResetValuesSP();
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = false;
        }

        private void btnBoquaHD_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn bỏ qua hóa đơn hiện tại?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ResetForm(); // Đặt lại toàn bộ form
                btnThem.Enabled = true;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnBoqua.Enabled = false;
                btnLuuHD.Enabled = false;
                MessageBox.Show("Đã bỏ qua hóa đơn. Bạn có thể tạo hóa đơn mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDSSP.CurrentRow == null) return;

            string maHDN = txtMaHDN.Text;
            string maSP = cboMaSP.SelectedValue.ToString();
            double donGia = Convert.ToDouble(txtDonGiaNhap.Text);
            int soLuong = Convert.ToInt32(nudSoluong.Value);
            double giamGia = Convert.ToDouble(nudGiamgia.Value);
            double thanhTien = soLuong * donGia * (1 - giamGia / 100);

            string sql = "UPDATE tblChiTietHoaDonNhap SET SoLuong = " + soLuong + ", DonGia = " + donGia + ", GiamGia = " + giamGia + ", ThanhTien = " + thanhTien +
                         " WHERE SoHDN = '" + maHDN + "' AND MaHang = '" + maSP + "'";
            Functions.Runsql(sql);
            Load_dgvDSSP();
            TinhTongTien();
            ResetValuesSP();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = false;
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(PrintPage);
            pd.Print();
        }
        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString("HÓA ĐƠN NHẬP HÀNG", new Font("Arial", 16), Brushes.Black, 100, 50);
            e.Graphics.DrawString("Số hóa đơn: " + txtMaHDN.Text, new Font("Arial", 12), Brushes.Black, 100, 80);
            // Thêm logic in chi tiết từ dgvDSSP
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đóng form này? Dữ liệu chưa lưu sẽ bị mất!", "Xác nhận đóng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close(); // Đóng form
            }
        }

       
    }
}