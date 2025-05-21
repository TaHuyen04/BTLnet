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
using System.Globalization;
using QLCHBanXeMay.QLCH_BanXeDataSet1TableAdapters;
using Excel = Microsoft.Office.Interop.Excel;


namespace QLCHBanXeMay.form
{
    public partial class frmDSHoadonnhap : Form
    {
        public frmDSHoadonnhap()
        {
            InitializeComponent();
        }

        private void frmHoadonnhap_Load(object sender, EventArgs e)
        {

            Load_dgridDanhsachHDN();
            dgridDanhsachHDN.CellDoubleClick += dgridDanhsachHDN_CellContentdoubleClick;

        }
        private void Load_dgridDanhsachHDN()
        {

            string sql;
            sql = "SELECT SoHDN,MaNV, NgayNhap, MaNCC, TongTien FROM tblHoaDonNhap";
            DataTable tblHoadonnhap = Class.Functions.getdatatotable(sql);
            dgridDanhsachHDN.DataSource = tblHoadonnhap;
            //do dl tu bang vao datagridview
            dgridDanhsachHDN.Columns[0].HeaderText = "Số hóa đơn nhập";
            dgridDanhsachHDN.Columns[1].HeaderText = "Mã nhân viên";
            dgridDanhsachHDN.Columns[2].HeaderText = "Ngày nhập";
            dgridDanhsachHDN.Columns[3].HeaderText = "Mã nhà cung cấp";
            dgridDanhsachHDN.Columns[4].HeaderText = "Tổng tiền";
            dgridDanhsachHDN.Columns[0].Width = 150;
            dgridDanhsachHDN.Columns[1].Width = 120;
            dgridDanhsachHDN.Columns[2].Width = 120;
            dgridDanhsachHDN.Columns[3].Width = 120;
            dgridDanhsachHDN.Columns[4].Width = 120;
        }

        private void dgridDanhsachHDN_CellContentdoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string SoHDN = dgridDanhsachHDN.Rows[e.RowIndex].Cells["SoHDN"].Value.ToString();

