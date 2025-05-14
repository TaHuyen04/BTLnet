namespace QLCHBanXeMay.form
{
    partial class frmHangSX
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
            this.btnDong = new System.Windows.Forms.Button();
            this.btnBoqua = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.dgvHangsanxuat = new System.Windows.Forms.DataGridView();
            this.txtTenhangsanxuat = new System.Windows.Forms.TextBox();
            this.txtMahangsanxuat = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHangsanxuat)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnDong.Location = new System.Drawing.Point(681, 393);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(64, 33);
            this.btnDong.TabIndex = 10;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click_1);
            // 
            // btnBoqua
            // 
            this.btnBoqua.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnBoqua.Location = new System.Drawing.Point(541, 393);
            this.btnBoqua.Name = "btnBoqua";
            this.btnBoqua.Size = new System.Drawing.Size(89, 33);
            this.btnBoqua.TabIndex = 11;
            this.btnBoqua.Text = "Bỏ qua";
            this.btnBoqua.UseVisualStyleBackColor = false;
            this.btnBoqua.Click += new System.EventHandler(this.btnBoqua_Click_1);
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnLuu.Location = new System.Drawing.Point(440, 393);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(64, 33);
            this.btnLuu.TabIndex = 12;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click_1);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnSua.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnSua.Location = new System.Drawing.Point(307, 393);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(64, 33);
            this.btnSua.TabIndex = 13;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click_1);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnXoa.Location = new System.Drawing.Point(188, 393);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(64, 33);
            this.btnXoa.TabIndex = 14;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click_1);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnThem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnThem.Location = new System.Drawing.Point(60, 393);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(64, 33);
            this.btnThem.TabIndex = 15;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click_1);
            // 
            // dgvHangsanxuat
            // 
            this.dgvHangsanxuat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHangsanxuat.Location = new System.Drawing.Point(45, 198);
            this.dgvHangsanxuat.Name = "dgvHangsanxuat";
            this.dgvHangsanxuat.RowHeadersWidth = 51;
            this.dgvHangsanxuat.RowTemplate.Height = 24;
            this.dgvHangsanxuat.Size = new System.Drawing.Size(711, 150);
            this.dgvHangsanxuat.TabIndex = 9;
            this.dgvHangsanxuat.Click += new System.EventHandler(this.dgvHangsanxuat_Click_1);
            // 
            // txtTenhangsanxuat
            // 
            this.txtTenhangsanxuat.Location = new System.Drawing.Point(184, 131);
            this.txtTenhangsanxuat.Name = "txtTenhangsanxuat";
            this.txtTenhangsanxuat.Size = new System.Drawing.Size(100, 22);
            this.txtTenhangsanxuat.TabIndex = 7;
            this.txtTenhangsanxuat.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTenhangsanxuat_KeyUp_1);
            // 
            // txtMahangsanxuat
            // 
            this.txtMahangsanxuat.Location = new System.Drawing.Point(184, 89);
            this.txtMahangsanxuat.Name = "txtMahangsanxuat";
            this.txtMahangsanxuat.Size = new System.Drawing.Size(100, 22);
            this.txtMahangsanxuat.TabIndex = 8;
            this.txtMahangsanxuat.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMahangsanxuat_KeyUp_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tên hãng sản xuất";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mã hãng sản xuất";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(204, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(407, 33);
            this.label1.TabIndex = 6;
            this.label1.Text = "DANH MỤC HÃNG SẢN XUẤT";
            // 
            // frmHangSX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnBoqua);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dgvHangsanxuat);
            this.Controls.Add(this.txtTenhangsanxuat);
            this.Controls.Add(this.txtMahangsanxuat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmHangSX";
            this.Text = "frmHangSX";
            this.Load += new System.EventHandler(this.frmHangSX_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHangsanxuat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnBoqua;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.DataGridView dgvHangsanxuat;
        private System.Windows.Forms.TextBox txtTenhangsanxuat;
        private System.Windows.Forms.TextBox txtMahangsanxuat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}