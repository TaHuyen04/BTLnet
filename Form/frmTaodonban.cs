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
using Excel = Microsoft.Office.Interop.Excel;

namespace QLCHBanXeMay.form
{
    public partial class frmTaodonban : Form
    {
        public frmTaodonban()
        {
            InitializeComponent();
        }
        DataTable tblDSSP;
        private bool isHoaDonCreated = false;
        private bool isHoaDonSaved = false;

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

            // Khởi tạo giá trị mặc định cho thuế (không cần điều khiển nhập vì thuế cố định 5%)
            ResetForm();
            Load_dgvDSSP();
            btnThem.Enabled = false;
            btnBoqua.Enabled = false;
            btnInHD.Enabled = false;
            btnXoaHD.Enabled = false;
            btnLuuHD.Enabled = false;
        }

        private string GenerateNewInvoiceCode()
        {
            string newCode = "DDH001";
            string sql = "SELECT TOP 1 SoDDH FROM tblDonDatHang ORDER BY SoDDH DESC";
            string lastCode = Functions.GetFieldValues(sql);

            if (!string.IsNullOrEmpty(lastCode))
            {
                string numberPart = lastCode.Substring(3);
                int number = int.Parse(numberPart) + 1;
                newCode = "DDH" + number.ToString("D3");
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
                    TinhThanhTien();
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
        {
            string sql = "SELECT bt.MaHang, dm.TenHang, dm.DonGiaBan, bt.SoLuong, bt.GiamGia, bt.ThanhTien " +
                         "FROM tblChiTietDonDatHang bt " +
                         "INNER JOIN tblDMHang dm ON bt.MaHang = dm.MaHang " +
                         "WHERE bt.SoDDH = '" + txtMaHDB.Text + "'";
            tblDSSP = Functions.getdatatotable(sql);
            dgvDSSP.DataSource = tblDSSP;

            if (dgvDSSP.Columns.Count > 0)
            {
                dgvDSSP.Columns[0].HeaderText = "Mã hàng";
                dgvDSSP.Columns[1].HeaderText = "Tên hàng";
                dgvDSSP.Columns[2].HeaderText = "Đơn giá bán";
                dgvDSSP.Columns[3].HeaderText = "Số lượng";
                dgvDSSP.Columns[4].HeaderText = "Giảm giá (%)";
                dgvDSSP.Columns[5].HeaderText = "Thành tiền";

                dgvDSSP.Columns[0].Width = 100;
                dgvDSSP.Columns[1].Width = 200;
                dgvDSSP.Columns[2].Width = 120;
                dgvDSSP.Columns[3].Width = 80;
                dgvDSSP.Columns[4].Width = 100;
                dgvDSSP.Columns[5].Width = 120;
            }

            dgvDSSP.AllowUserToAddRows = false;
            dgvDSSP.EditMode = DataGridViewEditMode.EditProgrammatically;

            TinhTongTien();
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

            string maSP = dgvDSSP.CurrentRow.Cells["MaHang"].Value.ToString();
            cboMaSP.SelectedValue = maSP; // Hiển thị mã sản phẩm trong ComboBox

            string sql = "SELECT TenHang, DonGiaBan FROM tblDMHang WHERE MaHang = '" + maSP + "'";
            DataTable dt = Functions.getdatatotable(sql);
            if (dt.Rows.Count > 0)
            {
                txtTenSP.Text = dt.Rows[0]["TenHang"].ToString();
                txtDongiaban.Text = dt.Rows[0]["DonGiaBan"].ToString();
            }
            nudSoluong.Value = Convert.ToDecimal(dgvDSSP.CurrentRow.Cells["SoLuong"].Value);
            nudGiamgia.Value = Convert.ToDecimal(dgvDSSP.CurrentRow.Cells["GiamGia"].Value);
            txtThanhtien.Text = dgvDSSP.CurrentRow.Cells["ThanhTien"].Value.ToString();
            TinhThanhTien();

            if (!isHoaDonSaved)
            {
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnBoqua.Enabled = true;
                btnThem.Enabled = false;
            }
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
            lblTongtienSP.Text = lblTongtienSP.Text;
            lblDatcoc.Text = lblDatcoc.Text;
            lblThue.Text = lblThue.Text;
            lblTongtienDH.Text = lblTongtienDH.Text;
            lblSoluongSP.Text = lblSoluongSP.Text;
            lblSoSP.Text = lblSoSP.Text;
            lblTongtienChu.Text = lblTongtienChu.Text;

            ResetValuesSP();
            isHoaDonCreated = false;
            isHoaDonSaved = false;

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

        private void btnTaomoi_Click(object sender, EventArgs e)
        {
            if (cboMaNV.SelectedIndex == -1 || cboMaKH.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn mã nhân viên và mã khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            string sqlInsert = "INSERT INTO tblDonDatHang (SoDDH, MaNV, MaKhach, NgayMua, TongTien, DatCoc, Thue) " +
                               "VALUES ('" + maDDH + "', '" + cboMaNV.SelectedValue + "', '" + cboMaKH.SelectedValue + "', '" + dtpNgayban.Value.ToString("yyyy-MM-dd") + "', 0, 0, 0)";
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
            if (!isHoaDonCreated)
            {
                MessageBox.Show("Không có hóa đơn để bỏ qua!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn bỏ qua hóa đơn hiện tại?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql1 = "DELETE FROM tblChiTietDonDatHang WHERE SoDDH = '" + txtMaHDB.Text + "'";
                Functions.Runsql(sql1);
                string sql2 = "DELETE FROM tblDonDatHang WHERE SoDDH = '" + txtMaHDB.Text + "'";
                Functions.Runsql(sql2);

                ResetForm();
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
            isHoaDonSaved = false;
        }

        private void TinhTongTien()
        {
            double tongTienSP = 0;
            int tongSL = 0;
            for (int i = 0; i < dgvDSSP.Rows.Count; i++)
            {
                try
                {
                    tongTienSP += Convert.ToDouble(dgvDSSP.Rows[i].Cells["ThanhTien"].Value);
                    tongSL += Convert.ToInt32(dgvDSSP.Rows[i].Cells["SoLuong"].Value);
                }
                catch
                {
                    // Bỏ qua nếu có lỗi chuyển đổi
                }
            }

            // Tính đặt cọc (30% tổng tiền sản phẩm)
            double datCoc = tongTienSP * 0.3;

            // Tính thuế (5% tổng tiền sản phẩm)
            double thue = tongTienSP * 0.05;

            // Tính tổng tiền đơn hàng
            double tongTienDonHang = tongTienSP + thue;

            // Hiển thị trên form
            lblTongtienSP.Text = lblTongtienSP.Text + tongTienSP.ToString("N0");
            lblDatcoc.Text = lblDatcoc.Text + datCoc.ToString("N0");
            lblThue.Text = lblThue.Text + thue.ToString("N0");
            lblTongtienDH.Text = lblTongtienDH.Text+ tongTienDonHang.ToString("N0");
            lblSoluongSP.Text = lblSoluongSP.Text + tongSL.ToString();
            lblSoSP.Text = lblSoSP.Text + dgvDSSP.Rows.Count.ToString();

            lblTongtienChu.Text = lblTongtienChu.Text + Functions.ChuyenSoSangChu(tongTienDonHang.ToString());
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
            isHoaDonSaved = false;
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

        private void btnLuuHD_Click(object sender, EventArgs e)
        {
            if (dgvDSSP.Rows.Count == 0)
            {
                MessageBox.Show("Hóa đơn chưa có sản phẩm nào. Vui lòng thêm sản phẩm trước khi lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double tongTienSP = 0;
            try
            {
                tongTienSP = dgvDSSP.Rows.Cast<DataGridViewRow>().Sum(r => Convert.ToDouble(r.Cells["ThanhTien"].Value));
            }
            catch
            {
                MessageBox.Show("Có lỗi khi tính tổng tiền. Vui lòng kiểm tra dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double datCoc = tongTienSP * 0.3; // 30% tổng tiền sản phẩm
            double thue = tongTienSP * 0.05;  // 5% tổng tiền sản phẩm
            double tongTienDonHang = tongTienSP + thue;

            string sqlUpdateHD = "UPDATE tblDonDatHang SET TongTien = " + tongTienDonHang + ", DatCoc = " + datCoc + ", Thue = " + thue + " WHERE SoDDH = '" + txtMaHDB.Text + "'";
            Functions.Runsql(sqlUpdateHD);

            foreach (DataGridViewRow row in dgvDSSP.Rows)
            {
                string maHang = row.Cells["MaHang"].Value.ToString();
                int soLuongBan = Convert.ToInt32(row.Cells["SoLuong"].Value);

                string sqlGetSoLuong = "SELECT SoLuong FROM tblDMHang WHERE MaHang = '" + maHang + "'";
                string soLuongTonHienTaiStr = Functions.GetFieldValues(sqlGetSoLuong);
                int soLuongTonHienTai = string.IsNullOrEmpty(soLuongTonHienTaiStr) ? 0 : Convert.ToInt32(soLuongTonHienTaiStr);

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

        private void InHD()
        {
            // Khởi tạo đối tượng Excel
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.Sheets[1];

            // Thông tin cửa hàng
            worksheet.Cells[1, 1] = "Cửa hàng bán xe máy";
            worksheet.Cells[1, 1].Font.Color = Color.Blue;
            worksheet.Cells[2, 1] = "Địa chỉ: 12 Chùa Bộc, Quang Trung, Đống Đa, Hà Nội";
            worksheet.Cells[2, 1].Font.Color = Color.Blue;
            worksheet.Cells[3, 1] = "Số điện thoại: 077 226 0934";
            worksheet.Cells[3, 1].Font.Color = Color.Blue;

            // Tiêu đề hóa đơn
            Excel.Range mergeRange = worksheet.Range[worksheet.Cells[5, 1], worksheet.Cells[5, 11]];
            mergeRange.Merge();
            mergeRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            mergeRange.Value = "HÓA ĐƠN BÁN HÀNG";
            mergeRange.Font.Size = 18;
            mergeRange.Font.Color = Color.Red;

            // Thông tin hóa đơn
            worksheet.Cells[7, 2] = "Số hóa đơn: ";
            worksheet.Cells[7, 3] = txtMaHDB.Text;
            worksheet.Cells[8, 2] = "Ngày bán: ";
            worksheet.Cells[8, 3] = dtpNgayban.Value.ToString("dd/MM/yyyy");
            worksheet.Columns[3].ColumnWidth = 12;
            worksheet.Columns[2].ColumnWidth = 13;
            worksheet.Columns[7].ColumnWidth = 13;
            worksheet.Cells[8, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

            worksheet.Cells[7, 7] = "Khách hàng: ";
            worksheet.Cells[7, 8] = txtTenKH.Text;
            worksheet.Cells[8, 7] = "Điện thoại: ";
            worksheet.Cells[8, 8] = txtSDT.Text;
            worksheet.Cells[9, 7] = "Địa chỉ: ";
            worksheet.Cells[9, 8] = txtDiachi.Text;
            worksheet.Cells[9, 2] = "Nhân viên: ";
            worksheet.Cells[9, 3] = txtTenNV.Text;

            // Tiêu đề bảng
            worksheet.Cells[11, 2] = "STT";
            worksheet.Cells[11, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            worksheet.Cells[11, 2].Borders.Weight = Excel.XlBorderWeight.xlThin;
            worksheet.Cells[11, 2].Interior.Color = Color.LightYellow;
            worksheet.Cells[11, 2].Font.Size = 12;
            worksheet.Cells[11, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            for (int i = 1; i <= dgvDSSP.Columns.Count; i++)
            {
                worksheet.Cells[11, i + 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                worksheet.Cells[11, i + 2].Borders.Weight = Excel.XlBorderWeight.xlThin;
                worksheet.Cells[11, i + 2].Value = dgvDSSP.Columns[i - 1].HeaderText;
                worksheet.Cells[11, i + 2].Interior.Color = Color.LightYellow;
                worksheet.Cells[11, i + 2].Font.Size = 12;
                worksheet.Cells[11, i + 2].EntireColumn.AutoFit();
                worksheet.Cells[11, i + 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }

            // Điền số thứ tự và dữ liệu từ DataGridView vào Excel
            for (int i = 0; i < dgvDSSP.Rows.Count; i++)
            {
                worksheet.Cells[i + 12, 2].Value = i + 1; // Điền số thứ tự
                worksheet.Cells[i + 12, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                worksheet.Cells[i + 12, 2].Borders.Weight = Excel.XlBorderWeight.xlThin;
                worksheet.Cells[i + 12, 2].Font.Size = 12;
                worksheet.Cells[i + 12, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                for (int j = 0; j < dgvDSSP.Columns.Count; j++)
                {
                    worksheet.Cells[i + 12, j + 3].Value = dgvDSSP.Rows[i].Cells[j].Value?.ToString();
                    worksheet.Cells[i + 12, j + 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    worksheet.Cells[i + 12, j + 3].Borders.Weight = Excel.XlBorderWeight.xlThin;
                    worksheet.Cells[i + 12, j + 3].Font.Size = 12;
                    worksheet.Cells[i + 12, j + 3].EntireColumn.AutoFit();
                    worksheet.Cells[i + 12, j + 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                }
            }

            // Thông tin tổng hợp
            worksheet.Cells[(dgvDSSP.Rows.Count + 12) + 2, 8] = lblSoSP.Text; // Số sản phẩm
            worksheet.Cells[(dgvDSSP.Rows.Count + 12) + 3, 8] = lblSoluongSP.Text; // Tổng số lượng
            worksheet.Cells[(dgvDSSP.Rows.Count + 12) + 4, 8] = lblTongtienSP.Text; // Tổng tiền sản phẩm
            worksheet.Cells[(dgvDSSP.Rows.Count + 12) + 5, 8] = lblDatcoc.Text; // Đặt cọc
            worksheet.Cells[(dgvDSSP.Rows.Count + 12) + 6, 8] = lblThue.Text; // Thuế
            worksheet.Cells[(dgvDSSP.Rows.Count + 12) + 7, 8] = lblTongtienDH.Text; // Tổng tiền đơn hàng
            worksheet.Cells[(dgvDSSP.Rows.Count + 12) + 9, 2] = lblTongtienChu.Text; // Bằng chữ
            worksheet.Cells[(dgvDSSP.Rows.Count + 12) + 11, 6] = "Hà Nội, Ngày " + dtpNgayban.Value.Day + " tháng " + dtpNgayban.Value.Month + " năm " + dtpNgayban.Value.Year;
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            InHD();
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
        }
    }
}