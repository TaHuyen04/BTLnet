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
    public partial class frmThongke : Form
    {
        public frmThongke()
        {
            InitializeComponent();
        }

        private void frmThongke_Load(object sender, EventArgs e)
        {
            Load_Baocaohientai();
            LoadChartData();
            LoadChartDataSP();
            cboThoigian.SelectedIndex = 0;
            lblBD.Visible = false;
            lblKT.Visible = false;
            dgridBaoCao.CurrentCell = null;
        }
        DataTable tblBaocaoDT;
        private void Load_Baocaohientai()
        {
            string sql = @"
                    WITH Ngay AS (
                    SELECT CAST('" + DateTime.Today.ToString("yyyy-MM-dd") + @"' AS DATE) AS Ngay
                     UNION ALL
                     SELECT DATEADD(day, 1, Ngay)
                     FROM Ngay
                     WHERE DATEADD(day, 1, Ngay) <= CAST('" + DateTime.Today.ToString("yyyy-MM-dd") + @"' AS DATE)
                           )
                     SELECT Ngay.Ngay, 
                      COALESCE(COUNT( DISTINCT tblHoaDonNhap.SoHDN), 0) AS SoluongHDN,
                      COALESCE(SUM( tblChitietHoaDonNhap.Soluong), 0) AS Soluongnhap,
                      COALESCE(SUM( tblChitietHoaDonNhap.Thanhtien), 0) AS Chiphinhap,
                      COALESCE(COUNT(DISTINCT tblDonDatHang.SoDDH), 0) AS SoLuongHoaDon, 
                      COALESCE(SUM( DISTINCT tblChitietDonDatHang.Soluong), 0) AS Soluongsanpham, 
                      COALESCE(SUM( DISTINCT tblChitietDonDatHang.Thanhtien), 0) AS TongDoanhThu 
                        FROM Ngay
                        LEFT JOIN tblDonDatHang ON CONVERT(DATE, Ngay.Ngay) = CONVERT(DATE, tblDonDatHang.NgayMua)
                        LEFT JOIN tblHoaDonNhap ON CONVERT(DATE, Ngay.Ngay) = CONVERT(DATE, tblHoaDonNhap.Ngaynhap)
                        LEFT JOIN tblChitietDonDatHang ON tblDonDatHang.SoDDH = tblChitietDonDatHang.SoDDH
                        LEFT JOIN tblDMHang ON tblChitietDonDatHang.MaHang = tblDMHang.MaHang
                        LEFT JOIN tblChitietHoaDonNhap ON tblHoaDonNhap.SoHDN = tblChitietHoaDonNhap.SoHDN
                        GROUP BY Ngay.Ngay
                        ORDER BY Ngay.Ngay
                        OPTION (MAXRECURSION 0);
                        ";
            tblBaocaoDT = Functions.getdatatotable(sql);
            dgridBaoCao.DataSource = tblBaocaoDT;
            dgridBaoCao.Columns[0].HeaderText = "Ngày";
            dgridBaoCao.Columns[1].HeaderText = "Số hoá đơn nhập";
            dgridBaoCao.Columns[2].HeaderText = "Số xe máy đã nhập";
            dgridBaoCao.Columns[3].HeaderText = "Chi phí nhập hàng";
            dgridBaoCao.Columns[4].HeaderText = "Số đơn đặt hàng";
            dgridBaoCao.Columns[5].HeaderText = "Số xe máy đã bán";
            dgridBaoCao.Columns[6].HeaderText = "Doanh thu";
            dgridBaoCao.Columns[0].Width = 100;
            dgridBaoCao.Columns[1].Width = 130;
            dgridBaoCao.Columns[2].Width = 140;
            dgridBaoCao.Columns[3].Width = 130;
            dgridBaoCao.Columns[4].Width = 130;
            dgridBaoCao.Columns[5].Width = 140;
            dgridBaoCao.Columns[6].Width = 130;
            dgridBaoCao.AllowUserToAddRows = false;
            dgridBaoCao.EditMode = DataGridViewEditMode.EditProgrammatically;

            //Khai báo các giá trị cần tính
            int sumSoDonDatHang = 0;
            int sumSoSanPhamDaNhap = 0;
            decimal sumChiPhiNhapHang = 0;
            int sumSoDonHang = 0;
            int sumSoSanPhamDaBan = 0;
            decimal sumDoanhThu = 0;
            //Duyệt qua từng hàng 
            for (int i = 0; i < tblBaocaoDT.Rows.Count; ++i)
            {
                sumSoDonDatHang += Convert.ToInt32(tblBaocaoDT.Rows[i]["SoluongHDN"]);
                sumSoSanPhamDaNhap += Convert.ToInt32(tblBaocaoDT.Rows[i]["Soluongnhap"]);
                sumChiPhiNhapHang += Convert.ToDecimal(tblBaocaoDT.Rows[i]["Chiphinhap"]);
                sumSoDonHang += Convert.ToInt32(tblBaocaoDT.Rows[i]["SoLuongHoaDon"]);
                sumSoSanPhamDaBan += Convert.ToInt32(tblBaocaoDT.Rows[i]["Soluongsanpham"]);
                sumDoanhThu += Convert.ToDecimal(tblBaocaoDT.Rows[i]["TongDoanhThu"]);
            }
            //Hiển thị lên form
            lblTongHDN.Text = "Tổng số hoá đơn nhập: " + sumSoDonDatHang;
            lblTongSPN.Text = "Tổng số xe máy nhập vào: " + sumSoSanPhamDaNhap;
            lblTongchiphi.Text = "Tổng chi phí nhập: " + sumChiPhiNhapHang;
            lblTongHDB.Text = "Tổng số Đơn đặt hàng bán: " + sumSoDonHang;
            lblTongSPB.Text = "Tổng số xe máy bán ra: " + sumSoSanPhamDaBan;
            lblTongdoanhthu.Text = "Tổng doanh thu: " + sumDoanhThu;
            return;


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
                series.XValueMember = "Ngay";
                series.YValueMembers = "TongDoanhThu";
                chart.Series.Add(series);
                series.Name = "Tổng doanh thu";
                chart.DataSource = tblBaocaoDT;
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
                series.XValueMember = "Ngay";
                series.YValueMembers = "Chiphinhap";
                chart1.Series.Add(series);
                series.Name = "Tổng chi phí nhập hàng";

                chart1.DataSource = tblBaocaoDT;
                chart1.DataBind();
            }
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
            dgridBaoCao.CurrentCell = null;
            lblTongHDN.Text = "Tổng số hoá đơn nhập: ";
            lblTongSPN.Text = "Tổng số xe máy nhập vào: ";
            lblTongchiphi.Text = "Tổng chi phí nhập: ";
            lblTongHDB.Text = "Tổng số đơn đặt hàng: ";
            lblTongSPB.Text = "Tổng số xe máy bán ra: ";
            lblTongdoanhthu.Text = "Tổng doanh thu: ";
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

        private void btnHienthi_Click(object sender, EventArgs e)
        {
            Load_Baocaongay();
            Load_Baocaoquy();
            Load_Baocaothang();
            Load_Baocaonam();
            LoadChartData();
            LoadChartDataSP();
        }
        private void Load_Baocaongay()
        {
            if (cboThoigian.Text == "Ngày")
            {

                DateTime ngayBatDau = dtpBD.Value;
                DateTime ngayKetThuc = dtpKT.Value;
                if (ngayKetThuc < ngayBatDau)
                {
                    MessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtpKT.Focus();
                    return;

                }
                if (ngayBatDau >= DateTime.Today.AddDays(1) || ngayKetThuc >= DateTime.Today.AddDays(1))
                {

                    MessageBox.Show("Thời gian đã chọn chưa đủ số liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtpBD.Value = DateTime.Today;
                    dtpKT.Value = DateTime.Today;
                    return;
                }
                string sql = @"
                    WITH Ngay AS (
                    SELECT CAST('" + ngayBatDau.ToString("yyyy-MM-dd") + @"' AS DATE) AS Ngay
                     UNION ALL
                     SELECT DATEADD(day, 1, Ngay)
                     FROM Ngay
                     WHERE DATEADD(day, 1, Ngay) <= CAST('" + ngayKetThuc.ToString("yyyy-MM-dd") + @"' AS DATE)
                           )
                     SELECT Ngay.Ngay, 
                      COALESCE(COUNT( DISTINCT tblHoadonnhap.SoHDN), 0) AS SoluongHDN,
                      COALESCE(SUM(  tblChitietHoadonnhap.Soluong), 0) AS Soluongnhap,
                      COALESCE(SUM(  tblChitietHoadonnhap.Thanhtien), 0) AS Chiphinhap,
                      COALESCE(COUNT(DISTINCT tblDonDatHang.SoDDH), 0) AS SoLuongHoaDon, 
                      COALESCE(SUM(  tblChitietDonDatHang.Soluong), 0) AS Soluongsanpham, 
                      COALESCE(SUM(  tblChitietDonDatHang.Thanhtien), 0) AS TongDoanhThu 
                        FROM Ngay
                        LEFT JOIN tblDonDatHang ON CONVERT(DATE, Ngay.Ngay) = CONVERT(DATE, tblDonDatHang.NgayMua)
                        LEFT JOIN tblHoadonnhap ON CONVERT(DATE, Ngay.Ngay) = CONVERT(DATE, tblHoadonnhap.Ngaynhap)
                        LEFT JOIN tblChitietDonDatHang ON tblDonDatHang.SoDDH = tblChitietDonDatHang.SoDDH
                        LEFT JOIN tblDMHang ON tblChitietDonDatHang.MaHang = tblDMHang.MaHang
                        LEFT JOIN tblChitietHoaDonNhap ON tblHoadonnhap.SoHDN = tblChitietHoaDonNhap.SoHDN
                        GROUP BY Ngay.Ngay
                        ORDER BY Ngay.Ngay
                        OPTION (MAXRECURSION 0);
                        ";
                tblBaocaoDT = Functions.getdatatotable(sql);
                dgridBaoCao.DataSource = tblBaocaoDT;
                dgridBaoCao.Columns[0].HeaderText = "Ngày";
                dgridBaoCao.Columns[1].HeaderText = "Số hoá đơn nhập";
                dgridBaoCao.Columns[2].HeaderText = "Số xe máy đã nhập";
                dgridBaoCao.Columns[3].HeaderText = "Chi phí nhập hàng";
                dgridBaoCao.Columns[4].HeaderText = "Số đơn đặt hàng";
                dgridBaoCao.Columns[5].HeaderText = "Số xe máy đã bán";
                dgridBaoCao.Columns[6].HeaderText = "Doanh thu";
                dgridBaoCao.Columns[0].Width = 100;
                dgridBaoCao.Columns[1].Width = 130;
                dgridBaoCao.Columns[2].Width = 140;
                dgridBaoCao.Columns[3].Width = 130;
                dgridBaoCao.Columns[4].Width = 130;
                dgridBaoCao.Columns[5].Width = 140;
                dgridBaoCao.Columns[6].Width = 130;
                dgridBaoCao.AllowUserToAddRows = false;
                dgridBaoCao.EditMode = DataGridViewEditMode.EditProgrammatically;
                LoadChartData();
                gpbThoigian.Enabled = false;

                int sumSoHoaDonNhap = 0;
                int sumSoSanPhamDaNhap = 0;
                decimal sumChiPhiNhapHang = 0;
                int sumSoDonHang = 0;
                int sumSoSanPhamDaBan = 0;
                decimal sumDoanhThu = 0;

                for (int i = 0; i < tblBaocaoDT.Rows.Count; ++i)
                {
                    sumSoHoaDonNhap += Convert.ToInt32(tblBaocaoDT.Rows[i]["SoluongHDN"]);
                    sumSoSanPhamDaNhap += Convert.ToInt32(tblBaocaoDT.Rows[i]["Soluongnhap"]);
                    sumChiPhiNhapHang += Convert.ToDecimal(tblBaocaoDT.Rows[i]["Chiphinhap"]);
                    sumSoDonHang += Convert.ToInt32(tblBaocaoDT.Rows[i]["SoLuongHoaDon"]);
                    sumSoSanPhamDaBan += Convert.ToInt32(tblBaocaoDT.Rows[i]["Soluongsanpham"]);
                    sumDoanhThu += Convert.ToDecimal(tblBaocaoDT.Rows[i]["TongDoanhThu"]);
                }
                lblTongHDN.Text = "Tổng số hoá đơn nhập: " + sumSoHoaDonNhap;
                lblTongSPN.Text = "Tổng số xe máy nhập vào: " + sumSoSanPhamDaNhap;
                lblTongchiphi.Text = "Tổng chi phí nhập: " + sumChiPhiNhapHang;
                lblTongHDB.Text = "Tổng số đơn đặt hàng: " + sumSoDonHang;
                lblTongSPB.Text = "Tổng số xe máy bán ra: " + sumSoSanPhamDaBan;
                lblTongdoanhthu.Text = "Tổng doanh thu: " + sumDoanhThu;
                return;

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
                    return;
                }


                if (cboKT.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn tháng kết thúc", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboKT.Focus();
                    dgridBaoCao.DataSource = null;
                    return;

                }
                if (cboNamBD.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn năm bắt đầu", "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                    cboNamBD.Focus();
                    dgridBaoCao.DataSource = null;
                    return;
                }
                if (cboNamKT.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn năm kết thúc", "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                    cboNamKT.Focus();
                    dgridBaoCao.DataSource = null;
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

                    return;
                }
                if (namBD > namKT)

                {
                    MessageBox.Show("Thời gian bắt đầu không được lớn hơn thời gian kết thúc", "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                    cboNamBD.Focus();
                    dgridBaoCao.DataSource = null;
                    return;
                }
                if (namBD == DateTime.Today.Year && BD > DateTime.Today.Month)
                {
                    MessageBox.Show("Thời gian đã chọn chưa đủ số liệu", "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                    cboBD.Focus();
                    dgridBaoCao.DataSource = null;
                    return;

                }
                if (namKT == DateTime.Today.Year && KT > DateTime.Today.Month)
                {
                    MessageBox.Show("Thời gian đã chọn chưa đủ số liệu", "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                    cboKT.Focus();
                    dgridBaoCao.DataSource = null;
                    return;

                }

                string sql = @"
                    WITH Ngay AS (
                        SELECT DATEFROMPARTS(" + namBD + @", " + BD + @", 1) AS Ngay
                        UNION ALL
                        SELECT DATEADD(MONTH, 1, Ngay)
                        FROM Ngay
                        WHERE DATEADD(MONTH, 1, Ngay) <= DATEFROMPARTS(" + namKT + @", " + KT + @", 1)
                    )
                    SELECT FORMAT(Ngay.Ngay, 'MM/yyyy') AS Ngay, 
                      COALESCE(COUNT( DISTINCT tblHoadonnhap.SoHDN), 0) AS SoluongHDN,
                      COALESCE(SUM(  tblChitietHoaDonNhap.Soluong), 0) AS Soluongnhap,
                      COALESCE(SUM(  tblChitietHoaDonNhap.Thanhtien), 0) AS Chiphinhap,
                      COALESCE(COUNT(DISTINCT tblDonDatHang.SoDDH), 0) AS SoLuongHoaDon, 
                      COALESCE(SUM(  tblChitietDonDatHang.Soluong), 0) AS Soluongsanpham, 
                      COALESCE(SUM(  tblChitietDonDatHang.Thanhtien), 0) AS TongDoanhThu  
                    FROM Ngay
                    LEFT JOIN tblDonDatHang ON MONTH(Ngay.Ngay) = MONTH(tblDonDatHang.NgayMua) AND YEAR(Ngay.Ngay) = YEAR(tblDonDatHang.NgayMua)
                    LEFT JOIN tblHoadonnhap ON MONTH(Ngay.Ngay) = MONTH(tblHoadonnhap.Ngaynhap) AND YEAR(Ngay.Ngay) = YEAR(tblHoadonnhap.Ngaynhap)       
                    LEFT JOIN tblChitietDonDatHang ON tblDonDatHang.SoDDH = tblChitietDonDatHang.SoDDH
                    LEFT JOIN tblDMHang ON tblChitietDonDatHang.MaHang = tblDMHang.MaHang
                    LEFT JOIN tblChitietHoaDonNhap ON tblHoadonnhap.SoHDN = tblChitietHoaDonNhap.SoHDN
                    GROUP BY FORMAT(Ngay.Ngay, 'MM/yyyy'), MONTH(Ngay.Ngay), YEAR(Ngay.Ngay)
                    ORDER BY YEAR(Ngay.Ngay), MONTH(Ngay.Ngay)
                    OPTION (MAXRECURSION 0);
                    ";


                tblBaocaoDT = Functions.getdatatotable(sql);
                dgridBaoCao.DataSource = tblBaocaoDT;
                dgridBaoCao.Columns[0].HeaderText = "Tháng";
                dgridBaoCao.Columns[1].HeaderText = "Số hoá đơn nhập";
                dgridBaoCao.Columns[2].HeaderText = "Số xe máy đã nhập";
                dgridBaoCao.Columns[3].HeaderText = "Chi phí nhập hàng";
                dgridBaoCao.Columns[4].HeaderText = "Số đơn đặt hàng";
                dgridBaoCao.Columns[5].HeaderText = "Số xe máy đã bán";
                dgridBaoCao.Columns[6].HeaderText = "Doanh thu";
                dgridBaoCao.Columns[0].Width = 100;
                dgridBaoCao.Columns[1].Width = 130;
                dgridBaoCao.Columns[2].Width = 140;
                dgridBaoCao.Columns[3].Width = 130;
                dgridBaoCao.Columns[4].Width = 130;
                dgridBaoCao.Columns[5].Width = 140;
                dgridBaoCao.Columns[6].Width = 130;
                dgridBaoCao.AllowUserToAddRows = false;
                dgridBaoCao.EditMode = DataGridViewEditMode.EditProgrammatically;
                LoadChartData();
                gpbThoigian.Enabled = false;

                int sumSoHoaDonNhap = 0;
                int sumSoSanPhamDaNhap = 0;
                decimal sumChiPhiNhapHang = 0;
                int sumSoDonHang = 0;
                int sumSoSanPhamDaBan = 0;
                decimal sumDoanhThu = 0;

                for (int i = 0; i < tblBaocaoDT.Rows.Count; ++i)
                {
                    sumSoHoaDonNhap += Convert.ToInt32(tblBaocaoDT.Rows[i]["SoluongHDN"]);
                    sumSoSanPhamDaNhap += Convert.ToInt32(tblBaocaoDT.Rows[i]["Soluongnhap"]);
                    sumChiPhiNhapHang += Convert.ToDecimal(tblBaocaoDT.Rows[i]["Chiphinhap"]);
                    sumSoDonHang += Convert.ToInt32(tblBaocaoDT.Rows[i]["SoLuongHoaDon"]);
                    sumSoSanPhamDaBan += Convert.ToInt32(tblBaocaoDT.Rows[i]["Soluongsanpham"]);
                    sumDoanhThu += Convert.ToDecimal(tblBaocaoDT.Rows[i]["TongDoanhThu"]);
                }
                lblTongHDN.Text = "Tổng số hoá đơn nhập: " + sumSoHoaDonNhap;
                lblTongSPN.Text = "Tổng số xe máy nhập vào: " + sumSoSanPhamDaNhap;
                lblTongchiphi.Text = "Tổng chi phí nhập: " + sumChiPhiNhapHang;
                lblTongHDB.Text = "Tổng số đơn đặt hàng: " + sumSoDonHang;
                lblTongSPB.Text = "Tổng số xe máy bán ra: " + sumSoSanPhamDaBan;
                lblTongdoanhthu.Text = "Tổng doanh thu: " + sumDoanhThu;
                return;

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
                    return;
                }
                if (cboKT.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn quý kết thúc", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboKT.Focus();
                    dgridBaoCao.DataSource = null;
                    return;
                }
                if (cboNamBD.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn năm bắt đầu", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNamBD.Focus();
                    dgridBaoCao.DataSource = null;
                    return;
                }
                if (cboNamKT.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn năm kết thúc", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNamKT.Focus();
                    dgridBaoCao.DataSource = null;
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
                    return;
                }
                if (namBD > namKT)
                {
                    MessageBox.Show("Thời gian bắt đầu không được lớn hơn thời gian kết thúc", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNamBD.Focus();
                    dgridBaoCao.DataSource = null;
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
                    return;
                }
                if (namKT == DateTime.Today.Year && quykt[2] > DateTime.Today.Month)
                {
                    MessageBox.Show("Thời gian đã chọn chưa đủ số liệu", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNamKT.Focus();
                    dgridBaoCao.DataSource = null;
                    return;
                }
                string sql = @"
                    WITH Ngay AS (
                        SELECT DATEFROMPARTS(" + namBD + @", " + quybd[0] + @", 1) AS Ngay
                        UNION ALL
                        SELECT DATEADD(MONTH, 1, Ngay)
                        FROM Ngay
                        WHERE DATEADD(MONTH, 1, Ngay) <= DATEFROMPARTS(" + namKT + @", " + quykt[2] + @", 1)
                    )
                    SELECT  CAST((MONTH(Ngay.Ngay)-1)/3 + 1 AS NVARCHAR(1)) + '/' + CAST(YEAR(Ngay.Ngay) AS NVARCHAR(4)) AS Ngay, 
                      COALESCE(COUNT( DISTINCT tblHoadonnhap.SoHDN), 0) AS SoluongHDN,
                      COALESCE(SUM( tblChitietHoaDonNhap.Soluong), 0) AS Soluongnhap,
                      COALESCE(SUM( tblChitietHoaDonNhap.Thanhtien), 0) AS Chiphinhap,
                      COALESCE(COUNT(DISTINCT tblDonDatHang.SoDDH), 0) AS SoLuongHoaDon, 
                      COALESCE(SUM(  tblChitietDonDatHang.Soluong), 0) AS Soluongsanpham, 
                      COALESCE(SUM(  tblChitietDonDatHang.Thanhtien), 0) AS TongDoanhThu 
                    FROM Ngay
                    LEFT JOIN tblDonDatHang ON MONTH(Ngay.Ngay) = MONTH(tblDonDatHang.NgayMua) AND YEAR(Ngay.Ngay) = YEAR(tblDonDatHang.NgayMua)
                    LEFT JOIN tblHoadonnhap ON MONTH(Ngay.Ngay) = MONTH(tblHoadonnhap.Ngaynhap) AND YEAR(Ngay.Ngay) = YEAR(tblHoadonnhap.Ngaynhap)
                    LEFT JOIN tblChitietDonDatHang ON tblDonDatHang.SoDDH = tblChitietDonDatHang.SoDDH
                    LEFT JOIN tblDMHang ON tblChitietDonDatHang.MaHang = tblDMHang.MaHang
                    LEFT JOIN tblChitietHoaDonNhap ON tblHoadonnhap.SoHDN = tblChitietHoaDonNhap.SoHDN
                    GROUP BY  CAST((MONTH(Ngay.Ngay)-1)/3 + 1 AS NVARCHAR(1)) + '/' + CAST(YEAR(Ngay.Ngay) AS NVARCHAR(4)), (MONTH(Ngay.Ngay)-1)/3 + 1, YEAR(Ngay.Ngay)
                    ORDER BY YEAR(Ngay.Ngay), (MONTH(Ngay.Ngay)-1)/3 + 1
                    OPTION (MAXRECURSION 0);
                    ";

                tblBaocaoDT = Functions.getdatatotable(sql);
                dgridBaoCao.DataSource = tblBaocaoDT;
                dgridBaoCao.Columns[0].HeaderText = "Quý";
                dgridBaoCao.Columns[1].HeaderText = "Số hoá đơn nhập";
                dgridBaoCao.Columns[2].HeaderText = "Số xe máy đã nhập";
                dgridBaoCao.Columns[3].HeaderText = "Chi phí nhập hàng";
                dgridBaoCao.Columns[4].HeaderText = "Số đơn đặt hàng";
                dgridBaoCao.Columns[5].HeaderText = "Số xe máy đã bán";
                dgridBaoCao.Columns[6].HeaderText = "Doanh thu";
                dgridBaoCao.Columns[0].Width = 100;
                dgridBaoCao.Columns[1].Width = 130;
                dgridBaoCao.Columns[2].Width = 140;
                dgridBaoCao.Columns[3].Width = 130;
                dgridBaoCao.Columns[4].Width = 130;
                dgridBaoCao.Columns[5].Width = 140;
                dgridBaoCao.Columns[6].Width = 130;
                dgridBaoCao.AllowUserToAddRows = false;
                dgridBaoCao.EditMode = DataGridViewEditMode.EditProgrammatically;
                LoadChartData();
                gpbThoigian.Enabled = false;

                int sumSoHoaDonNhap = 0;
                int sumSoSanPhamDaNhap = 0;
                decimal sumChiPhiNhapHang = 0;
                int sumSoDonHang = 0;
                int sumSoSanPhamDaBan = 0;
                decimal sumDoanhThu = 0;

                for (int i = 0; i < tblBaocaoDT.Rows.Count; ++i)
                {
                    sumSoHoaDonNhap += Convert.ToInt32(tblBaocaoDT.Rows[i]["SoluongHDN"]);
                    sumSoSanPhamDaNhap += Convert.ToInt32(tblBaocaoDT.Rows[i]["Soluongnhap"]);
                    sumChiPhiNhapHang += Convert.ToDecimal(tblBaocaoDT.Rows[i]["Chiphinhap"]);
                    sumSoDonHang += Convert.ToInt32(tblBaocaoDT.Rows[i]["SoLuongHoaDon"]);
                    sumSoSanPhamDaBan += Convert.ToInt32(tblBaocaoDT.Rows[i]["Soluongsanpham"]);
                    sumDoanhThu += Convert.ToDecimal(tblBaocaoDT.Rows[i]["TongDoanhThu"]);
                }
                lblTongHDN.Text = "Tổng số hoá đơn nhập: " + sumSoHoaDonNhap;
                lblTongSPN.Text = "Tổng số xe máy nhập vào: " + sumSoSanPhamDaNhap;
                lblTongchiphi.Text = "Tổng chi phí nhập: " + sumChiPhiNhapHang;
                lblTongHDB.Text = "Tổng số đơn đặt hàng: " + sumSoDonHang;
                lblTongSPB.Text = "Tổng số xe máy bán ra: " + sumSoSanPhamDaBan;
                lblTongdoanhthu.Text = "Tổng doanh thu: " + sumDoanhThu;
                return;
            }
        }
        private void Load_Baocaonam()
        {

            if (cboThoigian.Text == "Năm")
            {
                if (cboThoigian.Text == "Năm")
                {
                    if (cboBD.Text.Length == 0)
                    {
                        MessageBox.Show("Bạn phải chọn năm bắt đầu", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboBD.Focus();
                        dgridBaoCao.DataSource = null;
                        return;
                    }

                    if (cboKT.Text.Length == 0)
                    {
                        MessageBox.Show("Bạn phải chọn năm kết thúc", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboKT.Focus();
                        dgridBaoCao.DataSource = null;
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
                        return;
                    }

                    if (namBD > DateTime.Today.Year || namKT > DateTime.Today.Year)
                    {
                        MessageBox.Show("Thời gian đã chọn chưa đủ số liệu", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboBD.Focus();
                        dgridBaoCao.DataSource = null;
                        return;
                    }

                    string sql = @"
                    WITH Ngay AS (
                        SELECT DATEFROMPARTS(" + namBD + @", 1, 1) AS Ngay
                        UNION ALL
                        SELECT DATEADD(YEAR, 1, Ngay)
                        FROM Ngay
                        WHERE DATEADD(YEAR, 1, Ngay) <= DATEFROMPARTS(" + namKT + @", 1, 1)
                    )
                    SELECT YEAR(Ngay.Ngay) AS Ngay, 
                      COALESCE(COUNT( DISTINCT tblHoadonnhap.SoHDN), 0) AS SoluongHDN,
                      COALESCE(SUM( tblChitietHoaDonNhap.Soluong), 0) AS Soluongnhap,
                      COALESCE(SUM( tblChitietHoaDonNhap.Thanhtien), 0) AS Chiphinhap,
                      COALESCE(COUNT(DISTINCT tblDonDatHang.SoDDH), 0) AS SoLuongHoaDon, 
                      COALESCE(SUM(  tblChitietDonDatHang.Soluong), 0) AS Soluongsanpham, 
                      COALESCE(SUM(  tblChitietDonDatHang.Thanhtien), 0) AS TongDoanhThu 
                    FROM Ngay
                    LEFT JOIN tblDonDatHang ON YEAR(Ngay) = YEAR(tblDonDatHang.NgayMua)
                    LEFT JOIN tblHoadonnhap ON YEAR(Ngay) = YEAR(tblHoadonnhap.Ngaynhap)
                    LEFT JOIN tblChitietDonDatHang ON tblDonDatHang.SoDDH = tblChitietDonDatHang.SoDDH
                    LEFT JOIN tblDMHang ON tblChitietDonDatHang.MaHang = tblDMHang.MaHang
                    LEFT JOIN tblChitietHoaDonNhap ON tblHoadonnhap.SoHDN = tblChitietHoaDonNhap.SoHDN
                    GROUP BY Ngay
                    ORDER BY Ngay
                    OPTION (MAXRECURSION 0);
                    ";

                    tblBaocaoDT = Functions.getdatatotable(sql);
                    dgridBaoCao.DataSource = tblBaocaoDT;
                    dgridBaoCao.Columns[0].HeaderText = "Năm";
                    dgridBaoCao.Columns[1].HeaderText = "Số hoá đơn nhập";
                    dgridBaoCao.Columns[2].HeaderText = "Số xe máy đã nhập";
                    dgridBaoCao.Columns[3].HeaderText = "Chi phí nhập hàng";
                    dgridBaoCao.Columns[4].HeaderText = "Số đơn đặt hàng";
                    dgridBaoCao.Columns[5].HeaderText = "Số xe máy đã bán";
                    dgridBaoCao.Columns[6].HeaderText = "Doanh thu";
                    dgridBaoCao.Columns[0].Width = 100;
                    dgridBaoCao.Columns[1].Width = 130;
                    dgridBaoCao.Columns[2].Width = 140;
                    dgridBaoCao.Columns[3].Width = 130;
                    dgridBaoCao.Columns[4].Width = 130;
                    dgridBaoCao.Columns[5].Width = 140;
                    dgridBaoCao.Columns[6].Width = 130;
                    dgridBaoCao.AllowUserToAddRows = false;
                    dgridBaoCao.EditMode = DataGridViewEditMode.EditProgrammatically;
                    LoadChartData();
                    gpbThoigian.Enabled = false;

                    int sumSoHoaDonNhap = 0;
                    int sumSoSanPhamDaNhap = 0;
                    decimal sumChiPhiNhapHang = 0;
                    int sumSoDonHang = 0;
                    int sumSoSanPhamDaBan = 0;
                    decimal sumDoanhThu = 0;

                    for (int i = 0; i < tblBaocaoDT.Rows.Count; ++i)
                    {
                        sumSoHoaDonNhap += Convert.ToInt32(tblBaocaoDT.Rows[i]["SoluongHDN"]);
                        sumSoSanPhamDaNhap += Convert.ToInt32(tblBaocaoDT.Rows[i]["Soluongnhap"]);
                        sumChiPhiNhapHang += Convert.ToDecimal(tblBaocaoDT.Rows[i]["Chiphinhap"]);
                        sumSoDonHang += Convert.ToInt32(tblBaocaoDT.Rows[i]["SoLuongHoaDon"]);
                        sumSoSanPhamDaBan += Convert.ToInt32(tblBaocaoDT.Rows[i]["Soluongsanpham"]);
                        sumDoanhThu += Convert.ToDecimal(tblBaocaoDT.Rows[i]["TongDoanhThu"]);
                    }
                    lblTongHDN.Text = "Tổng số hoá đơn nhập: " + sumSoHoaDonNhap;
                    lblTongSPN.Text = "Tổng số xe máy nhập vào: " + sumSoSanPhamDaNhap;
                    lblTongchiphi.Text = "Tổng chi phí nhập: " + sumChiPhiNhapHang;
                    lblTongHDB.Text = "Tổng số đơn đặt hàng: " + sumSoDonHang;
                    lblTongSPB.Text = "Tổng số xe máy bán ra: " + sumSoSanPhamDaBan;
                    lblTongdoanhthu.Text = "Tổng doanh thu: " + sumDoanhThu;
                    return;

                }
            }

        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            cboThoigian.SelectedIndex = 0;
            Load_Baocaohientai();
            LoadChartData();
            LoadChartDataSP();
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

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
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
            worksheet.Cells[2, 1] = "Địa chỉ: 13 Chùa Bộc, Quang Trung, Đống Đa, Hà Nội ";
            worksheet.Cells[2, 1].Font.Color = Color.Blue;
            worksheet.Cells[3, 1] = "Số điện thoại: 070 531 1047 ";
            worksheet.Cells[3, 1].Font.Color = Color.Blue;

            Excel.Range mergeRange = worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[4, 9]];
            mergeRange.Merge();
            mergeRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            mergeRange.Value = "BÁO CÁO TÌNH HÌNH KINH DOANH";
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

            worksheet.Cells[5, 8] = lblTongHDN.Text;
            worksheet.Cells[5, 8].Interior.Color = Color.LightPink;
            worksheet.Cells[6, 8] = lblTongSPN.Text;
            worksheet.Cells[6, 8].Interior.Color = Color.LightPink;
            worksheet.Cells[7, 8] = lblTongchiphi.Text;
            worksheet.Cells[7, 8].Interior.Color = Color.LightPink;
            worksheet.Cells[8, 8] = lblTongHDB.Text;
            worksheet.Cells[8, 8].Interior.Color = Color.LightPink;
            worksheet.Cells[9, 8] = lblTongSPB.Text;
            worksheet.Cells[9, 8].Interior.Color = Color.LightPink;
            worksheet.Cells[10, 8] = lblTongdoanhthu.Text;
            worksheet.Cells[10, 8].Interior.Color = Color.LightPink;

            // Đặt tiêu đề các cột và điền số thứ tự
            worksheet.Cells[13, 2] = "STT";
            worksheet.Cells[13, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            worksheet.Cells[13, 2].Borders.Weight = Excel.XlBorderWeight.xlThin;
            worksheet.Cells[13, 2].Interior.Color = Color.LightPink;
            worksheet.Cells[13, 2].Font.Size = 13;
            worksheet.Cells[13, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            for (int i = 1; i <= dgridBaoCao.Columns.Count; i++)
            {
                worksheet.Cells[13, i + 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                worksheet.Cells[13, i + 2].Borders.Weight = Excel.XlBorderWeight.xlThin;
                worksheet.Cells[13, i + 2].Value = dgridBaoCao.Columns[i - 1].HeaderText;
                worksheet.Cells[13, i + 2].Interior.Color = Color.LightPink;
                worksheet.Cells[13, i + 2].Font.Size = 13;
                worksheet.Cells[13, i + 2].EntireColumn.AutoFit();
                worksheet.Cells[13, i + 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }

            // Điền số thứ tự và đổ dữ liệu từ DataGridView vào Excel
            for (int i = 0; i < dgridBaoCao.Rows.Count; i++)
            {
                worksheet.Cells[i + 14, 2].Value = i + 1; // Điền số thứ tự
                worksheet.Cells[i + 14, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                worksheet.Cells[i + 14, 2].Borders.Weight = Excel.XlBorderWeight.xlThin;
                worksheet.Cells[i + 14, 2].Font.Size = 13;
                worksheet.Cells[i + 14, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }
            for (int j = 0; j < dgridBaoCao.Columns.Count; j++)
                for (int i = 0; i < dgridBaoCao.Rows.Count; i++)
                {
                    if (cboThoigian.Text == "Quý" || cboThoigian.Text == "Tháng")
                    {

                        if (j == 0)
                        {
                            worksheet.Cells[i + 14, j + 3].Numberformat = "mm-yyyy";
                        }
                        worksheet.Cells[i + 14, j + 3].Value = dgridBaoCao.Rows[i].Cells[j].Value;
                        worksheet.Cells[i + 14, j + 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        worksheet.Cells[i + 14, j + 3].Borders.Weight = Excel.XlBorderWeight.xlThin;
                        worksheet.Cells[i + 14, j + 3].Font.Size = 13;
                        worksheet.Cells[i + 14, j + 3].EntireColumn.AutoFit();
                        worksheet.Cells[i + 14, j + 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    }
                }


            for (int j = 0; j < dgridBaoCao.Columns.Count; j++)
                for (int i = 0; i < dgridBaoCao.Rows.Count; i++)
                {

                    worksheet.Cells[i + 14, j + 3].Value = dgridBaoCao.Rows[i].Cells[j].Value;
                    worksheet.Cells[i + 14, j + 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    worksheet.Cells[i + 14, j + 3].Borders.Weight = Excel.XlBorderWeight.xlThin;
                    worksheet.Cells[i + 14, j + 3].Font.Size = 13;
                    worksheet.Cells[i + 14, j + 3].EntireColumn.AutoFit();
                    worksheet.Cells[i + 14, j + 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                }
        }
    }
}
