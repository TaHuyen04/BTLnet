namespace QLCHBanXeMay.form
{
    partial class frmBaoCaoSanPham
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea11 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend11 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea12 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend12 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnXuatexcel = new System.Windows.Forms.Button();
            this.btnLammoi = new System.Windows.Forms.Button();
            this.btnHienthi = new System.Windows.Forms.Button();
            this.dtpKT = new System.Windows.Forms.DateTimePicker();
            this.dtpBD = new System.Windows.Forms.DateTimePicker();
            this.cboNamKT = new System.Windows.Forms.ComboBox();
            this.cboNamBD = new System.Windows.Forms.ComboBox();
            this.cboKT = new System.Windows.Forms.ComboBox();
            this.cboBD = new System.Windows.Forms.ComboBox();
            this.lblNam2 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNam1 = new System.Windows.Forms.Label();
            this.lblKT = new System.Windows.Forms.Label();
            this.lblBD = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgridXephang = new System.Windows.Forms.DataGridView();
            this.gpbThoigian = new System.Windows.Forms.GroupBox();
            this.cboThoigian = new System.Windows.Forms.ComboBox();
            this.dgridBaoCao = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgridXephang)).BeginInit();
            this.gpbThoigian.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgridBaoCao)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea11.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea11);
            legend11.Name = "Legend1";
            this.chart1.Legends.Add(legend11);
            this.chart1.Location = new System.Drawing.Point(854, 405);
            this.chart1.Name = "chart1";
            series11.ChartArea = "ChartArea1";
            series11.Legend = "Legend1";
            series11.Name = "Series1";
            this.chart1.Series.Add(series11);
            this.chart1.Size = new System.Drawing.Size(427, 213);
            this.chart1.TabIndex = 152;
            this.chart1.Text = "chart1";
            // 
            // btnXuatexcel
            // 
            this.btnXuatexcel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnXuatexcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnXuatexcel.Location = new System.Drawing.Point(1013, 709);
            this.btnXuatexcel.Name = "btnXuatexcel";
            this.btnXuatexcel.Size = new System.Drawing.Size(120, 33);
            this.btnXuatexcel.TabIndex = 149;
            this.btnXuatexcel.Text = "Xuất Excel";
            this.btnXuatexcel.UseVisualStyleBackColor = false;
            this.btnXuatexcel.Click += new System.EventHandler(this.btnXuatexcel_Click);
            // 
            // btnLammoi
            // 
            this.btnLammoi.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnLammoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnLammoi.Location = new System.Drawing.Point(864, 709);
            this.btnLammoi.Name = "btnLammoi";
            this.btnLammoi.Size = new System.Drawing.Size(120, 33);
            this.btnLammoi.TabIndex = 148;
            this.btnLammoi.Text = "Làm mới";
            this.btnLammoi.UseVisualStyleBackColor = false;
            this.btnLammoi.Click += new System.EventHandler(this.btnLammoi_Click);
            // 
            // btnHienthi
            // 
            this.btnHienthi.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnHienthi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnHienthi.Location = new System.Drawing.Point(694, 39);
            this.btnHienthi.Name = "btnHienthi";
            this.btnHienthi.Size = new System.Drawing.Size(120, 33);
            this.btnHienthi.TabIndex = 137;
            this.btnHienthi.Text = "Hiển thị";
            this.btnHienthi.UseVisualStyleBackColor = false;
            this.btnHienthi.Click += new System.EventHandler(this.btnHienthi_Click);
            // 
            // dtpKT
            // 
            this.dtpKT.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpKT.Location = new System.Drawing.Point(372, 85);
            this.dtpKT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpKT.Name = "dtpKT";
            this.dtpKT.Size = new System.Drawing.Size(132, 22);
            this.dtpKT.TabIndex = 1;
            this.dtpKT.Visible = false;
            // 
            // dtpBD
            // 
            this.dtpBD.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBD.Location = new System.Drawing.Point(372, 26);
            this.dtpBD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpBD.Name = "dtpBD";
            this.dtpBD.Size = new System.Drawing.Size(132, 22);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12.2F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label2.Location = new System.Drawing.Point(10, 468);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(291, 24);
            this.label2.TabIndex = 147;
            this.label2.Text = "Dòng xe máy bán chạy nhất";
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnDong.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnDong.Location = new System.Drawing.Point(1180, 709);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(93, 33);
            this.btnDong.TabIndex = 150;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(443, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(456, 33);
            this.label1.TabIndex = 146;
            this.label1.Text = "BÁO CÁO DOANH SỐ SẢN PHẨM";
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
            // chart
            // 
            chartArea12.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea12);
            legend12.Name = "Legend1";
            this.chart.Legends.Add(legend12);
            this.chart.Location = new System.Drawing.Point(854, 77);
            this.chart.Name = "chart";
            series12.ChartArea = "ChartArea1";
            series12.Legend = "Legend1";
            series12.Name = "Series1";
            this.chart.Series.Add(series12);
            this.chart.Size = new System.Drawing.Size(427, 213);
            this.chart.TabIndex = 151;
            this.chart.Text = "chart2";
            // 
            // dgridXephang
            // 
            this.dgridXephang.BackgroundColor = System.Drawing.Color.White;
            this.dgridXephang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridXephang.Location = new System.Drawing.Point(12, 507);
            this.dgridXephang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgridXephang.Name = "dgridXephang";
            this.dgridXephang.RowHeadersWidth = 62;
            this.dgridXephang.RowTemplate.Height = 28;
            this.dgridXephang.Size = new System.Drawing.Size(836, 142);
            this.dgridXephang.TabIndex = 145;
            // 
            // gpbThoigian
            // 
            this.gpbThoigian.Controls.Add(this.btnHienthi);
            this.gpbThoigian.Controls.Add(this.dtpKT);
            this.gpbThoigian.Controls.Add(this.dtpBD);
            this.gpbThoigian.Controls.Add(this.cboNamKT);
            this.gpbThoigian.Controls.Add(this.cboNamBD);
            this.gpbThoigian.Controls.Add(this.cboKT);
            this.gpbThoigian.Controls.Add(this.cboBD);
            this.gpbThoigian.Controls.Add(this.lblNam2);
            this.gpbThoigian.Controls.Add(this.lblNam1);
            this.gpbThoigian.Controls.Add(this.lblKT);
            this.gpbThoigian.Controls.Add(this.lblBD);
            this.gpbThoigian.Controls.Add(this.cboThoigian);
            this.gpbThoigian.Controls.Add(this.label5);
            this.gpbThoigian.Controls.Add(this.label4);
            this.gpbThoigian.Location = new System.Drawing.Point(12, 50);
            this.gpbThoigian.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbThoigian.Name = "gpbThoigian";
            this.gpbThoigian.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbThoigian.Size = new System.Drawing.Size(836, 120);
            this.gpbThoigian.TabIndex = 143;
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
            this.cboThoigian.SelectedIndexChanged += new System.EventHandler(this.cboThoigian_SelectedIndexChanged);
            // 
            // dgridBaoCao
            // 
            this.dgridBaoCao.BackgroundColor = System.Drawing.Color.White;
            this.dgridBaoCao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridBaoCao.Location = new System.Drawing.Point(12, 187);
            this.dgridBaoCao.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgridBaoCao.Name = "dgridBaoCao";
            this.dgridBaoCao.RowHeadersWidth = 62;
            this.dgridBaoCao.RowTemplate.Height = 28;
            this.dgridBaoCao.Size = new System.Drawing.Size(836, 267);
            this.dgridBaoCao.TabIndex = 144;
            // 
            // frmBaoCaoSanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1299, 770);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.btnXuatexcel);
            this.Controls.Add(this.btnLammoi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.dgridXephang);
            this.Controls.Add(this.gpbThoigian);
            this.Controls.Add(this.dgridBaoCao);
            this.Name = "frmBaoCaoSanPham";
            this.Text = "frmBaoCaoSanPham";
            this.Load += new System.EventHandler(this.frmBaoCaoSanPham_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgridXephang)).EndInit();
            this.gpbThoigian.ResumeLayout(false);
            this.gpbThoigian.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgridBaoCao)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button btnXuatexcel;
        private System.Windows.Forms.Button btnLammoi;
        private System.Windows.Forms.Button btnHienthi;
        private System.Windows.Forms.DateTimePicker dtpKT;
        private System.Windows.Forms.DateTimePicker dtpBD;
        private System.Windows.Forms.ComboBox cboNamKT;
        private System.Windows.Forms.ComboBox cboNamBD;
        private System.Windows.Forms.ComboBox cboKT;
        private System.Windows.Forms.ComboBox cboBD;
        private System.Windows.Forms.Label lblNam2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNam1;
        private System.Windows.Forms.Label lblKT;
        private System.Windows.Forms.Label lblBD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.DataGridView dgridXephang;
        private System.Windows.Forms.GroupBox gpbThoigian;
        private System.Windows.Forms.ComboBox cboThoigian;
        private System.Windows.Forms.DataGridView dgridBaoCao;
    }
}