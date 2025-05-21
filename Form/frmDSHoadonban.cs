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

        private void frmDondathang_Load(object sender, EventArgs e)
        {
            LoadDonDatHang();
            dgvDanhsachhoadonban.CellDoubleClick += dgvDanhsachhoadonban_CellDoubleClick;
        }
        private void LoadDonDatHang()
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
        private void dgvDanhsachhoadonban_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy mã đơn đặt hàng từ dòng đang được double click
                string soDDH = dgvDanhsachhoadonban.Rows[e.RowIndex].Cells["SoDDH"].Value.ToString();

                // Gọi form chi tiết và truyền mã đơn đặt hàng sang
                frmChitietHDban chitietForm = new frmChitietHDban(soDDH);
                chitietForm.ShowDialog(); // hoặc .Show() nếu bạn không muốn chặn form chính
            }
        }

        private void txtMaHDD_TextChanged(object sender, EventArgs e)
        {

        }

        private void MaNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void MaNCC_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpThoigiantu_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpThoigianden_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtTongtientu_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTongtienden_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
