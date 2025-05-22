namespace QLCHBanXeMay.form
{
    partial class frmTaodonnhap
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboMaNCC = new System.Windows.Forms.ComboBox();
            this.txtDiachi = new System.Windows.Forms.TextBox();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.txtTenNCC = new System.Windows.Forms.TextBox();
            this.txtTenNV = new System.Windows.Forms.TextBox();
            this.cboMaNV = new System.Windows.Forms.ComboBox();
            this.dtpNgaynhap = new System.Windows.Forms.DateTimePicker();
            this.txtMaHDN = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtThanhtien = new System.Windows.Forms.TextBox();
            this.nudSoluong = new System.Windows.Forms.NumericUpDown();
            this.nudGiamgia = new System.Windows.Forms.NumericUpDown();
            this.txtDonGiaNhap = new System.Windows.Forms.TextBox();
            this.txtTenSP = new System.Windows.Forms.TextBox();
            this.cboMaSP = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvDSSP = new System.Windows.Forms.DataGridView();
            this.lblTongtienChu = new System.Windows.Forms.Label();
            this.lblSoSP = new System.Windows.Forms.Label();
            this.lblSoluongSP = new System.Windows.Forms.Label();
            this.lblTongtien = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnBoquaHD = new System.Windows.Forms.Button();
            this.btnLuuHD = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnBoqua = new System.Windows.Forms.Button();
            this.btnInHD = new System.Windows.Forms.Button();
            this.btnXoaHD = new System.Windows.Forms.Button();
            this.btnTaomoi = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoluong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiamgia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSSP)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(415, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(321, 33);
            this.label1.TabIndex = 33;
            this.label1.Text = "HÓA ĐƠN NHẬP HÀNG";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboMaNCC);
            this.groupBox1.Controls.Add(this.txtDiachi);
            this.groupBox1.Controls.Add(this.txtSDT);
            this.groupBox1.Controls.Add(this.txtTenNCC);
            this.groupBox1.Controls.Add(this.txtTenNV);
            this.groupBox1.Controls.Add(this.cboMaNV);
            this.groupBox1.Controls.Add(this.dtpNgaynhap);
            this.groupBox1.Controls.Add(this.txtMaHDN);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(49, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(774, 148);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin hóa đơn";
            // 
            // cboMaNCC
            // 
            this.cboMaNCC.FormattingEnabled = true;
            this.cboMaNCC.Location = new System.Drawing.Point(619, 23);
            this.cboMaNCC.Name = "cboMaNCC";
            this.cboMaNCC.Size = new System.Drawing.Size(124, 24);
            this.cboMaNCC.TabIndex = 15;
            this.cboMaNCC.SelectedIndexChanged += new System.EventHandler(this.cboMaNCC_SelectedIndexChanged_1);
            // 
            // txtDiachi
            // 
            this.txtDiachi.Location = new System.Drawing.Point(619, 116);
            this.txtDiachi.Name = "txtDiachi";
            this.txtDiachi.Size = new System.Drawing.Size(124, 22);
            this.txtDiachi.TabIndex = 14;
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(619, 85);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(124, 22);
            this.txtSDT.TabIndex = 13;
            // 
            // txtTenNCC
            // 
            this.txtTenNCC.Location = new System.Drawing.Point(619, 57);
            this.txtTenNCC.Name = "txtTenNCC";
            this.txtTenNCC.Size = new System.Drawing.Size(124, 22);
            this.txtTenNCC.TabIndex = 12;
            // 
            // txtTenNV
            // 
            this.txtTenNV.Location = new System.Drawing.Point(356, 59);
            this.txtTenNV.Name = "txtTenNV";
            this.txtTenNV.Size = new System.Drawing.Size(124, 22);
            this.txtTenNV.TabIndex = 11;
            // 
            // cboMaNV
            // 
            this.cboMaNV.FormattingEnabled = true;
            this.cboMaNV.Location = new System.Drawing.Point(356, 26);
            this.cboMaNV.Name = "cboMaNV";
            this.cboMaNV.Size = new System.Drawing.Size(124, 24);
            this.cboMaNV.TabIndex = 10;
            this.cboMaNV.SelectedIndexChanged += new System.EventHandler(this.cboMaNV_SelectedIndexChanged_1);
            // 
            // dtpNgaynhap
            // 
            this.dtpNgaynhap.Location = new System.Drawing.Point(122, 55);
            this.dtpNgaynhap.Name = "dtpNgaynhap";
            this.dtpNgaynhap.Size = new System.Drawing.Size(124, 22);
            this.dtpNgaynhap.TabIndex = 9;
            // 
            // txtMaHDN
            // 
            this.txtMaHDN.Location = new System.Drawing.Point(122, 26);
            this.txtMaHDN.Name = "txtMaHDN";
            this.txtMaHDN.Size = new System.Drawing.Size(124, 22);
            this.txtMaHDN.TabIndex = 8;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(499, 119);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 16);
            this.label14.TabIndex = 7;
            this.label14.Text = "Địa chỉ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(499, 91);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(85, 16);
            this.label13.TabIndex = 6;
            this.label13.Text = "Số điện thoại";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(499, 59);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(114, 16);
            this.label12.TabIndex = 5;
            this.label12.Text = "Tên nhà cung cấp";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(498, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(109, 16);
            this.label11.TabIndex = 4;
            this.label11.Text = "Mã nhà cung cấp";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(264, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 16);
            this.label10.TabIndex = 3;
            this.label10.Text = "Tên nhân viên";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(264, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 16);
            this.label9.TabIndex = 2;
            this.label9.Text = "Mã nhân viên";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 16);
            this.label8.TabIndex = 1;
            this.label8.Text = "Ngày nhập";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "Mã hóa đơn nhập";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtThanhtien);
            this.groupBox2.Controls.Add(this.nudSoluong);
            this.groupBox2.Controls.Add(this.nudGiamgia);
            this.groupBox2.Controls.Add(this.txtDonGiaNhap);
            this.groupBox2.Controls.Add(this.txtTenSP);
            this.groupBox2.Controls.Add(this.cboMaSP);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Location = new System.Drawing.Point(49, 222);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(774, 92);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin sản phẩm";
            // 
            // txtThanhtien
            // 
            this.txtThanhtien.Location = new System.Drawing.Point(619, 59);
            this.txtThanhtien.Name = "txtThanhtien";
            this.txtThanhtien.Size = new System.Drawing.Size(124, 22);
            this.txtThanhtien.TabIndex = 44;
            // 
            // nudSoluong
            // 
            this.nudSoluong.Location = new System.Drawing.Point(345, 22);
            this.nudSoluong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSoluong.Name = "nudSoluong";
            this.nudSoluong.Size = new System.Drawing.Size(124, 22);
            this.nudSoluong.TabIndex = 43;
            this.nudSoluong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSoluong.ValueChanged += new System.EventHandler(this.nudSoluong_ValueChanged);
            // 
            // nudGiamgia
            // 
            this.nudGiamgia.Location = new System.Drawing.Point(619, 22);
            this.nudGiamgia.Name = "nudGiamgia";
            this.nudGiamgia.Size = new System.Drawing.Size(124, 22);
            this.nudGiamgia.TabIndex = 42;
            this.nudGiamgia.ValueChanged += new System.EventHandler(this.nudGiamgia_ValueChanged);
            // 
            // txtDonGiaNhap
            // 
            this.txtDonGiaNhap.Location = new System.Drawing.Point(345, 59);
            this.txtDonGiaNhap.Name = "txtDonGiaNhap";
            this.txtDonGiaNhap.Size = new System.Drawing.Size(124, 22);
            this.txtDonGiaNhap.TabIndex = 18;
            // 
            // txtTenSP
            // 
            this.txtTenSP.Location = new System.Drawing.Point(95, 59);
            this.txtTenSP.Name = "txtTenSP";
            this.txtTenSP.Size = new System.Drawing.Size(124, 22);
            this.txtTenSP.TabIndex = 16;
            // 
            // cboMaSP
            // 
            this.cboMaSP.FormattingEnabled = true;
            this.cboMaSP.Location = new System.Drawing.Point(95, 22);
            this.cboMaSP.Name = "cboMaSP";
            this.cboMaSP.Size = new System.Drawing.Size(124, 24);
            this.cboMaSP.TabIndex = 16;
            this.cboMaSP.SelectedIndexChanged += new System.EventHandler(this.cboMaSP_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(502, 59);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(69, 16);
            this.label20.TabIndex = 5;
            this.label20.Text = "Thảnh tiền";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(502, 25);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(84, 16);
            this.label19.TabIndex = 4;
            this.label19.Text = "Giảm giá (%)";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(237, 59);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(86, 16);
            this.label18.TabIndex = 3;
            this.label18.Text = "Đơn giá nhập";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(240, 25);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(60, 16);
            this.label17.TabIndex = 2;
            this.label17.Text = "Số lượng";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 58);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 16);
            this.label16.TabIndex = 1;
            this.label16.Text = "Tên hàng";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 16);
            this.label15.TabIndex = 0;
            this.label15.Text = "Mã hàng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 328);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 16);
            this.label2.TabIndex = 36;
            this.label2.Text = "Danh sách sản phẩm đã chọn";
            // 
            // dgvDSSP
            // 
            this.dgvDSSP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDSSP.Location = new System.Drawing.Point(49, 347);
            this.dgvDSSP.Name = "dgvDSSP";
            this.dgvDSSP.RowHeadersWidth = 51;
            this.dgvDSSP.RowTemplate.Height = 24;
            this.dgvDSSP.Size = new System.Drawing.Size(774, 197);
            this.dgvDSSP.TabIndex = 37;
            this.dgvDSSP.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDSSP_CellClick);
            // 
            // lblTongtienChu
            // 
            this.lblTongtienChu.AutoSize = true;
            this.lblTongtienChu.Location = new System.Drawing.Point(46, 574);
            this.lblTongtienChu.Name = "lblTongtienChu";
            this.lblTongtienChu.Size = new System.Drawing.Size(132, 16);
            this.lblTongtienChu.TabIndex = 38;
            this.lblTongtienChu.Text = "Tổng tiền (bằng chữ):";
            // 
            // lblSoSP
            // 
            this.lblSoSP.AutoSize = true;
            this.lblSoSP.Location = new System.Drawing.Point(841, 421);
            this.lblSoSP.Name = "lblSoSP";
            this.lblSoSP.Size = new System.Drawing.Size(92, 16);
            this.lblSoSP.TabIndex = 39;
            this.lblSoSP.Text = "Số sản phẩm: ";
            // 
            // lblSoluongSP
            // 
            this.lblSoluongSP.AutoSize = true;
            this.lblSoluongSP.Location = new System.Drawing.Point(841, 455);
            this.lblSoluongSP.Name = "lblSoluongSP";
            this.lblSoluongSP.Size = new System.Drawing.Size(125, 16);
            this.lblSoluongSP.TabIndex = 40;
            this.lblSoluongSP.Text = "Số lượng sản phẩm:";
            // 
            // lblTongtien
            // 
            this.lblTongtien.AutoSize = true;
            this.lblTongtien.Location = new System.Drawing.Point(841, 488);
            this.lblTongtien.Name = "lblTongtien";
            this.lblTongtien.Size = new System.Drawing.Size(66, 16);
            this.lblTongtien.TabIndex = 41;
            this.lblTongtien.Text = "Tổng tiền:";
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnDong.Location = new System.Drawing.Point(958, 616);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(64, 33);
            this.btnDong.TabIndex = 42;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnBoquaHD
            // 
            this.btnBoquaHD.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnBoquaHD.Location = new System.Drawing.Point(885, 148);
            this.btnBoquaHD.Name = "btnBoquaHD";
            this.btnBoquaHD.Size = new System.Drawing.Size(105, 33);
            this.btnBoquaHD.TabIndex = 43;
            this.btnBoquaHD.Text = "Bỏ qua";
            this.btnBoquaHD.UseVisualStyleBackColor = false;
            this.btnBoquaHD.Click += new System.EventHandler(this.btnBoquaHD_Click);
            // 
            // btnLuuHD
            // 
            this.btnLuuHD.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnLuuHD.Location = new System.Drawing.Point(609, 616);
            this.btnLuuHD.Name = "btnLuuHD";
            this.btnLuuHD.Size = new System.Drawing.Size(105, 33);
            this.btnLuuHD.TabIndex = 44;
            this.btnLuuHD.Text = "Lưu hóa đơn";
            this.btnLuuHD.UseVisualStyleBackColor = false;
            this.btnLuuHD.Click += new System.EventHandler(this.btnLuuHD_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnSua.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnSua.Location = new System.Drawing.Point(946, 236);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(76, 33);
            this.btnSua.TabIndex = 45;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnXoa.Location = new System.Drawing.Point(865, 280);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(76, 33);
            this.btnXoa.TabIndex = 46;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnThem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnThem.Location = new System.Drawing.Point(865, 235);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(76, 33);
            this.btnThem.TabIndex = 47;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnBoqua
            // 
            this.btnBoqua.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnBoqua.Location = new System.Drawing.Point(946, 280);
            this.btnBoqua.Name = "btnBoqua";
            this.btnBoqua.Size = new System.Drawing.Size(76, 33);
            this.btnBoqua.TabIndex = 48;
            this.btnBoqua.Text = "Bỏ qua";
            this.btnBoqua.UseVisualStyleBackColor = false;
            this.btnBoqua.Click += new System.EventHandler(this.btnBoqua_Click_1);
            // 
            // btnInHD
            // 
            this.btnInHD.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnInHD.Location = new System.Drawing.Point(729, 616);
            this.btnInHD.Name = "btnInHD";
            this.btnInHD.Size = new System.Drawing.Size(105, 33);
            this.btnInHD.TabIndex = 49;
            this.btnInHD.Text = "In hóa đơn";
            this.btnInHD.UseVisualStyleBackColor = false;
            this.btnInHD.Click += new System.EventHandler(this.btnInHD_Click);
            // 
            // btnXoaHD
            // 
            this.btnXoaHD.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnXoaHD.Location = new System.Drawing.Point(844, 616);
            this.btnXoaHD.Name = "btnXoaHD";
            this.btnXoaHD.Size = new System.Drawing.Size(105, 33);
            this.btnXoaHD.TabIndex = 50;
            this.btnXoaHD.Text = "Xóa hóa đơn";
            this.btnXoaHD.UseVisualStyleBackColor = false;
            this.btnXoaHD.Click += new System.EventHandler(this.btnXoaHD_Click);
            // 
            // btnTaomoi
            // 
            this.btnTaomoi.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnTaomoi.Location = new System.Drawing.Point(885, 107);
            this.btnTaomoi.Name = "btnTaomoi";
            this.btnTaomoi.Size = new System.Drawing.Size(105, 33);
            this.btnTaomoi.TabIndex = 51;
            this.btnTaomoi.Text = "Tạo mới";
            this.btnTaomoi.UseVisualStyleBackColor = false;
            this.btnTaomoi.Click += new System.EventHandler(this.btnTaomoi_Click);
            // 
            // frmTaodonnhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 672);
            this.Controls.Add(this.btnTaomoi);
            this.Controls.Add(this.btnXoaHD);
            this.Controls.Add(this.btnInHD);
            this.Controls.Add(this.btnBoqua);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnBoquaHD);
            this.Controls.Add(this.btnLuuHD);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.lblTongtien);
            this.Controls.Add(this.lblSoluongSP);
            this.Controls.Add(this.lblSoSP);
            this.Controls.Add(this.lblTongtienChu);
            this.Controls.Add(this.dgvDSSP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "frmTaodonnhap";
            this.Text = "frmTaodonnhap";
            this.Load += new System.EventHandler(this.frmTaodonnhap_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoluong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiamgia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSSP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvDSSP;
        private System.Windows.Forms.Label lblTongtienChu;
        private System.Windows.Forms.Label lblSoSP;
        private System.Windows.Forms.Label lblSoluongSP;
        private System.Windows.Forms.Label lblTongtien;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpNgaynhap;
        private System.Windows.Forms.TextBox txtMaHDN;
        private System.Windows.Forms.ComboBox cboMaNV;
        private System.Windows.Forms.TextBox txtTenNV;
        private System.Windows.Forms.ComboBox cboMaNCC;
        private System.Windows.Forms.TextBox txtDiachi;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.TextBox txtTenNCC;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtDonGiaNhap;
        private System.Windows.Forms.TextBox txtTenSP;
        private System.Windows.Forms.ComboBox cboMaSP;
        private System.Windows.Forms.TextBox txtThanhtien;
        private System.Windows.Forms.NumericUpDown nudSoluong;
        private System.Windows.Forms.NumericUpDown nudGiamgia;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnBoquaHD;
        private System.Windows.Forms.Button btnLuuHD;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnBoqua;
        private System.Windows.Forms.Button btnInHD;
        private System.Windows.Forms.Button btnXoaHD;
        private System.Windows.Forms.Button btnTaomoi;
    }
}