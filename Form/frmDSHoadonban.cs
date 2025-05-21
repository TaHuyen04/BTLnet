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
    public partial class frmDSHoadonban : Form
    {
        public frmDSHoadonban()
        {
            InitializeComponent();
        }
        private void frmDSHoadonban_Load(object sender, EventArgs e)
        {
            LoadDanhSachHoaDon();
            dgvDanhsachhoadonban.CellDoubleClick += dgvDanhsachhoadonban_CellDoubleClick;
        }
        private void LoadDanhSachHoaDon()
        {
            string sql = "SELECT SoDDH, MaNV, MaKhach, NgayMua, DatCoc, Thue, TongTien FROM tblDonDatHang";
            DataTable dt = Class.Functions.getdatatotable(sql);
            dgvDanhsachhoadonban.DataSource = dt;

            dgvDanhsachhoadonban.AllowUserToAddRows = false;
            dgvDanhsachhoadonban.EditMode = DataGridViewEditMode.EditProgrammatically;

            dgvDanhsachhoadonban.Columns[0].HeaderText = "Số Đơn Đặt Hàng";
            dgvDanhsachhoadonban.Columns[1].HeaderText = "Mã Nhân Viên";
            dgvDanhsachhoadonban.Columns[2].HeaderText = "Mã Khách";
            dgvDanhsachhoadonban.Columns[3].HeaderText = "Ngày Mua";
            dgvDanhsachhoadonban.Columns[4].HeaderText = "Đặt Cọc";
            dgvDanhsachhoadonban.Columns[5].HeaderText = "Thuế";
            dgvDanhsachhoadonban.Columns[6].HeaderText = "Tổng Tiền";
        }


        private void dgvDanhsachhoadonban_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvDanhsachhoadonban_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string soDDH = dgvDanhsachhoadonban.Rows[e.RowIndex].Cells["SoDDH"].Value.ToString();

                // Mở form chi tiết hóa đơn, truyền soDDH
                frmChitietHDban chitietForm = new frmChitietHDban(soDDH);
                chitietForm.ShowDialog();

                // Có thể load lại danh sách nếu cần
                LoadDanhSachHoaDon();
            }
        }

        private void txtSoHDN_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTongtientu_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTongtienden_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpThoigiantu_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpThoigianden_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM tblDonDatHang WHERE 1=1";

                // Số hóa đơn
                if (!string.IsNullOrWhiteSpace(txtSoHDN.Text))
                {
                    sql += $" AND SoDDH LIKE N'%{txtSoHDN.Text.Trim()}%'";
                }

                // Mã nhân viên
                if (!string.IsNullOrWhiteSpace(txtMaNV.Text))
                {
                    sql += $" AND MaNV LIKE N'%{txtMaNV.Text.Trim()}%'";
                }

                // Mã nhà cung cấp
                if (!string.IsNullOrWhiteSpace(txtMaKH.Text))
                {
                    sql += $" AND MaNCC LIKE N'%{txtMaKH.Text.Trim()}%'";
                }

                // Tổng tiền từ
                if (!string.IsNullOrWhiteSpace(txtTongtientu.Text))
                {
                    if (decimal.TryParse(txtTongtientu.Text.Trim(), out decimal tongtientu))
                    {
                        sql += $" AND TongTien >= {tongtientu}";
                    }
                }

                // Tổng tiền đến
                if (!string.IsNullOrWhiteSpace(txtTongtienden.Text))
                {
                    if (decimal.TryParse(txtTongtienden.Text.Trim(), out decimal tongtienden))
                    {
                        sql += $" AND TongTien <= {tongtienden}";
                    }
                }

                // Thời gian
                if (dtpThoigiantu.Value <= dtpThoigianden.Value)
                {
                    sql += $" AND NgayBan >= '{dtpThoigiantu.Value:yyyy-MM-dd}' AND NgayBan <= '{dtpThoigianden.Value:yyyy-MM-dd}'";
                }

                DataTable dt = Functions.getdatatotable(sql);
                dgvDanhsachhoadonban.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            {
           
    }
    }
    } 
}
