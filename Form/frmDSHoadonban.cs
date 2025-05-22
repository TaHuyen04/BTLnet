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
using Excel = Microsoft.Office.Interop.Excel;

namespace QLCHBanXeMay.form
{
    public partial class frmDSHoadonban : Form
    {
        public frmDSHoadonban()
        {
            InitializeComponent();
        }
        private void frmDSHoadonban_Load(object sender, EventArgs e)
        {
            LoadDanhSachHoaDon();
            dgvDanhsachhoadonban.CellDoubleClick += dgvDanhsachhoadonban_CellDoubleClick;
        }
        private void LoadDanhSachHoaDon()
        {
            string sql = "SELECT SoDDH, MaNV, MaKhach, NgayMua, DatCoc, Thue, TongTien FROM tblDonDatHang";
            DataTable dt = Class.Functions.getdatatotable(sql);
            dgvDanhsachhoadonban.DataSource = dt;

            dgvDanhsachhoadonban.AllowUserToAddRows = false;
            dgvDanhsachhoadonban.EditMode = DataGridViewEditMode.EditProgrammatically;

            dgvDanhsachhoadonban.Columns[0].HeaderText = "Số Đơn Đặt Hàng";
            dgvDanhsachhoadonban.Columns[1].HeaderText = "Mã Nhân Viên";
            dgvDanhsachhoadonban.Columns[2].HeaderText = "Mã Khách";
            dgvDanhsachhoadonban.Columns[3].HeaderText = "Ngày Mua";
            dgvDanhsachhoadonban.Columns[4].HeaderText = "Đặt Cọc";
            dgvDanhsachhoadonban.Columns[5].HeaderText = "Thuế";
            dgvDanhsachhoadonban.Columns[6].HeaderText = "Tổng Tiền";
        }


        private void dgvDanhsachhoadonban_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDanhsachhoadonban_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string soDDH = dgvDanhsachhoadonban.Rows[e.RowIndex].Cells["SoDDH"].Value.ToString();

                // Mở form chi tiết hóa đơn, truyền soDDH
                frmChitietHDban chitietForm = new frmChitietHDban(soDDH);
                chitietForm.ShowDialog();

                // Có thể load lại danh sách nếu cần
                LoadDanhSachHoaDon();
            }
        }

        private void txtSoHDN_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTongtientu_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTongtienden_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpThoigiantu_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpThoigianden_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM tblDonDatHang WHERE 1=1";

                // Số hóa đơn
                if (!string.IsNullOrWhiteSpace(txtSoHDN.Text))
                {
                    sql += $" AND SoDDH LIKE N'%{txtSoHDN.Text.Trim()}%'";
                }

                // Mã nhân viên
                if (!string.IsNullOrWhiteSpace(txtMaNV.Text))
                {
                    sql += $" AND MaNV LIKE N'%{txtMaNV.Text.Trim()}%'";
                }

                // Mã nhà cung cấp
                if (!string.IsNullOrWhiteSpace(txtMaKH.Text))
                {
                    sql += $" AND MaNCC LIKE N'%{txtMaKH.Text.Trim()}%'";
                }

                // Tổng tiền từ
                if (!string.IsNullOrWhiteSpace(txtTongtientu.Text))
                {
                    if (decimal.TryParse(txtTongtientu.Text.Trim(), out decimal tongtientu))
                    {
                        sql += $" AND TongTien >= {tongtientu}";
                    }
                }

                // Tổng tiền đến
                if (!string.IsNullOrWhiteSpace(txtTongtienden.Text))
                {
                    if (decimal.TryParse(txtTongtienden.Text.Trim(), out decimal tongtienden))
                    {
                        sql += $" AND TongTien <= {tongtienden}";
                    }
                }

                // Thời gian
                if (dtpThoigiantu.Value <= dtpThoigianden.Value)
                {
                    sql += $" AND NgayBan >= '{dtpThoigiantu.Value:yyyy-MM-dd}' AND NgayBan <= '{dtpThoigianden.Value:yyyy-MM-dd}'";
                }

                DataTable dt = Functions.getdatatotable(sql);
                dgvDanhsachhoadonban.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            {

            }
        }

        private void btnTaohoadon_Click(object sender, EventArgs e)
        {
            frmTaodonban TaodonbanFrm = new frmTaodonban();
            TaodonbanFrm.StartPosition = FormStartPosition.CenterScreen; // Đặt giữa màn hình
            TaodonbanFrm.TopMost = true; // Đưa form này lên trước tất cả cửa sổ khác
            TaodonbanFrm.Show();
            // ẩn form hiện tại:
            //this.Hide();
            // đóng luôn form hiện tại:
            //this.Close();
        }

        private void btnXuatdanhsach_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachhoadonban.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Khởi tạo Excel
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                if (excelApp == null)
                {
                    MessageBox.Show("Không thể khởi tạo Excel. Hãy kiểm tra lại Office!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                excelApp.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.Sheets[1];
                worksheet.Name = "Danh sách hóa đơn";

                // Thêm tiêu đề lớn
                Microsoft.Office.Interop.Excel.Range titleRange = worksheet.get_Range("A1", "G1");
                titleRange.Merge();
                titleRange.Value2 = "DANH SÁCH HÓA ĐƠN BÁN XE MÁY";
                titleRange.Font.Size = 18;
                titleRange.Font.Bold = true;
                titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Thêm ngày giờ xuất
                worksheet.Cells[2, 1] = $"Ngày xuất: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";

                // Tạo tiêu đề cột
                string[] headers = { "Số Đơn Đặt Hàng", "Mã Nhân Viên", "Mã Khách", "Ngày Mua", "Đặt Cọc", "Thuế", "Tổng Tiền" };
                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[4, i + 1] = headers[i];
                    Microsoft.Office.Interop.Excel.Range headerCell = worksheet.Cells[4, i + 1];
                    headerCell.Font.Bold = true;
                    headerCell.Interior.Color = Color.LightGray;
                    headerCell.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    headerCell.ColumnWidth = 18;
                }

                // Đổ dữ liệu từ DataGridView
                for (int i = 0; i < dgvDanhsachhoadonban.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvDanhsachhoadonban.Columns.Count; j++)
                    {
                        var cell = worksheet.Cells[i + 5, j + 1];
                        cell.Value2 = dgvDanhsachhoadonban.Rows[i].Cells[j].Value?.ToString();
                        cell.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    }
                }

                // Định dạng ngày và tiền cho cột
                worksheet.Columns[4].NumberFormat = "dd/MM/yyyy"; // Ngày
                worksheet.Columns[5].NumberFormat = "#,##0 đ";    // Đặt cọc
                worksheet.Columns[6].NumberFormat = "0\\%";        // Thuế
                worksheet.Columns[7].NumberFormat = "#,##0 đ";    // Tổng tiền

                MessageBox.Show("Xuất danh sách hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
