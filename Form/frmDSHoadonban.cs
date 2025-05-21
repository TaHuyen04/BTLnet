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
            string sql = "SELECT * FROM tblDonDatHang";
            DataTable dtDDH = Functions.getdatatotable(sql);
            dgvDanhsachhoadonban.DataSource = dtDDH;
            dgvDanhsachhoadonban.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDanhsachhoadonban.ReadOnly = true;
        }

        private void dgvDanhsachhoadonban_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
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
