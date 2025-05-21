using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public frmChitietHDban(string soDDH)
        {
            InitializeComponent();
            this.soDDH = soDDH;
        }
        private void frmChitietdondathang_Load(object sender, EventArgs e)
        {
            // 1. Lấy thông tin đơn hàng
            string sqlInfo = $@"
                SELECT 
                    ddh.SoDDH,
                    ddh.MaNV, nv.TenNV,
                    ddh.MaKhach, kh.TenKhach, kh.DienThoai, kh.DiaChi
                FROM tblDonDatHang ddh
                JOIN tblNhanVien nv ON ddh.MaNV = nv.MaNV
                JOIN tblKhachHang kh ON ddh.MaKhach = kh.MaKhach
                WHERE ddh.SoDDH = '{soDDH}'";

            DataTable dtInfo = Functions.getdatatotable(sqlInfo);
            if (dtInfo.Rows.Count > 0)
            {
                DataRow row = dtInfo.Rows[0];
                txtSoDDH.Text = row["SoDDH"].ToString();
                txtMaNV.Text = row["MaNV"].ToString();
                txtTenNV.Text = row["TenNV"].ToString();
                txtMaKH.Text = row["MaKhach"].ToString();
                txtTenKH.Text = row["TenKhach"].ToString();
                txtSoDT.Text = row["DienThoai"].ToString();
                txtDiachi.Text = row["DiaChi"].ToString();
            }

            // 2. Đẩy danh sách sản phẩm lên dgv
            string sqlProducts = $@"
                SELECT 
                    ctdh.MaHang, h.TenHang, tl.TenLoai,
                    hsx.TenHangSX, ms.TenMau, px.TenPhanh,
                    dc.TenDongCo, h.NamSX, h.DungTichBinhXang,
                    nsx.TenNuocSX, tt.TenTinhTrang,
                    h.DonGiaBan, ctdh.SoLuong, ctdh.GiamGia, ctdh.ThanhTien
                FROM tblChiTietDonDatHang ctdh
                JOIN tblDMHang h ON ctdh.MaHang = h.MaHang
                JOIN tblTheLoai tl ON h.MaLoai = tl.MaLoai
                JOIN tblHangSX hsx ON h.MaHangSX = hsx.MaHangSX
                JOIN tblMauSac ms ON h.MaMau = ms.MaMau
                JOIN tblPhanhXe px ON h.MaPhanh = px.MaPhanh
                JOIN tblDongCo dc ON h.MaDongCo = dc.MaDongCo
                JOIN tblNuocSX nsx ON h.MaNuocSX = nsx.MaNuocSX
                JOIN tblTinhTrang tt ON h.MaTinhTrang = tt.MaTinhTrang
                WHERE ctdh.SoDDH = '{soDDH}'";

            DataTable dtProducts = Functions.getdatatotable(sqlProducts);
            dgvDanhsachsanpham.DataSource = dtProducts;

            // Đặt lại tiêu đề các cột
            if (dgvDanhsachsanpham.Columns.Count > 0)
            {
                dgvDanhsachsanpham.Columns["MaHang"].HeaderText = "Mã hàng";
                dgvDanhsachsanpham.Columns["TenHang"].HeaderText = "Tên hàng";
                dgvDanhsachsanpham.Columns["TenLoai"].HeaderText = "Loại";
                dgvDanhsachsanpham.Columns["TenHangSX"].HeaderText = "Hãng sản xuất";
                dgvDanhsachsanpham.Columns["TenMau"].HeaderText = "Màu";
                dgvDanhsachsanpham.Columns["TenPhanh"].HeaderText = "Phanh";
                dgvDanhsachsanpham.Columns["TenDongCo"].HeaderText = "Động cơ";
                dgvDanhsachsanpham.Columns["NamSX"].HeaderText = "Năm sản xuất";
                dgvDanhsachsanpham.Columns["DungTichBinhXang"].HeaderText = "Dung tích bình xăng";
                dgvDanhsachsanpham.Columns["TenNuocSX"].HeaderText = "Nước sản xuất";
                dgvDanhsachsanpham.Columns["TenTinhTrang"].HeaderText = "Tình trạng";
                dgvDanhsachsanpham.Columns["DonGiaBan"].HeaderText = "Đơn giá bán";
                dgvDanhsachsanpham.Columns["SoLuong"].HeaderText = "Số lượng";
                dgvDanhsachsanpham.Columns["GiamGia"].HeaderText = "Giảm giá (%)";
                dgvDanhsachsanpham.Columns["ThanhTien"].HeaderText = "Thành tiền";
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
                  
                    Functions.Runsql(sqlDeleteCT);
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
    }
}
