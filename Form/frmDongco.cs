using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHBanXeMay.form
{
    public partial class frmDongco : Form
    {
        public frmDongco()
        {
            InitializeComponent();
        }

        private void frmDongco_Load(object sender, EventArgs e)
        {
            txtMadongco.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
           
        }
        
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


