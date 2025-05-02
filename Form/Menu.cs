using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLCHBanXeMay.Class;

namespace QLCHBanXeMay.form
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            Class.KetNoi.Ketnoi();

            panelMenu.BackColor = Color.FromArgb(245, 245, 255); 
            // Duyệt qua tất cả control trong panelMenu
            foreach (Control ctrl in panelMenu.Controls)
            {
                // Nếu là GroupBox thì xử lý riêng
                if (ctrl is GroupBox group)
                {
                    group.ForeColor = Color.MidnightBlue;
                    group.BackColor = Color.Transparent;
                    // Duyệt các nút bên trong GroupBox
                    foreach (Control btn in group.Controls)
                    {
                        if (btn is Button button)
                        {
                            FormatButton(button);
                        }
                    }
                }
            }

            foreach (Control btn in grbDM.Controls)
            {
                if (btn is Button button)
                {
                    FormatButton(button);
                }
            }
            panelNhansu.BackColor = Color.FromArgb(245, 245, 255); 
            foreach (Control btn in panelNhansu.Controls)
            {
                if (btn is Button button)
                {
                    FormatButton(button);
                }
            }
        }

        private void FormatButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = SystemColors.GradientActiveCaption;  // 🎨 Màu xanh hệ thống
            btn.ForeColor = Color.DarkSlateGray;
            btn.Font = new Font("Arial", 9F, FontStyle.Bold);

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(178, 235, 242); // hover
        }



        private void button1_Click(object sender, EventArgs e)
        {
           grbDM.Visible = !grbDM.Visible;
            panelNhansu.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelNhansu.Visible = !panelNhansu.Visible;
            grbDM.Visible = false;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            frmLoaixe frmLoaixe = new frmLoaixe();
            frmLoaixe.ShowDialog();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            frmMausac frmMausac = new frmMausac();
            frmMausac.ShowDialog();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            frmDongco frmDongco = new frmDongco();
            frmDongco.ShowDialog();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            frmPhanhxe frmPhanhxe = new frmPhanhxe();
            frmPhanhxe.ShowDialog();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            frmTinhtrang frmTinhtrang = new frmTinhtrang();
            frmTinhtrang.ShowDialog();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            frmHangSX frmHangSX = new frmHangSX();
            frmHangSX.ShowDialog();
        }



        private void btnSanpham_Click(object sender, EventArgs e)
        {
            frmSanpham frmSanpham = new frmSanpham();
            frmSanpham.ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            frmNhanvien frmNhanvien = new frmNhanvien();
            frmNhanvien.ShowDialog();
        }



        private void btnKH_Click(object sender, EventArgs e)
        {
            frmKhachhang frmKhachhang = new frmKhachhang();
            frmKhachhang.ShowDialog();
        }

        private void btnNCC_Click(object sender, EventArgs e)
        {
            frmNCC frmNCC = new frmNCC();
            frmNCC.ShowDialog();
        }

        private void btnDSN_Click(object sender, EventArgs e)
        {
            frmHoadonnhap frmHoadonnhap = new frmHoadonnhap();
            frmHoadonnhap.ShowDialog();
        }

        private void btnTaoDN_Click(object sender, EventArgs e)
        {
            frmTaodonnhap frmTaodonnhap = new frmTaodonnhap();
            frmTaodonnhap.ShowDialog();
        }

        private void btnDSB_Click(object sender, EventArgs e)
        {
            frmDondathang frmDondathang = new frmDondathang();
            frmDondathang.ShowDialog();
        }

        private void btnTaoDB_Click(object sender, EventArgs e)
        {
            frmTaodonban frmTaodonban = new frmTaodonban();
            frmTaodonban.ShowDialog();
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            frmThongke frmThongke = new frmThongke();
            frmThongke.ShowDialog();
        }

        private void btnBC_Click(object sender, EventArgs e)
        {
            frmBaocao frmBaocao = new frmBaocao();
            frmBaocao.ShowDialog();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            frmCongviec frmCongviec = new frmCongviec();
            frmCongviec.ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            frmNuocSX frmNuocSX = new frmNuocSX();
            frmNuocSX.ShowDialog();
        }
    }
}
