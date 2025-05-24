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
    public partial class frmChitietHDban : Form
    {
        public frmChitietHDban()
        {
            InitializeComponent();
        }

        private string soDDH;
        private FormBorderStyle originalBorderStyle;
        private bool originalTopLevel;
        private bool originalControlBox;
        private string originalText;


        public frmChitietHDban(string soDDH)
        {
            InitializeComponent();
            this.soDDH = soDDH;
        }
        private PrintDocument printDocument1 = new PrintDocument();
        private void frmChitietdondathang_Load(object sender, EventArgs e)
        {
            LoadThongTinHoaDon();
            LoadSanPham();
            Hienthitien();
        }
        public void Hienthitien()
        {
            // Lấy tổng tiền
            string sqlTongTien = $"SELECT TongTien FROM tblDonDatHang WHERE SoDDH = '{soDDH}'";
            string tongtien = Functions.GetFieldValues(sqlTongTien);
            lblTOngtien.Text = "Tổng tiền: " + tongtien;

            // Lấy tổng số lượng sản phẩm
            string sqlSoLuong = $"SELECT SUM(SoLuong) FROM tblChiTietDonDatHang WHERE SoDDH = '{soDDH}'";
            string soluong = Functions.GetFieldValues(sqlSoLuong);
            lblTongsoluongsanpham.Text = "Tổng số lượng sản phẩm: " + soluong;

            // Chuyển tổng tiền sang chữ
            string tongtienbangchu = Functions.ChuyenSoSangChu(tongtien);
            lblTongtienbangchu.Text = "Tổng tiền bằng chữ: " + tongtienbangchu;
        }
 

        private void LoadSanPham()
        {
            if (string.IsNullOrEmpty(soDDH))
            {
                MessageBox.Show("Chưa có số đơn đặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sql = $@"
SELECT h.MaHang, h.TenHang, h.DonGiaBan, c.SoLuong, 
       (h.DonGiaBan * c.SoLuong) AS ThanhTien
FROM tblChiTietDonDatHang c
INNER JOIN tblDMHang h ON c.MaHang = h.MaHang
WHERE c.SoDDH = '{soDDH}'";

                DataTable dt = Functions.getdatatotable(sql);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Đơn hàng không có sản phẩm nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dgvDanhsachsanpham.DataSource = dt;

                // Thiết lập tiêu đề cột
                dgvDanhsachsanpham.Columns["MaHang"].HeaderText = "Mã Hàng";
                dgvDanhsachsanpham.Columns["TenHang"].HeaderText = "Tên Hàng";
                dgvDanhsachsanpham.Columns["DonGiaBan"].HeaderText = "Đơn Giá Bán";
                dgvDanhsachsanpham.Columns["SoLuong"].HeaderText = "Số Lượng";
                dgvDanhsachsanpham.Columns["ThanhTien"].HeaderText = "Thành Tiền";

                // Căn đều các cột
                dgvDanhsachsanpham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Căn giữa nội dung trong các cột (nếu cần)
                foreach (DataGridViewColumn column in dgvDanhsachsanpham.Columns)
                {
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // Đặt định dạng số cho các cột liên quan (nếu cần)
                dgvDanhsachsanpham.Columns["DonGiaBan"].DefaultCellStyle.Format = "N0"; // Định dạng số nguyên, có dấu phân cách
                dgvDanhsachsanpham.Columns["ThanhTien"].DefaultCellStyle.Format = "N0"; // Định dạng số nguyên, có dấu phân cách

                dgvDanhsachsanpham.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadThongTinHoaDon()
        {
            string sql = $"SELECT * FROM tblDonDatHang WHERE SoDDH = '{soDDH}'";
            DataTable dtDDH = Functions.getdatatotable(sql);
            if (dtDDH.Rows.Count > 0)
            {
                DataRow row = dtDDH.Rows[0];
                txtSoDDH.Text = row["SoDDH"].ToString();
                txtMaNV.Text = row["MaNV"].ToString();

                // Lấy tên nhân viên
                txtTenNV.Text = Functions.GetFieldValues($"SELECT TenNV FROM tblNhanVien WHERE MaNV = '{txtMaNV.Text}'");

                txtMaKH.Text = row["MaKhach"].ToString();

                // Lấy thông tin khách hàng
                string sqlKH = $"SELECT TenKhach, DienThoai, DiaChi FROM tblKhachHang WHERE MaKhach = '{txtMaKH.Text}'";
                DataTable dtKH = Functions.getdatatotable(sqlKH);
                if (dtKH.Rows.Count > 0)
                {
                    txtTenKH.Text = dtKH.Rows[0]["TenKhach"].ToString();
                    txtSoDT.Text = dtKH.Rows[0]["DienThoai"].ToString();
                    txtDiachi.Text = dtKH.Rows[0]["DiaChi"].ToString();
                }

                // Gán ngày nhập
                if (DateTime.TryParse(row["NgayMua"].ToString(), out DateTime ngayMua))
                {
                    dtpNgaynhap.Value = ngayMua;
                }
                else
                {
                    dtpNgaynhap.Value = DateTime.Now;
                }
            }
        }

        private void txtSoDDH_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpNgaynhap_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSoDT_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDiachi_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvDanhsachsanpham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void lblTongtienbangchu_Click(object sender, EventArgs e)
        {

        }

        private void lblTongsosanpham_Click(object sender, EventArgs e)
        {

        }

        private void lblTongsoluongsanpham_Click(object sender, EventArgs e)
        {

        }

        private void lblTOngtien_Click(object sender, EventArgs e)
        {

        }

       

        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Tạo bitmap chụp hình toàn bộ form
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));

            // Vẽ bitmap lên trang in, có thể tùy chỉnh kích thước cho vừa trang in
            e.Graphics.DrawImage(bmp, e.MarginBounds);

            // Giải phóng tài nguyên
            bmp.Dispose();
        }

        

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(soDDH))
            {
                MessageBox.Show("Không có mã đơn đặt hàng để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa đơn hàng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    // Xóa chi tiết đơn đặt hàng trước
                    string sqlDeleteCT = $"DELETE FROM tblChiTietDonDatHang WHERE SoDDH = '{soDDH}'";
                    Functions.Runsql(sqlDeleteCT);

                    // Xóa đơn đặt hàng
                    string sqlDeleteDDH = $"DELETE FROM tblDonDatHang WHERE SoDDH = '{soDDH}'";
                    Functions.Runsql(sqlDeleteDDH);  // <-- Sửa chỗ này

                    MessageBox.Show("Xóa đơn hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Đóng form
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp = new Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("Không thể khởi tạo Excel. Vui lòng kiểm tra cài đặt Office.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Excel.Workbook xlWorkBook = xlApp.Workbooks.Add(Type.Missing);
            Excel.Worksheet xlSheet = (Excel.Worksheet)xlWorkBook.Worksheets[1];
            xlSheet.Name = "PhieuDonHang";

            xlApp.StandardFontSize = 12;
            xlApp.StandardFont = "Times New Roman";

            // Tiêu đề phiếu
            xlSheet.Range["A1", "E1"].Merge();
            xlSheet.Cells[1, 1] = "PHIẾU ĐẶT HÀNG";
            xlSheet.Range["A1"].Font.Size = 18;
            xlSheet.Range["A1"].Font.Bold = true;
            xlSheet.Range["A1"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            // Thông tin chung
            xlSheet.Cells[3, 1] = "Số đơn hàng:";
            xlSheet.Cells[3, 2] = txtSoDDH.Text;
            xlSheet.Cells[4, 1] = "Ngày mua:";
            xlSheet.Cells[4, 2] = dtpNgaynhap.Text;
            xlSheet.Cells[5, 1] = "Nhân viên:";
            xlSheet.Cells[5, 2] = txtTenNV.Text;
            xlSheet.Cells[6, 1] = "Khách hàng:";
            xlSheet.Cells[6, 2] = txtTenKH.Text;
            xlSheet.Cells[7, 1] = "Điện thoại:";
            xlSheet.Cells[7, 2] = txtSoDT.Text;
            xlSheet.Cells[8, 1] = "Địa chỉ:";
            xlSheet.Cells[8, 2] = txtDiachi.Text;

            // Dòng tiêu đề sản phẩm
            int startRow = 10;
            xlSheet.Cells[startRow, 1] = "STT";
            xlSheet.Cells[startRow, 2] = "Tên hàng";
            xlSheet.Cells[startRow, 3] = "Đơn giá";
            xlSheet.Cells[startRow, 4] = "Số lượng";
            xlSheet.Cells[startRow, 5] = "Thành tiền";

            Excel.Range headerRange = xlSheet.Range["A" + startRow, "E" + startRow];
            headerRange.Font.Bold = true;
            headerRange.Interior.Color = Color.LightGray;
            headerRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

            // Đổ dữ liệu sản phẩm
            for (int i = 0; i < dgvDanhsachsanpham.Rows.Count; i++)
            {
                DataGridViewRow row = dgvDanhsachsanpham.Rows[i];
                xlSheet.Cells[i + startRow + 1, 1] = i + 1;
                xlSheet.Cells[i + startRow + 1, 2] = row.Cells["TenHang"].Value;
                xlSheet.Cells[i + startRow + 1, 3] = row.Cells["DonGiaBan"].Value;
                xlSheet.Cells[i + startRow + 1, 4] = row.Cells["SoLuong"].Value;
                xlSheet.Cells[i + startRow + 1, 5] = row.Cells["ThanhTien"].Value;
            }

            // Tổng kết
            int endRow = startRow + dgvDanhsachsanpham.Rows.Count + 1;
            xlSheet.Cells[endRow, 4] = "Tổng SL:";
            xlSheet.Cells[endRow, 5] = lblTongsoluongsanpham.Text.Replace("Tổng số lượng sản phẩm: ", "");

            xlSheet.Cells[endRow + 1, 4] = "Tổng tiền:";
            xlSheet.Cells[endRow + 1, 5] = lblTOngtien.Text.Replace("Tổng tiền: ", "");

            xlSheet.Range["A" + (endRow + 3), "E" + (endRow + 3)].Merge();
            xlSheet.Cells[endRow + 3, 1] = "Bằng chữ: " + lblTongtienbangchu.Text.Replace("Tổng tiền bằng chữ: ", "");
            xlSheet.Range["A" + (endRow + 3)].Font.Italic = true;

            // Kẻ bảng
            Excel.Range fullTable = xlSheet.Range["A" + startRow, "E" + (endRow + 1)];
            fullTable.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            fullTable.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            // Autofit
            xlSheet.Columns.AutoFit();

            // Hiện Excel
            xlApp.Visible = true;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đóng form không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
