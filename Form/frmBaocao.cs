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
            dgridNhanvien.CurrentCell = null;
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
            dgridNhanvien.DataSource = tblBaocaoNV;
            dgridNhanvien.Columns[0].HeaderText = "Mã nhân viên";
            dgridNhanvien.Columns[1].HeaderText = "Tên nhân viên";
            dgridNhanvien.Columns[2].HeaderText = "Số đơn hàng đã bán";
            dgridNhanvien.Columns[3].HeaderText = "Tổng doanh thu";
            dgridNhanvien.Columns[0].Width = 110;
            dgridNhanvien.Columns[1].Width = 170;
            dgridNhanvien.Columns[2].Width = 160;
            dgridNhanvien.Columns[3].Width = 160;
            dgridNhanvien.AllowUserToAddRows = false;
            dgridNhanvien.EditMode = DataGridViewEditMode.EditProgrammatically;

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
            dgridNhanvien.DataSource = "";
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
            if (dgridNhanvien.Rows.Count == 0)
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
            if (dgridNhanvien.Rows.Count == 0)
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
                series.Name = "Tổng số đơn hàng";

                chart1.DataSource = tblBaocaoNV;
                chart1.DataBind();
            }
        }


    }
}
