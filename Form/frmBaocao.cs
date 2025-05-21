using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Excel = Microsoft.Office.Interop.Excel;
using QLCHBanXeMay.Class;

namespace QLCHBanXeMay.form
{
    public partial class frmBaocao : Form
    {
        public frmBaocao()
        {
            InitializeComponent();
        }

        private void frmBaocao_Load(object sender, EventArgs e)
        {
            Load_Baocaohientai();
            LoadChartData();
            LoadChartDataSP();
            cboThoigian.SelectedIndex = 0;
            lblBD.Visible = false;
            lblKT.Visible = false;
            dgridBaoCao.CurrentCell = null;
            dgridXephang.CurrentCell = null;
        }
        DataTable tblBaocaoNV;
        DataTable tblXephangNV;
        private void Load_Baocaohientai() {
            string sql;
            sql = @"SELECT tblNhanvien.MaNV, tblNhanvien.TenNV, 
                COUNT(DISTINCT(tblDonDatHang.SoDDH)) AS SoLuongHoaDon,
                COALESCE(SUM(tblChitietDonDatHang.Thanhtien), 0) AS TongDoanhThu
                FROM tblNhanvien
                LEFT JOIN tblDonDatHang ON tblNhanvien.MaNV = tblDonDatHang.MaNV
                AND tblDonDatHang.NgayMua = '" + DateTime.Today.ToString("yyyy-MM-dd") + @"'
                LEFT JOIN tblChitietDonDatHang ON tblDonDatHang.SoDDH = tblChitietDonDatHang.SoDDH
                GROUP BY tblNhanvien.MaNV, tblNhanvien.TenNV; ";
            tblBaocaoNV = Class.Functions.getdatatotable(sql);
            dgridBaoCao.DataSource = tblBaocaoNV;
            dgridBaoCao.Columns[0].HeaderText = "Mã nhân viên";
            dgridBaoCao.Columns[1].HeaderText = "Tên nhân viên";
            dgridBaoCao.Columns[2].HeaderText = "Số xe máy đã bán";
            dgridBaoCao.Columns[3].HeaderText = "Tổng doanh thu";
            dgridBaoCao.Columns[0].Width = 110;
            dgridBaoCao.Columns[1].Width = 170;
            dgridBaoCao.Columns[2].Width = 160;
            dgridBaoCao.Columns[3].Width = 160;
            dgridBaoCao.AllowUserToAddRows = false;
            dgridBaoCao.EditMode = DataGridViewEditMode.EditProgrammatically;

            sql = @"  SELECT tblNhanvien.MaNV, tblNhanvien.TenNV, 
                COUNT(DISTINCT(tblDonDatHang.SoDDH)) AS SoLuongHoaDon,
                COALESCE(SUM(tblChitietDonDatHang.Thanhtien), 0) AS TongDoanhThu
                FROM tblNhanvien
                LEFT JOIN tblDonDatHang ON tblNhanvien.MaNV = tblDonDatHang.MaNV
                AND tblDonDatHang.NgayMua = '" + DateTime.Today.ToString("yyyy-MM-dd") + @"'
                LEFT JOIN tblChitietDonDatHang ON tblDonDatHang.SoDDH = tblChitietDonDatHang.SoDDH
                GROUP BY tblNhanvien.MaNV, tblNhanvien.TenNV
                HAVING SUM(tblChitietDonDatHang.Thanhtien) = (
                SELECT MAX(TongDoanhThu)
                FROM (
                SELECT SUM(tblChitietDonDatHang.Thanhtien) AS TongDoanhThu
                FROM tblNhanvien 
                LEFT JOIN tblDonDatHang ON tblNhanvien.MaNV = tblDonDatHang.MaNV 
                AND tblDonDatHang.NgayMua ='" + DateTime.Today.ToString("yyyy-MM-dd") + @"'
                LEFT JOIN tblChitietDonDatHang ON tblDonDatHang.SoDDH = tblChitietDonDatHang.SoDDH GROUP BY tblNhanvien.MaNV, tblNhanvien.TenNV) AS SubQuery
                );";

            tblXephangNV = Class.Functions.getdatatotable(sql);
            dgridXephang.DataSource = tblXephangNV;
            dgridXephang.Columns[0].HeaderText = "Mã nhân viên";
            dgridXephang.Columns[1].HeaderText = "Tên nhân viên";
            dgridXephang.Columns[2].HeaderText = "Số đơn hàng đã bán";
            dgridXephang.Columns[3].HeaderText = "Tổng doanh thu";
            dgridXephang.Columns[0].Width = 110;
            dgridXephang.Columns[1].Width = 170;
            dgridXephang.Columns[2].Width = 160;
            dgridXephang.Columns[3].Width = 160;
            dgridXephang.AllowUserToAddRows = false;
            dgridXephang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }


        private void ResetValues()
        {
            cboBD.Text = "";
            cboBD.Items.Clear();
            cboKT.Items.Clear();
            cboKT.Text = "";
            cboNamBD.Items.Clear();
            cboNamKT.Items.Clear();
            cboNamBD.Text = "";
            cboNamKT.Text = "";
            dtpBD.Text = "";
            dtpKT.Text = "";
            cboBD.SelectedIndex = -1;
            cboKT.SelectedIndex = -1;
            cboNamBD.SelectedIndex = -1;
            cboNamKT.SelectedIndex = -1;
            dgridBaoCao.DataSource = "";
            dgridXephang.DataSource = "";
            chart.ChartAreas.Clear();
            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            gpbThoigian.Enabled = true;
            cboBD.Visible = false;
            cboKT.Visible = false;
            cboNamBD.Visible = false;
            cboNamKT.Visible = false;
            dtpBD.Visible = false;
            dtpKT.Visible = false;
            lblNam1.Visible = false;
            lblNam2.Visible = false;
        }
        private void LoadChartData()
        {
            if (dgridBaoCao.Rows.Count == 0)
            {
                chart.Series.Clear();
                chart.ChartAreas.Clear();
            }
            else
            {
                chart.Series.Clear();
                chart.ChartAreas.Clear();
                ChartArea chartArea = new ChartArea();
                chart.ChartAreas.Add(chartArea);

                Series series = new Series();
                series.ChartType = SeriesChartType.Column;
                series.XValueMember = "TenNV";
                series.YValueMembers = "TongDoanhThu";
                chart.Series.Add(series);
                series.Name = "Tổng doanh thu";
                chart.DataSource = tblBaocaoNV;
                chart.DataBind();
            }
        }
        private void LoadChartDataSP()
        {
            if (dgridBaoCao.Rows.Count == 0)
            {
                chart1.Series.Clear();
                chart1.ChartAreas.Clear();
            }
            else
            {
                chart1.Series.Clear();
                chart1.ChartAreas.Clear();
                ChartArea chartArea = new ChartArea();
                chart1.ChartAreas.Add(chartArea);

                Series series = new Series();
                series.ChartType = SeriesChartType.Column;
                series.XValueMember = "TenNV";
                series.YValueMembers = "Soluonghoadon";
                chart1.Series.Add(series);
                series.Name = "Tổng số xe máy bán ra";

                chart1.DataSource = tblBaocaoNV;
                chart1.DataBind();
            }
        }

