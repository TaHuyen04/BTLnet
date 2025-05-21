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
    public partial class frmTaodonban : Form
    {
        public frmTaodonban()
        {
            InitializeComponent();
        }
        DataTable tblDSSP;

        private void frmTaodonban_Load(object sender, EventArgs e)
        {
            // Đặt ReadOnly cho các TextBox để không cho nhập thủ công
            txtTenSP.ReadOnly = true;
            txtTenNV.ReadOnly = true;
            txtTenKH.ReadOnly = true;
            txtSDT.ReadOnly = true;
            txtDiachi.ReadOnly = true;
            txtThanhtien.ReadOnly = true;
            txtMaHDB.ReadOnly = true;

            // Load ComboBox Mã nhân viên 
            string sqlNhanVien = "SELECT MaNV FROM tblNhanVien";
            Class.Functions.FillCombo(sqlNhanVien, cboMaNV, "MaNV", "MaNV");
            cboMaNV.SelectedIndex = -1;

            // Load ComboBox Mã khách hàng 
            string sqlNCC = "SELECT MaKhach FROM tblKhachHang";
            Class.Functions.FillCombo(sqlNCC, cboMaKH, "MaKhach", "MaKhach");
            cboMaKH.SelectedIndex = -1;

            // Load ComboBox Mã sản phẩm 
            string sqlSP = "SELECT MaHang FROM tblDMHang";
            Class.Functions.FillCombo(sqlSP, cboMaSP, "MaHang", "MaHang");
            cboMaSP.SelectedIndex = -1;

            // Tự sinh mã hóa đơn
            txtMaHDB.Text = GenerateNewInvoiceCode();

            // Khởi tạo giá trị mặc định cho thuế
            nudThue.Value = 0; // Giá trị mặc định là 0%
            nudThue.Minimum = 0;
            nudThue.Maximum = 100;

            ResetForm();
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
            string newCode = "DDH001"; // Mã mặc định nếu không có hóa đơn nào
            string sql = "SELECT TOP 1 SoDDH FROM tblDonDatHang ORDER BY SoDDH DESC";
            string lastCode = Functions.GetFieldValues(sql);

            if (!string.IsNullOrEmpty(lastCode))
            {
                // Tách phần số từ mã (VD: "DDH01" -> "001")
                string numberPart = lastCode.Substring(3); // Bỏ "DDH"
                int number = int.Parse(numberPart) + 1; // Tăng số lên 1
                newCode = "DDH" + number.ToString("D3"); // Định dạng lại (VD: "DDH002")
            }

            return newCode;
        }

        private void cboMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaKH.SelectedIndex != -1)
            {
                string sql = "SELECT TenKhach, DienThoai, DiaChi FROM tblKhachHang WHERE MaKhach = '" + cboMaKH.SelectedValue + "'";
                DataTable dt = Functions.getdatatotable(sql);
                if (dt.Rows.Count > 0)
                {
                    txtTenKH.Text = dt.Rows[0]["TenKhach"].ToString();
                    txtSDT.Text = dt.Rows[0]["DienThoai"].ToString();
                    txtDiachi.Text = dt.Rows[0]["DiaChi"].ToString();
                }
                else
                {
                    txtTenKH.Text = "";
                    txtSDT.Text = "";
                    txtDiachi.Text = "";
                }
            }
            else
            {
                txtTenKH.Text = "";
                txtSDT.Text = "";
                txtDiachi.Text = "";
            }
        }

        private void cboMaNV_SelectedIndexChanged(object sender, EventArgs e)
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
                string sql = "SELECT TenHang, DonGiaBan FROM tblDMHang WHERE MaHang = '" + cboMaSP.SelectedValue + "'";
                DataTable dt = Functions.getdatatotable(sql);
                if (dt.Rows.Count > 0)
                {
                    txtTenSP.Text = dt.Rows[0]["TenHang"].ToString();
                    txtDongiaban.Text = dt.Rows[0]["DonGiaBan"].ToString();
                    TinhThanhTien(); // Tự động tính thành tiền khi chọn sản phẩm
                }
            }
            else
            {
                txtTenSP.Text = "";
                txtDongiaban.Text = "";
                txtThanhtien.Text = "";
            }

        }
        private void Load_dgvDSSP()
        {// Sử dụng JOIN để lấy TenHang từ tblDMHang và thêm cột GiamGia
            string sql = "SELECT bt.MaHang, dm.TenHang, dm.DonGiaBan, bt.SoLuong, bt.GiamGia, bt.ThanhTien " +
                         "FROM tblChiTietDonDatHang bt " +
                         "INNER JOIN tblDMHang dm ON bt.MaHang = dm.MaHang " +
                         "WHERE bt.SoDDH = '" + txtMaHDB.Text + "'";
            tblDSSP = Functions.getdatatotable(sql);
            dgvDSSP.DataSource = tblDSSP;

            // Cập nhật header và độ rộng cột
            if (dgvDSSP.Columns.Count > 0)
            {
                dgvDSSP.Columns[0].HeaderText = "Mã hàng";
                dgvDSSP.Columns[1].HeaderText = "Tên hàng";
                dgvDSSP.Columns[2].HeaderText = "Đơn giá bán";
                dgvDSSP.Columns[3].HeaderText = "Số lượng";
                dgvDSSP.Columns[4].HeaderText = "Giảm giá (%)"; // Thêm cột giảm giá
                dgvDSSP.Columns[5].HeaderText = "Thành tiền";

                dgvDSSP.Columns[0].Width = 100;
                dgvDSSP.Columns[1].Width = 200;
                dgvDSSP.Columns[2].Width = 120;
                dgvDSSP.Columns[3].Width = 80;
                dgvDSSP.Columns[4].Width = 100; // Độ rộng cho cột giảm giá
                dgvDSSP.Columns[5].Width = 120;
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
            string sql = "SELECT TenHang, DonGiaBan FROM tblDMHang WHERE MaHang = '" + maSP + "'";
            DataTable dt = Functions.getdatatotable(sql);
            if (dt.Rows.Count > 0)
            {
                txtTenSP.Text = dt.Rows[0]["TenHang"].ToString();
                txtDongiaban.Text = dt.Rows[0]["DonGiaBan"].ToString();
            }
            nudSoluong.Text = dgvDSSP.CurrentRow.Cells["SoLuong"].Value.ToString();
            nudGiamgia.Value = Convert.ToDecimal(dgvDSSP.CurrentRow.Cells["GiamGia"].Value); // Cập nhật giảm giá
            txtThanhtien.Text = dgvDSSP.CurrentRow.Cells["ThanhTien"].Value.ToString();
            TinhThanhTien(); // Cập nhật thành tiền

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoqua.Enabled = true;
            btnThem.Enabled = false;
        }
        private void TinhThanhTien()
        {
            if (!string.IsNullOrEmpty(txtDongiaban.Text) && nudSoluong.Value > 0)
            {
                try
                {
                    double donGia = Convert.ToDouble(txtDongiaban.Text);
                    int soLuong = Convert.ToInt32(nudSoluong.Value);
                    double giamGia = Convert.ToDouble(nudGiamgia.Value);
                    double thanhTien = soLuong * donGia * (1 - giamGia / 100);
                    txtThanhtien.Text = thanhTien.ToString("N0");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tính thành tiền: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtThanhtien.Text = "0";
                }
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
            txtDongiaban.Text = "";
            nudSoluong.Value = 1;
            nudGiamgia.Value = 0;
            txtThanhtien.Text = "";
        }

        private void ResetForm()
        {
            txtMaHDB.Text = GenerateNewInvoiceCode();
            cboMaNV.SelectedIndex = -1;
            cboMaKH.SelectedIndex = -1;
            txtTenNV.Text = "";
            txtTenKH.Text = "";
            txtSDT.Text = "";
            txtDiachi.Text = "";
            dgvDSSP.DataSource = null;
            lblTongtienSP.Text = "0";
            lblSoluongSP.Text = "0";
            lblSoSP.Text = "0";
            lblTongtienChu.Text = "";

            ResetValuesSP();
            isHoaDonCreated = false;
            isHoaDonSaved = false;

            // Đặt lại trạng thái các nút
            btnTaomoi.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = false;
            btnBoquaHD.Enabled = false;
            btnLuuHD.Enabled = false;
            btnXoaHD.Enabled = false;
            btnInHD.Enabled = false;
            btnDong.Enabled = true;
        }


        private bool isHoaDonCreated = false; // Biến trạng thái kiểm tra hóa đơn đã tạo chưa

        private void btnTaomoi_Click(object sender, EventArgs e)
        {
            if (cboMaNV.SelectedIndex == -1 || cboMaKH.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn mã nhân viên và mã nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maDDH = txtMaHDB.Text.Trim();
            string sqlCheck = "SELECT SoDDH FROM tblDonDatHang WHERE SoDDH = '" + maDDH + "'";
            if (Functions.Checkkey(sqlCheck))
            {
                MessageBox.Show("Số hóa đơn này đã tồn tại. Hãy tạo mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHDB.Text = GenerateNewInvoiceCode();
                return;
            }

            string sqlInsert = "INSERT INTO tblDonDatHang (SoDDH, MaNV, MaKhach, NgayMua, TongTien) " +
                               "VALUES ('" + maDDH + "', '" + cboMaNV.SelectedValue + "', '" + cboMaKH.SelectedValue + "', '" + dtpNgayban.Value.ToString("yyyy-MM-dd") + "', 0)";
            Functions.Runsql(sqlInsert);

            isHoaDonCreated = true;
            btnThem.Enabled = true;
            btnBoqua.Enabled = true;
            btnLuuHD.Enabled = true;
            btnXoaHD.Enabled = true;
            MessageBox.Show("Đã tạo hóa đơn. Tiếp tục thêm sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Load_dgvDSSP();
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!isHoaDonCreated)
            {
                MessageBox.Show("Vui lòng tạo hóa đơn trước khi thêm sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnTaomoi.Focus();
                return;
            }

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

            string maSP = cboMaSP.SelectedValue.ToString();
            int soLuong = Convert.ToInt32(nudSoluong.Value);

            // Kiểm tra số lượng tồn kho từ tblDMHang
            string sqlGetSoLuong = "SELECT SoLuong FROM tblDMHang WHERE MaHang = '" + maSP + "'";
            string soLuongStr = Functions.GetFieldValues(sqlGetSoLuong);
            int soLuongTon = string.IsNullOrEmpty(soLuongStr) ? 0 : Convert.ToInt32(soLuongStr);

            if (soLuong > soLuongTon)
            {
                MessageBox.Show($"Số lượng tồn kho của sản phẩm {maSP} chỉ còn {soLuongTon}! Không đủ để bán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maDDH = txtMaHDB.Text.Trim();
            double donGia = Convert.ToDouble(txtDongiaban.Text);
            double giamGia = Convert.ToDouble(nudGiamgia.Value);
            double thanhTien = Convert.ToDouble(txtThanhtien.Text);

            string sqlCheck = "SELECT * FROM tblChiTietDonDatHang WHERE SoDDH = '" + maDDH + "' AND MaHang = '" + maSP + "'";
            if (Functions.Checkkey(sqlCheck))
            {
                MessageBox.Show("Sản phẩm này đã có trong hóa đơn. Vui lòng chỉnh sửa thay vì thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sqlInsertCT = "INSERT INTO tblChiTietDonDatHang (SoDDH, MaHang, SoLuong, GiamGia, ThanhTien) " +
                                 "VALUES('" + maDDH + "', '" + maSP + "', " + soLuong + ", " + giamGia + ", " + thanhTien + ")";
            Functions.Runsql(sqlInsertCT);

            Load_dgvDSSP();
            ResetValuesSP();
            btnBoqua.Enabled = false;
            btnThem.Enabled = true;
            TinhTongTien();
            isHoaDonSaved = false;  // Đặt lại trạng thái lưu khi thêm sản phẩm mới
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
            lblTongtienSP.Text = tong.ToString("N0");
            lblSoluongSP.Text = tongSL.ToString();
            lblSoSP.Text = dgvDSSP.Rows.Count.ToString();

            lblTongtienChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChu(tong.ToString());
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDSSP.CurrentRow == null) return;

            string maDDH = txtMaHDB.Text;
            string maSP = dgvDSSP.CurrentRow.Cells["MaHang"].Value.ToString();

            if (MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này khỏi hóa đơn?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string sql = "DELETE FROM tblChiTietDonDatHang WHERE SoDDH = '" + maDDH + "' AND MaHang = '" + maSP + "'";
                Functions.Runsql(sql);
                Load_dgvDSSP();
                TinhTongTien();
            }
            isHoaDonSaved = false; // Đặt lại trạng thái lưu khi xóa sản phẩm
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetValuesSP();
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDSSP.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isHoaDonSaved)
            {
                MessageBox.Show("Hóa đơn đã được lưu hoàn tất. Không thể sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnThem.Enabled = false;

            string maDDH = txtMaHDB.Text;
            string maSP = dgvDSSP.CurrentRow.Cells["MaHang"].Value.ToString();

            double donGiaCu = Convert.ToDouble(dgvDSSP.CurrentRow.Cells["DonGiaBan"].Value);
            int soLuongCu = Convert.ToInt32(dgvDSSP.CurrentRow.Cells["SoLuong"].Value);
            double giamGiaCu = Convert.ToDouble(dgvDSSP.CurrentRow.Cells["GiamGia"].Value);
            double thanhTienCu = Convert.ToDouble(dgvDSSP.CurrentRow.Cells["ThanhTien"].Value);

            if (string.IsNullOrEmpty(txtDongiaban.Text) || nudSoluong.Value <= 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ và hợp lệ Đơn giá và Số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnThem.Enabled = true;
                return;
            }

            int soLuongMoi = Convert.ToInt32(nudSoluong.Value);

            // Kiểm tra số lượng tồn kho
            string sqlGetSoLuong = "SELECT SoLuong FROM tblDMHang WHERE MaHang = '" + maSP + "'";
            string soLuongTonStr = Functions.GetFieldValues(sqlGetSoLuong);
            int soLuongTon = string.IsNullOrEmpty(soLuongTonStr) ? 0 : Convert.ToInt32(soLuongTonStr);

            int soLuongCanThem = soLuongMoi - soLuongCu;
            if (soLuongCanThem > soLuongTon)
            {
                MessageBox.Show($"Số lượng tồn kho của sản phẩm {maSP} chỉ còn {soLuongTon}! Không đủ để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnThem.Enabled = true;
                return;
            }

            double donGia = Convert.ToDouble(txtDongiaban.Text);
            double giamGia = Convert.ToDouble(nudGiamgia.Value);
            double thanhTien = soLuongMoi * donGia * (1 - giamGia / 100);

            if (donGia == donGiaCu && soLuongMoi == soLuongCu && giamGia == giamGiaCu && Math.Abs(thanhTien - thanhTienCu) < 0.01)
            {
                MessageBox.Show("Không có thay đổi nào để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnThem.Enabled = true;
                return;
            }

            string sql = "UPDATE tblChiTietDonDatHang SET SoLuong = " + soLuongMoi + ", GiamGia = " + giamGia + ", ThanhTien = " + thanhTien +
                         " WHERE SoDDH = '" + maDDH + "' AND MaHang = '" + maSP + "'";
            Functions.Runsql(sql);

            Load_dgvDSSP();
            TinhTongTien();
            ResetValuesSP();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = false;
            btnThem.Enabled = true;
            MessageBox.Show("Thông tin sản phẩm đã được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            isHoaDonSaved = false;
        }
        private bool isHoaDonSaved = false; // biến kiểm tra trạng thái hóa đơn đã được lưu chưa

        private void btnLuuHD_Click(object sender, EventArgs e)
        {
            if (dgvDSSP.Rows.Count == 0)
            {
                MessageBox.Show("Hóa đơn chưa có sản phẩm nào. Vui lòng thêm sản phẩm trước khi lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

            string sqlUpdateHD = "UPDATE tblDonDatHang SET TongTien = " + tong + " WHERE SoDDH = '" + txtMaHDB.Text + "'";
            Functions.Runsql(sqlUpdateHD);

            foreach (DataGridViewRow row in dgvDSSP.Rows)
            {
                string maHang = row.Cells["MaHang"].Value.ToString();
                int soLuongBan = Convert.ToInt32(row.Cells["SoLuong"].Value);

                string sqlGetSoLuong = "SELECT SoLuong FROM tblDMHang WHERE MaHang = '" + maHang + "'";
                string soLuongTonHienTaiStr = Functions.GetFieldValues(sqlGetSoLuong);
                int soLuongTonHienTai = string.IsNullOrEmpty(soLuongTonHienTaiStr) ? 0 : Convert.ToInt32(soLuongTonHienTaiStr);

                // Giảm số lượng tồn kho khi bán
                int soLuongTonMoi = soLuongTonHienTai - soLuongBan;

                if (soLuongTonMoi < 0)
                {
                    MessageBox.Show($"Số lượng tồn kho của sản phẩm {maHang} không đủ để bán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string sqlUpdateSP = "UPDATE tblDMHang SET SoLuong = " + soLuongTonMoi + " WHERE MaHang = '" + maHang + "'";
                Functions.Runsql(sqlUpdateSP);
            }

            btnTaomoi.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = false;
            btnBoquaHD.Enabled = false;
            btnLuuHD.Enabled = false;

            btnXoaHD.Enabled = true;
            btnInHD.Enabled = true;
            btnDong.Enabled = true;

            MessageBox.Show("Hóa đơn đã được lưu và số lượng sản phẩm đã được cập nhật! Hóa đơn sẵn sàng để in", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            isHoaDonSaved = true;
        }

        private void btnXoaHD_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa toàn bộ hóa đơn và chi tiết?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string sql1 = "DELETE FROM tblChiTietDonDatHang WHERE SoDDH = '" + txtMaHDB.Text + "'";
                Functions.Runsql(sql1);
                string sql2 = "DELETE FROM tblDonDatHang WHERE SoDDH = '" + txtMaHDB.Text + "'";
                Functions.Runsql(sql2);
                MessageBox.Show("Đã xóa hóa đơn nhập!", "Thông báo");
                ResetForm();
            }
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(PrintPage);
            pd.Print();
        }
        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            float yPos = 50;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;

            // In tiêu đề
            e.Graphics.DrawString("HÓA ĐƠN BÁN HÀNG", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, leftMargin, yPos);
            yPos += 30;

            // In thông tin hóa đơn
            e.Graphics.DrawString($"Số hóa đơn: {txtMaHDB.Text}", new Font("Arial", 12), Brushes.Black, leftMargin, yPos);
            yPos += 20;
            e.Graphics.DrawString($"Ngày bán: {dtpNgayban.Value.ToString("dd/MM/yyyy")}", new Font("Arial", 12), Brushes.Black, leftMargin, yPos);
            yPos += 20;
            e.Graphics.DrawString($"Nhân viên: {txtTenNV.Text}", new Font("Arial", 12), Brushes.Black, leftMargin, yPos);
            yPos += 20;
            e.Graphics.DrawString($"Khách hàng: {txtTenKH.Text}", new Font("Arial", 12), Brushes.Black, leftMargin, yPos);
            yPos += 40;

            // In tiêu đề bảng
            e.Graphics.DrawString("STT", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, leftMargin, yPos);
            e.Graphics.DrawString("Mã hàng", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, leftMargin + 50, yPos);
            e.Graphics.DrawString("Tên hàng", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, leftMargin + 150, yPos);
            e.Graphics.DrawString("Đơn giá", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, leftMargin + 350, yPos);
            e.Graphics.DrawString("Số lượng", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, leftMargin + 450, yPos);
            e.Graphics.DrawString("Giảm giá (%)", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, leftMargin + 550, yPos);
            e.Graphics.DrawString("Thành tiền", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, leftMargin + 650, yPos);
            yPos += 25;

            // In danh sách sản phẩm từ dgvDSSP
            for (int i = 0; i < dgvDSSP.Rows.Count; i++)
            {
                e.Graphics.DrawString((i + 1).ToString(), new Font("Arial", 10), Brushes.Black, leftMargin, yPos);
                e.Graphics.DrawString(dgvDSSP.Rows[i].Cells["MaHang"].Value.ToString(), new Font("Arial", 10), Brushes.Black, leftMargin + 50, yPos);
                e.Graphics.DrawString(dgvDSSP.Rows[i].Cells["TenHang"].Value.ToString(), new Font("Arial", 10), Brushes.Black, leftMargin + 150, yPos);
                e.Graphics.DrawString(Convert.ToDouble(dgvDSSP.Rows[i].Cells["DonGiaBan"].Value).ToString("N0"), new Font("Arial", 10), Brushes.Black, leftMargin + 350, yPos);
                e.Graphics.DrawString(dgvDSSP.Rows[i].Cells["SoLuong"].Value.ToString(), new Font("Arial", 10), Brushes.Black, leftMargin + 450, yPos);
                e.Graphics.DrawString(dgvDSSP.Rows[i].Cells["GiamGia"].Value.ToString(), new Font("Arial", 10), Brushes.Black, leftMargin + 550, yPos);
                e.Graphics.DrawString(Convert.ToDouble(dgvDSSP.Rows[i].Cells["ThanhTien"].Value).ToString("N0"), new Font("Arial", 10), Brushes.Black, leftMargin + 650, yPos);
                yPos += 20;
            }

            // In tổng tiền
            yPos += 20;
            e.Graphics.DrawString($"Tổng tiền: {lblTongtienSP.Text} VNĐ", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, leftMargin, yPos);
            yPos += 20;
            e.Graphics.DrawString($"Bằng chữ: {lblTongtienChu.Text}", new Font("Arial", 12), Brushes.Black, leftMargin, yPos);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (isHoaDonCreated && !isHoaDonSaved)
            {
                DialogResult result = MessageBox.Show("Hóa đơn chưa được lưu. Bạn có muốn lưu trước khi đóng không?", "Cảnh báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    btnLuuHD.PerformClick();
                    if (!isHoaDonSaved)
                    {
                        return;
                    }
                }
                else if (result == DialogResult.No)
                {
                    DialogResult confirmDiscard = MessageBox.Show("Các thay đổi sẽ không được lưu. Bạn có muốn xóa hóa đơn hiện tại?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirmDiscard == DialogResult.Yes)
                    {
                        string sql1 = "DELETE FROM tblChiTietDonDatHang WHERE SoDDH = '" + txtMaHDB.Text + "'";
                        Functions.Runsql(sql1);
                        string sql2 = "DELETE FROM tblDonDatHang WHERE SoDDH = '" + txtMaHDB.Text + "'";
                        Functions.Runsql(sql2);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            ResetForm();
            this.Close();
            this.Close();
            
        }
    }
}
