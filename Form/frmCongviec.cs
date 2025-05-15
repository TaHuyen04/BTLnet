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
    public partial class frmCongviec : Form
    {
        public frmCongviec()
        {
            InitializeComponent();
        }

        private void frmCongviec_Load(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = false;
            txtMacongviec.Enabled = false;

            string sql = "SELECT * FROM tblCongviec";
            DataTable dt = Functions.getdatatotable(sql);
            dgvCongviec.DataSource = dt;
            dgvCongviec.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCongviec.ReadOnly = true;

            // Gắn sự kiện CellClick
            dgvCongviec.CellClick += dgvCongviec_CellClick;

            btnLuu.Enabled = false;
            txtMacongviec.Enabled = false;
            SinhMaCongViec();

        }

        private void SinhMaCongViec()
        {
            int i = 1;
            while (true)
            {
                string ma = "CV" + i.ToString("D3");
                bool daTonTai = false;
                foreach (DataGridViewRow row in dgvCongviec.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells["MaCV"].Value != null && row.Cells["MaCV"].Value.ToString() == ma)
                    {
                        daTonTai = true;
                        break;
                    }
                }
                if (!daTonTai)
                {
                    txtMacongviec.Text = ma;
                    break;
                }
                i++;
            }
        }

        private void txtMacongviec_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTencongviec_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTencongviec.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập tên công việc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTencongviec.Focus();
                return;
            }

            if (txtLuongthang.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập lương tháng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLuongthang.Focus();
                return;
            }

            string sqlCheckTen = "SELECT COUNT(*) FROM tblCongviec WHERE TenCV = N'" + txtTencongviec.Text.Trim() + "'";
            int soLuongTrung = Convert.ToInt32(Functions.GetFieldValues(sqlCheckTen));
            if (soLuongTrung > 0)
            {
                MessageBox.Show("Tên công việc đã tồn tại. Vui lòng nhập tên khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTencongviec.Focus();
                return;
            }

            if (!decimal.TryParse(txtLuongthang.Text.Trim(), out decimal luong))
            {
                MessageBox.Show("Lương tháng phải là một số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLuongthang.Focus();
                return;
            }
            if (luong <= 0)
            {
                MessageBox.Show("Lương tháng phải lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLuongthang.Focus();
                return;
            }

            string sqlInsert = "INSERT INTO tblCongviec (MaCV, TenCV, Luongthang) VALUES ('"
                               + txtMacongviec.Text + "', N'"
                               + txtTencongviec.Text.Trim() + "', '"
                               + txtLuongthang.Text.Trim() + "')";
            Functions.Runsql(sqlInsert);

            dgvCongviec.DataSource = Functions.getdatatotable("SELECT * FROM tblCongviec");

            txtTencongviec.Text = "";
            txtLuongthang.Text = "";
            SinhMaCongViec();

            btnThem.Enabled = true;
            btnLuu.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMacongviec.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn công việc để sửa!", "Thông báo");
                return;
            }

            string sqlUpdate = "UPDATE tblCongviec SET TenCV = N'" + txtTencongviec.Text +
                               "', Luongthang = '" + txtLuongthang.Text +
                               "' WHERE MaCV = '" + txtMacongviec.Text + "'";
            Functions.Runsql(sqlUpdate);

            string sql = "SELECT * FROM tblCongviec";
            DataTable dt = Functions.getdatatotable(sql);
            dgvCongviec.DataSource = dt;

            MessageBox.Show("Đã cập nhật thành công!", "Thông báo");

            txtTencongviec.Text = "";
            txtLuongthang.Text = "";
            txtMacongviec.Text = "";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = true;
            btnLuu.Enabled = false; ;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvCongviec.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xóa!", "Thông báo");
                txtMacongviec.Focus();
                return;
            }
            {
                string macv = dgvCongviec.CurrentRow.Cells["MaCV"].Value.ToString();
                string sqlDelete = "DELETE FROM tblCongviec WHERE MaCV = '" + macv + "'";
                Functions.Runsqldel(sqlDelete);

                string sqlSelect = "SELECT * FROM tblCongviec";
                DataTable dt = Functions.getdatatotable(sqlSelect);
                dgvCongviec.DataSource = dt;
                dgvCongviec.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtTencongviec.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập tên công việc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTencongviec.Focus();
                return;
            }
            if (txtLuongthang.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập lương tháng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLuongthang.Focus();
                return;
            }

            string sqlInsert = "INSERT INTO tblCongviec (MaCV, TenCV, Luongthang) VALUES ('"
                               + txtMacongviec.Text + "', N'"
                               + txtTencongviec.Text + "', '"
                               + txtLuongthang.Text + "')";
            Functions.Runsql(sqlInsert);

            dgvCongviec.DataSource = Functions.getdatatotable("SELECT * FROM tblCongviec");

            txtTencongviec.Text = "";
            txtLuongthang.Text = "";
            SinhMaCongViec();

            btnThem.Enabled = true;
            btnLuu.Enabled = false;
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            txtMacongviec.Text = "";
            txtTencongviec.Text = "";
            txtLuongthang.Text = "";

            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = false;

            btnThem.Enabled = true;

            txtMacongviec.Enabled = false;
            txtTencongviec.Enabled = true;
            txtLuongthang.Enabled = true;

            SinhMaCongViec();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dgvCongviec_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCongviec.Rows[e.RowIndex];
                txtMacongviec.Text = row.Cells["MaCV"].Value.ToString();
                txtTencongviec.Text = row.Cells["TenCV"].Value.ToString();
                txtLuongthang.Text = row.Cells["Luongthang"].Value.ToString();

                btnThem.Enabled = false;
                btnXoa.Enabled = true;
                btnSua.Enabled = true;
                btnBoqua.Enabled = true;

                txtTencongviec.Enabled = true;
                txtLuongthang.Enabled = true;
                txtMacongviec.Enabled = false;
            }
        }
        private void dgvCongviec_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCongviec.Rows[e.RowIndex];
                txtMacongviec.Text = row.Cells["MaCV"].Value?.ToString();
                txtTencongviec.Text = row.Cells["TenCV"].Value?.ToString();
                txtLuongthang.Text = row.Cells["Luongthang"].Value?.ToString();

                btnThem.Enabled = false;
                btnXoa.Enabled = true;
                btnSua.Enabled = true;
                btnBoqua.Enabled = true;

                txtTencongviec.Enabled = true;
                txtLuongthang.Enabled = true;
                txtMacongviec.Enabled = false;
            }
        }

        private void txtLuongthang_TextChanged(object sender, EventArgs e)
        {

        }
    }
}