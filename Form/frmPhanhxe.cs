using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHBanXeMay.form
{
    public partial class frmPhanhxe : Form
    {
        public frmPhanhxe()
        {
            InitializeComponent();
        }

        private void frmPhanhxe_Load(object sender, EventArgs e)
        {
            txtMaphanh.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            Load_DataGridView();
        }
        DataTable tblPhanhXe;
        private void Load_DataGridView()
        {
            string sql;
            sql = "SELECT MaPhanh, TenPhanh FROM tblPhanhXe";
            tblPhanhXe = Class.Functions.getdatatotable(sql);
            dgvPhanhxe.DataSource = tblPhanhXe;

            //do dl tu bang vao datagridview

            dgvPhanhxe.Columns[0].HeaderText = "Mã phanh xe";
            dgvPhanhxe.Columns[1].HeaderText = "Tên phanh xe";
            dgvPhanhxe.Columns[0].Width = 100;
            dgvPhanhxe.Columns[1].Width = 300;
            // Không cho phép thêm mới dữ liệu trực tiếp trên lưới
            dgvPhanhxe.AllowUserToAddRows = false;
            // Không cho phép sửa dữ liệu trực tiếp trên lưới
            dgvPhanhxe.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvPhanhxe_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaphanh.Focus();
                return;
            }
            if (tblPhanhXe.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            txtMaphanh.Text = dgvPhanhxe.CurrentRow.Cells["MaPhanh"].Value.ToString();
            txtTenphanh.Text = dgvPhanhxe.CurrentRow.Cells["TenPhanh"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoqua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            
        }
        private void ResetValues()
        {
            txtMaphanh.Text = "";
            txtTenphanh.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            

        }

        private void txtMaphanh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");

        }

        private void txtTenphanh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaphanh.Enabled = true;
            txtMaphanh.Focus();
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            string sql;
            if (tblPhanhXe.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
            MessageBoxIcon.Information);
                return;
            }
            if (txtMaphanh.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblPhanhXe WHERE MaPhanh=N'" + txtMaphanh.Text + "'";
                Class.Functions.Runsqldel(sql);
                Load_DataGridView();
                ResetValues();
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            string sql;
            if (tblPhanhXe.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            if (txtMaphanh.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenphanh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên", "Thông báo",

                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenphanh.Focus();
                return;
            }
            sql = "UPDATE tblPhanhXe SET TenPhanh=N'" + txtTenphanh.Text.ToString() +
            "' WHERE MaPhanh=N'" + txtMaphanh.Text + "'";
            Class.Functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnBoqua.Enabled = false;
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            string sql;
            if (txtMaphanh.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaphanh.Focus();
                return;
            }
            if (txtTenphanh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenphanh.Focus();
                return;
            }
            sql = "SELECT MaPhanh FROM tblPhanhXe WHERE MaPhanh=N'" +
            txtMaphanh.Text.Trim() + "'";
            if (Class.Functions.Checkkey(sql))
            {
                MessageBox.Show("Mã này đã có, bạn phải nhập mã khác", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaphanh.Focus();
                txtMaphanh.Text = "";
                return;
            }
            sql = "INSERT INTO tblPhanhXe(MaPhanh,TenPhanh) VALUES(N'" +
            txtMaphanh.Text + "',N'" + txtTenphanh.Text + "')";
            Class.Functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaphanh.Enabled = false;
        }

        private void btnBoqua_Click_1(object sender, EventArgs e)
        {
            ResetValues();
            btnBoqua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaphanh.Enabled = false;
        }

        private void btnDong_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}




