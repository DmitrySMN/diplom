using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB
{
    internal class Db
    {
        private string conStr;
        private MySqlConnection con;
        private MySqlCommand cmd;
        private MySqlDataAdapter adapter;
        private MySqlDataReader reader;
        private System.Data.DataTable dt;
        private string captchaText;


        public Db(string connnectionString)
        {
            this.conStr = connnectionString;
        }

        public string getCaptchaText()
        {
            return this.captchaText;
        }

        public void FillDGV(DataGridView dgv, string query)
        {
            con = new MySqlConnection(conStr);
            cmd = new MySqlCommand(query, con);
            adapter = new MySqlDataAdapter(cmd);
            dt = new System.Data.DataTable();
            adapter.Fill(dt);
            dgv.DataSource = dt;
        }

        
        public List<string> getValuesFromColumn(string selectQuery)
        {
            List<string> values = new List<string>();
            using (con = new MySqlConnection(conStr))
            {
                con.Open();
                cmd = new MySqlCommand(selectQuery, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    values.Add(reader.GetString(0).ToString());
                }
            }
            return values;
        }

        public List<DateTime> getDateValuesFromColumn(string selectQuery)
        {
            List<DateTime> values = new List<DateTime>();
            using (con = new MySqlConnection(conStr))
            {
                con.Open();
                cmd = new MySqlCommand(selectQuery, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    values.Add(reader.GetDateTime(0));
                }
            }
            return values;
        }

        public List<int> getIntValuesFromColumn(string selectQuery)
        {
            List<int> values = new List<int>();
            using (con = new MySqlConnection(conStr))
            {
                con.Open();
                cmd = new MySqlCommand(selectQuery, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    values.Add(reader.GetInt32(0));
                }
            }
            return values;
        }

        public void FillLabel(Label label, string selectQuery)
        {
            using (con = new MySqlConnection(conStr))
            {
                con.Open();
                cmd = new MySqlCommand(selectQuery, con);
                reader = cmd.ExecuteReader();
                reader.Read();
                string res = reader.GetString(0);
                label.Text += res;
            }
        }

        public string getHashFromPassword(string password)
        {
            byte[] bytesPass = Encoding.UTF8.GetBytes(password);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytesPass);
            string hashPasswd = string.Empty;
            foreach (byte x in hash)
            {
                hashPasswd += String.Format("{0:x2}", x);
            }
            hashstring.Dispose();
            return hashPasswd;
        }


        public int executeNonQuery(string nonQuery)
        {
            using (con = new MySqlConnection(conStr))
            {
                cmd = new MySqlCommand(nonQuery, con);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public bool comparePasswords(string password, string selectQuery)
        {
            string dbPassword = null;
            using (con = new MySqlConnection(conStr))
            {
                con.Open();
                cmd = new MySqlCommand(selectQuery, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dbPassword = reader.GetString(0);
                }
                return getHashFromPassword(password) == dbPassword;
            }
        }


        public void setUpDgvImages(DataGridView dgvName, string photoColumnName)
        {
            dgvName.Columns["photo"].Visible = false;

            if (dgvName.Columns[photoColumnName] != null)
            {
                dgvName.Columns.Remove(photoColumnName);
            }

            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = photoColumnName;
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dgvName.Columns.Add(imageColumn);
            foreach (DataGridViewRow row in dgvName.Rows)
            {
                string photoName = row.Cells["photo"].Value.ToString();

                using (Image image = Image.FromFile(Directory.GetCurrentDirectory() + "\\assets\\images\\estate\\" + photoName))
                {
                    row.Cells[photoColumnName].Value = new Bitmap(image);
                }
            }
        }

        public Bitmap createImageForCaptcha(int Width, int Height, int codeLength)
        {
            Random rnd = new Random();


            Bitmap result = new Bitmap(Width, Height);


            int Xpos = Width / 4;
            int Ypos = Height / 4;


            Brush[] colors = {
                                Brushes.Black,
                                Brushes.Red,
                                Brushes.RoyalBlue,
                                Brushes.Green,
                                Brushes.Yellow,
                                Brushes.White,
                                Brushes.Tomato,
                                Brushes.Sienna,
                                Brushes.Pink };


            Pen[] colorpens = {
                                Pens.Black,
                                Pens.Red,
                                Pens.RoyalBlue,
                                Pens.Green,
                                Pens.Yellow,
                                Pens.White,
                                Pens.Tomato,
                                Pens.Sienna,
                                Pens.Pink };


            FontStyle[] fontstyle = {
                                FontStyle.Bold,
                                FontStyle.Italic,
                                FontStyle.Regular,
                                FontStyle.Strikeout,
                                FontStyle.Underline};


            Int16[] rotate = { 1, -1, 2, -2, 3, -3, 4, -4, 5, -5, 6, -6 };


            Graphics g = Graphics.FromImage(result);


            g.Clear(Color.Gray);


            g.RotateTransform(rnd.Next(rotate.Length));


            captchaText = String.Empty;
            string ALF = "1234567890qwertyuiopasdfghjklzxcvbnm";
            for (int i = 0; i < codeLength; ++i)
            {
                captchaText += ALF[rnd.Next(ALF.Length)];
            }


            g.DrawString(captchaText,
                new System.Drawing.Font("Arial", 35, fontstyle[rnd.Next(fontstyle.Length)]),
                colors[rnd.Next(colors.Length)],
                new PointF(Xpos, Ypos));


            g.DrawLine(colorpens[rnd.Next(colorpens.Length)],
                       new System.Drawing.Point(0, 0),
                       new System.Drawing.Point(Width - 1, Height - 1));

            g.DrawLine(colorpens[rnd.Next(colorpens.Length)],
                       new System.Drawing.Point(0, Height - 1),
                       new System.Drawing.Point(Width - 1, 0));



            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, Color.White);
            return result;
        }
    }
}
