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
    public partial class frmMausac : Form
    {
        public frmMausac()
        {
            InitializeComponent();
        }

        private void frmMausac_Load(object sender, EventArgs e)
        {
            txtMamau.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            Load_DataGridView();
        }
        DataTable tblMauSac;
        private void Load_DataGridView()
        {
            string sql;
            sql = "SELECT MaMau, TenMau FROM tblMauSac";
            tblMauSac = Class.Functions.getdatatotable(sql);
            dgvMausac.DataSource = tblMauSac;

            //do dl tu bang vao datagridview

            dgvMausac.Columns[0].HeaderText = "Mã màu";
            dgvMausac.Columns[1].HeaderText = "Tên màu";
            dgvMausac.Columns[0].Width = 100;
            dgvMausac.Columns[1].Width = 300;
            // Không cho phép thêm mới dữ liệu trực tiếp trên lưới
            dgvMausac.AllowUserToAddRows = false;
            // Không cho phép sửa dữ liệu trực tiếp trên lưới
            dgvMausac.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvMausac_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMamau.Focus();
                return;
            }
            if (tblMauSac.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            txtMamau.Text = dgvMausac.CurrentRow.Cells["MaMau"].Value.ToString();
            txtTenmau.Text = dgvMausac.CurrentRow.Cells["TenMau"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoqua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMamau.Enabled = true;
            txtMamau.Focus();
        }
        private void ResetValues()
        {
            txtMamau.Text = "";
            txtTenmau.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMamau.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMamau.Focus();
                return;
            }
            if (txtTenmau.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenmau.Focus();
                return;
            }
            sql = "SELECT MaMau FROM tblMauSac WHERE MaMau=N'" +
            txtMamau.Text.Trim() + "'";
            if (Class.Functions.Checkkey(sql))
            {
                MessageBox.Show("Mã chất liệu này đã có, bạn phải nhập mã khác", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMamau.Focus();
                txtMamau.Text = "";
                return;
            }
            sql = "INSERT INTO tblMauSac(MaMau,TenMau) VALUES(N'" +
            txtMamau.Text + "',N'" + txtTenmau.Text + "')";
            Class.Functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            txtMamau.Enabled = false;

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblMauSac.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            if (txtMamau.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenmau.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên chất liệu", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenmau.Focus();
                return;
            }
            sql = "UPDATE tblMauSac SET TenMau=N'" + txtTenmau.Text.ToString() +
            "' WHERE MaMau=N'" + txtMamau.Text + "'";
            Class.Functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnBoqua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblMauSac.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
            MessageBoxIcon.Information);
                return;
            }
            if (txtMamau.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblMauSac WHERE MaMau=N'" + txtMamau.Text + "'";
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
            txtMamau.Enabled = false;

        }

        private void txtMamau_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");

        }

        private void txtTenmau_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvMausac_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMamau.Focus();
                return;
            }
            if (tblMauSac.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            txtMamau.Text = dgvMausac.CurrentRow.Cells["MaMau"].Value.ToString();
            txtTenmau.Text = dgvMausac.CurrentRow.Cells["TenMau"].Value.ToString();
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
            txtMamau.Enabled = true;
            txtMamau.Focus();

        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            string sql;
            if (txtMamau.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMamau.Focus();
                return;
            }
            if (txtTenmau.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenmau.Focus();
                return;
            }
            sql = "SELECT MaMau FROM tblMauSac WHERE MaMau=N'" +
            txtMamau.Text.Trim() + "'";
            if (Class.Functions.Checkkey(sql))
            {
                MessageBox.Show("Mã chất liệu này đã có, bạn phải nhập mã khác", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMamau.Focus();
                txtMamau.Text = "";
                return;
            }
            sql = "INSERT INTO tblMauSac(MaMau,TenMau) VALUES(N'" +
            txtMamau.Text + "',N'" + txtTenmau.Text + "')";
            Class.Functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            txtMamau.Enabled = false;

        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            string sql;
            if (tblMauSac.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            if (txtMamau.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenmau.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên chất liệu", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenmau.Focus();
                return;
            }
            sql = "UPDATE tblMauSac SET TenMau=N'" + txtTenmau.Text.ToString() +
            "' WHERE MaMau=N'" + txtMamau.Text + "'";
            Class.Functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnBoqua.Enabled = false;

        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            string sql;
            if (tblMauSac.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
            MessageBoxIcon.Information);
                return;
            }
            if (txtMamau.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblMauSac WHERE MaMau=N'" + txtMamau.Text + "'";
                Class.Functions.Runsqldel(sql);
                Load_DataGridView();
                ResetValues();
            }

        }

        private void btnBoqua_Click_1(object sender, EventArgs e)
        {
            ResetValues();
            btnBoqua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMamau.Enabled = false;

        }

        private void txtMamau_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");

        }

        private void txtTenmau_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");

        }

        private void btnDong_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSua_Click_2(object sender, EventArgs e)
        {
            string sql;
            if (tblMauSac.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            if (txtMamau.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenmau.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên chất liệu", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenmau.Focus();
                return;
            }
            sql = "UPDATE tblMauSac SET TenMau=N'" + txtTenmau.Text.ToString() +
            "' WHERE MaMau=N'" + txtMamau.Text + "'";
            Class.Functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnBoqua.Enabled = false;
        }

        private void dgvMausac_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMamau.Focus();
                return;
            }
            if (tblMauSac.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            txtMamau.Text = dgvMausac.CurrentRow.Cells["MaMau"].Value.ToString();
            txtTenmau.Text = dgvMausac.CurrentRow.Cells["TenMau"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoqua.Enabled = true;
        }

        private void dgvMausac_Click_1(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMamau.Focus();
                return;
            }
            if (tblMauSac.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            txtMamau.Text = dgvMausac.CurrentRow.Cells["MaMau"].Value.ToString();
            txtTenmau.Text = dgvMausac.CurrentRow.Cells["TenMau"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoqua.Enabled = true;
        }

        private void btnThem_Click_2(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMamau.Enabled = true;
            txtMamau.Focus();
        }

        private void btnXoa_Click_2(object sender, EventArgs e)
        {
            string sql;
            if (tblMauSac.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
            MessageBoxIcon.Information);
                return;
            }
            if (txtMamau.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblMauSac WHERE MaMau=N'" + txtMamau.Text + "'";
                Class.Functions.Runsqldel(sql);
                Load_DataGridView();
                ResetValues();
            }
        }

        private void btnLuu_Click_2(object sender, EventArgs e)
        {
            string sql;
            if (txtMamau.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMamau.Focus();
                return;
            }
            if (txtTenmau.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenmau.Focus();
                return;
            }
            sql = "SELECT MaMau FROM tblMauSac WHERE MaMau=N'" +
            txtMamau.Text.Trim() + "'";
            if (Class.Functions.Checkkey(sql))
            {
                MessageBox.Show("Mã chất liệu này đã có, bạn phải nhập mã khác", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMamau.Focus();
                txtMamau.Text = "";
                return;
            }
            sql = "INSERT INTO tblMauSac(MaMau,TenMau) VALUES(N'" +
            txtMamau.Text + "',N'" + txtTenmau.Text + "')";
            Class.Functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            txtMamau.Enabled = false;
        }

        private void btnBoqua_Click_2(object sender, EventArgs e)
        {
            ResetValues();
            btnBoqua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMamau.Enabled = false;
        }

        private void btnDong_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMamau_KeyUp_2(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtTenmau_KeyUp_2(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }
    }
}