        private void cboThoigian_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboThoigian.Text == "Hôm nay")
            {
                Load_Baocaohientai();
                LoadChartData();
                LoadChartDataSP();
                dtpBD.Visible = false;
                dtpKT.Visible = false;
                lblNam1.Visible = false;
                lblNam2.Visible = false;
                cboNamBD.Visible = false;
                cboNamKT.Visible = false;
                cboBD.Visible = false;
                cboKT.Visible = false;
                lblBD.Visible = false;
                lblKT.Visible = false;
            }
            if (cboThoigian.Text == "Ngày")
            {
                ResetValues();

                lblBD.Visible = true;
                lblKT.Visible = true;
                dtpBD.Visible = true;
                dtpKT.Visible = true;
                lblNam1.Visible = false;
                lblNam2.Visible = false;
                cboNamBD.Visible = false;
                cboNamKT.Visible = false;
                cboBD.Visible = false;
                cboKT.Visible = false;

            }
            if (cboThoigian.Text == "Tháng")
            {
                ResetValues();

                if (cboBD.Items.Count == 0)
                {
                    cboBD.Items.AddRange(new object[] {
                "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"
            });
                }

                if (cboKT.Items.Count == 0)
                {
                    cboKT.Items.AddRange(new object[] {
                "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"
            });
                }

                if (cboNamBD.Items.Count == 0)
                {
                    cboNamBD.Items.AddRange(new object[] {
                "2020", "2021", "2022", "2023", "2024","2025" });
                }

                if (cboNamKT.Items.Count == 0)
                {
                    cboNamKT.Items.AddRange(new object[] {
                "2020", "2021", "2022", "2023", "2024","2025" });
                }


                lblBD.Visible = true;
                lblKT.Visible = true;
                cboBD.Visible = true;
                cboKT.Visible = true;
                cboNamBD.Visible = true;
                cboNamKT.Visible = true;
                dtpBD.Visible = false;
                dtpKT.Visible = false;
                lblNam1.Visible = true;
                lblNam2.Visible = true;

            }


