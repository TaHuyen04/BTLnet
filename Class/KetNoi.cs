using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace QLCHBanXeMay.Class
{
    internal class KetNoi
    {
        public static SqlConnection Conn;
        public static string Connstring;
        public static void Ketnoi()
        {
            // Connstring = "Data Source=XUAN;Initial Catalog=QLCH_BanXeMay;Integrated Security=True ";
            //hyn
            //Connstring = "Data Source=DESKTOP-R3DMC9I;Initial Catalog=QLCH_BanXe;Integrated Security=True";
            //Connstring = @"Data Source=LAPTOP-L1V19H5J\HOANGEXPRESS;Initial Catalog=QLCH_BanXe;Integrated Security=True;Encrypt=False";
            Connstring = "Data Source=PTMINH;Initial Catalog=QLCH_BanXe;Integrated Security=True";
            Conn = new SqlConnection();
            Conn.ConnectionString = Connstring;
            Conn.Open();
            MessageBox.Show("Ket noi thanh cong");
        }
    }
}
