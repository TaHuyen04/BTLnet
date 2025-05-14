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
    public partial class frmNuocSX : Form
    {
        public frmNuocSX()
        {
            InitializeComponent();
        }

        private void frmNuocSX_Load(object sender, EventArgs e)
        {
            Load_DataGridView();
        }
            
        DataTable tblNuocSX;
        private void Load_DataGridView()
        {
            string sql;
            sql = "SELECT MaNuocSX, TenNuocSX FROM tblNuocSX";
            tblNuocSX = Class.Functions.getdatatotable(sql);
            dgvNuocsanxuat.DataSource = tblNuocSX;

            //do dl tu bang vao datagridview

            dgvNuocsanxuat.Columns[0].HeaderText = "Mã sản xuất";
            dgvNuocsanxuat.Columns[1].HeaderText = "Tên sản xuất";
            dgvNuocsanxuat.Columns[0].Width = 100;
            dgvNuocsanxuat.Columns[1].Width = 300;
            // Không cho phép thêm mới dữ liệu trực tiếp trên lưới
            dgvNuocsanxuat.AllowUserToAddRows = false;
            // Không cho phép sửa dữ liệu trực tiếp trên lưới
            dgvNuocsanxuat.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvNuocsanxuat_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtManuocsanxuat.Focus();
                return;
            }
            if (tblNuocSX.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            txtManuocsanxuat.Text = dgvNuocsanxuat.CurrentRow.Cells["MaNuocSX"].Value.ToString();
            txtTennuocsanxuat.Text = dgvNuocsanxuat.CurrentRow.Cells["TenNuocSX"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoqua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

        }
        private void ResetValues()
        {
            txtManuocsanxuat.Text = "";
            txtTennuocsanxuat.Text = "";
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

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtManuocsanxuat_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtTennuocsanxuat_KeyUp_1(object sender, KeyEventArgs e)
        {
            
        }

        private void dgvNuocsanxuat_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtManuocsanxuat.Enabled = true;
            txtManuocsanxuat.Focus();
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btnBoqua_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btnDong_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNuocSX_Load_1(object sender, EventArgs e)
        {
            txtManuocsanxuat.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            Load_DataGridView();
        }

        private void txtManuocsanxuat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtTennuocsanxuat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnXoa_Click_2(object sender, EventArgs e)
        {
            string sql;
            if (tblNuocSX.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
            MessageBoxIcon.Information);
                return;
            }
            if (txtManuocsanxuat.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblNuocSX WHERE MaNuocSX=N'" + txtManuocsanxuat.Text + "'";
                Class.Functions.Runsqldel(sql);
                Load_DataGridView();
                ResetValues();
            }
        }

        private void btnSua_Click_2(object sender, EventArgs e)
        {
            string sql;
            if (tblNuocSX.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            if (txtManuocsanxuat.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTennuocsanxuat.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTennuocsanxuat.Focus();
                return;
            }
            sql = "UPDATE tblNuocSX SET TenNuocSX=N'" + txtTennuocsanxuat.Text.ToString() +
            "' WHERE MaNuocSX=N'" + txtManuocsanxuat.Text + "'";
            Class.Functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnBoqua.Enabled = false;
        }

        private void btnLuu_Click_2(object sender, EventArgs e)
        {
            string sql;
            if (txtManuocsanxuat.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtManuocsanxuat.Focus();
                return;
            }
            if (txtTennuocsanxuat.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTennuocsanxuat.Focus();
                return;
            }
            sql = "SELECT MaNuocSX FROM tblNuocSX WHERE MaNuocSX=N'" +
            txtManuocsanxuat.Text.Trim() + "'";
            if (Class.Functions.Checkkey(sql))
            {
                MessageBox.Show("Mã này đã có, bạn phải nhập mã khác", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtManuocsanxuat.Focus();
                txtManuocsanxuat.Text = "";
                return;
            }
            sql = "INSERT INTO tblNuocSX(MaNuocSX,TenNuocSX) VALUES(N'" +
            txtManuocsanxuat.Text + "',N'" + txtTennuocsanxuat.Text + "')";
            Class.Functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            txtManuocsanxuat.Enabled = false;
        }

        private void btnBoqua_Click_2(object sender, EventArgs e)
        {
            ResetValues();
            btnBoqua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtManuocsanxuat.Enabled = false;
        }

        private void btnDong_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }


}
}









