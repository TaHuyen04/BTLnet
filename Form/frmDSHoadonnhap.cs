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
    public partial class frmDSHoadonnhap : Form
    {
        public frmDSHoadonnhap()
        {
            InitializeComponent();
        }

        private void frmHoadonnhap_Load(object sender, EventArgs e)
        {
            txtSoHDN.Enabled = false;
            Load_dgridDanhsachHDN();
        }
        private void Load_dgridDanhsachHDN()
        {

            string sql;
            sql = "SELECT SoHDN,MaNV, NgayNhap, MaNCC, TongTien FROM tblHoaDonNhap";
            DataTable tblHoadonnhap = Class.Functions.getdatatotable(sql);
            dgridDanhsachHDN.DataSource = tblHoadonnhap;
            //do dl tu bang vao datagridview
            dgridDanhsachHDN.Columns[0].HeaderText = "Số hóa đơn nhập";
            dgridDanhsachHDN.Columns[1].HeaderText = "Mã nhân viên";
            dgridDanhsachHDN.Columns[2].HeaderText = "Ngày nhập";
            dgridDanhsachHDN.Columns[3].HeaderText = "Mã nhà cung cấp";
            dgridDanhsachHDN.Columns[4].HeaderText = "Tổng tiền";
            dgridDanhsachHDN.Columns[0].Width = 150;
            dgridDanhsachHDN.Columns[1].Width = 120;
            dgridDanhsachHDN.Columns[2].Width = 120;
            dgridDanhsachHDN.Columns[3].Width = 120;
            dgridDanhsachHDN.Columns[4].Width = 120;
        }

        private void dgridDanhsachHDN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            
            string sql;
            if ((txtkhoangbd.Text != "") && (txtkhoangkt.Text == ""))
            { MessageBox.Show("Hãy nhập khoảng tiến tối đa nuốn tìm kiến", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtkhoangkt.Focus();
                return;
            }
            if ((txtkhoangbd.Text == "") && (txtkhoangkt.Text != ""))
            { MessageBox.Show("Hãy nhập khoảng tiến tối thiểu muốn tin kiến", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtkhoangbd.Focus();
                return;
            }
            if ((txtkhoangbd.Text != "") && (txtkhoangkt.Text != "") && Convert.ToInt32(txtkhoangbd.Text) > Convert.ToInt32(txtkhoangkt.Text))
            { MessageBox.Show("khoảng tiến tối thiểu phải không được lớn hơn khoảng tiền tối đa muốn tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtkhoangbd.Focus();
                return;
            }
            if ((mskngaybd.Text != "  / /") && (mskngaykt.Text == "  / /"))
            { MessageBox.Show("Hãy nhập khoảng thời gian kết thúc muốn tin kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); mskngaykt.Focus();
                return;
            }
            if ((mskngaybd.Text == "  / / ") && (mskngaykt.Text != "  / / "))
            { MessageBox.Show("Hãy nhập khoảng thời gian bắt đầu muốn tìn kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); mskngaybd.Focus();
                return;
            }
            if (mskngaybd.Text != " / / " && !Functions.IsDate1(mskngaybd.Text))
            { MessageBox.Show("Thời gian bắt đầu không đúng, hãy nhập lại!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); mskngaybd.Focus();
                return;
            }
            if (mskngaykt.Text != "  / / " && !Functions.IsDate1(mskngaykt.Text))
            { MessageBox.Show("Thời gian kết thúc không đúng, hãy nhập lại!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); mskngaykt.Focus();
                return;
            }
                if ((txtSoHDN.Text == "") && (txtMaNV.Text == "") && (txtMaNCC.Text == "") && (txtkhoangbd.Text == "") && (txtkhoangkt.Text == "") &&
    (mskngaybd.Text == "  /  /") && (mskngaykt.Text == "  /  /"))
                {
                    MessageBox.Show("Hãy nhập điều kiện tìm kiến!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            DateTime ngaybd = DateTime.MinValue;
            DateTime ngaykt = DateTime.MinValue;
            if (mskngaybd.Text != "  / / ") 
                    ngaybd = Convert.ToDateTime(mskngaybd.Text);
            if (mskngaykt.Text != " / / " )
                    ngaykt =  Convert.ToDateTime(mskngaykt.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
