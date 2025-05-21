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
    public partial class frmKhachhang : Form
    {
        DataTable tblKhachHang;
        bool isAdding = false;
        public frmKhachhang()
        {
            InitializeComponent();
        }

        private Dictionary<string, string> initialValues = new Dictionary<string, string>();
        private void frmKhachhang_Load(object sender, EventArgs e)
        {
            DgridKhach.CellClick += DgridKhach_CellClick;
            txtMaKH.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            Load_DgridKhach();
            
            ResetValues();
        }

        private void Load_DgridKhach()
        {
            string sql = "SELECT * FROM tblKhachHang";


            DgridKhach.AllowUserToAddRows = false;
            DgridKhach.EditMode = DataGridViewEditMode.EditProgrammatically;
            tblKhachHang = Functions.getdatatotable(sql); 
            DgridKhach.DataSource = tblKhachHang;
            DgridKhach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (!string.IsNullOrEmpty(txtMaKH.Text) && tblKhachHang.Rows.Count > 0)
            {
                foreach (DataRow row in tblKhachHang.Rows)
                {
                    if (row["MaKhach"].ToString() == txtMaKH.Text)
                    {
                        int rowIndex = tblKhachHang.Rows.IndexOf(row);
                        DgridKhach.ClearSelection();
                        DgridKhach.Rows[rowIndex].Selected = true;
                        DgridKhach.CurrentCell = DgridKhach.Rows[rowIndex].Cells[0];
                        break;
                    }
                }
            }
            DgridKhach.Columns[0].HeaderText = "Mã Khách";
            DgridKhach.Columns[1].HeaderText = "Tên Khách";
            DgridKhach.Columns[2].HeaderText = "Địa Chỉ";
            DgridKhach.Columns[3].HeaderText = "Điện Thoại";
        }
        private void ResetValues()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            mskDienThoai.Text = "";
            initialValues.Clear();
        }
        private void SaveInitialValues()
        {
            initialValues.Clear();
            initialValues["txtMaKH"] = txtMaKH.Text;
            initialValues["txtTenKH"] = txtTenKH.Text;
            initialValues["txtDiaChi"] = txtDiaChi.Text;
            initialValues["mskDienThoai"] = mskDienThoai.Text;

        }
        private bool HasChanges()
        {
            return txtMaKH.Text != initialValues["txtMaKH"] ||
                     txtTenKH.Text != initialValues["txtTenKH"] ||
                     txtDiaChi.Text != initialValues["txtDiaChi"] ||
                     mskDienThoai.Text != initialValues["mskDienThoai"];
        }

        private void dgvKhachHang_Click(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void DgridKhach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
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
            txtMaKH.Enabled = true;
            btnLuu.Enabled = true;
            btnBoqua.Enabled = true;
            txtMaKH.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaKH.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo");
                    return;
                }
                string sql = "DELETE tblKhachHang WHERE MaKhach='" + txtMaKH.Text + "'";
                Functions.Runsqldel(sql);
                Load_DgridKhach();
                ResetValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (tblKhachHang.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để sửa!", "Thông báo");
                return;
            }

            if (txtMaKH.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm cần sửa!", "Thông báo");
                return;
            }

            // Lưu trạng thái ban đầu của các trường
            SaveInitialValues();

            // Bật các nút điều khiển
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnBoqua.Enabled = true;
            btnSua.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (txtMaKH.Text.Trim() == "")
                {
                    MessageBox.Show("Bạn phải nhập MÃ KHÁCH", "Thông báo");
                    txtMaKH.Focus();
                    return;
                }
                if (txtTenKH.Text.Trim() == "")
                {
                    MessageBox.Show("Bạn phải nhập TÊN KHÁCH", "Thông báo");
                    txtTenKH.Focus();
                    return;
                }
                if (mskDienThoai.Text.Trim().Length < 10)
                {
                    MessageBox.Show("Số điện thoại không hợp lệ!", "Thông báo");
                    mskDienThoai.Focus();
                    return;
                }

                string sqlCheck = "SELECT MaKhach FROM tblKhachHang WHERE MaKHach = '" + txtMaKH.Text.Trim() + "'";
                if (isAdding) // nếu đang ở chế độ thêm
                {
                    if (Functions.Checkkey(sqlCheck))
                    {
                        MessageBox.Show("Mã khách này đã tồn tại, hãy nhập mã khác!", "Thông báo");
                        txtMaKH.Focus();
                        return;
                    }

                    string sqlInsert = "INSERT INTO tblKhachHang(Makhach, TenKhach, Dienthoai, Diachi) " +
                                       "VALUES('" + txtMaKH.Text + "', N'" + txtTenKH.Text + "', '" +
                                       mskDienThoai.Text + "', N'" + txtDiaChi.Text + "')";
                    Functions.Runsql(sqlInsert);
                }
                else // chế độ sửa
                {
                    string sqlUpdate = "UPDATE tblKhachhang SET " +
                                       "TenKHach = N'" + txtTenKH.Text + "', " +
                                       "Dienthoai = '" + mskDienThoai.Text + "', " +
                                       "Diachi = N'" + txtDiaChi.Text + "' " +
                                       "WHERE MaKhach = '" + txtMaKH.Text + "'";
                    Functions.Runsql(sqlUpdate);
                }

                Load_DgridKhach();
                ResetValues();
                btnBoqua.Enabled = false;
                btnLuu.Enabled = false;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                txtMaKH.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM tblKhachHang WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(txtMaKH.Text))
                {
                    sql += " AND MaKhach LIKE '%" + txtMaKH.Text.Trim() + "%'";
                }

                if (!string.IsNullOrWhiteSpace(txtTenKH.Text))
                {
                    sql += " AND TenKhach LIKE N'%" + txtTenKH.Text.Trim() + "%'";
                }

                if (!string.IsNullOrWhiteSpace(txtDiaChi.Text))
                {
                    sql += " AND DiaChi LIKE N'%" + txtDiaChi.Text.Trim() + "%'";
                }

                string soDienThoai = mskDienThoai.Text.Replace(" ", "").Trim();
                if (!string.IsNullOrWhiteSpace(soDienThoai))
                {
                    sql += " AND DienThoai LIKE '%" + soDienThoai + "%'";
                }

                tblKhachHang = Functions.getdatatotable(sql);
                DgridKhach.DataSource = tblKhachHang;

                if (tblKhachHang.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy khách hàng nào phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHienthi_Click(object sender, EventArgs e)
        {
            Load_DgridKhach();
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
        private void DgridKhach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DgridKhach.Rows[e.RowIndex];

                // Gán giá trị từ DataGridView vào các TextBox
                txtMaKH.Text = row.Cells["MaKhach"].Value?.ToString() ?? "";
                txtTenKH.Text = row.Cells["TenKhach"].Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? "";
                mskDienThoai.Text = row.Cells["DienThoai"].Value?.ToString() ?? "";

                // Vô hiệu hóa việc sửa mã khách
                txtMaKH.Enabled = false;

                // Bật các nút thao tác
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnBoqua.Enabled = true;
            }
        }

    }
}
