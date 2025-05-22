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

            //tính toán sản phẩm trong hóa đơn
            int sumSoluongSP = 0;
            int sumSP = DS.Rows.Count;
            decimal sumTongtien = 0;

            for (int i = 0; i < DS.Rows.Count; i++)
            {
                sumSoluongSP += Convert.ToInt32(DS.Rows[i]["Soluong"]);
                sumTongtien += Convert.ToDecimal(DS.Rows[i]["Thanhtien"]);
            }
            lblTongSP.Text = "Số lượng sản phẩm: " + sumSoluongSP;
            lblSoluongSP.Text = "Số sản phẩm: " + sumSP;
            lblTongtien.Text = "Tổng tiền: " + sumTongtien;
            lblBangchu.Text = "Tổng tiền (bằng chữ): " + Functions.ChuyenSoSangChu(sumTongtien.ToString());
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
    }
}
