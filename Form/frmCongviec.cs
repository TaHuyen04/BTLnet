using System;
using System.Data;
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
            ResetForm();
            LoadData();
            dgvCongviec.CellClick += dgvCongviec_CellClick;
        }

        private void LoadData()
        {
            string sql = "SELECT * FROM tblCongviec";
            DataTable dt = Functions.getdatatotable(sql);
            dgvCongviec.DataSource = dt;
            dgvCongviec.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCongviec.ReadOnly = true;
        }

        private void ResetForm()
        {
            txtMacongviec.Enabled = false;
            txtTencongviec.Text = "";
            txtLuongthang.Text = "";

            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;

            txtTencongviec.Enabled = true;
            txtLuongthang.Enabled = true;

            SinhMaCongViec();
        }

        private void SinhMaCongViec()
        {
            int i = 1;
            while (true)
            {
                string ma = "CV" + i.ToString("D3");
                bool exists = false;
                foreach (DataGridViewRow row in dgvCongviec.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells["MaCV"].Value?.ToString() == ma)
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    txtMacongviec.Text = ma;
                    break;
                }
                i++;
            }
        }

        private bool ValidateInputs()
        {
            if (txtTencongviec.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập tên công việc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTencongviec.Focus();
                return false;
            }

            if (txtLuongthang.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập lương tháng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLuongthang.Focus();
                return false;
            }

            if (!decimal.TryParse(txtLuongthang.Text.Trim(), out decimal luong) || luong <= 0)
            {
                MessageBox.Show("Lương tháng phải là số và lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLuongthang.Focus();
                return false;
            }

            return true;
        }

        private void dgvCongviec_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvCongviec.Rows[e.RowIndex];
            txtMacongviec.Text = row.Cells["MaCV"].Value.ToString();
            txtTencongviec.Text = row.Cells["TenCV"].Value.ToString();
            txtLuongthang.Text = row.Cells["Luongthang"].Value.ToString();

            txtTencongviec.Enabled = true;
            txtLuongthang.Enabled = true;
            txtMacongviec.Enabled = false;

            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoqua.Enabled = true;
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string checkSql = "SELECT COUNT(*) FROM tblCongviec WHERE TenCV = N'" + txtTencongviec.Text.Trim() + "'";
            if (Convert.ToInt32(Functions.GetFieldValues(checkSql)) > 0)
            {
                MessageBox.Show("Tên công việc đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sqlInsert = $"INSERT INTO tblCongviec (MaCV, TenCV, Luongthang) VALUES ('{txtMacongviec.Text}', N'{txtTencongviec.Text.Trim()}', '{txtLuongthang.Text.Trim()}')";
            Functions.Runsql(sqlInsert);
            LoadData();
            ResetForm();
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            if (dgvCongviec.Rows.Count == 0 || txtMacongviec.Text == "")
            {
                MessageBox.Show("Không có dữ liệu để xóa!", "Thông báo");
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa công việc này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string sqlDelete = $"DELETE FROM tblCongviec WHERE MaCV = '{txtMacongviec.Text}'";
                Functions.Runsqldel(sqlDelete);
                LoadData();
                ResetForm();
            }
        }

        private void btnBoqua_Click_1(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btnDong_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMacongviec.Text == "")
            {
                MessageBox.Show("Vui lòng chọn công việc để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;
            string checkSql = $"SELECT COUNT(*) FROM tblCongviec WHERE TenCV = N'{txtTencongviec.Text.Trim()}' AND MaCV <> '{txtMacongviec.Text}'";
            if (Convert.ToInt32(Functions.GetFieldValues(checkSql)) > 0)
            {
                MessageBox.Show("Tên công việc đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sqlUpdate = $"UPDATE tblCongviec SET TenCV = N'{txtTencongviec.Text.Trim()}', Luongthang = '{txtLuongthang.Text.Trim()}' WHERE MaCV = '{txtMacongviec.Text}'";
            Functions.Runsql(sqlUpdate);

            MessageBox.Show("Cập nhật công việc thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
            ResetForm();
        }
    }
}
