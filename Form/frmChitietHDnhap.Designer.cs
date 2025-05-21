namespace QLCHBanXeMay.form
{
    partial class frmChitietHDnhap
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSoHDN = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.msksdt = new System.Windows.Forms.MaskedTextBox();
            this.txtdiachi = new System.Windows.Forms.TextBox();
            this.txtTenNCC = new System.Windows.Forms.TextBox();
            this.txtMaNCC = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTenNV = new System.Windows.Forms.TextBox();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.qlcH_BanXeDataSet11 = new QLCHBanXeMay.QLCH_BanXeDataSet1();
            this.qlcH_BanXeDataSet12 = new QLCHBanXeMay.QLCH_BanXeDataSet1();
            this.dgridDanhsachSP = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.lblTongSP = new System.Windows.Forms.Label();
            this.lblSoluongSP = new System.Windows.Forms.Label();
            this.lblTongtien = new System.Windows.Forms.Label();
            this.lblBangchu = new System.Windows.Forms.Label();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnIn = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.dtpngaynhap = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qlcH_BanXeDataSet11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qlcH_BanXeDataSet12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgridDanhsachSP)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpngaynhap);
            this.groupBox1.Controls.Add(this.txtSoHDN);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(26, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(744, 162);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtSoHDN
            // 
            this.txtSoHDN.Location = new System.Drawing.Point(127, 36);
            this.txtSoHDN.Name = "txtSoHDN";
            this.txtSoHDN.Size = new System.Drawing.Size(114, 22);
            this.txtSoHDN.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.msksdt);
            this.groupBox3.Controls.Add(this.txtdiachi);
            this.groupBox3.Controls.Add(this.txtTenNCC);
            this.groupBox3.Controls.Add(this.txtMaNCC);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(473, 21);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(265, 135);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // msksdt
            // 
            this.msksdt.Location = new System.Drawing.Point(143, 75);
            this.msksdt.Mask = "99- 0000-0000";
            this.msksdt.Name = "msksdt";
            this.msksdt.Size = new System.Drawing.Size(100, 22);
            this.msksdt.TabIndex = 1;
            // 
            // txtdiachi
            // 
            this.txtdiachi.Location = new System.Drawing.Point(143, 107);
            this.txtdiachi.Name = "txtdiachi";
            this.txtdiachi.Size = new System.Drawing.Size(100, 22);
            this.txtdiachi.TabIndex = 7;
            // 
            // txtTenNCC
            // 
            this.txtTenNCC.Location = new System.Drawing.Point(143, 46);
            this.txtTenNCC.Name = "txtTenNCC";
            this.txtTenNCC.Size = new System.Drawing.Size(100, 22);
            this.txtTenNCC.TabIndex = 5;
            // 
            // txtMaNCC
            // 
            this.txtMaNCC.Location = new System.Drawing.Point(143, 18);
            this.txtMaNCC.Name = "txtMaNCC";
            this.txtMaNCC.Size = new System.Drawing.Size(100, 22);
            this.txtMaNCC.TabIndex = 4;
            this.txtMaNCC.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 16);
            this.label8.TabIndex = 3;
            this.label8.Text = "Địa Chỉ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 16);
            this.label7.TabIndex = 2;
            this.label7.Text = "Số Điện Thoại";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Tên Nhà Cung Cấp";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Mã Nhà Cung Cấp";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTenNV);
            this.groupBox2.Controls.Add(this.txtMaNV);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(247, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 94);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // txtTenNV
            // 
            this.txtTenNV.Location = new System.Drawing.Point(113, 43);
            this.txtTenNV.Name = "txtTenNV";
            this.txtTenNV.Size = new System.Drawing.Size(100, 22);
            this.txtTenNV.TabIndex = 5;
            // 
            // txtMaNV
            // 
            this.txtMaNV.Location = new System.Drawing.Point(113, 15);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(100, 22);
            this.txtMaNV.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Tên Nhân Viên";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Mã Nhân Viên";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ngày Nhập";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Số Hóa Đơn Nhập";
            // 
            // qlcH_BanXeDataSet11
            // 
            this.qlcH_BanXeDataSet11.DataSetName = "QLCH_BanXeDataSet1";
            this.qlcH_BanXeDataSet11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // qlcH_BanXeDataSet12
            // 
            this.qlcH_BanXeDataSet12.DataSetName = "QLCH_BanXeDataSet1";
            this.qlcH_BanXeDataSet12.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dgridDanhsachSP
            // 
            this.dgridDanhsachSP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridDanhsachSP.Location = new System.Drawing.Point(26, 196);
            this.dgridDanhsachSP.Name = "dgridDanhsachSP";
            this.dgridDanhsachSP.RowHeadersWidth = 51;
            this.dgridDanhsachSP.RowTemplate.Height = 24;
            this.dgridDanhsachSP.Size = new System.Drawing.Size(738, 150);
            this.dgridDanhsachSP.TabIndex = 1;
            this.dgridDanhsachSP.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridDanhsachSP_CellContentClick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 177);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(138, 16);
            this.label9.TabIndex = 2;
            this.label9.Text = "Danh Sách Sản Phẩm";
            // 
            // lblTongSP
            // 
            this.lblTongSP.AutoSize = true;
            this.lblTongSP.Location = new System.Drawing.Point(496, 349);
            this.lblTongSP.Name = "lblTongSP";
            this.lblTongSP.Size = new System.Drawing.Size(119, 16);
            this.lblTongSP.TabIndex = 3;
            this.lblTongSP.Text = "Tổng số sản phẩm";
            this.lblTongSP.Click += new System.EventHandler(this.label10_Click);
            // 
            // lblSoluongSP
            // 
            this.lblSoluongSP.AutoSize = true;
            this.lblSoluongSP.Location = new System.Drawing.Point(496, 365);
            this.lblSoluongSP.Name = "lblSoluongSP";
            this.lblSoluongSP.Size = new System.Drawing.Size(155, 16);
            this.lblSoluongSP.TabIndex = 4;
            this.lblSoluongSP.Text = "Tổng số lượng sản phẩm";
            this.lblSoluongSP.Click += new System.EventHandler(this.label11_Click);
            // 
            // lblTongtien
            // 
            this.lblTongtien.AutoSize = true;
            this.lblTongtien.Location = new System.Drawing.Point(496, 381);
            this.lblTongtien.Name = "lblTongtien";
            this.lblTongtien.Size = new System.Drawing.Size(63, 16);
            this.lblTongtien.TabIndex = 5;
            this.lblTongtien.Text = "Tổng tiền";
            // 
            // lblBangchu
            // 
            this.lblBangchu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblBangchu.Location = new System.Drawing.Point(0, 455);
            this.lblBangchu.Name = "lblBangchu";
            this.lblBangchu.Size = new System.Drawing.Size(783, 16);
            this.lblBangchu.TabIndex = 6;
            this.lblBangchu.Text = "Tổng tiền (Bằng chữ)";
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnXoa.Location = new System.Drawing.Point(440, 414);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(103, 45);
            this.btnXoa.TabIndex = 32;
            this.btnXoa.Text = "Xóa Hóa Đơn";
            this.btnXoa.UseVisualStyleBackColor = false;
            // 
            // btnIn
            // 
            this.btnIn.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnIn.Location = new System.Drawing.Point(549, 414);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(103, 45);
            this.btnIn.TabIndex = 33;
            this.btnIn.Text = "In Hóa Đơn";
            this.btnIn.UseVisualStyleBackColor = false;
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnDong.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnDong.Location = new System.Drawing.Point(658, 414);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(103, 45);
            this.btnDong.TabIndex = 34;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            // 
            // dtpngaynhap
            // 
            this.dtpngaynhap.Location = new System.Drawing.Point(127, 71);
            this.dtpngaynhap.Name = "dtpngaynhap";
            this.dtpngaynhap.Size = new System.Drawing.Size(114, 22);
            this.dtpngaynhap.TabIndex = 5;
            this.dtpngaynhap.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // frmChitietHDnhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 471);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.lblBangchu);
            this.Controls.Add(this.lblTongtien);
            this.Controls.Add(this.lblSoluongSP);
            this.Controls.Add(this.lblTongSP);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dgridDanhsachSP);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmChitietHDnhap";
            this.Text = "frmChitietHDN";
            this.Load += new System.EventHandler(this.frmChitietHDN_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qlcH_BanXeDataSet11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qlcH_BanXeDataSet12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgridDanhsachSP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSoHDN;
        private System.Windows.Forms.MaskedTextBox msksdt;
        private System.Windows.Forms.TextBox txtdiachi;
        private System.Windows.Forms.TextBox txtTenNCC;
        private System.Windows.Forms.TextBox txtMaNCC;
        private System.Windows.Forms.TextBox txtTenNV;
        private System.Windows.Forms.TextBox txtMaNV;
        private QLCH_BanXeDataSet1 qlcH_BanXeDataSet11;
        private QLCH_BanXeDataSet1 qlcH_BanXeDataSet12;
        private System.Windows.Forms.DataGridView dgridDanhsachSP;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblTongSP;
        private System.Windows.Forms.Label lblSoluongSP;
        private System.Windows.Forms.Label lblTongtien;
        private System.Windows.Forms.Label lblBangchu;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.DateTimePicker dtpngaynhap;
    }
}