            if (cboThoigian.Text == "Quý")
            {
                ResetValues();

                if (cboBD.Items.Count == 0)
                {
                    cboBD.Items.AddRange(new object[]
                { "1", "2", "3", "4"});
                }

                if (cboKT.Items.Count == 0)
                {
                    cboKT.Items.AddRange(new object[] {
                "1", "2", "3", "4"
            });
                }

                if (cboNamBD.Items.Count == 0)
                {
                    cboNamBD.Items.AddRange(new object[] {
                "2020", "2021", "2022", "2023", "2024","2025"
            });
                }

                if (cboNamKT.Items.Count == 0)
                {
                    cboNamKT.Items.AddRange(new object[] {
                "2020", "2021", "2022", "2023", "2024","2025"
            });
                }


                lblBD.Visible = true;
                lblKT.Visible = true;
                cboBD.Visible = true;
                cboKT.Visible = true;
                cboNamBD.Visible = true;
                cboNamKT.Visible = true;
                dtpBD.Visible = false;
                dtpKT.Visible = false;
                lblNam1.Visible = true;
                lblNam2.Visible = true;

            }
            if (cboThoigian.Text == "Năm")
            {
                ResetValues();

                if (cboBD.Items.Count == 0)
                {
                    cboBD.Items.AddRange(new object[] {
                "2020", "2021", "2022", "2023", "2024","2025"
            });
                }

                if (cboKT.Items.Count == 0)
                {
                    cboKT.Items.AddRange(new object[] {
                "2020", "2021", "2022", "2023", "2024","2025"
            });
                }
                lblBD.Visible = true;
                lblKT.Visible = true;
                cboBD.Visible = true;
                cboKT.Visible = true;
                cboNamBD.Visible = false;
                cboNamKT.Visible = false;
                dtpBD.Visible = false;
                dtpKT.Visible = false;
                lblNam1.Visible = false;
                lblNam2.Visible = false;
            }
        }
        private void Load_Baocaongay()
        {

            if (cboThoigian.Text == "Ngày")
            {
                DateTime ngayBatDau = dtpBD.Value;
                DateTime ngayKetThuc = dtpKT.Value;
                if (ngayKetThuc < ngayBatDau)
                {
                    // Ngày kết thúc phải sau ngày bắt đầu
                    MessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtpKT.Focus();
                    return;

                }
                if (ngayBatDau >= DateTime.Today.AddDays(1) || ngayKetThuc >= DateTime.Today.AddDays(1))
                {
                    // Không được chọn ngày trong tương lai
                    MessageBox.Show("Thời gian đã chọn chưa đủ số liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtpBD.Value = DateTime.Today.AddDays(-1);
                    dtpKT.Value = DateTime.Today.AddDays(-1);
                    return;
                }

                string sql;
                sql = @"SELECT tblNhanvien.MaNV, tblNhanvien.TenNV, COUNT(DISTINCT(tblDonDatHang.SoDDH)) AS SoLuongHoaDon,
                COALESCE(SUM(tblChitietDonDatHang.Thanhtien), 0) AS TongDoanhThu 
                FROM tblNhanvien 
                LEFT JOIN tblDonDatHang ON tblNhanvien.MaNV = tblDonDatHang.MaNV 
                AND tblDonDatHang.NgayMua BETWEEN '" + dtpBD.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpKT.Value.ToString("yyyy-MM-dd") + @"'
                LEFT JOIN tblChitietDonDatHang ON tblDonDatHang.SoDDH = tblChitietDonDatHang.SoDDH GROUP BY tblNhanvien.MaNV, tblNhanvien.TenNV;";

                tblBaocaoNV = Class.Functions.getdatatotable(sql);
                dgridBaoCao.DataSource = tblBaocaoNV;
                dgridBaoCao.Columns[0].HeaderText = "Mã nhân viên";
                dgridBaoCao.Columns[1].HeaderText = "Tên nhân viên";
                dgridBaoCao.Columns[2].HeaderText = "Số đơn hàng đã bán";
                dgridBaoCao.Columns[3].HeaderText = "Tổng doanh thu";
                dgridBaoCao.Columns[0].Width = 110;
                dgridBaoCao.Columns[1].Width = 170;
                dgridBaoCao.Columns[2].Width = 160;
                dgridBaoCao.Columns[3].Width = 160;
                dgridBaoCao.AllowUserToAddRows = false;
                dgridBaoCao.EditMode = DataGridViewEditMode.EditProgrammatically;
                LoadChartData();

                sql = @"
                    SELECT tblNhanvien.MaNV, tblNhanvien.TenNV, 
                        COUNT(DISTINCT(tblDonDatHang.SoDDH)) AS SoLuongHoaDon, 
                        COALESCE(SUM(tblChitietDonDatHang.Thanhtien), 0) AS TongDoanhThu
                        FROM tblNhanvien 
                        LEFT JOIN tblDonDatHang ON tblNhanvien.MaNV = tblDonDatHang.MaNV 
                        AND tblDonDatHang.NgayMua BETWEEN '" + dtpBD.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpKT.Value.ToString("yyyy-MM-dd") + @"'
                        LEFT JOIN tblChitietDonDatHang ON tblDonDatHang.SoDDH = tblChitietDonDatHang.SoDDH GROUP BY tblNhanvien.MaNV, tblNhanvien.TenNV
                        HAVING SUM(tblChitietDonDatHang.Thanhtien) = (
                        SELECT MAX(TongDoanhThu)
                        FROM (
                        SELECT SUM(tblChitietDonDatHang.Thanhtien) AS TongDoanhThu
                        FROM tblNhanvien 
                        LEFT JOIN tblDonDatHang ON tblNhanvien.MaNV = tblDonDatHang.MaNV 
                        AND tblDonDatHang.NgayMua BETWEEN '" + dtpBD.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpKT.Value.ToString("yyyy-MM-dd") + @"'
                        LEFT JOIN tblChitietDonDatHang ON tblDonDatHang.SoDDH = tblChitietDonDatHang.SoDDH 
                       GROUP BY tblNhanvien.MaNV, tblNhanvien.TenNV
                        ) AS SubQuery
                    );";

                DataTable tblXephangNV = Class.Functions.getdatatotable(sql);
                dgridXephang.DataSource = tblXephangNV;
                dgridXephang.Columns[0].HeaderText = "Mã nhân viên";
                dgridXephang.Columns[1].HeaderText = "Tên nhân viên";
                dgridXephang.Columns[2].HeaderText = "Số đơn hàng đã bán";
                dgridXephang.Columns[3].HeaderText = "Tổng doanh thu";
                dgridXephang.Columns[0].Width = 110;
                dgridXephang.Columns[1].Width = 170;
                dgridXephang.Columns[2].Width = 160;
                dgridXephang.Columns[3].Width = 160;
                dgridXephang.AllowUserToAddRows = false;
                dgridXephang.EditMode = DataGridViewEditMode.EditProgrammatically;
                gpbThoigian.Enabled = false;

            }
        }
        private void Load_Baocaothang()
        {
            if (cboThoigian.Text == "Tháng")
            {

                if (cboBD.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn tháng bắt đầu", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboBD.Focus();
                    dgridBaoCao.DataSource = null;
                    dgridXephang.DataSource = null;
                    return;
                }


                if (cboKT.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn tháng kết thúc", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboKT.Focus();
                    dgridBaoCao.DataSource = null;
                    dgridXephang.DataSource = null;
                    return;

                }
                if (cboNamBD.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn năm bắt đầu", "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                    cboNamBD.Focus();
                    dgridBaoCao.DataSource = null;
                    dgridXephang.DataSource = null;
                    return;
                }
                if (cboNamKT.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn năm kết thúc", "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                    cboNamKT.Focus();
                    dgridBaoCao.DataSource = null;
                    dgridXephang.DataSource = null;
                    return;
                }


                int namBD = int.Parse(cboNamBD.Text);
                int namKT = int.Parse(cboNamKT.Text);
                int BD = int.Parse(cboBD.Text);
                int KT = int.Parse(cboKT.Text);

                if (BD > KT && namBD == namKT)

                {
                    MessageBox.Show("Thời gian bắt đầu không được lớn hơn thời gian kết thúc", "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                    cboNamBD.Focus();
                    dgridBaoCao.DataSource = null;
                    dgridXephang.DataSource = null;

                    return;
                }
                if (namBD > namKT)

                {
                    MessageBox.Show("Thời gian bắt đầu không được lớn hơn thời gian kết thúc", "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                    cboNamBD.Focus();
                    dgridBaoCao.DataSource = null;
                    dgridXephang.DataSource = null;
                    return;
                }
                if (namBD == DateTime.Today.Year && BD > DateTime.Today.Month)
                {
                    MessageBox.Show("Thời gian đã chọn chưa đủ số liệu", "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                    cboBD.Focus();
                    dgridBaoCao.DataSource = null;
                    dgridXephang.DataSource = null;
                    return;

                }
                if (namKT == DateTime.Today.Year && KT > DateTime.Today.Month)
                {
                    MessageBox.Show("Thời gian đã chọn chưa đủ số liệu", "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                    cboKT.Focus();
                    dgridBaoCao.DataSource = null;
                    dgridXephang.DataSource = null;
                    return;

                }

                string sql;
                sql = @"SELECT tblNhanvien.MaNV, tblNhanvien.TenNV, 
                       COUNT(DISTINCT(tblDonDatHang.SoDDH)) AS SoLuongHoaDon, 
                       COALESCE(SUM(tblChitietDonDatHang.Thanhtien), 0) AS TongDoanhThu 
                        FROM tblNhanvien
                        LEFT JOIN tblDonDatHang ON tblNhanvien.MaNV = tblDonDatHang.MaNV
                        AND Month(tblDonDatHang.NgayMua) BETWEEN '" + BD + "' AND '" + KT + "'AND Year(tblDonDatHang.NgayMua)  BETWEEN '" + namBD + "' AND '" + namKT + @"'
                        LEFT JOIN tblChitietDonDatHang ON tblDonDatHang.SoDDH = tblChitietDonDatHang.SoDDH 
                        GROUP BY tblNhanvien.MaNV, tblNhanvien.TenNV;";

                tblBaocaoNV = Class.Functions.getdatatotable(sql);
                dgridBaoCao.DataSource = tblBaocaoNV;
                dgridBaoCao.Columns[0].HeaderText = "Mã nhân viên";
                dgridBaoCao.Columns[1].HeaderText = "Tên nhân viên";
                dgridBaoCao.Columns[2].HeaderText = "Số đơn hàng đã bán";
                dgridBaoCao.Columns[3].HeaderText = "Tổng doanh thu";
                dgridBaoCao.Columns[0].Width = 110;
                dgridBaoCao.Columns[1].Width = 170;
                dgridBaoCao.Columns[2].Width = 160;
                dgridBaoCao.Columns[3].Width = 160;
                dgridBaoCao.AllowUserToAddRows = false;
                dgridBaoCao.EditMode = DataGridViewEditMode.EditProgrammatically;
                gpbThoigian.Enabled = false;

                sql = @"
                    SELECT tblNhanvien.MaNV, tblNhanvien.TenNV, COUNT(DISTINCT(tblDonDatHang.SoDDH)) AS SoLuongHoaDon,
                    COALESCE(SUM(tblChitietDonDatHang.Thanhtien), 0) AS TongDoanhThu
                    FROM tblNhanvien
                        LEFT JOIN tblDonDatHang ON tblNhanvien.MaNV = tblDonDatHang.MaNV
                        AND Month(tblDonDatHang.NgayMua) BETWEEN '" + BD + "' AND '" + KT + "'AND Year(tblDonDatHang.NgayMua)  BETWEEN '" + namBD + "' AND '" + namKT + @"'
                        LEFT JOIN tblChitietDonDatHang ON tblDonDatHang.SoDDH = tblChitietDonDatHang.SoDDH 
                        GROUP BY tblNhanvien.MaNV, tblNhanvien.TenNV
                        HAVING SUM(tblChitietDonDatHang.Thanhtien) = (
                        SELECT MAX(TongDoanhThu)
                        FROM (
                                SELECT COALESCE(SUM(tblChitietDonDatHang.Thanhtien), 0) AS TongDoanhThu
                            FROM tblNhanvien
                            LEFT JOIN tblDonDatHang ON tblNhanvien.MaNV = tblDonDatHang.MaNV
                            AND Month(tblDonDatHang.NgayMua) BETWEEN '" + BD + "' AND '" + KT + "'AND Year(tblDonDatHang.NgayMua)  BETWEEN '" + namBD + "' AND '" + namKT + @"'
                            LEFT JOIN tblChitietDonDatHang ON tblDonDatHang.SoDDH = tblChitietDonDatHang.SoDDH 
                            GROUP BY tblNhanvien.MaNV, tblNhanvien.TenNV
                            ) AS SubQuery
                    );";

                DataTable tblXephangNV = Class.Functions.getdatatotable(sql);
                dgridXephang.DataSource = tblXephangNV;
                dgridXephang.Columns[0].HeaderText = "Mã nhân viên";
                dgridXephang.Columns[1].HeaderText = "Tên nhân viên";
                dgridXephang.Columns[2].HeaderText = "Số đơn hàng đã bán";
                dgridXephang.Columns[3].HeaderText = "Tổng doanh thu";
                dgridXephang.Columns[0].Width = 110;
                dgridXephang.Columns[1].Width = 170;
                dgridXephang.Columns[2].Width = 160;
                dgridXephang.Columns[3].Width = 160;
                dgridXephang.AllowUserToAddRows = false;
                dgridXephang.EditMode = DataGridViewEditMode.EditProgrammatically;
                gpbThoigian.Enabled = false;

            }
        }

        private void Load_Baocaoquy()
        {

            if (cboThoigian.Text == "Quý")
            {
                if (cboBD.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn quý bắt đầu", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboBD.Focus();
                    dgridBaoCao.DataSource = null;
                    dgridXephang.DataSource = null;
                    return;
                }
                if (cboKT.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn quý kết thúc", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboKT.Focus();
                    dgridBaoCao.DataSource = null;
                    dgridXephang.DataSource = null;
                    return;
                }
                if (cboNamBD.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn năm bắt đầu", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNamBD.Focus();
                    dgridBaoCao.DataSource = null;
                    dgridXephang.DataSource = null;
                    return;
                }
                if (cboNamKT.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn năm kết thúc", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNamKT.Focus();
                    dgridBaoCao.DataSource = null;
                    dgridXephang.DataSource = null;
                    return;
                }
                int QuyBD = int.Parse(cboBD.Text);
                int QuyKT = int.Parse(cboKT.Text);
                int namBD = int.Parse(cboNamBD.Text);
                int namKT = int.Parse(cboNamKT.Text);

                if (QuyBD > QuyKT && namBD == namKT)
                {
                    MessageBox.Show("Thời gian bắt đầu không được lớn hơn thời gian kết thúc", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNamBD.Focus();
                    dgridBaoCao.DataSource = null;
                    dgridXephang.DataSource = null;
                    return;
                }
                if (namBD > namKT)
                {
                    MessageBox.Show("Thời gian bắt đầu không được lớn hơn thời gian kết thúc", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNamBD.Focus();
                    dgridBaoCao.DataSource = null;
                    dgridXephang.DataSource = null;
                    return;
                }

                Dictionary<int, int[]> Quy1 = new Dictionary<int, int[]>()
                        {
                            { 1, new int[] { 1, 2, 3 } },
                            { 2, new int[] { 4, 5, 6 } },
                            { 3, new int[] { 7, 8, 9 } },
                            { 4, new int[] { 10, 11, 12 } }
                        };

                List<int> quybd = new List<int>();
                for (int i = QuyBD; i <= 4; i++)
                {
                    if (Quy1.ContainsKey(i))
                    {
                        quybd.AddRange(Quy1[i]);
                    }
                }
                Dictionary<int, int[]> Quy2 = new Dictionary<int, int[]>()
                        {
                            { 1, new int[] { 1, 2, 3 } },
                            { 2, new int[] { 4, 5, 6 } },
                            { 3, new int[] { 7, 8, 9 } },
                            { 4, new int[] { 10, 11, 12 } }
                        };

                List<int> quykt = new List<int>();
                for (int i = QuyKT; i <= 4; i++)
                {
                    if (Quy2.ContainsKey(i))
                    {
                        quykt.AddRange(Quy2[i]);
                    }
                }
                if (namBD == DateTime.Today.Year && quybd[2] > DateTime.Today.Month)
                {
                    MessageBox.Show("Thời gian đã chọn chưa đủ số liệu", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNamBD.Focus();
                    dgridBaoCao.DataSource = null;
                    dgridXephang.DataSource = null;
                    return;
                }
                if (namKT == DateTime.Today.Year && quykt[2] > DateTime.Today.Month)
                {
                    MessageBox.Show("Thời gian đã chọn chưa đủ số liệu", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNamKT.Focus();
                    dgridBaoCao.DataSource = null;
                    dgridXephang.DataSource = null;
                    return;
                }
                string sql;
                sql = @"SELECT tblNhanvien.MaNV, tblNhanvien.TenNV, COUNT(DISTINCT(tblDonDatHang.SoDDH)) AS SoLuongHoaDon,  COALESCE(SUM(tblChitietDonDatHang.Thanhtien), 0) AS TongDoanhThu 
                            FROM tblNhanvien
                            LEFT JOIN tblDonDatHang ON tblNhanvien.MaNV = tblDonDatHang.MaNV
                            AND Month(tblDonDatHang.NgayMua) BETWEEN '" + quybd[0] + "' AND '" + quykt[2] + "'AND Year(tblDonDatHang.NgayMua)  BETWEEN '" + namBD + "' AND '" + namKT + @"'
                            LEFT JOIN tblChitietDonDatHang ON tblDonDatHang.SoDDH = tblChitietDonDatHang.SoDDH
                            GROUP BY tblNhanvien.MaNV, tblNhanvien.TenNV; ";


                tblBaocaoNV = Class.Functions.getdatatotable(sql);
                dgridBaoCao.DataSource = tblBaocaoNV;
                dgridBaoCao.Columns[0].HeaderText = "Mã nhân viên";
                dgridBaoCao.Columns[1].HeaderText = "Tên nhân viên";
                dgridBaoCao.Columns[2].HeaderText = "Số đơn hàng đã bán";
                dgridBaoCao.Columns[3].HeaderText = "Tổng doanh thu";
                dgridBaoCao.Columns[0].Width = 110;
                dgridBaoCao.Columns[1].Width = 170;
                dgridBaoCao.Columns[2].Width = 160;
                dgridBaoCao.Columns[3].Width = 160;
                dgridBaoCao.AllowUserToAddRows = false;
                dgridBaoCao.EditMode = DataGridViewEditMode.EditProgrammatically;
                gpbThoigian.Enabled = false;

                sql = @"
                    SELECT tblNhanvien.MaNV, tblNhanvien.TenNV, COUNT(DISTINCT(tblDonDatHang.SoDDH)) AS SoLuongHoaDon,  COALESCE(SUM(tblChitietDonDatHang.Thanhtien), 0) AS TongDoanhThu
                         FROM tblNhanvien
                         LEFT JOIN tblDonDatHang ON tblNhanvien.MaNV = tblDonDatHang.MaNV
                         AND Month(tblDonDatHang.NgayMua) BETWEEN '" + quybd[0] + "' AND '" + quykt[2] + "'AND Year(tblDonDatHang.NgayMua)  BETWEEN '" + namBD + "' AND '" + namKT + @"'
                         LEFT JOIN tblChitietDonDatHang ON tblDonDatHang.SoDDH = tblChitietDonDatHang.SoDDH
                         GROUP BY tblNhanvien.MaNV, tblNhanvien.TenNV
                    HAVING SUM(tblChitietDonDatHang.Thanhtien) = (
                        SELECT MAX(TongDoanhThu)
                        FROM (
                            SELECT COALESCE(SUM(tblChitietDonDatHang.Thanhtien), 0) AS TongDoanhThu
                            FROM tblNhanvien
                            LEFT JOIN tblDonDatHang ON tblNhanvien.MaNV = tblDonDatHang.MaNV
                            AND Month(tblDonDatHang.NgayMua) BETWEEN '" + quybd[0] + "' AND '" + quykt[2] + "'AND Year(tblDonDatHang.NgayMua)  BETWEEN '" + namBD + "' AND '" + namKT + @"'
                            LEFT JOIN tblChitietDonDatHang ON tblDonDatHang.SoDDH = tblChitietDonDatHang.SoDDH
                            GROUP BY tblNhanvien.MaNV, tblNhanvien.TenNV
                        ) AS SubQuery  );";

                DataTable tblXephangNV = Class.Functions.getdatatotable(sql);
                dgridXephang.DataSource = tblXephangNV;
                dgridXephang.Columns[0].HeaderText = "Mã nhân viên";
                dgridXephang.Columns[1].HeaderText = "Tên nhân viên";
                dgridXephang.Columns[2].HeaderText = "Số đơn hàng đã bán";
                dgridXephang.Columns[3].HeaderText = "Tổng doanh thu";
                dgridXephang.Columns[0].Width = 110;
                dgridXephang.Columns[1].Width = 170;
                dgridXephang.Columns[2].Width = 160;
                dgridXephang.Columns[3].Width = 160;
                dgridXephang.AllowUserToAddRows = false;
                dgridXephang.EditMode = DataGridViewEditMode.EditProgrammatically;
                gpbThoigian.Enabled = false;
            }
        }
        private void Load_Baocaonam()
        {

            if (cboThoigian.Text == "Năm")
            {
                if (cboBD.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn năm bắt đầu", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboBD.Focus();
                    dgridBaoCao.DataSource = null;
                    dgridXephang.DataSource = null;
                    return;
                }

                if (cboKT.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn năm kết thúc", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboKT.Focus();
                    dgridBaoCao.DataSource = null;
                    dgridXephang.DataSource = null;
                    return;
                }

                int namBD = int.Parse(cboBD.Text);
                int namKT = int.Parse(cboKT.Text);

                if (namKT < namBD)
                {
                    MessageBox.Show("Năm bắt đầu không được lớn hơn năm kết thúc", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboKT.Focus();
                    dgridBaoCao.DataSource = null;
                    dgridXephang.DataSource = null;
                    return;
                }


                if (namBD > DateTime.Today.Year || namKT > DateTime.Today.Year)
                {
                    MessageBox.Show("Thời gian đã chọn chưa đủ số liệu", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboBD.Focus();
                    dgridBaoCao.DataSource = null;
                    dgridXephang.DataSource = null;
                    return;
                }
                string sql;
                sql = @"SELECT tblNhanvien.MaNV, tblNhanvien.TenNV, COUNT(DISTINCT(tblDonDatHang.SoDDH)) AS SoLuongHoaDon,
                        COALESCE(SUM(tblChitietDonDatHang.Thanhtien), 0) AS TongDoanhThu 
                         FROM tblNhanvien
                         LEFT JOIN tblDonDatHang ON tblNhanvien.MaNV = tblDonDatHang.MaNV
                         AND Year(tblDonDatHang.NgayMua) BETWEEN '" + namBD + "' AND '" + namKT + @"'
                         LEFT JOIN tblChitietDonDatHang ON tblDonDatHang.SoDDH = tblChitietDonDatHang.SoDDH
                         GROUP BY tblNhanvien.MaNV, tblNhanvien.TenNV;";
                tblBaocaoNV = Class.Functions.getdatatotable(sql);
                dgridBaoCao.DataSource = tblBaocaoNV;
                dgridBaoCao.Columns[0].HeaderText = "Mã nhân viên";
                dgridBaoCao.Columns[1].HeaderText = "Tên nhân viên";
                dgridBaoCao.Columns[2].HeaderText = "Số đơn hàng đã bán";
                dgridBaoCao.Columns[3].HeaderText = "Tổng doanh thu";
                dgridBaoCao.Columns[0].Width = 110;
                dgridBaoCao.Columns[1].Width = 170;
                dgridBaoCao.Columns[2].Width = 160;
                dgridBaoCao.Columns[3].Width = 160;
                dgridBaoCao.AllowUserToAddRows = false;
                dgridBaoCao.EditMode = DataGridViewEditMode.EditProgrammatically;
                LoadChartData();

                sql = @"
                    SELECT tblNhanvien.MaNV, tblNhanvien.TenNV, COUNT(DISTINCT(tblDonDatHang.SoDDH)) AS SoLuongHoaDon,  COALESCE(SUM(tblChitietDonDatHang.Thanhtien), 0) AS TongDoanhThu
                        FROM tblNhanvien
                         LEFT JOIN tblDonDatHang ON tblNhanvien.MaNV = tblDonDatHang.MaNV
                         AND Year(tblDonDatHang.NgayMua) BETWEEN '" + namBD + "' AND '" + namKT + @"'
                         LEFT JOIN tblChitietDonDatHang ON tblDonDatHang.SoDDH = tblChitietDonDatHang.SoDDH
                         GROUP BY tblNhanvien.MaNV, tblNhanvien.TenNV
                    HAVING SUM(tblChitietDonDatHang.Thanhtien) = (
                        SELECT MAX(TongDoanhThu)
                        FROM (
                         SELECT COALESCE(SUM(tblChitietDonDatHang.Thanhtien), 0) AS TongDoanhThu
                         FROM tblNhanvien
                         LEFT JOIN tblDonDatHang ON tblNhanvien.MaNV = tblDonDatHang.MaNV
                         AND Year(tblDonDatHang.NgayMua) BETWEEN '" + namBD + "' AND '" + namKT + @"'
                         LEFT JOIN tblChitietDonDatHang ON tblDonDatHang.SoDDH = tblChitietDonDatHang.SoDDH
                         GROUP BY tblNhanvien.MaNV, tblNhanvien.TenNV
                        ) AS SubQuery
                    );";

                DataTable tblXephangNV = Class.Functions.getdatatotable(sql);
                dgridXephang.DataSource = tblXephangNV;
                dgridXephang.Columns[0].HeaderText = "Mã nhân viên";
                dgridXephang.Columns[1].HeaderText = "Tên nhân viên";
                dgridXephang.Columns[2].HeaderText = "Số đơn hàng đã bán";
                dgridXephang.Columns[3].HeaderText = "Tổng doanh thu";
                dgridXephang.Columns[0].Width = 110;
                dgridXephang.Columns[1].Width = 170;
                dgridXephang.Columns[2].Width = 160;
                dgridXephang.Columns[3].Width = 160;
                dgridXephang.AllowUserToAddRows = false;
                dgridXephang.EditMode = DataGridViewEditMode.EditProgrammatically;
                gpbThoigian.Enabled = false;

            }
        }

        private void btnHienthi_Click(object sender, EventArgs e)
        {
            Load_Baocaongay();
            Load_Baocaothang();
            Load_Baocaoquy();
            Load_Baocaonam();
            LoadChartData();
            LoadChartDataSP();

            dgridBaoCao.CurrentCell = null;
            dgridXephang.CurrentCell = null;

            if (cboThoigian.Text == "")
            {
                MessageBox.Show("Bạn phải chọn loại thời gian", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboThoigian.Focus();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
       
        
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            cboThoigian.SelectedIndex = 0;
            lblBD.Visible = false;
            lblKT.Visible = false;
            cboBD.Text = "";
            cboBD.Items.Clear();
            cboKT.Items.Clear();
            cboKT.Text = "";
            cboNamBD.Items.Clear();
            cboNamKT.Items.Clear();
            cboNamBD.Text = "";
            cboNamKT.Text = "";
            dtpBD.Text = "";
            dtpKT.Text = "";
            cboBD.SelectedIndex = -1;
            cboKT.SelectedIndex = -1;
            cboNamBD.SelectedIndex = -1;
            cboNamKT.SelectedIndex = -1;
            gpbThoigian.Enabled = true;
            cboBD.Visible = false;
            cboKT.Visible = false;
            cboNamBD.Visible = false;
            cboNamKT.Visible = false;
            dtpBD.Visible = false;
            dtpKT.Visible = false;
            lblNam1.Visible = false;
            lblNam2.Visible = false;
        }

        private void btnXuatexcel_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu DataGridView không có dữ liệu
            if (dgridBaoCao.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất ra Excel!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Khởi tạo đối tượng Excel
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.Sheets[1];

            // Đặt tiêu đề cho các cột
            worksheet.Cells[1, 1] = "Cửa hàng bán xe máy ";
            worksheet.Cells[1, 1].Font.Color = Color.Blue;
            worksheet.Cells[2, 1] = "Địa chỉ: 12 Chùa Bộc, Quang Trung, Đống Đa, Hà Nội ";
            worksheet.Cells[2, 1].Font.Color = Color.Blue;
            worksheet.Cells[3, 1] = "Số điện thoại: 070 531 1048 ";
            worksheet.Cells[3, 1].Font.Color = Color.Blue;

            Excel.Range mergeRange = worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[4, 8]];
            mergeRange.Merge();
            mergeRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            mergeRange.Value = "BÁO CÁO NHÂN VIÊN";
            mergeRange.Font.Size = 22;
            mergeRange.Font.Color = Color.Red;

            worksheet.Cells[5, 1] = "Báo cáo theo: " + cboThoigian.Text;
            worksheet.Cells[7, 1] = "Thời gian tạo báo cáo:  " + DateTime.Now;

            if (cboThoigian.Text == "Hôm nay")
            {
                worksheet.Cells[5, 1] = "";
                worksheet.Cells[6, 1] = "Ngày: " + DateTime.Today.ToString("dd/MM/yyyy");
                worksheet.Cells[7, 1] = "Thời gian tạo báo cáo:  " + DateTime.Now;
            }
            if (cboThoigian.Text == "Ngày" && dtpBD.Text == dtpKT.Text)
            {
                worksheet.Cells[6, 1] = "Ngày: " + dtpBD.Text;
            }
            if (cboThoigian.Text == "Ngày" && dtpBD.Text != dtpKT.Text)
            {
                worksheet.Cells[6, 1] = "Từ ngày: " + dtpBD.Text + "   " + " Đến ngày: " + dtpKT.Text;
            }

            if (cboThoigian.Text == "Tháng" && cboBD.Text == cboKT.Text && cboNamBD.Text == cboNamKT.Text)
            {
                worksheet.Cells[6, 1] = "Tháng: " + cboBD.Text + "/" + cboNamBD.Text;
            }
            if (cboThoigian.Text == "Tháng" && cboBD.Text == cboKT.Text && cboNamBD.Text != cboNamKT.Text)
            {
                worksheet.Cells[6, 1] = "Từ tháng: " + cboBD.Text + "/" + cboNamBD.Text + " Đến tháng: " + cboKT.Text + "/" + cboNamKT.Text;
            }
            if (cboThoigian.Text == "Tháng" && cboBD.Text != cboKT.Text && cboNamBD.Text == cboNamKT.Text)
            {
                worksheet.Cells[6, 1] = "Từ tháng: " + cboBD.Text + "/" + cboNamBD.Text + " Đến tháng: " + cboKT.Text + "/" + cboNamKT.Text;
            }
            if (cboThoigian.Text == "Tháng" && cboBD.Text != cboKT.Text && cboNamBD.Text != cboNamKT.Text)
            {
                worksheet.Cells[6, 1] = "Từ tháng: " + cboBD.Text + "/" + cboNamBD.Text + " Đến tháng: " + cboKT.Text + "/" + cboNamKT.Text;
            }

            if (cboThoigian.Text == "Quý" && cboBD.Text == cboKT.Text && cboNamBD.Text == cboNamKT.Text)
            {
                worksheet.Cells[6, 1] = "Quý " + cboBD.Text + "/" + cboNamBD.Text;
            }
            if (cboThoigian.Text == "Quý" && cboBD.Text == cboKT.Text && cboNamBD.Text != cboNamKT.Text)
            {
                worksheet.Cells[6, 1] = "Từ: Quý " + cboBD.Text + "/" + cboNamBD.Text + "  Đến: Quý " + cboKT.Text + "/" + cboNamKT.Text;
            }
            if (cboThoigian.Text == "Quý" && cboBD.Text != cboKT.Text && cboNamBD.Text == cboNamKT.Text)
            {
                worksheet.Cells[6, 1] = "Từ: Quý " + cboBD.Text + "/" + cboNamBD.Text + "  Đến: Quý " + cboKT.Text + "/" + cboNamKT.Text;
            }
            if (cboThoigian.Text == "Quý" && cboBD.Text != cboKT.Text && cboNamBD.Text != cboNamKT.Text)
            {
                worksheet.Cells[6, 1] = "Từ: Quý " + cboBD.Text + "/" + cboNamBD.Text + "  Đến: Quý " + cboKT.Text + "/" + cboNamKT.Text;
            }

            if (cboThoigian.Text == "Năm" && cboBD.Text == cboKT.Text)
            {
                worksheet.Cells[6, 1] = "Năm: " + cboBD.Text;
            }
            if (cboThoigian.Text == "Năm" && cboBD.Text != cboKT.Text)
            {
                worksheet.Cells[6, 1] = "Từ năm: " + cboBD.Text + "   Đến năm: " + cboKT.Text;
            }


            // Đặt tiêu đề các cột và điền số thứ tự
            worksheet.Cells[9, 2] = "STT";
            worksheet.Cells[9, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            worksheet.Cells[9, 2].Borders.Weight = Excel.XlBorderWeight.xlThin;
            worksheet.Cells[9, 2].Interior.Color = Color.LightPink;
            worksheet.Cells[9, 2].Font.Size = 13;
            worksheet.Cells[9, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            for (int i = 1; i <= dgridBaoCao.Columns.Count; i++)
            {
                worksheet.Cells[9, i + 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                worksheet.Cells[9, i + 2].Borders.Weight = Excel.XlBorderWeight.xlThin;
                worksheet.Cells[9, i + 2].Value = dgridBaoCao.Columns[i - 1].HeaderText;
                worksheet.Cells[9, i + 2].Interior.Color = Color.LightPink;
                worksheet.Cells[9, i + 2].Font.Size = 13;
                worksheet.Cells[9, i + 2].EntireColumn.AutoFit();
                worksheet.Cells[9, i + 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }

            // Điền số thứ tự và đổ dữ liệu từ DataGridView vào Excel
            for (int i = 0; i < dgridBaoCao.Rows.Count; i++)
            {
                worksheet.Cells[i + 10, 2].Value = i + 1; // Điền số thứ tự
                worksheet.Cells[i + 10, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                worksheet.Cells[i + 10, 2].Borders.Weight = Excel.XlBorderWeight.xlThin;
                worksheet.Cells[i + 10, 2].Font.Size = 13;
                worksheet.Cells[i + 10, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }
            for (int j = 0; j < dgridBaoCao.Columns.Count; j++)
                for (int i = 0; i < dgridBaoCao.Rows.Count; i++)
                {
                    worksheet.Cells[i + 10, j + 3].Value = dgridBaoCao.Rows[i].Cells[j].Value?.ToString();
                    worksheet.Cells[i + 10, j + 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    worksheet.Cells[i + 10, j + 3].Borders.Weight = Excel.XlBorderWeight.xlThin;
                    worksheet.Cells[i + 10, j + 3].Font.Size = 13;
                    worksheet.Cells[i + 10, j + 3].EntireColumn.AutoFit();
                    worksheet.Cells[i + 10, j + 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                }
            // Bảng báo cáo xếp hạng.
            int startRow = dgridBaoCao.Rows.Count + 12; // 10 là bắt đầu dữ liệu + số dòng dữ liệu + 2 dòng trống

            // Tiêu đề bảng thứ 2
            Excel.Range mergeRange2 = worksheet.Range[worksheet.Cells[startRow, 1], worksheet.Cells[startRow, 8]];
            mergeRange2.Merge();
            mergeRange2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            mergeRange2.Value = "BÁO CÁO XẾP HẠNG";
            mergeRange2.Font.Size = 18;
            mergeRange2.Font.Color = Color.Red;

            // Dòng tiêu đề bảng xếp hạng
            worksheet.Cells[startRow + 2, 2] = "STT";
            worksheet.Cells[startRow + 2, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            worksheet.Cells[startRow + 2, 2].Borders.Weight = Excel.XlBorderWeight.xlThin;
            worksheet.Cells[startRow + 2, 2].Interior.Color = Color.LightPink;
            worksheet.Cells[startRow + 2, 2].Font.Size = 13;
            worksheet.Cells[startRow + 2, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            for (int i = 1; i <= dgridXephang.Columns.Count; i++)
            {
                worksheet.Cells[startRow + 2, i + 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                worksheet.Cells[startRow + 2, i + 2].Borders.Weight = Excel.XlBorderWeight.xlThin;
                worksheet.Cells[startRow + 2, i + 2].Value = dgridXephang.Columns[i - 1].HeaderText;
                worksheet.Cells[startRow + 2, i + 2].Interior.Color = Color.LightPink;
                worksheet.Cells[startRow + 2, i + 2].Font.Size = 13;
                worksheet.Cells[startRow + 2, i + 2].EntireColumn.AutoFit();
                worksheet.Cells[startRow + 2, i + 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }

            // Đổ dữ liệu bảng xếp hạng
            for (int i = 0; i < dgridXephang.Rows.Count; i++)
            {
                worksheet.Cells[startRow + 3 + i, 2].Value = i + 1; // STT
                worksheet.Cells[startRow + 3 + i, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                worksheet.Cells[startRow + 3 + i, 2].Borders.Weight = Excel.XlBorderWeight.xlThin;
                worksheet.Cells[startRow + 3 + i, 2].Font.Size = 13;
                worksheet.Cells[startRow + 3 + i, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }

            for (int j = 0; j < dgridXephang.Columns.Count; j++)
                for (int i = 0; i < dgridXephang.Rows.Count; i++)
                {
                    worksheet.Cells[startRow + 3 + i, j + 3].Value = dgridXephang.Rows[i].Cells[j].Value?.ToString();
                    worksheet.Cells[startRow + 3 + i, j + 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    worksheet.Cells[startRow + 3 + i, j + 3].Borders.Weight = Excel.XlBorderWeight.xlThin;
                    worksheet.Cells[startRow + 3 + i, j + 3].Font.Size = 13;
                    worksheet.Cells[startRow + 3 + i, j + 3].EntireColumn.AutoFit();
                    worksheet.Cells[startRow + 3 + i, j + 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                }
        }
    }
}
