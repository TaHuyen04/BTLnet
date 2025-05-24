namespace QLCHBanXeMay.form
{
    partial class frmDSHoadonnhap
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
            this.mskngaybd = new System.Windows.Forms.MaskedTextBox();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnXuat = new System.Windows.Forms.Button();
            this.btnTaoHDN = new System.Windows.Forms.Button();
            this.btnBoqua = new System.Windows.Forms.Button();
            this.btnTimkiem = new System.Windows.Forms.Button();
            this.dgridDanhsachHDN = new System.Windows.Forms.DataGridView();
            this.txtkhoangbd = new System.Windows.Forms.TextBox();
            this.txtMaNCC = new System.Windows.Forms.TextBox();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.txtSoHDN = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtkhoangkt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mskngaykt = new System.Windows.Forms.MaskedTextBox();
            this.lblTongHD = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgridDanhsachHDN)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mskngaybd
            // 
            this.mskngaybd.Location = new System.Drawing.Point(136, 21);
            this.mskngaybd.Mask = "00/00/0000";
            this.mskngaybd.Name = "mskngaybd";
            this.mskngaybd.Size = new System.Drawing.Size(100, 22);
            this.mskngaybd.TabIndex = 34;
            this.mskngaybd.ValidatingType = typeof(System.DateTime);
            this.mskngaybd.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.mskngaybd_MaskInputRejected);
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnDong.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnDong.Location = new System.Drawing.Point(775, 494);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(103, 45);
            this.btnDong.TabIndex = 33;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnXuat
            // 
            this.btnXuat.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnXuat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnXuat.Location = new System.Drawing.Point(616, 494);
            this.btnXuat.Name = "btnXuat";
            this.btnXuat.Size = new System.Drawing.Size(92, 45);
            this.btnXuat.TabIndex = 32;
            this.btnXuat.Text = "Xuất danh sách";
            this.btnXuat.UseVisualStyleBackColor = false;
            this.btnXuat.Click += new System.EventHandler(this.btnXuat_Click);
            // 
            // btnTaoHDN
            // 
            this.btnTaoHDN.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnTaoHDN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnTaoHDN.Location = new System.Drawing.Point(433, 494);
            this.btnTaoHDN.Name = "btnTaoHDN";
            this.btnTaoHDN.Size = new System.Drawing.Size(103, 45);
            this.btnTaoHDN.TabIndex = 31;
            this.btnTaoHDN.Text = "Tạo hóa đơn mới";
            this.btnTaoHDN.UseVisualStyleBackColor = false;
            this.btnTaoHDN.Click += new System.EventHandler(this.btnTaoHDN_Click);
            // 
            // btnBoqua
            // 
            this.btnBoqua.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnBoqua.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnBoqua.Location = new System.Drawing.Point(179, 431);
            this.btnBoqua.Name = "btnBoqua";
            this.btnBoqua.Size = new System.Drawing.Size(64, 45);
            this.btnBoqua.TabIndex = 30;
            this.btnBoqua.Text = "Bỏ qua";
            this.btnBoqua.UseVisualStyleBackColor = false;
            this.btnBoqua.Click += new System.EventHandler(this.btnBoqua_Click);
            // 
            // btnTimkiem
            // 
            this.btnTimkiem.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnTimkiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnTimkiem.Location = new System.Drawing.Point(56, 431);
            this.btnTimkiem.Name = "btnTimkiem";
            this.btnTimkiem.Size = new System.Drawing.Size(64, 45);
            this.btnTimkiem.TabIndex = 29;
            this.btnTimkiem.Text = "Tìm kiếm";
            this.btnTimkiem.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTimkiem.UseVisualStyleBackColor = false;
            this.btnTimkiem.Click += new System.EventHandler(this.btnTimkiem_Click);
            // 
            // dgridDanhsachHDN
            // 
            this.dgridDanhsachHDN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridDanhsachHDN.Location = new System.Drawing.Point(342, 80);
            this.dgridDanhsachHDN.Name = "dgridDanhsachHDN";
            this.dgridDanhsachHDN.ReadOnly = true;
            this.dgridDanhsachHDN.RowHeadersWidth = 51;
            this.dgridDanhsachHDN.RowTemplate.Height = 24;
            this.dgridDanhsachHDN.Size = new System.Drawing.Size(580, 396);
            this.dgridDanhsachHDN.TabIndex = 28;
            this.dgridDanhsachHDN.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridDanhsachHDN_CellContentdoubleClick);
            // 
            // txtkhoangbd
            // 
            this.txtkhoangbd.Location = new System.Drawing.Point(141, 21);
            this.txtkhoangbd.Name = "txtkhoangbd";
            this.txtkhoangbd.Size = new System.Drawing.Size(100, 22);
            this.txtkhoangbd.TabIndex = 27;
            this.txtkhoangbd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtkhoangbd_KeyPress);
            // 
            // txtMaNCC
            // 
            this.txtMaNCC.Location = new System.Drawing.Point(179, 134);
            this.txtMaNCC.Name = "txtMaNCC";
            this.txtMaNCC.Size = new System.Drawing.Size(100, 22);
            this.txtMaNCC.TabIndex = 26;
            // 
            // txtMaNV
            // 
            this.txtMaNV.Location = new System.Drawing.Point(179, 106);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(100, 22);
            this.txtMaNV.TabIndex = 25;
            // 
            // txtSoHDN
            // 
            this.txtSoHDN.Location = new System.Drawing.Point(179, 73);
            this.txtSoHDN.Name = "txtSoHDN";
            this.txtSoHDN.Size = new System.Drawing.Size(100, 22);
            this.txtSoHDN.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 16);
            this.label5.TabIndex = 22;
            this.label5.Text = "Ngày nhập";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 16);
            this.label4.TabIndex = 21;
            this.label4.Text = "Tổng tiền";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "Mã nhà cung cấp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "Mã nhân viên";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Số hóa đơn nhập";
            // 
            // txtkhoangkt
            // 
            this.txtkhoangkt.Location = new System.Drawing.Point(141, 49);
            this.txtkhoangkt.Name = "txtkhoangkt";
            this.txtkhoangkt.Size = new System.Drawing.Size(100, 22);
            this.txtkhoangkt.TabIndex = 35;
            this.txtkhoangkt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtkhoangkt_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtkhoangbd);
            this.groupBox1.Controls.Add(this.txtkhoangkt);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(32, 268);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(247, 100);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mskngaykt);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.mskngaybd);
            this.groupBox2.Location = new System.Drawing.Point(31, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(247, 100);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            // 
            // mskngaykt
            // 
            this.mskngaykt.Location = new System.Drawing.Point(136, 49);
            this.mskngaykt.Mask = "00/00/0000";
            this.mskngaykt.Name = "mskngaykt";
            this.mskngaykt.Size = new System.Drawing.Size(100, 22);
            this.mskngaykt.TabIndex = 35;
            this.mskngaykt.ValidatingType = typeof(System.DateTime);
            // 
            // lblTongHD
            // 
            this.lblTongHD.AutoSize = true;
            this.lblTongHD.Location = new System.Drawing.Point(325, 61);
            this.lblTongHD.Name = "lblTongHD";
            this.lblTongHD.Size = new System.Drawing.Size(151, 16);
            this.lblTongHD.TabIndex = 38;
            this.lblTongHD.Text = "Số lượng hóa đơn nhập: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label7.Location = new System.Drawing.Point(208, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(406, 33);
            this.label7.TabIndex = 39;
            this.label7.Text = "DANH SÁCH HÓA ĐƠN NHẬP";
//            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // frmDSHoadonnhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 626);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblTongHD);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnXuat);
            this.Controls.Add(this.btnTaoHDN);
            this.Controls.Add(this.btnBoqua);
            this.Controls.Add(this.btnTimkiem);
            this.Controls.Add(this.dgridDanhsachHDN);
            this.Controls.Add(this.txtMaNCC);
            this.Controls.Add(this.txtMaNV);
            this.Controls.Add(this.txtSoHDN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmDSHoadonnhap";
            this.Text = "frmHoadonnhap";
            this.Load += new System.EventHandler(this.frmHoadonnhap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgridDanhsachHDN)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox mskngaybd;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnXuat;
        private System.Windows.Forms.Button btnTaoHDN;
        private System.Windows.Forms.Button btnBoqua;
        private System.Windows.Forms.Button btnTimkiem;
        private System.Windows.Forms.DataGridView dgridDanhsachHDN;
        private System.Windows.Forms.TextBox txtkhoangbd;
        private System.Windows.Forms.TextBox txtMaNCC;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.TextBox txtSoHDN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtkhoangkt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MaskedTextBox mskngaykt;
        private System.Windows.Forms.Label lblTongHD;
        private System.Windows.Forms.Label label7;
    }
}