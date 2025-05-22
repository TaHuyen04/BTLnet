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
    public partial class frmNCC : Form
    {
        DataTable tblNCC;
        bool isAdding = false;
        private Dictionary<string, string> initialValues = new Dictionary<string, string>();
        public frmNCC()
        {
            InitializeComponent();
        }

        private void frmNCC_Load(object sender, EventArgs e)
        {
            DgridNCC.CellClick += DgridNCC_CellClick;
            txtMaNCC.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            Load_DgridNCC();
            ResetValues();
        }
        private void Load_DgridNCC()
        {
            string sql = "SELECT * FROM tblNhaCungCap";

            DgridNCC.AllowUserToAddRows = false;
            DgridNCC.EditMode = DataGridViewEditMode.EditProgrammatically;
            tblNCC = Functions.getdatatotable(sql);
            DgridNCC.DataSource = tblNCC;
            DgridNCC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DgridNCC.Columns[0].HeaderText = "Mã NCC";
            DgridNCC.Columns[1].HeaderText = "Tên NCC";
            DgridNCC.Columns[2].HeaderText = "Địa Chỉ";
            DgridNCC.Columns[3].HeaderText = "Điện Thoại";
        }
        private void ResetValues()
        {
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDiaChi.Text = "";
            txtDiaChi.Text = "";
            mskDienThoai.Text = "";
        }
        private void SaveInitialValues()
        {
            initialValues.Clear();
            initialValues["MaNCC"] = txtMaNCC.Text;
            initialValues["TenNCC"] = txtTenNCC.Text;
            initialValues["DiaChi"] = txtDiaChi.Text;
            initialValues["DienThoai"] = mskDienThoai.Text;


        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;

            ResetValues();
            txtMaNCC.Enabled = true;
            txtMaNCC.Focus();
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaNCC.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo");
                return;
            }

            string sql = "DELETE tblNhaCungCap WHERE MaNCC='" + txtMaNCC.Text + "'";
            Functions.Runsqldel(sql);
            Load_DgridNCC();
            ResetValues();
        }



        private void btnSua_Click_(object sender, EventArgs e)
        {
            if (tblNCC.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để sửa!", "Thông báo");
                return;
            }

            if (txtMaNCC.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi cần sửa!", "Thông báo");
                return;
            }

            SaveInitialValues();

            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnBoqua.Enabled = true;
            btnSua.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaNCC.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập MÃ NCC", "Thông báo");
                txtMaNCC.Focus();
                return;
            }
            if (txtTenNCC.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập TÊN NCC", "Thông báo");
                txtTenNCC.Focus();
                return;
            }
            if (mskDienThoai.Text.Trim().Length < 10)
            {
                MessageBox.Show("Số điện thoại không hợp lệ!", "Thông báo");
                mskDienThoai.Focus();
                return;
            }

            if (isAdding)
            {
                string sqlCheck = "SELECT MaNCC FROM tblNhaCungCap WHERE MaNCC = '" + txtMaNCC.Text.Trim() + "'";
                if (Functions.Checkkey(sqlCheck))
                {
                    MessageBox.Show("Mã nhà cung cấp này đã tồn tại, hãy nhập mã khác!", "Thông báo");
                    txtMaNCC.Focus();
                    return;
                }

                string sqlInsert = "INSERT INTO tblNhaCungCap(MaNCC, TenNCC, Dienthoai, Diachi) " +
                                   "VALUES('" + txtMaNCC.Text + "', N'" + txtTenNCC.Text + "', '" +
                                   mskDienThoai.Text + "', N'" + txtDiaChi.Text + "')";
                Functions.Runsql(sqlInsert);
            }
            else
            {
                string sqlUpdate = "UPDATE tblNhaCungCap SET " +
                                   "TenNCC = N'" + txtTenNCC.Text + "', " +
                                   "Dienthoai = '" + mskDienThoai.Text + "', " +
                                   "Diachi = N'" + txtDiaChi.Text + "' " +
                                   "WHERE MaNCC = '" + txtMaNCC.Text + "'";
                Functions.Runsql(sqlUpdate);
            }

            Load_DgridNCC();
            ResetValues();
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            txtMaNCC.Enabled = false;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tblNhaCungCap WHERE 1=1";

            if (!string.IsNullOrWhiteSpace(txtMaNCC.Text))
                sql += " AND MaNCC LIKE '%" + txtMaNCC.Text.Trim() + "%'";
            if (!string.IsNullOrWhiteSpace(txtTenNCC.Text))
                sql += " AND TenNCC LIKE N'%" + txtTenNCC.Text.Trim() + "%'";
            if (!string.IsNullOrWhiteSpace(txtDiaChi.Text))
                sql += " AND DiaChi LIKE N'%" + txtDiaChi.Text.Trim() + "%'";

            string soDienThoai = mskDienThoai.Text.Replace(" ", "").Trim();
            if (!string.IsNullOrWhiteSpace(soDienThoai))
                sql += " AND DienThoai LIKE '%" + soDienThoai + "%'";

            tblNCC = Functions.getdatatotable(sql);
            DgridNCC.DataSource = tblNCC;

            if (tblNCC.Rows.Count == 0)
                MessageBox.Show("Không tìm thấy nhà cung cấp nào!", "Thông báo");
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            isAdding = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHienthi_Click(object sender, EventArgs e)
        {
            Load_DgridNCC();
        }
        private void DgridNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DgridNCC.Rows[e.RowIndex];

                txtMaNCC.Text = row.Cells["MaNCC"].Value?.ToString() ?? "";
                txtTenNCC.Text = row.Cells["TenNCC"].Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? "";
                mskDienThoai.Text = row.Cells["DienThoai"].Value?.ToString() ?? "";

                txtMaNCC.Enabled = false;

                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnBoqua.Enabled = true;
            }
        }
    }
}
