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
    }
}
