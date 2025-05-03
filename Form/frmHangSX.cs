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
    public partial class frmHangSX : Form
    {
        public frmHangSX()
        {
            InitializeComponent();
        }

        private void frmHangSX_Load(object sender, EventArgs e)
        {
            txtMahangsanxuat.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            Load_DataGridView();
        }
        DataTable tblHangSX;
        private void Load_DataGridView()
        {
            string sql;
            sql = "SELECT MaHangSX, TenHangSX FROM tblHangSX";
            tblHangSX = Class.Functions.getdatatotable(sql);
            dgvHangsanxuat.DataSource = tblHangSX;

            //do dl tu bang vao datagridview

            dgvHangsanxuat.Columns[0].HeaderText = "Mã sản xuất";
            dgvHangsanxuat.Columns[1].HeaderText = "Tên sản xuất";
            dgvHangsanxuat.Columns[0].Width = 100;
            dgvHangsanxuat.Columns[1].Width = 300;
            // Không cho phép thêm mới dữ liệu trực tiếp trên lưới
            dgvHangsanxuat.AllowUserToAddRows = false;
            // Không cho phép sửa dữ liệu trực tiếp trên lưới
            dgvHangsanxuat.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvHangsanxuat_Click(object sender, EventArgs e)
        {
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            
        }
        private void ResetValues()
        {
            txtMahangsanxuat.Text = "";
            txtTenhangsanxuat.Text = "";
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

        private void txtMahangsanxuat_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtTenhangsanxuat_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void dgvHangsanxuat_Click_1(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMahangsanxuat.Focus();
                return;
            }
            if (tblHangSX.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            txtMahangsanxuat.Text = dgvHangsanxuat.CurrentRow.Cells["MaHangSX"].Value.ToString();
            txtTenhangsanxuat.Text = dgvHangsanxuat.CurrentRow.Cells["TenHangSX"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoqua.Enabled = true;
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMahangsanxuat.Enabled = true;
            txtMahangsanxuat.Focus();
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            string sql;
            if (tblHangSX.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
            MessageBoxIcon.Information);
                return;
            }
            if (txtMahangsanxuat.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblHangSX WHERE MaHangSX=N'" + txtMahangsanxuat.Text + "'";
                Class.Functions.Runsqldel(sql);
                Load_DataGridView();
                ResetValues();
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            string sql;
            if (tblHangSX.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            if (txtMahangsanxuat.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenhangsanxuat.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenhangsanxuat.Focus();
                return;
            }
            sql = "UPDATE tblHangSX SET TenHangSX=N'" + txtTenhangsanxuat.Text.ToString() +
            "' WHERE MaHangSX=N'" + txtMahangsanxuat.Text + "'";
            Class.Functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnBoqua.Enabled = false;
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            string sql;
            if (txtMahangsanxuat.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMahangsanxuat.Focus();
                return;
            }
            if (txtTenhangsanxuat.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenhangsanxuat.Focus();
                return;
            }
            sql = "SELECT MaHangSX FROM tblHangSX WHERE MaHangSX=N'" +
            txtMahangsanxuat.Text.Trim() + "'";
            if (Class.Functions.Checkkey(sql))
            {
                MessageBox.Show("Mã này đã có, bạn phải nhập mã khác", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMahangsanxuat.Focus();
                txtMahangsanxuat.Text = "";
                return;
            }
            sql = "INSERT INTO tblHangSX(MaHangSX,TenHangSX) VALUES(N'" +
            txtMahangsanxuat.Text + "',N'" + txtTenhangsanxuat.Text + "')";
            Class.Functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            txtMahangsanxuat.Enabled = false;
        }

        private void btnBoqua_Click_1(object sender, EventArgs e)
        {
            ResetValues();
            btnBoqua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMahangsanxuat.Enabled = false;
        }

        private void btnDong_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}






