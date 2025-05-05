using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DB;
using System.Text.RegularExpressions;
using DemoEx.utility;


namespace DemoEx
{
    public partial class AddClientForm : Form
    {
        private Db db = new Db(Connection.connectionString);
        private bool a = false;
        private int id;
        public AddClientForm(int id = 0)
        {
            InitializeComponent();
            this.id = id;
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            type.DropDownStyle = ComboBoxStyle.DropDownList;

            type.Items.Add("Покупатель");
            type.Items.Add("Арендатель");
            type.Items.Add("Продавец");
            type.Items.Add("Арендодатель");


            type.SelectedIndex = 0;
            dateTimePicker1.MinDate = DateTime.Now.AddYears(-99);
            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);

            if (id == 0)
            {
                button1.Text = "Добавить";  
            }
            else
            {
                button1.Text = "Редактировать";
                surname.Text = db.getValuesFromColumn($"select surname from clients where id={id};")[0];
                name.Text = db.getValuesFromColumn($"select name from clients where id={id};")[0];
                pat.Text = db.getValuesFromColumn($"select Patronymic from clients where id={id};")[0];
                passport.Text = db.getValuesFromColumn($"select passport from clients where id={id};")[0];
                phone.Text = db.getValuesFromColumn($"select phone_number from clients where id={id};")[0];
                type.Text = db.getValuesFromColumn($"select type from clients where id={id};")[0];
                address.Text = db.getValuesFromColumn($"select address from clients where id={id};")[0];
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputFieldCorrection.ruLettersField(e);
        }             

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (id == 0)
                {
                    db.executeNonQuery($"INSERT INTO `db17`.`clients` (`Surname`, `Name`, `Patronymic`, `passport`, `address`,`birth`, `phone_number`, `type`) VALUES ('{surname.Text}', '{name.Text}', '{pat.Text}', '{passport.Text}', '{address.Text}','{dateTimePicker1.Value.ToString("yyyy-MM-dd")}', '{phone.Text}', '{type.Text}');");
                    MessageStore.clientAddedMessage();
                    clearAllFields();            
                }
                else
                {
                    db.executeNonQuery($"UPDATE `db17`.`clients` SET `Surname` = '{surname.Text}', `Name` = '{name.Text}', `Patronymic` = '{pat.Text}', `passport` = '{passport.Text}', `address` = '{address.Text}', `birth` = '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}', `phone_number` = '{phone.Text}', `type` = '{type.Text}' WHERE (`id` = '{id}');");
                    MessageStore.clientDataEditedMessage();
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageStore.somethingWentWrongMessage();
            }
        }

        private void clearAllFields()
        {
            surname.Clear();
            name.Clear();
            pat.Clear();
            phone.Clear();
            address.Clear();
            passport.Clear();
        }

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            InputFieldCorrection.ruLettersField(e);
        }

        private void textBox3_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            InputFieldCorrection.ruLettersField(e);
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputFieldCorrection.numbersField(e);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}