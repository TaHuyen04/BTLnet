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
    public partial class frmTaodonnhap : Form
    {
        public frmTaodonnhap()
        {
            InitializeComponent();
            
        }

        private bool isHoaDonCreated = false; // Biến trạng thái kiểm tra hóa đơn đã tạo chưa
        private bool isHoaDonSaved = false; // biến kiểm tra trạng thái hóa đơn đã được lưu chưa


        private void frmTaodonnhap_Load(object sender, EventArgs e)
        {
            // Đặt ReadOnly cho các TextBox để không cho nhập thủ công
            txtTenSP.ReadOnly = true;
            txtDonGiaNhap.ReadOnly = true;
            txtTenNV.ReadOnly = true;
            txtTenNCC.ReadOnly = true;
            txtSDT.ReadOnly = true;
            txtDiachi.ReadOnly = true;
            txtThanhtien.ReadOnly = true;
            txtMaHDN.ReadOnly = true; // Ngăn thay đổi mã hóa đơn sau khi tạo

            // Load ComboBox Mã nhân viên 
            string sqlNhanVien = "SELECT MaNV FROM tblNhanVien";
            Class.Functions.FillCombo(sqlNhanVien, cboMaNV, "MaNV", "MaNV");
            cboMaNV.SelectedIndex = -1;

            // Load ComboBox Mã nhà cung cấp 
            string sqlNCC = "SELECT MaNCC FROM tblNhaCungCap";
            Class.Functions.FillCombo(sqlNCC, cboMaNCC, "MaNCC", "MaNCC");
            cboMaNCC.SelectedIndex = -1;

            // Load ComboBox Mã sản phẩm 
            string sqlSP = "SELECT MaHang FROM tblDMHang";
            Class.Functions.FillCombo(sqlSP, cboMaSP, "MaHang", "MaHang");
            cboMaSP.SelectedIndex = -1;
;

            // Tự sinh mã hóa đơn
            txtMaHDN.Text = GenerateNewInvoiceCode();

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
            string newCode = "HDN001"; // Mã mặc định nếu không có hóa đơn nào
            string sql = "SELECT TOP 1 SoHDN FROM tblHoaDonNhap ORDER BY SoHDN DESC";
            string lastCode = Functions.GetFieldValues(sql);

            if (!string.IsNullOrEmpty(lastCode))
            {
                string numberPart = lastCode.Substring(3); // Bỏ "HDN"
                int number = int.Parse(numberPart) + 1; // Tăng số lên 1
                newCode = "HDN" + number.ToString("D3"); // Định dạng lại 
            }
            return newCode;
        }

        private void cboMaNCC_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            if (cboMaNCC.SelectedIndex != -1)
            {
                string sql = "SELECT TenNCC, DienThoai, DiaChi FROM tblNhaCungCap WHERE MaNCC = '" + cboMaNCC.SelectedValue + "'";
                DataTable dt = Functions.getdatatotable(sql);
                if (dt.Rows.Count > 0)
                {
                    txtTenNCC.Text = dt.Rows[0]["TenNCC"].ToString();
                    txtSDT.Text = dt.Rows[0]["DienThoai"].ToString();
                    txtDiachi.Text = dt.Rows[0]["DiaChi"].ToString();
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
        {// Sử dụng JOIN để lấy TenHang từ tblDMHang và thêm cột GiamGia
            string sql = "SELECT ct.MaHang, dm.TenHang, ct.DonGia, ct.SoLuong, ct.GiamGia, ct.ThanhTien " +
                         "FROM tblChiTietHoaDonNhap ct " +
                         "INNER JOIN tblDMHang dm ON ct.MaHang = dm.MaHang " +
                         "WHERE ct.SoHDN = '" + txtMaHDN.Text + "'";
             DataTable tblDSSP = Functions.getdatatotable(sql);
            dgvDSSP.DataSource = tblDSSP;

            // Cập nhật header và độ rộng cột
            if (dgvDSSP.Columns.Count > 0)
            {
                dgvDSSP.Columns[0].HeaderText = "Mã hàng";
                dgvDSSP.Columns[1].HeaderText = "Tên hàng";
                dgvDSSP.Columns[2].HeaderText = "Đơn giá nhập";
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
            lblTongtien.Text = lblTongtien.Text;
            lblSoluongSP.Text = lblSoluongSP.Text;
            lblSoSP.Text = lblSoSP.Text;
            lblTongtienChu.Text = lblTongtienChu.Text;

            ResetValuesSP();
            isHoaDonCreated = false;
            isHoaDonSaved = false;

            // Đặt lại trạng thái các nút
            btnTaomoi.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = false;
            btnBoquaHD.Enabled = true;
            btnLuuHD.Enabled = false;
            btnXoaHD.Enabled = false;
            btnInHD.Enabled = false;
            btnDong.Enabled = true;
        }


        

        private void btnTaomoi_Click(object sender, EventArgs e)
        {

            if (cboMaNV.SelectedIndex == -1 || cboMaNCC.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn mã nhân viên và mã nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maHDN = txtMaHDN.Text.Trim();
            txtMaHDN.Text = GenerateNewInvoiceCode();
               

            string sqlInsert = "INSERT INTO tblHoaDonNhap (SoHDN, MaNV, MaNCC, NgayNhap, TongTien) " +
                               "VALUES ('" + maHDN + "', '" + cboMaNV.SelectedValue + "', '" + cboMaNCC.SelectedValue + "', '" + dtpNgaynhap.Value.ToString("yyyy-MM-dd") + "', 0)";
            Functions.Runsql(sqlInsert);

            isHoaDonCreated = true;
            btnThem.Enabled = true;
            btnBoqua.Enabled = true;
            btnLuuHD.Enabled = true;
            btnXoaHD.Enabled = true;
            btnBoquaHD.Enabled = false;
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
            if (cboMaSP.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải chọn mã sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaSP.Focus();
                return;
            }

            string maHDN = txtMaHDN.Text.Trim();
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
            TinhTongTien();

            isHoaDonSaved = false; // Đặt lại trạng thái lưu khi thêm sản phẩm mới

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
            lblTongtien.Text = lblTongtien.Text+ tong.ToString("N0");
            lblSoluongSP.Text = lblSoluongSP.Text + tongSL.ToString();
            lblSoSP.Text = lblSoSP.Text + dgvDSSP.Rows.Count.ToString();

            lblTongtienChu.Text = lblTongtienChu.Text + Functions.ChuyenSoSangChu(tong.ToString());
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
            isHoaDonSaved = false; // Đặt lại trạng thái lưu khi xóa sản phẩm
        }
        private void btnBoqua_Click_1(object sender, EventArgs e)
        {
            ResetValuesSP();
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Vô hiệu hóa nút thêm khi bắt đầu sửa
            btnThem.Enabled = false;

            string maHDN = txtMaHDN.Text;
            string maSP = dgvDSSP.CurrentRow.Cells["MaHang"].Value.ToString();

            // Lấy giá trị cũ từ DataGridView
            double donGiaCu = Convert.ToDouble(dgvDSSP.CurrentRow.Cells["DonGia"].Value);
            int soLuongCu = Convert.ToInt32(dgvDSSP.CurrentRow.Cells["SoLuong"].Value);
            double giamGiaCu = Convert.ToDouble(dgvDSSP.CurrentRow.Cells["GiamGia"].Value);
            double thanhTienCu = Convert.ToDouble(dgvDSSP.CurrentRow.Cells["ThanhTien"].Value);

            // Lấy giá trị mới từ các điều khiển
            if (string.IsNullOrEmpty(txtDonGiaNhap.Text) || nudSoluong.Value <= 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ và hợp lệ Đơn giá và Số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double donGia = Convert.ToDouble(txtDonGiaNhap.Text);
            int soLuong = Convert.ToInt32(nudSoluong.Value);
            double giamGia = Convert.ToDouble(nudGiamgia.Value);
            double thanhTien = soLuong * donGia * (1 - giamGia / 100);

            // Kiểm tra nếu không có sự thay đổi
            if (donGia == donGiaCu && soLuong == soLuongCu && giamGia == giamGiaCu && Math.Abs(thanhTien - thanhTienCu) < 0.01) // So sánh với độ chính xác nhỏ
            {
                MessageBox.Show("Không có thay đổi nào để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cập nhật bảng tblChiTietHoaDonNhap
            string sql = "UPDATE tblChiTietHoaDonNhap SET SoLuong = " + soLuong + ", DonGia = " + donGia + ", GiamGia = " + giamGia + ", ThanhTien = " + thanhTien +
                         " WHERE SoHDN = '" + maHDN + "' AND MaHang = '" + maSP + "'";
            Functions.Runsql(sql);

            Load_dgvDSSP();
            TinhTongTien();
            ResetValuesSP();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = false;
            btnThem.Enabled = true; // Kích hoạt lại btnThem sau khi sửa thành công
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

            // Cập nhật tổng tiền trong tblHoaDonNhap
            string sqlUpdateHD = "UPDATE tblHoaDonNhap SET TongTien = " + tong + " WHERE SoHDN = '" + txtMaHDN.Text + "'";
            Functions.Runsql(sqlUpdateHD);

            // Cập nhật số lượng tồn kho trong tblDMHang
            foreach (DataGridViewRow row in dgvDSSP.Rows)
            {
                string maHang = row.Cells["MaHang"].Value.ToString();
                int soLuongNhap = Convert.ToInt32(row.Cells["SoLuong"].Value);

                // Lấy số lượng tồn hiện tại
                string sqlGetSoLuongTon = "SELECT SoLuong FROM tblDMHang WHERE MaHang = '" + maHang + "'";
                string soLuongTonHienTaiStr = Functions.GetFieldValues(sqlGetSoLuongTon);
                int soLuongTonHienTai = string.IsNullOrEmpty(soLuongTonHienTaiStr) ? 0 : Convert.ToInt32(soLuongTonHienTaiStr);

                // Tính số lượng tồn mới
                int soLuongTonMoi = soLuongTonHienTai + soLuongNhap;

                // Cập nhật số lượng tồn trong tblDMHang
                string sqlUpdateSP = "UPDATE tblDMHang SET SoLuong= " + soLuongTonMoi + " WHERE MaHang = '" + maHang + "'";
                Functions.Runsql(sqlUpdateSP);
            }

            // Vô hiệu hóa các nút không cần thiết sau khi lưu
            btnTaomoi.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = false;
            btnBoquaHD.Enabled = false;
            btnLuuHD.Enabled = false;

            // Chỉ giữ các nút cần thiết
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
                string sql1 = "DELETE FROM tblChiTietHoaDonNhap WHERE SoHDN = '" + txtMaHDN.Text + "'";
                Functions.Runsql(sql1);
                string sql2 = "DELETE FROM tblHoaDonNhap WHERE SoHDN = '" + txtMaHDN.Text + "'";
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
            worksheet.Cells[1, 1] = "Cửa hàng bán xe máy ";
            worksheet.Cells[1, 1].Font.Color = Color.Blue;
            worksheet.Cells[2, 1] = "Địa chỉ: 12 Chùa Bộc, Quang Trung, Đống Đa, Hà Nội ";
            worksheet.Cells[2, 1].Font.Color = Color.Blue;
            worksheet.Cells[3, 1] = "Số điện thoại: 077 226 0934 ";
            worksheet.Cells[3, 1].Font.Color = Color.Blue;
            Excel.Range mergeRange = worksheet.Range[worksheet.Cells[5, 1], worksheet.Cells[5, 11]];
            mergeRange.Merge();
            mergeRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            mergeRange.Value = "HOÁ ĐƠN NHẬP HÀNG";
            mergeRange.Font.Size = 18;
            mergeRange.Font.Color = Color.Red;
            worksheet.Cells[7, 2] = "Số hoá đơn: ";
            worksheet.Cells[7, 3] = txtMaHDN.Text;
            worksheet.Cells[8, 2] = "Ngày nhập: ";
            worksheet.Cells[8, 3] = dtpNgaynhap.Value;
            worksheet.Columns[3].ColumnWidth = 12;
            worksheet.Columns[2].ColumnWidth = 13;
            worksheet.Columns[7].ColumnWidth = 13;
            worksheet.Cells[8, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            worksheet.Cells[7, 7] = "Nhà cung cấp: ";
            worksheet.Cells[7, 8] = txtTenNCC.Text;
            worksheet.Cells[8, 7] = "Điện thoại: ";
            worksheet.Cells[8, 8] = txtSDT.Text;
            worksheet.Cells[9, 7] = "Địa chỉ: ";
            worksheet.Cells[9, 8] = txtDiachi.Text;
            worksheet.Cells[9, 2] = "Tên nhân viên: ";
            worksheet.Cells[9, 3] = txtTenNV.Text;
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

            // Điền số thứ tự và đổ dữ liệu từ DataGridView vào Excel
            for (int i = 0; i < dgvDSSP.Rows.Count; i++)
            {
                worksheet.Cells[i + 12, 2].Value = i + 1; // Điền số thứ tự
                worksheet.Cells[i + 12, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                worksheet.Cells[i + 12, 2].Borders.Weight = Excel.XlBorderWeight.xlThin;
                worksheet.Cells[i + 12, 2].Font.Size = 12;
                worksheet.Cells[i + 12, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }
            for (int j = 0; j < dgvDSSP.Columns.Count; j++)
                for (int i = 0; i < dgvDSSP.Rows.Count; i++)
                {
                    worksheet.Cells[i + 12, j + 3].Value = dgvDSSP.Rows[i].Cells[j].Value?.ToString();
                    worksheet.Cells[i + 12, j + 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    worksheet.Cells[i + 12, j + 3].Borders.Weight = Excel.XlBorderWeight.xlThin;
                    worksheet.Cells[i + 12, j + 3].Font.Size = 12;
                    worksheet.Cells[i + 12, j + 3].EntireColumn.AutoFit();
                    worksheet.Cells[i + 12, j + 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                }
            worksheet.Cells[(dgvDSSP.Rows.Count + 12) + 2, 8] = lblSoSP.Text;
            worksheet.Cells[(dgvDSSP.Rows.Count + 12) + 3, 8] = lblSoluongSP.Text;
            worksheet.Cells[(dgvDSSP.Rows.Count + 12) + 4, 8] = lblTongtien.Text;
            worksheet.Cells[(dgvDSSP.Rows.Count + 12) + 6, 2] = lblTongtienChu.Text;
            worksheet.Cells[(dgvDSSP.Rows.Count + 12) + 8, 6] = "Hà Nội, Ngày " + dtpNgaynhap.Value.Day + ", tháng " + dtpNgaynhap.Value.Month + ", năm " + dtpNgaynhap.Value.Year;

        }


        private void btnInHD_Click(object sender, EventArgs e)
        {

            InHD();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (isHoaDonCreated && !isHoaDonSaved)
            {
                DialogResult result = MessageBox.Show("Vui lòng lưu hóa đơn trước khi đóng!", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    btnLuuHD.PerformClick(); // Tự động kích hoạt lưu nếu chọn Yes
                    if (!isHoaDonSaved) // Nếu lưu thất bại (do lỗi), không cho đóng
                    {
                        return;
                    }
                }
                else
                {
                    DialogResult confirmDiscard = MessageBox.Show("Các thay đổi sẽ không được lưu. Bạn có muốn xóa hóa đơn hiện tại?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirmDiscard == DialogResult.Yes)
                    {
                        string sql1 = "DELETE FROM tblChiTietHoaDonNhap WHERE SoHDN = '" + txtMaHDN.Text + "'";
                        Functions.Runsql(sql1);
                        string sql2 = "DELETE FROM tblHoaDonNhap WHERE SoHDN = '" + txtMaHDN.Text + "'";
                        Functions.Runsql(sql2);
                    }
                    else
                    {
                        return; // Không đóng form nếu chọn No và không muốn xóa
                    }
                }
            }

            ResetForm(); // Đặt lại form trước khi đóng
            this.Close();
        }

       
    }
}