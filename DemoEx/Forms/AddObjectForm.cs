using DB;
using DemoEx.utility;
using MySqlX.XDevAPI;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DemoEx.Forms
{
    public partial class AddObjectForm : Form
    {
        private Db db = new Db(Connection.connectionString);
        private string photoName = null;
        private int objectId;
        public AddObjectForm(int objectId = 0)
        {
            InitializeComponent();
            this.objectId = objectId;
        }

        private void AddObjectForm_Load(object sender, EventArgs e)
        {
            owner_cb.DropDownStyle = ComboBoxStyle.DropDownList;
            status_cb.DropDownStyle = ComboBoxStyle.DropDownList;
            object_type_cb.DropDownStyle = ComboBoxStyle.DropDownList;

            string[] status = { "В продаже", "Продан", "Арендуется", "Арендован" };
            status_cb.Items.AddRange(status);

            try
            {
                object_type_cb.Items.AddRange(db.getValuesFromColumn("SELECT type FROM db17.object_type;").ToArray());

                if (objectId == 0)
                {
                    owner_cb.Items.AddRange(db.getValuesFromColumn("SELECT concat(surname,' ', name, ' ', patronymic) FROM db17.clients where type='Продавец' or type='Арендодатель';").ToArray());
                    button1.Text = "Добавить";
                }
                else
                {
                    fillDataFields();
                    button1.Text = "Редактировать";
                }
            }
            catch (Exception ex)
            {
                MessageStore.somethingWentWrongMessage();
            }
        }

        private void fillDataFields()
        {
            owner_cb.Items.Add(db.getValuesFromColumn($"SELECT concat(clients.surname,' ', clients.name, ' ', clients.patronymic) FROM db17.object JOIN clients ON clients.id = object.owner_id where objectid = {objectId};")[0]);
            owner_cb.SelectedIndex = 0;
            string photoName = db.getValuesFromColumn($"SELECT photo FROM db17.object where objectid = {objectId};")[0];
            pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\assets\\images\\estate\\" + photoName);
            address_tb.Text = db.getValuesFromColumn($"SELECT object_address FROM db17.object where objectid = {objectId};")[0];
            square_tb.Text = db.getIntValuesFromColumn($"SELECT square FROM db17.object where objectid = {objectId};")[0].ToString();
            cadastral_tb.Text = db.getValuesFromColumn($"SELECT cadastral FROM db17.object where objectid = {objectId};")[0].ToString();
            price_tb.Text = db.getIntValuesFromColumn($"SELECT price FROM db17.object where objectid = {objectId};")[0].ToString();
            rooms_tb.Text = db.getIntValuesFromColumn($"SELECT rooms FROM db17.object where objectid = {objectId};")[0].ToString();
            status_cb.SelectedItem = db.getValuesFromColumn($"SELECT status FROM db17.object where objectid = {objectId};")[0].ToString();
            object_type_cb.SelectedIndex = db.getIntValuesFromColumn($"SELECT object_type FROM db17.object where objectid = {objectId};")[0] - 1;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (objectId == 0)
                {
                    db.executeNonQuery($"INSERT INTO `db17`.`object` (`object_type`, `owner_id`, `object_address`, `square`, `cadastral`, `rooms`, `price`, `photo`, `status`) VALUES ('{db.getIntValuesFromColumn($"SELECT id FROM db17.object_type where type='{object_type_cb.Text}';")[0]}', '{db.getIntValuesFromColumn($"SELECT id FROM db17.clients where concat(surname, ' ', name, ' ', Patronymic) = '{owner_cb.Text}';")[0]}', '{address_tb.Text}', '{square_tb.Text}', '{cadastral_tb.Text}', '{rooms_tb.Text}', '{price_tb.Text}', '{((photoName == null) ? "home.png" : photoName)}', '{status_cb.Text}');");
                    MessageStore.addObjectMessage();
                    clearAllFields();
                }
                else
                {
                    MessageBox.Show("Редактирование");
                    Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }    

        private void button3_Click(object sender, EventArgs e)
        {
            photoName = selectPhoto();
        }

        private void clearAllFields()
        {
            address_tb.Clear();
            square_tb.Clear();
            cadastral_tb.Clear();
            price_tb.Clear();
            rooms_tb.Clear();
        }

        private string selectPhoto()
        {
            openFileDialog1.Filter = "Изображения (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
            openFileDialog1.Title = "Выберита изображение";

            string photoname = "";
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(openFileDialog1.FileName);

                    string photosFolder = Environment.CurrentDirectory + $"\\assets\\images\\estate";

                    string fileName = Path.GetFileName(openFileDialog1.FileName);
                    photoname = fileName;
                    string destinationPath = Path.Combine(photosFolder, fileName);

                    if (File.Exists(destinationPath))
                    {
                        string newFileName = Path.GetFileNameWithoutExtension(fileName) + "_копия" + Path.GetExtension(fileName);
                        destinationPath = Path.Combine(photosFolder, newFileName);
                    }

                    File.Copy(openFileDialog1.FileName, destinationPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            return photoname;
        }

        private void square_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputFieldCorrection.numbersField(e);
        }

        private void rooms_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputFieldCorrection.numbersField(e);
        }

        private void price_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputFieldCorrection.numbersField(e);
        }

        private void address_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputFieldCorrection.ruAddressField(e);
        }

        private void rooms_tb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
