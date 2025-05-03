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
    public partial class frmDongco : Form
    {
        public frmDongco()
        {
            InitializeComponent();
        }

        private void frmDongco_Load(object sender, EventArgs e)
        {
            txtMadongco.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            Load_DataGridView();
        }
        DataTable tblDongCo;
        private void Load_DataGridView()
        {
            string sql;
            sql = "SELECT MaDongCo, TenDongCo FROM tblDongCo";
            tblDongCo = Class.Functions.getdatatotable(sql);
            dgvDongco.DataSource = tblDongCo;

            //do dl tu bang vao datagridview

            dgvDongco.Columns[0].HeaderText = "Mã động cơ";
            dgvDongco.Columns[1].HeaderText = "Tên động cơ";
            dgvDongco.Columns[0].Width = 100;
            dgvDongco.Columns[1].Width = 300;
            // Không cho phép thêm mới dữ liệu trực tiếp trên lưới
            dgvDongco.AllowUserToAddRows = false;
            // Không cho phép sửa dữ liệu trực tiếp trên lưới
            dgvDongco.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvDongco_Click(object sender, EventArgs e)
        {
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            
        }
        private void ResetValues()
        {
            txtMadongco.Text = "";
            txtTendongco.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblDongCo.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
            MessageBoxIcon.Information);
                return;
            }
            if (txtMadongco.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblDongCo WHERE MaDongCo=N'" + txtMadongco.Text + "'";
                Class.Functions.Runsqldel(sql);
                Load_DataGridView();
                ResetValues();
            }
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            

        }

        private void txtMadongco_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");

        }

        private void txtTendongco_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dgvDongco_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMadongco.Focus();
                return;
            }
            if (tblDongCo.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            txtMadongco.Text = dgvDongco.CurrentRow.Cells["MaDongCo"].Value.ToString();
            txtTendongco.Text = dgvDongco.CurrentRow.Cells["TenDongCo"].Value.ToString();
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
            txtMadongco.Enabled = true;
            txtMadongco.Focus();

        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            string sql;
            if (txtMadongco.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMadongco.Focus();
                return;
            }
            if (txtTendongco.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTendongco.Focus();
                return;
            }
            sql = "SELECT MaDongCo FROM tblDongCo WHERE MaDongCo=N'" +
            txtMadongco.Text.Trim() + "'";
            if (Class.Functions.Checkkey(sql))
            {
                MessageBox.Show("Mã chất liệu này đã có, bạn phải nhập mã khác", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMadongco.Focus();
                txtMadongco.Text = "";
                return;
            }
            sql = "INSERT INTO tblDongCo(MaDongCo,TenDongCo) VALUES(N'" +
            txtMadongco.Text + "',N'" + txtTendongco.Text + "')";
            Class.Functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            txtMadongco.Enabled = false;

        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            

        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            

        }

        private void btnBoqua_Click_1(object sender, EventArgs e)
        {
            ResetValues();
            btnBoqua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMadongco.Enabled = false;

        }

        private void btnDong_Click_1(object sender, EventArgs e)
        {
            
        }

        private void txtMadongco_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");

        }

        private void txtTendongco_KeyUp_1(object sender, KeyEventArgs e)
        {
            

        }

        private void dgvDongco_Click_1(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMadongco.Focus();
                return;
            }
            if (tblDongCo.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            txtMadongco.Text = dgvDongco.CurrentRow.Cells["MaDongCo"].Value.ToString();
            txtTendongco.Text = dgvDongco.CurrentRow.Cells["TenDongCo"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoqua.Enabled = true;
        }

        private void txtMadongco_KeyUp_2(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtTendongco_KeyUp_2(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnThem_Click_2(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMadongco.Enabled = true;
            txtMadongco.Focus();
        }

        private void btnDong_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click_2(object sender, EventArgs e)
        {
            string sql;
            if (tblDongCo.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
            MessageBoxIcon.Information);
                return;
            }
            if (txtMadongco.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblDongCo WHERE MaDongCo=N'" + txtMadongco.Text + "'";
                Class.Functions.Runsqldel(sql);
                Load_DataGridView();
                ResetValues();
            }
        }

        private void btnSua_Click_2(object sender, EventArgs e)
        {
            string sql;
            if (tblDongCo.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            if (txtMadongco.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTendongco.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTendongco.Focus();
                return;
            }
            sql = "UPDATE tblDongCo SET TenDongCo=N'" + txtTendongco.Text.ToString() +
            "' WHERE MaDongCo=N'" + txtMadongco.Text + "'";
            Class.Functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnBoqua.Enabled = false;
        }

        private void btnBoqua_Click_2(object sender, EventArgs e)
        {
            ResetValues();
            btnBoqua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMadongco.Enabled = false;
        }

        private void btnLuu_Click_2(object sender, EventArgs e)
        {
            string sql;
            if (txtMadongco.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMadongco.Focus();
                return;
            }
            if (txtTendongco.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTendongco.Focus();
                return;
            }
            sql = "SELECT MaDongCo FROM tblDongCo WHERE MaDongCo=N'" +
            txtMadongco.Text.Trim() + "'";
            if (Class.Functions.Checkkey(sql))
            {
                MessageBox.Show("Mã chất liệu này đã có, bạn phải nhập mã khác", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMadongco.Focus();
                txtMadongco.Text = "";
                return;
            }
            sql = "INSERT INTO tblDongCo(MaDongCo,TenDongCo) VALUES(N'" +
            txtMadongco.Text + "',N'" + txtTendongco.Text + "')";
            Class.Functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            txtMadongco.Enabled = false;
        }
    }
}








