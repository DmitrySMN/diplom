using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DB;

namespace DemoEx
{
    public partial class AddEstateForm : Form
    {
        private string photo = "home.png";
        private Db db = new Db(Connection.connectionString);
        private int id;
        public AddEstateForm(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddEstateForm_Load(object sender, EventArgs e)
        {
            type.DropDownStyle = ComboBoxStyle.DropDownList;
            status.DropDownStyle = ComboBoxStyle.DropDownList;
            type.Items.AddRange(db.getValuesFromColumn("select type from estate_type;").ToArray());
            
            type.SelectedIndex = 0;
            dataGridView1.RowTemplate.Height = 50;
            db.FillDGV(dataGridView1, "select id as '№', CONCAT(surname, name, patronymic) as 'ФИО', phone_number as 'Телефон' from clients where type='Продавец' or type ='Арендодатель';");
            dataGridView1.Columns["№"].Width = 35;
            dataGridView1.Columns["Телефон"].Width = 200;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(246, 246, 246);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.Enabled = false;

            if (id == 0)
            {
                button1.Text = "Добавить";
                status.Items.Clear();
                status.Items.Add("Продажа");
                status.Items.Add("Сдача в аренду");
            } else
            {
                status.Items.Clear();
                status.Items.Add("В продаже");
                status.Items.Add("Сдается в аренду");
                status.Text = db.getValuesFromColumn($"select status from estate where id={id};")[0];
                photo = db.getValuesFromColumn($"select photo from estate where id={id};")[0];
                button1.Text = "Редактировать";
                type.Text = db.getValuesFromColumn($"select (select type from estate_type where id=estate_type) from estate where id={id};")[0];
                type.Text = db.getValuesFromColumn($"select status from estate where id={id};")[0];
                address.Text = db.getValuesFromColumn($"select address from estate where id={id};")[0];
                squareCount.Text = db.getIntValuesFromColumn($"select square from estate where id={id};")[0].ToString();
                roomsCount.Text = db.getIntValuesFromColumn($"select rooms from estate where id={id};")[0].ToString();
                price.Text = db.getIntValuesFromColumn($"select price from estate where id={id};")[0].ToString();
                cadastral.Text = db.getValuesFromColumn($"select cadastral from estate where id={id};")[0];
                cadastral.Enabled = false;
                db.FillDGV(dataGridView1, $"select id, CONCAT(surname, name, patronymic) as 'ФИО', phone_number as 'Телефон' from clients where id=(select owner_id from estate where id={id});");
                dataGridView1.Columns["id"].Visible = false;
                pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\imges\\estate\\" + db.getValuesFromColumn($"select photo from estate where id={id};")[0]);
            }

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsNumber(ch) && !char.IsControl(ch))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsNumber(ch) && !char.IsControl(ch))
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                FileInfo fi = new FileInfo(filePath);
                string[] path = filePath.Split('\\');
                photo = path[path.Length - 1];
                string newFilePath = $"\\imges\\estate\\{photo}";
                fi.CopyTo("." + newFilePath, true);
                pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\imges\\estate\\" + photo);
            }
            else
            {
                MessageBox.Show("Выберите файл!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsNumber(ch) && !char.IsControl(ch))
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Regex.Match(ch.ToString(), @"[а-яА-Я]|[\b]|[-]").Success && !char.IsNumber(ch) && !char.IsControl(ch) && ch != ',' && ch != '.')
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (id == 0)
                {
                    if (dataGridView1.SelectedCells.Count > 0 || price.Text.Length > 4 || squareCount.Text.Length > 1 || roomsCount.Text.Length > 0 || address.Text.Length > 0)
                    {
                        db.executeNonQuery($"INSERT INTO `db17`.`estate` (`estate_type`, `owner_id`, `address`, `square`, `cadastral`,`rooms`, `price`, `photo`, `status`) VALUES ('{db.getIntValuesFromColumn($"select id from estate_type where type='{type.Text}'")[0]}', '{dataGridView1.SelectedCells[0].Value}', '{address.Text}', '{squareCount.Text}', '{cadastral.Text}', '{roomsCount.Text}', '{price.Text}', '{photo}', '{(status.Text == "Продажа" ? "В продаже" : "Сдается в аренду")}');");
                        MessageBox.Show("Объект успешно добавлен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        price.Clear();
                        squareCount.Clear();
                        roomsCount.Clear();
                        address.Clear();
                        dataGridView1.ClearSelection();
                    }
                    else
                    {
                        MessageBox.Show("Поля заполнены некорректно!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                   db.executeNonQuery($"UPDATE `db17`.`estate` SET `estate_type` = '{db.getValuesFromColumn($"select id from estate_type where type='{type.Text}';")[0]}', `owner_id` = '{dataGridView1.SelectedRows[0].Cells[0].Value}', `address` = '{address.Text}', `square` = '{squareCount.Text}', `rooms` = '{roomsCount.Text}', `price` = '{price.Text}', `photo` = '{photo}', `status` = '{status.Text}' WHERE (`id` = '{id}');");
                    MessageBox.Show("Данные объекта успешно отредактированы!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что-то пошло не так!");
            }
        }

        private void status_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (status.Text)
            { 
                case ("Продажа"):
                    dataGridView1.Enabled = true;
                    dataGridView1.RowTemplate.Height = 50;
                    db.FillDGV(dataGridView1, "select id as '№', CONCAT(surname, name, patronymic) as 'ФИО', phone_number as 'Телефон' from clients where type='Продавец';");
                    dataGridView1.Columns["№"].Width = 35;
                    dataGridView1.Columns["Телефон"].Width = 200;
                    dataGridView1.MultiSelect = false;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(246, 246, 246);
                    dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
                    break;
                case ("Сдача в аренду"):
                    dataGridView1.Enabled = true;
                    dataGridView1.RowTemplate.Height = 50;
                    db.FillDGV(dataGridView1, "select id as '№', CONCAT(surname, name, patronymic) as 'ФИО', phone_number as 'Телефон' from clients where type ='Арендодатель';");
                    dataGridView1.Columns["№"].Width = 35;
                    dataGridView1.Columns["Телефон"].Width = 200;
                    dataGridView1.MultiSelect = false;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(246, 246, 246);
                    dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
                    break;
            }
        }
    }
}
