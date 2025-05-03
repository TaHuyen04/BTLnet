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
    public partial class frmTinhtrang : Form
    {
        public frmTinhtrang()
        {
            InitializeComponent();
        }

        private void frmTinhtrang_Load(object sender, EventArgs e)
        {
            txtMatinhtrang.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            Load_DataGridView();
        }
        DataTable tblTinhTrang;
        private void Load_DataGridView()
        {
            string sql;
            sql = "SELECT MaTinhTrang, TenTinhTrang FROM tblTinhTrang";
            tblTinhTrang = Class.Functions.getdatatotable(sql);
            dgvTinhtrang.DataSource = tblTinhTrang;

            //do dl tu bang vao datagridview

            dgvTinhtrang.Columns[0].HeaderText = "Mã tình trạng";
            dgvTinhtrang.Columns[1].HeaderText = "Tên tình trạng";
            dgvTinhtrang.Columns[0].Width = 100;
            dgvTinhtrang.Columns[1].Width = 300;
            // Không cho phép thêm mới dữ liệu trực tiếp trên lưới
            dgvTinhtrang.AllowUserToAddRows = false;
            // Không cho phép sửa dữ liệu trực tiếp trên lưới
            dgvTinhtrang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvTinhtrang_Click(object sender, EventArgs e)
        {
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMatinhtrang.Enabled = true;
            txtMatinhtrang.Focus();
        }
        private void ResetValues()
        {
            txtMatinhtrang.Text = "";
            txtTentinhtrang.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMatinhtrang.Text == "")
            {
                MessageBox.Show("Bạn phải nhập Mã tình trạng", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatinhtrang.Focus();
                return;
            }
            if (txtTentinhtrang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTentinhtrang.Focus();
                return;
            }
            sql = "SELECT MaTinhTrang FROM tblTinhTrang WHERE MaTinhTrang=N'" +
            txtMatinhtrang.Text.Trim() + "'";
            if (Class.Functions.Checkkey(sql))
            {
                MessageBox.Show("Mã tình trạng này đã có, bạn phải nhập mã khác", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatinhtrang.Focus();
                txtMatinhtrang.Text = "";
                return;
            }
            sql = "INSERT INTO tblTinhTrang(MaTinhTrang,TenTinhTrang) VALUES(N'" +
            txtMatinhtrang.Text + "',N'" + txtTentinhtrang.Text + "')";
            Class.Functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            txtMatinhtrang.Enabled = false;

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblTinhTrang.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            if (txtMatinhtrang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTentinhtrang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTentinhtrang.Focus();
                return;
            }
            sql = "UPDATE tblTinhTrang SET TenTinhTrang=N'" + txtTentinhtrang.Text.ToString() +
            "' WHERE MaTinhTrang=N'" + txtMatinhtrang.Text + "'";
            Class.Functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnBoqua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblTinhTrang.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
            MessageBoxIcon.Information);
                return;
            }
            if (txtMatinhtrang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblTinhTrang WHERE MaTinhTrang=N'" + txtMatinhtrang.Text + "'";
                Class.Functions.Runsqldel(sql);
                Load_DataGridView();
                ResetValues();
            }
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoqua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMatinhtrang.Enabled = false;

        }

        private void txtMatinhtrang_KeyUp(object sender, KeyEventArgs e)
        {
            

        }

        private void txtTentinhtrang_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMatinhtrang_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtTentinhtrang_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void dgvTinhtrang_Click_1(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMatinhtrang.Focus();
                return;
            }
            if (tblTinhTrang.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            txtMatinhtrang.Text = dgvTinhtrang.CurrentRow.Cells["MaTinhTrang"].Value.ToString();
            txtTentinhtrang.Text = dgvTinhtrang.CurrentRow.Cells["TenTinhTrang"].Value.ToString();
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
            txtMatinhtrang.Enabled = true;
            txtMatinhtrang.Focus();
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            string sql;
            if (tblTinhTrang.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
            MessageBoxIcon.Information);
                return;
            }
            if (txtMatinhtrang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblTinhTrang WHERE MaTinhTrang=N'" + txtMatinhtrang.Text + "'";
                Class.Functions.Runsqldel(sql);
                Load_DataGridView();
                ResetValues();
            }

        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            string sql;
            if (tblTinhTrang.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            if (txtMatinhtrang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTentinhtrang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTentinhtrang.Focus();
                return;
            }
            sql = "UPDATE tblTinhTrang SET TenTinhTrang=N'" + txtTentinhtrang.Text.ToString() +
            "' WHERE MaTinhTrang=N'" + txtMatinhtrang.Text + "'";
            Class.Functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnBoqua.Enabled = false;
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            string sql;
            if (txtMatinhtrang.Text == "")
            {
                MessageBox.Show("Bạn phải nhập Mã tình trạng", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatinhtrang.Focus();
                return;
            }
            if (txtTentinhtrang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTentinhtrang.Focus();
                return;
            }
            sql = "SELECT MaTinhTrang FROM tblTinhTrang WHERE MaTinhTrang=N'" +
            txtMatinhtrang.Text.Trim() + "'";
            if (Class.Functions.Checkkey(sql))
            {
                MessageBox.Show("Mã tình trạng này đã có, bạn phải nhập mã khác", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatinhtrang.Focus();
                txtMatinhtrang.Text = "";
                return;
            }
            sql = "INSERT INTO tblTinhTrang(MaTinhTrang,TenTinhTrang) VALUES(N'" +
            txtMatinhtrang.Text + "',N'" + txtTentinhtrang.Text + "')";
            Class.Functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            txtMatinhtrang.Enabled = false;
        }

        private void btnBoqua_Click_1(object sender, EventArgs e)
        {
            ResetValues();
            btnBoqua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMatinhtrang.Enabled = false;
        }

        private void btnDong_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

