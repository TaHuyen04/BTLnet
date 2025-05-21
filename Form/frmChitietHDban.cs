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

        private void button1_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            // Ẩn tạm các nút không cần hiển thị trong bản in
            button2.Visible = false;       // Nút Xem trước in
            button1.Visible = false;       // Nút Xóa hóa đơn
            button3.Visible = false;       // Nút Đóng form

            // Bắt sự kiện in
            printDocument1.PrintPage += new PrintPageEventHandler(PrintDocument1_PrintPage);

            // Tạo hộp thoại xem trước in
            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = printDocument1;
            previewDialog.Width = 800;
            previewDialog.Height = 600;

            // Hiển thị hộp thoại xem trước in
            previewDialog.ShowDialog();

            // Hiện lại các nút sau khi xem trước in
            button2.Visible = true;
            button1.Visible = true;
            button3.Visible = true;
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

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đóng form không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
