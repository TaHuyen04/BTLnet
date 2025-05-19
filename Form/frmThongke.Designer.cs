namespace QLCHBanXeMay.form
{
    partial class frmThongke
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblTongdoanhthu = new System.Windows.Forms.Label();
            this.lblTongHDN = new System.Windows.Forms.Label();
            this.lblTongSPN = new System.Windows.Forms.Label();
            this.lblTongHDB = new System.Windows.Forms.Label();
            this.lblTongchiphi = new System.Windows.Forms.Label();
            this.dtpKT = new System.Windows.Forms.DateTimePicker();
            this.dtpBD = new System.Windows.Forms.DateTimePicker();
            this.cboNamKT = new System.Windows.Forms.ComboBox();
            this.cboNamBD = new System.Windows.Forms.ComboBox();
            this.btnHienthi = new System.Windows.Forms.Button();
            this.cboKT = new System.Windows.Forms.ComboBox();
            this.cboBD = new System.Windows.Forms.ComboBox();
            this.lblNam2 = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnXuatexcel = new System.Windows.Forms.Button();
            this.lblNam1 = new System.Windows.Forms.Label();
            this.lblKT = new System.Windows.Forms.Label();
            this.lblBD = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTongSPB = new System.Windows.Forms.Label();
            this.btnLammoi = new System.Windows.Forms.Button();
            this.dgridBaocao = new System.Windows.Forms.DataGridView();
            this.gpbThoigian = new System.Windows.Forms.GroupBox();
            this.cboThoigian = new System.Windows.Forms.ComboBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgridBaocao)).BeginInit();
            this.gpbThoigian.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTongdoanhthu
            // 
            this.lblTongdoanhthu.AutoSize = true;
            this.lblTongdoanhthu.Location = new System.Drawing.Point(643, 635);
            this.lblTongdoanhthu.Name = "lblTongdoanhthu";
            this.lblTongdoanhthu.Size = new System.Drawing.Size(103, 16);
            this.lblTongdoanhthu.TabIndex = 129;
            this.lblTongdoanhthu.Text = "Tổng doanh thu:";
            // 
            // lblTongHDN
            // 
            this.lblTongHDN.AutoSize = true;
            this.lblTongHDN.Location = new System.Drawing.Point(32, 603);
            this.lblTongHDN.Name = "lblTongHDN";
            this.lblTongHDN.Size = new System.Drawing.Size(145, 16);
            this.lblTongHDN.TabIndex = 127;
            this.lblTongHDN.Text = "Tổng số hoá đơn nhập:";
            // 
            // lblTongSPN
            // 
            this.lblTongSPN.AutoSize = true;
            this.lblTongSPN.Location = new System.Drawing.Point(323, 603);
            this.lblTongSPN.Name = "lblTongSPN";
            this.lblTongSPN.Size = new System.Drawing.Size(181, 16);
            this.lblTongSPN.TabIndex = 126;
            this.lblTongSPN.Text = "Tổng số sản phẩm nhập vào:";
            // 
            // lblTongHDB
            // 
            this.lblTongHDB.AutoSize = true;
            this.lblTongHDB.Location = new System.Drawing.Point(32, 635);
            this.lblTongHDB.Name = "lblTongHDB";
            this.lblTongHDB.Size = new System.Drawing.Size(138, 16);
            this.lblTongHDB.TabIndex = 125;
            this.lblTongHDB.Text = "Tổng số hoá đơn bán:";
            // 
            // lblTongchiphi
            // 
            this.lblTongchiphi.AutoSize = true;
            this.lblTongchiphi.Location = new System.Drawing.Point(643, 603);
            this.lblTongchiphi.Name = "lblTongchiphi";
            this.lblTongchiphi.Size = new System.Drawing.Size(119, 16);
            this.lblTongchiphi.TabIndex = 124;
            this.lblTongchiphi.Text = "Tổng chi phí nhập: ";
            // 
            // dtpKT
            // 
            this.dtpKT.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpKT.Location = new System.Drawing.Point(372, 85);
            this.dtpKT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpKT.Name = "dtpKT";
            this.dtpKT.Size = new System.Drawing.Size(128, 22);
            this.dtpKT.TabIndex = 1;
            this.dtpKT.Visible = false;
            // 
            // dtpBD
            // 
            this.dtpBD.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBD.Location = new System.Drawing.Point(372, 26);
            this.dtpBD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpBD.Name = "dtpBD";
            this.dtpBD.Size = new System.Drawing.Size(128, 22);
            this.dtpBD.TabIndex = 1;
            this.dtpBD.Visible = false;
            // 
            // cboNamKT
            // 
            this.cboNamKT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNamKT.FormattingEnabled = true;
            this.cboNamKT.Location = new System.Drawing.Point(572, 80);
            this.cboNamKT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboNamKT.Name = "cboNamKT";
            this.cboNamKT.Size = new System.Drawing.Size(81, 24);
            this.cboNamKT.TabIndex = 37;
            this.cboNamKT.Visible = false;
            // 
            // cboNamBD
            // 
            this.cboNamBD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNamBD.FormattingEnabled = true;
            this.cboNamBD.Location = new System.Drawing.Point(572, 22);
            this.cboNamBD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboNamBD.Name = "cboNamBD";
            this.cboNamBD.Size = new System.Drawing.Size(81, 24);
            this.cboNamBD.TabIndex = 36;
            this.cboNamBD.Visible = false;
            // 
            // btnHienthi
            // 
            this.btnHienthi.BackColor = System.Drawing.Color.SteelBlue;
            this.btnHienthi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHienthi.ForeColor = System.Drawing.Color.Snow;
            this.btnHienthi.Location = new System.Drawing.Point(701, 44);
            this.btnHienthi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHienthi.Name = "btnHienthi";
            this.btnHienthi.Size = new System.Drawing.Size(120, 36);
            this.btnHienthi.TabIndex = 92;
            this.btnHienthi.Text = "Hiển thị";
            this.btnHienthi.UseVisualStyleBackColor = false;
            // 
            // cboKT
            // 
            this.cboKT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKT.FormattingEnabled = true;
            this.cboKT.Location = new System.Drawing.Point(372, 83);
            this.cboKT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboKT.Name = "cboKT";
            this.cboKT.Size = new System.Drawing.Size(81, 24);
            this.cboKT.TabIndex = 35;
            this.cboKT.Visible = false;
            // 
            // cboBD
            // 
            this.cboBD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBD.FormattingEnabled = true;
            this.cboBD.Location = new System.Drawing.Point(372, 25);
            this.cboBD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboBD.Name = "cboBD";
            this.cboBD.Size = new System.Drawing.Size(81, 24);
            this.cboBD.TabIndex = 34;
            this.cboBD.Visible = false;
            // 
            // lblNam2
            // 
            this.lblNam2.AutoSize = true;
            this.lblNam2.Location = new System.Drawing.Point(521, 82);
            this.lblNam2.Name = "lblNam2";
            this.lblNam2.Size = new System.Drawing.Size(36, 16);
            this.lblNam2.TabIndex = 33;
            this.lblNam2.Text = "Năm";
            this.lblNam2.Visible = false;
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.SteelBlue;
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(1187, 687);
            this.btnDong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(112, 38);
            this.btnDong.TabIndex = 122;
            this.btnDong.Text = "Đóng";
            this.btnDong.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDong.UseVisualStyleBackColor = false;
            // 
            // btnXuatexcel
            // 
            this.btnXuatexcel.BackColor = System.Drawing.Color.SteelBlue;
            this.btnXuatexcel.ForeColor = System.Drawing.Color.White;
            this.btnXuatexcel.Location = new System.Drawing.Point(1011, 690);
            this.btnXuatexcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXuatexcel.Name = "btnXuatexcel";
            this.btnXuatexcel.Size = new System.Drawing.Size(131, 38);
            this.btnXuatexcel.TabIndex = 121;
            this.btnXuatexcel.Text = "Xuất Excel";
            this.btnXuatexcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnXuatexcel.UseVisualStyleBackColor = false;
            // 
            // lblNam1
            // 
            this.lblNam1.AutoSize = true;
            this.lblNam1.Location = new System.Drawing.Point(521, 26);
            this.lblNam1.Name = "lblNam1";
            this.lblNam1.Size = new System.Drawing.Size(36, 16);
            this.lblNam1.TabIndex = 32;
            this.lblNam1.Text = "Năm";
            this.lblNam1.Visible = false;
            // 
            // lblKT
            // 
            this.lblKT.AutoSize = true;
            this.lblKT.Location = new System.Drawing.Point(328, 85);
            this.lblKT.Name = "lblKT";
            this.lblKT.Size = new System.Drawing.Size(31, 16);
            this.lblKT.TabIndex = 31;
            this.lblKT.Text = "Đến";
            // 
            // lblBD
            // 
            this.lblBD.AutoSize = true;
            this.lblBD.Location = new System.Drawing.Point(328, 27);
            this.lblBD.Name = "lblBD";
            this.lblBD.Size = new System.Drawing.Size(23, 16);
            this.lblBD.TabIndex = 30;
            this.lblBD.Text = "Từ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 16);
            this.label5.TabIndex = 28;
            this.label5.Text = "Báo cáo theo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(147, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 16);
            this.label4.TabIndex = 26;
            // 
            // lblTongSPB
            // 
            this.lblTongSPB.AutoSize = true;
            this.lblTongSPB.Location = new System.Drawing.Point(323, 635);
            this.lblTongSPB.Name = "lblTongSPB";
            this.lblTongSPB.Size = new System.Drawing.Size(163, 16);
            this.lblTongSPB.TabIndex = 128;
            this.lblTongSPB.Text = "Tổng số sản phẩm bán ra:";
            // 
            // btnLammoi
            // 
            this.btnLammoi.BackColor = System.Drawing.Color.SteelBlue;
            this.btnLammoi.ForeColor = System.Drawing.Color.White;
            this.btnLammoi.Location = new System.Drawing.Point(830, 687);
            this.btnLammoi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLammoi.Name = "btnLammoi";
            this.btnLammoi.Size = new System.Drawing.Size(132, 41);
            this.btnLammoi.TabIndex = 123;
            this.btnLammoi.Text = "Làm mới";
            this.btnLammoi.UseVisualStyleBackColor = false;
            // 
            // dgridBaocao
            // 
            this.dgridBaocao.BackgroundColor = System.Drawing.Color.White;
            this.dgridBaocao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridBaocao.Location = new System.Drawing.Point(21, 187);
            this.dgridBaocao.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgridBaocao.Name = "dgridBaocao";
            this.dgridBaocao.RowHeadersWidth = 20;
            this.dgridBaocao.RowTemplate.Height = 28;
            this.dgridBaocao.Size = new System.Drawing.Size(928, 398);
            this.dgridBaocao.TabIndex = 120;
            // 
            // gpbThoigian
            // 
            this.gpbThoigian.Controls.Add(this.dtpKT);
            this.gpbThoigian.Controls.Add(this.dtpBD);
            this.gpbThoigian.Controls.Add(this.cboNamKT);
            this.gpbThoigian.Controls.Add(this.cboNamBD);
            this.gpbThoigian.Controls.Add(this.btnHienthi);
            this.gpbThoigian.Controls.Add(this.cboKT);
            this.gpbThoigian.Controls.Add(this.cboBD);
            this.gpbThoigian.Controls.Add(this.lblNam2);
            this.gpbThoigian.Controls.Add(this.lblNam1);
            this.gpbThoigian.Controls.Add(this.lblKT);
            this.gpbThoigian.Controls.Add(this.lblBD);
            this.gpbThoigian.Controls.Add(this.cboThoigian);
            this.gpbThoigian.Controls.Add(this.label5);
            this.gpbThoigian.Controls.Add(this.label4);
            this.gpbThoigian.Location = new System.Drawing.Point(21, 49);
            this.gpbThoigian.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbThoigian.Name = "gpbThoigian";
            this.gpbThoigian.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbThoigian.Size = new System.Drawing.Size(928, 120);
            this.gpbThoigian.TabIndex = 119;
            this.gpbThoigian.TabStop = false;
            this.gpbThoigian.Text = "Chọn thời gian";
            // 
            // cboThoigian
            // 
            this.cboThoigian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboThoigian.FormattingEnabled = true;
            this.cboThoigian.Items.AddRange(new object[] {
            "Hôm nay",
            "Ngày",
            "Tháng",
            "Quý",
            "Năm"});
            this.cboThoigian.Location = new System.Drawing.Point(182, 24);
            this.cboThoigian.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboThoigian.Name = "cboThoigian";
            this.cboThoigian.Size = new System.Drawing.Size(108, 24);
            this.cboThoigian.TabIndex = 29;
            // 
            // chart1
            // 
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(993, 49);
            this.chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(413, 214);
            this.chart1.TabIndex = 130;
            this.chart1.Text = "chart1";
            // 
            // chart2
            // 
            chartArea4.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart2.Legends.Add(legend4);
            this.chart2.Location = new System.Drawing.Point(993, 369);
            this.chart2.Name = "chart2";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chart2.Series.Add(series4);
            this.chart2.Size = new System.Drawing.Size(413, 216);
            this.chart2.TabIndex = 131;
            this.chart2.Text = "chart2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(458, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(478, 33);
            this.label1.TabIndex = 132;
            this.label1.Text = "BÁO CÁO TÌNH HÌNH KINH DOANH";
            // 
            // frmThongke
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1448, 755);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.lblTongdoanhthu);
            this.Controls.Add(this.lblTongHDN);
            this.Controls.Add(this.lblTongSPN);
            this.Controls.Add(this.lblTongHDB);
            this.Controls.Add(this.lblTongchiphi);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnXuatexcel);
            this.Controls.Add(this.lblTongSPB);
            this.Controls.Add(this.btnLammoi);
            this.Controls.Add(this.dgridBaocao);
            this.Controls.Add(this.gpbThoigian);
            this.Name = "frmThongke";
            this.Text = "frmThongke";
            this.Load += new System.EventHandler(this.frmThongke_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgridBaocao)).EndInit();
            this.gpbThoigian.ResumeLayout(false);
            this.gpbThoigian.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTongdoanhthu;
        private System.Windows.Forms.Label lblTongHDN;
        private System.Windows.Forms.Label lblTongSPN;
        private System.Windows.Forms.Label lblTongHDB;
        private System.Windows.Forms.Label lblTongchiphi;
        private System.Windows.Forms.DateTimePicker dtpKT;
        private System.Windows.Forms.DateTimePicker dtpBD;
        private System.Windows.Forms.ComboBox cboNamKT;
        private System.Windows.Forms.ComboBox cboNamBD;
        private System.Windows.Forms.Button btnHienthi;
        private System.Windows.Forms.ComboBox cboKT;
        private System.Windows.Forms.ComboBox cboBD;
        private System.Windows.Forms.Label lblNam2;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnXuatexcel;
        private System.Windows.Forms.Label lblNam1;
        private System.Windows.Forms.Label lblKT;
        private System.Windows.Forms.Label lblBD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTongSPB;
        private System.Windows.Forms.Button btnLammoi;
        private System.Windows.Forms.DataGridView dgridBaocao;
        private System.Windows.Forms.GroupBox gpbThoigian;
        private System.Windows.Forms.ComboBox cboThoigian;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.Label label1;
    }
}