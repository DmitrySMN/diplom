using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DB;
using DemoEx.utility;

namespace DemoEx
{
    public partial class AddEmployee : Form
    {
        private Db db = new Db(Connection.connectionString);
        private int id;
        public AddEmployee(int id = 0)
        {
            InitializeComponent();
            this.id = id;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputFieldCorrection.engLettersNumbersField(e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id == 0)
            {
                if (loginTb.Text.Length > 3 && passport.Text.Length == 11 && surname.Text.Length > 1 && name.Text.Length > 2 && pat.Text.Length > 4 && address.Text.Length > 0 && phone.Text.Length == 18)
                {
                    List<string> logins = db.getValuesFromColumn("select login from employees;");

                    if (logins.Contains(loginTb.Text))
                    {
                        MessageBox.Show("Сотрудник с таким логином уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    db.executeNonQuery($"INSERT INTO `db17`.`employees` (`login`, `password`, `Surname`, `Name`, `Patronymic`, `passport`, `birth`, `phone_number`, `address`, `post`, `photo`) VALUES ('{loginTb.Text}', '{db.getHashFromPassword(pwdTb.Text)}', '{surname.Text}', '{name.Text}', '{pat.Text}', '{passport.Text}', '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}', '{phone.Text}', '{address.Text}', '2', 'user.png');");
                    loginTb.Clear();
                    pwdTb.Clear();
                    surname.Clear();
                    name.Clear();
                    pat.Clear();
                    passport.Clear();
                    phone.Clear();
                    address.Clear();
                    MessageBox.Show("Сотрудник добавлен в систему!");

                }
                else
                {
                    MessageBox.Show("Некоторые поля заполнены некорректно!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            } else
            {
                if (pwdTb.Text.Length == 0)
                {
                    db.executeNonQuery($"UPDATE `db17`.`employees` SET `login` = '{loginTb.Text}', `Surname` = '{surname.Text}', `Name` = '{name.Text}', `Patronymic` = '{pat.Text}', `passport` = '{passport.Text}', `birth` = '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}', `phone_number` = '{phone.Text}', `address` = '{address.Text}' WHERE (`id` = {id});");
                    MessageBox.Show("Данные сотрудника изменены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    db.executeNonQuery($"UPDATE `db17`.`employees` SET `login` = '{loginTb.Text}', password = '{db.getHashFromPassword(pwdTb.Text)}', `Surname` = '{surname.Text}', `Name` = '{name.Text}', `Patronymic` = '{pat.Text}', `passport` = '{passport.Text}', `birth` = '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}', `phone_number` = '{phone.Text}', `address` = '{address.Text}' WHERE (`id` = {id});");

                    MessageBox.Show("Данные сотрудника изменены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void AddEmployee_Load(object sender, EventArgs e)
        {
            

            dateTimePicker1.MinDate = DateTime.Now.AddYears(-99);
            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);

            if (id == 0)
            {
                
            } else
            {
                groupBox7.Text = "Новый пароль";

                surname.Text = db.getValuesFromColumn($"select surname from employees where id={id};")[0];
                name.Text = db.getValuesFromColumn($"select name from employees where id={id};")[0];
                pat.Text = db.getValuesFromColumn($"select patronymic from employees where id={id};")[0];
                loginTb.Text = db.getValuesFromColumn($"select login from employees where id={id};")[0];
                passport.Text = db.getValuesFromColumn($"select passport from employees where id={id};")[0];
                phone.Text = db.getValuesFromColumn($"select phone_number from employees where id={id};")[0];
                address.Text = db.getValuesFromColumn($"select address from employees where id={id};")[0];
                //dateTimePicker1.Value = db.getDateValuesFromColumn($"select birth from employees where id={id};")[0];

                button1.Text = "Редактирование";
            }
        }

        private void surname_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputFieldCorrection.ruLettersField(e);
        }

        private void name_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputFieldCorrection.ruLettersField(e);
        }

        private void pat_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputFieldCorrection.ruLettersField(e);
        }

        private void pwdTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputFieldCorrection.engLettersNumbersField(e);
        }

        private void address_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputFieldCorrection.ruLettersNumbersField(e);
        }
    }
}