                // Tạo form chi tiết và truyền mã hóa đơn vào
                frmChitietHDnhap frmChiTiet = new frmChitietHDnhap(SoHDN);
                frmChiTiet.ShowDialog(); // hoặc Show() nếu bạn muốn mở song song
            }
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {

            string sql;
            bool isNgayBD_Empty = string.IsNullOrWhiteSpace(mskngaybd.Text.Replace("/", "").Trim());
            bool isNgayKT_Empty = string.IsNullOrWhiteSpace(mskngaykt.Text.Replace("/", "").Trim());

            // Kiểm tra nếu tất cả đều rỗng
            if (string.IsNullOrEmpty(txtSoHDN.Text) &&
                string.IsNullOrEmpty(txtMaNV.Text) &&
                string.IsNullOrEmpty(txtMaNCC.Text) &&
                string.IsNullOrEmpty(txtkhoangbd.Text) &&
                string.IsNullOrEmpty(txtkhoangkt.Text) &&
                isNgayBD_Empty && isNgayKT_Empty)
            {
                MessageBox.Show("Hãy nhập ít nhất một điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra khoảng tiền
            if (txtkhoangbd.Text != "" && txtkhoangkt.Text == "")
            {
                MessageBox.Show("Hãy nhập khoảng tiền tối đa muốn tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtkhoangkt.Focus();
                return;
            }
            if (txtkhoangbd.Text == "" && txtkhoangkt.Text != "")
            {
                MessageBox.Show("Hãy nhập khoảng tiền tối thiểu muốn tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtkhoangbd.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(txtkhoangbd.Text) && !string.IsNullOrEmpty(txtkhoangkt.Text))
            {
                if (decimal.TryParse(txtkhoangbd.Text, out decimal bd) && decimal.TryParse(txtkhoangkt.Text, out decimal kt))
                {
                    if (bd > kt)
                    {
                        MessageBox.Show("Khoảng tiền tối thiểu không được lớn hơn khoảng tiền tối đa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtkhoangbd.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đúng định dạng số cho khoảng tiền!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Kiểm tra ngày

            if (!isNgayBD_Empty && isNgayKT_Empty)
            {
                MessageBox.Show("Hãy nhập khoảng thời gian kết thúc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskngaykt.Focus();
                return;
            }
            if (isNgayBD_Empty && !isNgayKT_Empty)
            {
                MessageBox.Show("Hãy nhập khoảng thời gian bắt đầu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskngaybd.Focus();
                return;
            }
            if (!isNgayBD_Empty && !isNgayKT_Empty)
            {

                if (DateTime.TryParseExact(mskngaybd.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngaybd) &&
                    DateTime.TryParseExact(mskngaykt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngaykt))
                {
                    if (ngaybd > ngaykt)
                    {
                        MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc, vui lòng nhập lại ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        mskngaybd.Focus();
                        return;
                    }
                }

            }



            // Bắt đầu xây dựng SQL
            sql = "SELECT * FROM tblHoadonnhap WHERE 1=1";

            if (!string.IsNullOrEmpty(txtSoHDN.Text))
                sql += " AND SoHDN = N'" + txtSoHDN.Text.Trim() + "'";

            if (!string.IsNullOrEmpty(txtMaNV.Text))
                sql += " AND MaNV = N'" + txtMaNV.Text.Trim() + "'";

            if (!string.IsNullOrEmpty(txtMaNCC.Text))
                sql += " AND MaNCC = N'" + txtMaNCC.Text.Trim() + "'";

            if (!string.IsNullOrEmpty(txtkhoangbd.Text) && !string.IsNullOrEmpty(txtkhoangkt.Text))
            {
                sql += $" AND TongTien BETWEEN {txtkhoangbd.Text.Trim()} AND {txtkhoangkt.Text.Trim()}";
            }

            // Xử lý ngày nếu cả hai đã nhập
            if (!isNgayBD_Empty && !isNgayKT_Empty)
            {
                if (
                    DateTime.TryParseExact(mskngaybd.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngaybd) &&
                    DateTime.TryParseExact(mskngaykt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngaykt)
                )
                {
                    sql += $" AND NgayNhap BETWEEN '{ngaybd:yyyy-MM-dd}' AND '{ngaykt:yyyy-MM-dd}'";
                }
                else
                {
                    MessageBox.Show("Ngày nhập không hợp lệ! Định dạng phải là dd/MM/yyyy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }

            // Thực thi truy vấn
            DataTable tblHoaDonNhap = Class.Functions.getdatatotable(sql);
            if (tblHoaDonNhap.Rows.Count == 0)
            {
                MessageBox.Show("Không có hóa đơn nào thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Tìm thấy " + tblHoaDonNhap.Rows.Count + " hóa đơn nhập phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgridDanhsachHDN.DataSource = tblHoaDonNhap;
                lblTongHD.Text = "Số lượng hóa đơn nhập: " + tblHoaDonNhap.Rows.Count;
            }



        }



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            Load_dgridDanhsachHDN();
            txtSoHDN.Clear();
            txtMaNV.Clear();
            txtMaNCC.Clear();
            txtkhoangbd.Clear();
            txtkhoangkt.Clear();
            mskngaybd.Clear();
            mskngaykt.Clear();
            lblTongHD.Text = "Số lượng hóa đơn nhập: ";



        }



        private void mskngaybd_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void btnTaoHDN_Click(object sender, EventArgs e)
        {
            frmTaodonnhap taoDonNhapForm = new frmTaodonnhap();
            taoDonNhapForm.StartPosition = FormStartPosition.CenterScreen; // Đặt giữa màn hình
            taoDonNhapForm.TopMost = true; // Đưa form này lên trước tất cả cửa sổ khác
            taoDonNhapForm.Show();
            // ẩn form hiện tại:
            this.Hide();

            // đóng luôn form hiện tại:
            //this.Close();
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            // Khởi tạo đối tượng Excel
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.Sheets[1];

            // Thông tin cửa hàng
            worksheet.Cells[1, 1] = "CỬA HÀNG BÁN XE MÁY";
            worksheet.Cells[1, 1].Font.Color = Color.Blue;
            worksheet.Cells[2, 1] = "Địa chỉ: 12 Chùa Bộc, Quang Trung, Đống Đa, Hà Nội";
            worksheet.Cells[2, 1].Font.Color = Color.Blue;
            worksheet.Cells[3, 1] = "Số điện thoại: 088 888 8888";
            worksheet.Cells[3, 1].Font.Color = Color.Blue;

            // Tiêu đề danh sách
            Excel.Range mergeRange = worksheet.Range[worksheet.Cells[5, 1], worksheet.Cells[5, 8]];
            mergeRange.Merge();
            mergeRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            mergeRange.Value = "DANH SÁCH HÓA ĐƠN NHẬP";
            mergeRange.Font.Size = 18;
            mergeRange.Font.Color = Color.Red;

            worksheet.Cells[7, 1] = "Thời gian xuất danh sách: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            // Ghi tiêu đề cột
            for (int i = 0; i < dgridDanhsachHDN.Columns.Count; i++)
            {
                worksheet.Cells[11, i + 1] = dgridDanhsachHDN.Columns[i].HeaderText;
                worksheet.Cells[11, i + 1].Font.Bold = true;
                worksheet.Cells[11, i + 1].Interior.Color = Color.LightYellow;
                worksheet.Cells[11, i + 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            }

            // Ghi dữ liệu từ DataGridView
            for (int i = 0; i < dgridDanhsachHDN.Rows.Count; i++)
            {
                for (int j = 0; j < dgridDanhsachHDN.Columns.Count; j++)
                {
                    worksheet.Cells[i + 12, j + 1] = dgridDanhsachHDN.Rows[i].Cells[j].Value?.ToString();
                    worksheet.Cells[i + 12, j + 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                }
            }

            // Tự động chỉnh kích thước cột cho vừa nội dung
            worksheet.Columns.AutoFit();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
