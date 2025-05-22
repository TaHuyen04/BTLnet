namespace QLCHBanXeMay.form
{
    partial class frmDSHoadonban
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
            this.dgvDanhsachhoadonban = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaKH = new System.Windows.Forms.TextBox();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dtpThoigianden = new System.Windows.Forms.DateTimePicker();
            this.dtpThoigiantu = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTongtienden = new System.Windows.Forms.TextBox();
            this.txtTongtientu = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSoHDN = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnTimkiem = new System.Windows.Forms.Button();
            this.btnBoqua = new System.Windows.Forms.Button();
            this.lblSoluonghoadon = new System.Windows.Forms.Label();
            this.btnTaohoadon = new System.Windows.Forms.Button();
            this.btnXuatdanhsach = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsachhoadonban)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDanhsachhoadonban
            // 
            this.dgvDanhsachhoadonban.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhsachhoadonban.Location = new System.Drawing.Point(468, 105);
            this.dgvDanhsachhoadonban.Name = "dgvDanhsachhoadonban";
            this.dgvDanhsachhoadonban.RowHeadersWidth = 51;
            this.dgvDanhsachhoadonban.RowTemplate.Height = 24;
            this.dgvDanhsachhoadonban.Size = new System.Drawing.Size(541, 299);
            this.dgvDanhsachhoadonban.TabIndex = 33;
            this.dgvDanhsachhoadonban.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhsachhoadonban_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(437, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(388, 33);
            this.label1.TabIndex = 30;
            this.label1.Text = "DANH SÁCH HÓA ĐƠN BÁN";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMaKH);
            this.groupBox1.Controls.Add(this.txtMaNV);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txtSoHDN);
            this.groupBox1.Location = new System.Drawing.Point(17, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 346);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tìm kiếm";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(77, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 16);
            this.label4.TabIndex = 48;
            this.label4.Text = "Mã khách hàng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(89, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 16);
            this.label2.TabIndex = 47;
            this.label2.Text = "Mã nhân viên";
            // 
            // txtMaKH
            // 
            this.txtMaKH.Location = new System.Drawing.Point(181, 84);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.Size = new System.Drawing.Size(206, 22);
            this.txtMaKH.TabIndex = 46;
            this.txtMaKH.TextChanged += new System.EventHandler(this.txtMaKH_TextChanged);
            // 
            // txtMaNV
            // 
            this.txtMaNV.Location = new System.Drawing.Point(181, 56);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(206, 22);
            this.txtMaNV.TabIndex = 45;
            this.txtMaNV.TextChanged += new System.EventHandler(this.txtMaNV_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 16);
            this.label3.TabIndex = 44;
            this.label3.Text = "Số hóa đơn bán";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dtpThoigianden);
            this.groupBox4.Controls.Add(this.dtpThoigiantu);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(28, 208);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(387, 84);
            this.groupBox4.TabIndex = 41;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Thời gian";
            // 
            // dtpThoigianden
            // 
            this.dtpThoigianden.Location = new System.Drawing.Point(227, 35);
            this.dtpThoigianden.Name = "dtpThoigianden";
            this.dtpThoigianden.Size = new System.Drawing.Size(132, 22);
            this.dtpThoigianden.TabIndex = 41;
            this.dtpThoigianden.ValueChanged += new System.EventHandler(this.dtpThoigianden_ValueChanged);
            // 
            // dtpThoigiantu
            // 
            this.dtpThoigiantu.Location = new System.Drawing.Point(48, 35);
            this.dtpThoigiantu.Name = "dtpThoigiantu";
            this.dtpThoigiantu.Size = new System.Drawing.Size(136, 22);
            this.dtpThoigiantu.TabIndex = 40;
            this.dtpThoigiantu.ValueChanged += new System.EventHandler(this.dtpThoigiantu_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(190, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 16);
            this.label7.TabIndex = 39;
            this.label7.Text = "Đến";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 16);
            this.label8.TabIndex = 38;
            this.label8.Text = "Từ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTongtienden);
            this.groupBox2.Controls.Add(this.txtTongtientu);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(28, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(387, 90);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tổng tiền";
            // 
            // txtTongtienden
            // 
            this.txtTongtienden.Location = new System.Drawing.Point(242, 39);
            this.txtTongtienden.Name = "txtTongtienden";
            this.txtTongtienden.Size = new System.Drawing.Size(115, 22);
            this.txtTongtienden.TabIndex = 40;
            this.txtTongtienden.TextChanged += new System.EventHandler(this.txtTongtienden_TextChanged);
            // 
            // txtTongtientu
            // 
            this.txtTongtientu.Location = new System.Drawing.Point(60, 39);
            this.txtTongtientu.Name = "txtTongtientu";
            this.txtTongtientu.Size = new System.Drawing.Size(116, 22);
            this.txtTongtientu.TabIndex = 38;
            this.txtTongtientu.TextChanged += new System.EventHandler(this.txtTongtientu_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(205, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 16);
            this.label6.TabIndex = 39;
            this.label6.Text = "Đến";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 16);
            this.label5.TabIndex = 38;
            this.label5.Text = "Từ";
            // 
            // txtSoHDN
            // 
            this.txtSoHDN.Location = new System.Drawing.Point(181, 28);
            this.txtSoHDN.Name = "txtSoHDN";
            this.txtSoHDN.Size = new System.Drawing.Size(206, 22);
            this.txtSoHDN.TabIndex = 32;
            this.txtSoHDN.TextChanged += new System.EventHandler(this.txtSoHDN_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(660, 407);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(206, 16);
            this.label9.TabIndex = 41;
            this.label9.Text = "Click 2 lần để xem chi tiết hóa đơn";
            // 
            // btnTimkiem
            // 
            this.btnTimkiem.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnTimkiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnTimkiem.Location = new System.Drawing.Point(134, 440);
            this.btnTimkiem.Name = "btnTimkiem";
            this.btnTimkiem.Size = new System.Drawing.Size(87, 33);
            this.btnTimkiem.TabIndex = 43;
            this.btnTimkiem.Text = "Tìm kiếm";
            this.btnTimkiem.UseVisualStyleBackColor = false;
            this.btnTimkiem.Click += new System.EventHandler(this.btnTimkiem_Click);
            // 
            // btnBoqua
            // 
            this.btnBoqua.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnBoqua.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnBoqua.Location = new System.Drawing.Point(253, 440);
            this.btnBoqua.Name = "btnBoqua";
            this.btnBoqua.Size = new System.Drawing.Size(87, 33);
            this.btnBoqua.TabIndex = 44;
            this.btnBoqua.Text = "Bỏ qua";
            this.btnBoqua.UseVisualStyleBackColor = false;
            // 
            // lblSoluonghoadon
            // 
            this.lblSoluonghoadon.AutoSize = true;
            this.lblSoluonghoadon.Location = new System.Drawing.Point(465, 86);
            this.lblSoluonghoadon.Name = "lblSoluonghoadon";
            this.lblSoluonghoadon.Size = new System.Drawing.Size(115, 16);
            this.lblSoluonghoadon.TabIndex = 45;
            this.lblSoluonghoadon.Text = "Số lượng hóa đơn:";
            // 
            // btnTaohoadon
            // 
            this.btnTaohoadon.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnTaohoadon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnTaohoadon.Location = new System.Drawing.Point(585, 440);
            this.btnTaohoadon.Name = "btnTaohoadon";
            this.btnTaohoadon.Size = new System.Drawing.Size(116, 33);
            this.btnTaohoadon.TabIndex = 46;
            this.btnTaohoadon.Text = "Tạo hóa đơn";
            this.btnTaohoadon.UseVisualStyleBackColor = false;
            this.btnTaohoadon.Click += new System.EventHandler(this.btnTaohoadon_Click);
            // 
            // btnXuatdanhsach
            // 
            this.btnXuatdanhsach.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnXuatdanhsach.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnXuatdanhsach.Location = new System.Drawing.Point(707, 440);
            this.btnXuatdanhsach.Name = "btnXuatdanhsach";
            this.btnXuatdanhsach.Size = new System.Drawing.Size(116, 33);
            this.btnXuatdanhsach.TabIndex = 47;
            this.btnXuatdanhsach.Text = "Xuất danh sách";
            this.btnXuatdanhsach.UseVisualStyleBackColor = false;
            this.btnXuatdanhsach.Click += new System.EventHandler(this.btnXuatdanhsach_Click);
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnDong.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnDong.Location = new System.Drawing.Point(829, 440);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(116, 33);
            this.btnDong.TabIndex = 48;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            // 
            // frmDSHoadonban
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 490);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnXuatdanhsach);
            this.Controls.Add(this.btnTaohoadon);
            this.Controls.Add(this.lblSoluonghoadon);
            this.Controls.Add(this.btnBoqua);
            this.Controls.Add(this.btnTimkiem);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvDanhsachhoadonban);
            this.Controls.Add(this.label1);
            this.Name = "frmDSHoadonban";
            this.Text = "frmDSHoadonban";
            this.Load += new System.EventHandler(this.frmDSHoadonban_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsachhoadonban)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvDanhsachhoadonban;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker dtpThoigianden;
        private System.Windows.Forms.DateTimePicker dtpThoigiantu;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTongtienden;
        private System.Windows.Forms.TextBox txtTongtientu;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSoHDN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaKH;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTimkiem;
        private System.Windows.Forms.Button btnBoqua;
        private System.Windows.Forms.Label lblSoluonghoadon;
        private System.Windows.Forms.Button btnTaohoadon;
        private System.Windows.Forms.Button btnXuatdanhsach;
        private System.Windows.Forms.Button btnDong;
    }
}