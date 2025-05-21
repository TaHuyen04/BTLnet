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
            this.txtMaHDD = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.MaNV = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.MaNCC = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTongtientu = new System.Windows.Forms.TextBox();
            this.txtTongtienden = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpThoigiantu = new System.Windows.Forms.DateTimePicker();
            this.dtpThoigianden = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsachhoadonban)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDanhsachhoadonban
            // 
            this.dgvDanhsachhoadonban.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhsachhoadonban.Location = new System.Drawing.Point(522, 115);
            this.dgvDanhsachhoadonban.Name = "dgvDanhsachhoadonban";
            this.dgvDanhsachhoadonban.RowHeadersWidth = 51;
            this.dgvDanhsachhoadonban.RowTemplate.Height = 24;
            this.dgvDanhsachhoadonban.Size = new System.Drawing.Size(711, 416);
            this.dgvDanhsachhoadonban.TabIndex = 33;
            this.dgvDanhsachhoadonban.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhsachhoadonban_CellContentClick);
            // 
            // txtMaHDD
            // 
            this.txtMaHDD.Location = new System.Drawing.Point(118, 30);
            this.txtMaHDD.Name = "txtMaHDD";
            this.txtMaHDD.Size = new System.Drawing.Size(275, 22);
            this.txtMaHDD.TabIndex = 32;
            this.txtMaHDD.TextChanged += new System.EventHandler(this.txtMaHDD_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 29;
            this.label2.Text = "Số hóa đơn";
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
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.MaNCC);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.MaNV);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMaHDD);
            this.groupBox1.Location = new System.Drawing.Point(12, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(462, 420);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tìm kiếm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 16);
            this.label3.TabIndex = 33;
            this.label3.Text = "Mã nhân viên";
            // 
            // MaNV
            // 
            this.MaNV.Location = new System.Drawing.Point(118, 75);
            this.MaNV.Name = "MaNV";
            this.MaNV.Size = new System.Drawing.Size(275, 22);
            this.MaNV.TabIndex = 34;
            this.MaNV.TextChanged += new System.EventHandler(this.MaNV_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 16);
            this.label4.TabIndex = 35;
            this.label4.Text = "Mã nhà cung cấp";
            // 
            // MaNCC
            // 
            this.MaNCC.Location = new System.Drawing.Point(118, 115);
            this.MaNCC.Name = "MaNCC";
            this.MaNCC.Size = new System.Drawing.Size(275, 22);
            this.MaNCC.TabIndex = 36;
            this.MaNCC.TextChanged += new System.EventHandler(this.MaNCC_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTongtienden);
            this.groupBox2.Controls.Add(this.txtTongtientu);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(6, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(387, 116);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tổng tiền";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(62, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 16);
            this.label5.TabIndex = 38;
            this.label5.Text = "Từ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(62, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 16);
            this.label6.TabIndex = 39;
            this.label6.Text = "Đến";
            // 
            // txtTongtientu
            // 
            this.txtTongtientu.Location = new System.Drawing.Point(112, 25);
            this.txtTongtientu.Name = "txtTongtientu";
            this.txtTongtientu.Size = new System.Drawing.Size(171, 22);
            this.txtTongtientu.TabIndex = 38;
            this.txtTongtientu.TextChanged += new System.EventHandler(this.txtTongtientu_TextChanged);
            // 
            // txtTongtienden
            // 
            this.txtTongtienden.Location = new System.Drawing.Point(112, 65);
            this.txtTongtienden.Name = "txtTongtienden";
            this.txtTongtienden.Size = new System.Drawing.Size(171, 22);
            this.txtTongtienden.TabIndex = 40;
            this.txtTongtienden.TextChanged += new System.EventHandler(this.txtTongtienden_TextChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dtpThoigianden);
            this.groupBox4.Controls.Add(this.dtpThoigiantu);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(6, 296);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(387, 116);
            this.groupBox4.TabIndex = 41;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Thời gian";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(62, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 16);
            this.label7.TabIndex = 39;
            this.label7.Text = "Đến";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(62, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 16);
            this.label8.TabIndex = 38;
            this.label8.Text = "Từ";
            // 
            // dtpThoigiantu
            // 
            this.dtpThoigiantu.Location = new System.Drawing.Point(112, 24);
            this.dtpThoigiantu.Name = "dtpThoigiantu";
            this.dtpThoigiantu.Size = new System.Drawing.Size(169, 22);
            this.dtpThoigiantu.TabIndex = 40;
            this.dtpThoigiantu.ValueChanged += new System.EventHandler(this.dtpThoigiantu_ValueChanged);
            // 
            // dtpThoigianden
            // 
            this.dtpThoigianden.Location = new System.Drawing.Point(112, 65);
            this.dtpThoigianden.Name = "dtpThoigianden";
            this.dtpThoigianden.Size = new System.Drawing.Size(169, 22);
            this.dtpThoigianden.TabIndex = 41;
            this.dtpThoigianden.ValueChanged += new System.EventHandler(this.dtpThoigianden_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(66, 508);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 23);
            this.button1.TabIndex = 41;
            this.button1.Text = "Tìm kiếm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(233, 508);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 23);
            this.button2.TabIndex = 42;
            this.button2.Text = "Bỏ qua";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(762, 534);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(206, 16);
            this.label9.TabIndex = 41;
            this.label9.Text = "Click 2 lần để xem chi tiết hóa đơn";
            // 
            // frmDSHoadonban
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 600);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvDanhsachhoadonban);
            this.Controls.Add(this.label1);
            this.Name = "frmDSHoadonban";
            this.Text = "frmDSHoadonban";
            this.Load += new System.EventHandler(this.frmDondathang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsachhoadonban)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvDanhsachhoadonban;
        private System.Windows.Forms.TextBox txtMaHDD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox MaNCC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox MaNV;
        private System.Windows.Forms.Label label3;
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label9;
    }
}