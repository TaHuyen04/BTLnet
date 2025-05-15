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
    public partial class frmSanpham : Form
    {
        public frmSanpham()
        {
            InitializeComponent();
        }

        private void frmSanpham_Load(object sender, EventArgs e)
        {
            txtMaSP.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            Load_DataGridView();
            Functions.FillCombo("SELECT MaMau, TenMau FROM tblMauSac", cboMauSac, "MaMau", "TenMau");
            Functions.FillCombo("SELECT MaLoai, TenLoai FROM tblTheLoai", cboLoai, "MaLoai", "TenLoai");
            Functions.FillCombo("SELECT MaHangSX, TenHangSX FROM tblHangSX", cboNhaSX, "MaHangSX", "TenHangSX");
            Functions.FillCombo("SELECT MaPhanh, TenPhanh FROM tblPhanhXe", cboPhanh, "MaPhanh", "TenPhanh");
            Functions.FillCombo("SELECT MaDongCo, TenDongCo FROM tblDongCo", cboDongCo, "MaDongCo", "TenDongCo");
            Functions.FillCombo("SELECT MaTinhTrang, TenTinhTrang FROM tblTinhTrang", cboTinhTrang, "MaTinhTrang", "TenTinhTrang");

            ResetValues();
        }

        DataTable tblHang;

        private void Load_DataGridView()
        {
            string sql = "SELECT * FROM tblDMHang";
            tblHang = Functions.getdatatotable(sql);
            dgvSanPham.DataSource = tblHang;

            dgvSanPham.Columns[0].HeaderText = "Mã xe";
            dgvSanPham.Columns[1].HeaderText = "Tên xe";
            dgvSanPham.Columns[2].HeaderText = "Loại";
            dgvSanPham.Columns[3].HeaderText = "Hãng SX";
            dgvSanPham.Columns[4].HeaderText = "Màu";
            dgvSanPham.Columns[5].HeaderText = "Phanh";
            dgvSanPham.Columns[6].HeaderText = "Động cơ";
            dgvSanPham.Columns[7].HeaderText = "Nước SX";
            dgvSanPham.Columns[8].HeaderText = "Tình trạng";
            dgvSanPham.Columns[9].HeaderText = "Số lượng";
            dgvSanPham.Columns[10].HeaderText = "Đơn giá nhập";
            dgvSanPham.Columns[11].HeaderText = "Đơn giá bán";
            dgvSanPham.Columns[12].HeaderText = "Bảo hành";
            dgvSanPham.Columns[13].HeaderText = "Ảnh";

            dgvSanPham.AllowUserToAddRows = false;
            dgvSanPham.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ResetValues()
        {
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            txtSoLuong.Text = "0";
            txtDonGiaNhap.Text = "0";
            txtDonGiaBan.Text = "0";
            txtAnh.Text = "";
            picAnh.Image = null;
            cboLoai.SelectedIndex = -1;
            cboMauSac.SelectedIndex = -1;
            cboPhanh.SelectedIndex = -1;
            cboDongCo.SelectedIndex = -1;
            cboTinhTrang.SelectedIndex = -1;
            cboNhaSX.SelectedIndex = -1;
        }

        private void btnTham_Click(object sender, EventArgs e)
        {
            ResetValues();
            txtMaSP.Enabled = true;
            btnLuu.Enabled = true;
            btnBoqua.Enabled = true;
        }
    }
}
