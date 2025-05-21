using System;
using System.Data;
using System.Windows.Forms;
using QLCHBanXeMay.Class;

namespace QLCHBanXeMay.form
{
    public partial class dtpNgaysinh : Form
    {
        DataTable tblNV;

        public dtpNgaysinh()
        {
            InitializeComponent();
        }

        private void frmNhanvien_Load(object sender, EventArgs e)
        {
            
            LoadData();
            LoadCongviec();
            dgvNhanvien.CellClick += dgvNhanvien_CellClick;
        }

        private void LoadData()
        {
            string sql = "SELECT nv.MaNV, nv.TenNV, nv.GioiTinh, nv.NgaySinh, nv.DienThoai, nv.DiaChi, nv.MaCV, cv.TenCV " +
                  "FROM tblNhanvien nv JOIN tblCongviec cv ON nv.MaCV = cv.MaCV"; // ✅ sửa: thêm nv.MaCV
            tblNV = Functions.getdatatotable(sql);
            dgvNhanvien.DataSource = tblNV;
            dgvNhanvien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhanvien.ReadOnly = true;

            // Ẩn cột MaCV nếu không cần hiển thị
            if (dgvNhanvien.Columns.Contains("MaCV"))
                dgvNhanvien.Columns["MaCV"].Visible = false;
        }

        private void LoadCongviec()
        {
            string sql = "SELECT MaCV, TenCV FROM tblCongviec";
            DataTable dt = Functions.getdatatotable(sql);
            cbbCongviec.DataSource = dt;
            cbbCongviec.DisplayMember = "TenCV";
            cbbCongviec.ValueMember = "MaCV";
            cbbCongviec.SelectedIndex = -1;
        }


        private void ResetForm()
        {
            txtManhanvien.Enabled = false;
            txtTennhanvien.Text = "";
            cbbSex.Text = "";
            dtpNgaysinhh.Value = DateTime.Now;
            txtDienthoai.Text = "";
            txtDiachi.Text = "";
            cbbCongviec.Text = "";

            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;

            txtTennhanvien.Enabled = true;
            cbbSex.Enabled = true;
            dtpNgaysinhh.Enabled = true;
            txtDienthoai.Enabled = true;
            txtDiachi.Enabled = true;
            cbbCongviec.Enabled = true;

            SinhMaNhanVien();
        }
        private void SinhMaNhanVien()
        {
            int i = 1;
            while (true)
            {
                string ma = "NV" + i.ToString("D3");
                bool exists = false;
                foreach (DataRow row in tblNV.Rows)
                {
                    if (row["MaNV"].ToString() == ma)
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    txtManhanvien.Text = ma;
                    break;
                }
                i++;
            }
        }


        private void dgvNhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvNhanvien.Rows[e.RowIndex];
            txtManhanvien.Text = row.Cells["MaNV"].Value.ToString();
            txtTennhanvien.Text = row.Cells["TenNV"].Value.ToString();
            cbbSex.Text = row.Cells["GioiTinh"].Value.ToString();
            dtpNgaysinhh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
            txtDienthoai.Text = row.Cells["DienThoai"].Value.ToString();
            txtDiachi.Text = row.Cells["DiaChi"].Value.ToString();
            cbbCongviec.SelectedValue = row.Cells["MaCV"].Value;

            txtManhanvien.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoqua.Enabled = true;
        }
        private bool ValidateInputs()
        {
            if (txtTennhanvien.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập tên nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTennhanvien.Focus();
                return false;
            }
            if (cbbSex.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn giới tính.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbbSex.Focus();
                return false;
            }
            if (dtpNgaysinhh.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaysinhh.Focus();
                return false;
            }
            return true;
        }

        private void txtManhanvien_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTennhanvien_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbbSex_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtpNgaysinhh_ValueChanged(object sender, EventArgs e)
        {
            DateTime ngaysinh = dtpNgaysinhh.Value;
            if (ngaysinh > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không hợp lệ (lớn hơn ngày hiện tại).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaysinhh.Value = DateTime.Now;
            }
            string ngaysinhSQL = Functions.ConvertDateTime(ngaysinh.ToString("dd/MM/yyyy"));
            Console.WriteLine("Ngày sinh theo định dạng SQL: " + ngaysinhSQL);
        }

        private void txtDienthoai_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDiachi_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbbCongviec_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string checkSql = $"SELECT COUNT(*) FROM tblNhanvien WHERE TenNV = N'{txtTennhanvien.Text.Trim()}'";
            if (Convert.ToInt32(Functions.GetFieldValues(checkSql)) > 0)
            {
                MessageBox.Show("Tên nhân viên đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sqlInsert = "INSERT INTO tblNhanvien (MaNV, TenNV, GioiTinh, NgaySinh, DienThoai, DiaChi, MaCV) VALUES (" +
                $"N'{txtManhanvien.Text}', " +
                $"N'{txtTennhanvien.Text.Trim()}', " +
                $"N'{cbbSex.Text}', " +
                $"'{dtpNgaysinhh.Value:yyyy-MM-dd}', " +
                $"N'{txtDienthoai.Text.Trim()}', " +
                $"N'{txtDiachi.Text.Trim()}', " +
                $"N'{cbbCongviec.SelectedValue}')";

            Functions.Runsql(sqlInsert);
            LoadData();
            ResetForm();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtManhanvien.Text == "")
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult rs = MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                try
                {
                    string sqlDelete = $"DELETE FROM tblNhanvien WHERE MaNV = '{txtManhanvien.Text.Trim()}'";
                    Functions.Runsql(sqlDelete);  // Lưu ý: không phải Runsqldel
                    MessageBox.Show("Xóa nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ResetForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtManhanvien.Text == "")
            {
                MessageBox.Show("Vui lòng chọn nhân viên để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInputs()) return;

            string checkSql = $"SELECT COUNT(*) FROM tblNhanvien WHERE TenNV = N'{txtTennhanvien.Text.Trim()}' AND MaNV <> '{txtManhanvien.Text}'";
            if (Convert.ToInt32(Functions.GetFieldValues(checkSql)) > 0)
            {
                MessageBox.Show("Tên nhân viên đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sqlUpdate = "UPDATE tblNhanvien SET " +
                $"TenNV = N'{txtTennhanvien.Text.Trim()}', " +
                $"GioiTinh = N'{cbbSex.Text}', " +
                $"NgaySinh = '{dtpNgaysinhh.Value:yyyy-MM-dd}', " +
                $"DienThoai = N'{txtDienthoai.Text.Trim()}', " +
                $"DiaChi = N'{txtDiachi.Text.Trim()}', " +
                $"MaCV = N'{cbbCongviec.SelectedValue}' " +
                $"WHERE MaNV = '{txtManhanvien.Text}'";

            Functions.Runsql(sqlUpdate);
            MessageBox.Show("Cập nhật nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
            ResetForm();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}