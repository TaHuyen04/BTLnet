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
    internal class Functions
    {

        public static DataTable getdatatotable(string sql)
        {
            SqlDataAdapter mydata = new SqlDataAdapter();
            mydata.SelectCommand = new SqlCommand();
            mydata.SelectCommand.Connection = Class.KetNoi.Conn;
            mydata.SelectCommand.CommandText = sql;
            DataTable table = new DataTable();
            mydata.Fill(table);
            return table;
        }
        public static bool IsDate(string d)
        {
            string[] parts = d.Split('/');
            if ((Convert.ToInt32(parts[0]) >= 1) && (Convert.ToInt32(parts[0]) <= 31) &&
(Convert.ToInt32(parts[1]) >= 1) && (Convert.ToInt32(parts[1]) <= 12) && (Convert.ToInt32(parts[2]) >= 1900))
                return true;
            else
                return false;
        }
        public static bool IsDate1(string d)
        {
            if (string.IsNullOrWhiteSpace(d)) return false;
            string[] parts = d.Split('/');
            if (parts.Length != 3) return false;
            if (int.TryParse(parts[0], out int day) && int.TryParse(parts[1], out int month) && int.TryParse(parts[2], out int year))
            {
                return day >= 1 && day <= 31 && month >= 1 && month <= 12 && year >= 1900;
            }
            return false;
        }
        public static string ConvertDateTime(string d)
        {
            string[] parts = d.Split('/');
            string dt = String.Format("{0}/{1}/{2}", parts[1], parts[0], parts[2]);
            return dt;
        }
        public static bool Checkkey(string sql)
        {
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, KetNoi.Conn);
            DataTable table = new DataTable();
            Mydata.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else return false;
        }

        public static void Runsql(string sql)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Class.KetNoi.Conn;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();
            cmd = null;
        }
        public static void Runsqldel(string sql)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Class.KetNoi.Conn;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Du lieu dang dung, ko the xoa dc", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            cmd.Dispose();
            cmd = null;
        }
        public static void FillCombo(string sql, ComboBox cbo, string ma, string ten)
        {
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, KetNoi.Conn);
            DataTable table = new DataTable();
            Mydata.Fill(table);
            cbo.DataSource = table;

            cbo.ValueMember = ma;    // Truong gia tri
            cbo.DisplayMember = ten;    // Truong hien thi
        }
        public static string GetFieldValues(string sql)
        {
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, KetNoi.Conn);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ma = reader.GetValue(0).ToString();
            }
            reader.Close();
            return ma;
        }


        public static string ChuyenSoSangChu(string number)
            {
                string[] dv = { "", "nghìn", "triệu", "tỷ" };
                string[] cs = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };

                number = number.Replace(",", "");
                double num;
                try
                {
                    num = Convert.ToDouble(number);
                }
                catch
                {
                    return "Số không hợp lệ";
                }

                if (num == 0) return "Không đồng";

                string s = "";
                int i = 0;
                while (num > 0)
                {
                    int n = (int)(num % 1000);
                    if (n != 0)
                    {
                        string str = "";
                        int tram = n / 100;
                        int chuc = (n % 100) / 10;
                        int donvi = n % 10;

                        if (tram != 0)
                            str += cs[tram] + " trăm ";
                        else if (num >= 1000)
                            str += "không trăm ";

                        if (chuc != 0)
                        {
                            if (chuc == 1)
                                str += "mười ";
                            else
                                str += cs[chuc] + " mươi ";
                        }
                        else if (donvi != 0)
                        {
                            if (tram != 0)
                                str += "lẻ ";
                        }

                        if (donvi != 0)
                        {
                            if (chuc == 0 || chuc == 1)
                            {
                                if (donvi == 5)
                                    str += "năm ";
                                else
                                    str += cs[donvi] + " ";
                            }
                            else
                            {
                                if (donvi == 1)
                                    str += "mốt ";
                                else if (donvi == 5)
                                    str += "lăm ";
                                else
                                    str += cs[donvi] + " ";
                            }
                        }

                        s = str + dv[i] + " " + s;
                    }

                    num = Math.Floor(num / 1000);
                    i++;
                }

                s = s.Trim();
                s = char.ToUpper(s[0]) + s.Substring(1);
                return s + " đồng";
            }
        }


    
}
