using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLCHBanXeMay.Class;
using Excel = Microsoft.Office.Interop.Excel;

namespace QLCHBanXeMay.form
{
    
    public partial class frmChitietHDnhap : Form
    {
        // Declare the missing variable 'SoHDN'
        private DataTable DS = new DataTable();
        private string soHDN; 
        public frmChitietHDnhap(string soHDN)
        {
            InitializeComponent();
            this.soHDN = soHDN;
            dtpngaynhap.Format = DateTimePickerFormat.Custom;
            dtpngaynhap.CustomFormat = "dd/MM/yyyy";
        }

        private void frmChitietHDN_Load(object sender, EventArgs e)
        {
            Load_thongtinHDN();
            Load_DS();
            txtSoHDN.ReadOnly = true;
            txtTenNV.ReadOnly = true;
            txtTenNCC.ReadOnly = true;
            txtdiachi.ReadOnly = true;
            msksdt.ReadOnly = true;
            txtMaNCC.ReadOnly = true;
            txtMaNV.ReadOnly = true;
            dtpngaynhap.Enabled = false;

        }
        private void Load_thongtinHDN()
        {
            // 1. Lấy thông tin hóa đơn nhập
            string sqlInfo = $@"
            SELECT 
            hd.SoHDN,
            hd.MaNV, nv.TenNV,
            hd.MaNCC,hd.NgayNhap ,ncc.TenNCC, ncc.Dienthoai, ncc.Diachi 
            
            FROM tblHoadonnhap hd
            JOIN tblNhanvien nv ON hd.MaNV = nv.MaNV
            JOIN tblNhacungcap ncc ON hd.MaNCC = ncc.MaNCC
            WHERE hd.SoHDN = '{soHDN}'";

            DataTable dtInfo = Functions.getdatatotable(sqlInfo);
            if (dtInfo.Rows.Count > 0)
            {
                DataRow row = dtInfo.Rows[0];
                txtSoHDN.Text = row["SoHDN"].ToString();
                txtMaNV.Text = row["MaNV"].ToString();
                txtTenNV.Text = row["TenNV"].ToString();
                txtMaNCC.Text = row["MaNCC"].ToString();
                txtTenNCC.Text = row["TenNCC"].ToString();
                msksdt.Text = row["Dienthoai"].ToString();
                txtdiachi.Text = row["Diachi"].ToString();
                dtpngaynhap.Text = row["NgayNhap"].ToString();
            }
        }  
       
        private void Load_DS()
        {
            string sqlProducts = $@"
            SELECT 
            cthdn.MaHang, h.TenHang, tl.TenLoai,
            hsx.TenHangSX, ms.TenMau, px.TenPhanh,
            dc.TenDongCo, h.NamSX, h.DungTichBinhXang,
            nsx.TenNuocSX, tt.TenTinhTrang,
            h.DonGiaBan, cthdn.SoLuong, cthdn.GiamGia, cthdn.ThanhTien
            FROM tblChiTietHoaDonNhap cthdn
            JOIN tblDMHang h ON cthdn.MaHang = h.MaHang
            JOIN tblTheLoai tl ON h.MaLoai = tl.MaLoai
            JOIN tblHangSX hsx ON h.MaHangSX = hsx.MaHangSX
            JOIN tblMauSac ms ON h.MaMau = ms.MaMau
            JOIN tblPhanhXe px ON h.MaPhanh = px.MaPhanh
            JOIN tblDongCo dc ON h.MaDongCo = dc.MaDongCo
            JOIN tblNuocSX nsx ON h.MaNuocSX = nsx.MaNuocSX
            JOIN tblTinhTrang tt ON h.MaTinhTrang = tt.MaTinhTrang
            WHERE cthdn.SoHDN = '{soHDN}'";


            DataTable dtProducts = Functions.getdatatotable(sqlProducts);
            dgridDanhsachSP.DataSource = dtProducts;

            // ✅ Tính toán số liệu dựa trên DataGridView
            int sumSoluongSP = 0;
            int sumSP = dgridDanhsachSP.Rows.Count;
            decimal sumTongtien = 0;

            foreach (DataGridViewRow row in dgridDanhsachSP.Rows)
            {
                if (row.IsNewRow) continue; // bỏ qua dòng mới nếu có

                if (int.TryParse(row.Cells["SoLuong"].Value?.ToString(), out int soLuong))
                    sumSoluongSP += soLuong;

                if (decimal.TryParse(row.Cells["ThanhTien"].Value?.ToString(), out decimal thanhTien))
                    sumTongtien += thanhTien;
            }

            lblSoluongSP.Text = "Số sản phẩm: " + sumSP;
            lblTongtien.Text = "Tổng tiền: " + sumTongtien.ToString("N0") + " VNĐ";
            lblBangchu.Text = "Tổng tiền (bằng chữ): " + Functions.ChuyenSoSangChu(sumTongtien.ToString());
        

            // Đặt lại tiêu đề các cột
            if (dgridDanhsachSP.Columns.Count > 0)
            {
                dgridDanhsachSP.Columns["MaHang"].HeaderText = "Mã hàng";
                dgridDanhsachSP.Columns["TenHang"].HeaderText = "Tên hàng";
                dgridDanhsachSP.Columns["TenLoai"].HeaderText = "Loại";
                dgridDanhsachSP.Columns["TenHangSX"].HeaderText = "Hãng sản xuất";
                dgridDanhsachSP .Columns["TenMau"].HeaderText = "Màu";
                dgridDanhsachSP.Columns["TenPhanh"].HeaderText = "Phanh";
                dgridDanhsachSP .Columns["TenDongCo"].HeaderText = "Động cơ";
                dgridDanhsachSP.Columns["NamSX"].HeaderText = "Năm sản xuất";
                dgridDanhsachSP.Columns["DungTichBinhXang"].HeaderText = "Dung tích bình xăng";
                dgridDanhsachSP.Columns["TenNuocSX"].HeaderText = "Nước sản xuất";
                dgridDanhsachSP.Columns["TenTinhTrang"].HeaderText = "Tình trạng";
                dgridDanhsachSP.Columns["DonGiaBan"].HeaderText = "Đơn giá bán";
                dgridDanhsachSP.Columns["SoLuong"].HeaderText = "Số lượng";
                dgridDanhsachSP.Columns["GiamGia"].HeaderText = "Giảm giá (%)";
                dgridDanhsachSP.Columns["ThanhTien"].HeaderText = "Thành tiền";
            }

            
        }


        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dgridDanhsachSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

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
            xlSheet.Name = "Phieunhaphang";

            xlApp.StandardFontSize = 12;
            xlApp.StandardFont = "Times New Roman";

            // Tiêu đề phiếu
            xlSheet.Range["A1", "E1"].Merge();
            xlSheet.Cells[1, 1] = "PHIẾU NHẬP HÀNG";
            xlSheet.Range["A1"].Font.Size = 18;
            xlSheet.Range["A1"].Font.Bold = true;
            xlSheet.Range["A1"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            // Thông tin chung
            xlSheet.Cells[3, 1] = "Số đơn hàng:";
            xlSheet.Cells[3, 2] = txtSoHDN.Text;
            xlSheet.Cells[4, 1] = "Ngày mua:";
            xlSheet.Cells[4, 2] = dtpngaynhap.Text;
            xlSheet.Cells[5, 1] = "Nhân viên:";
            xlSheet.Cells[5, 2] = txtTenNV.Text;
            xlSheet.Cells[6, 1] = "Nhà cung cấp:";
            xlSheet.Cells[6, 2] = txtTenNCC.Text;
            xlSheet.Cells[7, 1] = "Điện thoại:";
            xlSheet.Cells[7, 2] = msksdt.Text;
            xlSheet.Cells[8, 1] = "Địa chỉ:";
            xlSheet.Cells[8, 2] = txtdiachi.Text;

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
            for (int i = 0; i < dgridDanhsachSP.Rows.Count; i++)
            {
                DataGridViewRow row = dgridDanhsachSP.Rows[i];
                xlSheet.Cells[i + startRow + 1, 1] = i + 1;
                xlSheet.Cells[i + startRow + 1, 2] = row.Cells["TenHang"].Value;
                xlSheet.Cells[i + startRow + 1, 3] = row.Cells["DonGiaBan"].Value;
                xlSheet.Cells[i + startRow + 1, 4] = row.Cells["SoLuong"].Value;
                xlSheet.Cells[i + startRow + 1, 5] = row.Cells["ThanhTien"].Value;
            }

            // Tổng kết
            int endRow = startRow + dgridDanhsachSP.Rows.Count + 1;
            xlSheet.Cells[endRow, 4] = "Tổng SL:";
            xlSheet.Cells[endRow, 5] = lblSoluongSP.Text.Replace("Tổng số lượng sản phẩm: ", "");

            xlSheet.Cells[endRow + 1, 4] = "Tổng tiền:";
            xlSheet.Cells[endRow + 1, 5] = lblSoluongSP.Text.Replace("Tổng tiền: ", "");

            xlSheet.Range["A" + (endRow + 3), "E" + (endRow + 3)].Merge();
            xlSheet.Cells[endRow + 3, 1] = "Bằng chữ: " + lblBangchu.Text.Replace("Tổng tiền bằng chữ: ", "");
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(soHDN))
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
                    string sqlDeleteCT = $"DELETE FROM tblChiTietDonDatHang WHERE SoDDH = '{soHDN}'";
                    Functions.Runsql(sqlDeleteCT);

                    // Xóa đơn đặt hàng
                    string sqlDeleteHDN = $"DELETE FROM tblDonDatHang WHERE SoDDH = '{soHDN}'";
                    Functions.Runsql(sqlDeleteHDN);  

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

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đóng form không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
    
}
