using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DB;
using DemoEx.data;
using DemoEx.utility;


namespace DemoEx
{
    public partial class AddDealForm : Form
    {
        private Db db = new Db(Connection.connectionString);
        private string empLogin;
        private int dealId;
        private string estateType;
        private int estateId;
        private int ownerId;
        public AddDealForm(string empLogin, int dealId = 0)
        {
            InitializeComponent();
            this.empLogin = empLogin;
            this.dealId = dealId;
        }

        private void AddDealForm_Load(object sender, EventArgs e)
        {
            objectDgv.RowTemplate.Height = 70;
            clientsDgv.RowTemplate.Height = 70;
            objectDgv.MultiSelect = false;
            objectDgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            objectDgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(246, 246, 246);
            objectDgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dealStatusCb.DropDownStyle = ComboBoxStyle.DropDownList;
            dealStatusCb.Items.Add("Новая");

            if (dealId == 0)
            {
                employeeFIO.Text = db.getValuesFromColumn($"SELECT concat(surname, ' ', name, ' ', patronymic) FROM db17.employees where login='{empLogin}';")[0].ToString();
                fillDgv();
                dealStatusCb.SelectedIndex = 0;
                //switch (type)
                //{
                //    case "Покупатель":
                //        db.FillDGV(dataGridView1, "select objectid, photo, square as 'Площадь', rooms as 'Кол-во комнат', price as 'Цена', owner_id as 'Владелец' from object where status='В продаже';");
                //        dataGridView1.Columns["photo"].Visible = false;
                //        dataGridView1.Columns["objectid"].Visible = false;
                //        dataGridView1.Columns["Владелец"].Visible = false;
                //        db.setUpDgvImages(dataGridView1, "Фото объекта");
                //        break;
                //    case "Арендатель":
                //        db.FillDGV(dataGridView1, "select objectid, photo, square as 'Площадь', rooms as 'Кол-во комнат', price as 'Цена', owner_id as 'Владелец' from object where status='Сдается в аренду';");
                //        dataGridView1.Columns["photo"].Visible = false;
                //        dataGridView1.Columns["objectid"].Visible = false;
                //        dataGridView1.Columns["Владелец"].Visible = false;
                //        db.setUpDgvImages(dataGridView1, "Фото объекта");
                //        break;
                //}

                //var dealNumber = db.getIntValuesFromColumn("select id from deals ORDER BY id DESC LIMIT 1;")[0] + 1;
                //numberLabel.Text += " " + dealNumber.ToString();
                //db.FillLabel(employeeFIO, $"select CONCAT(surname,' ', name,' ', patronymic) from employees where login='{empLogin}';");
                //clientFIO.Text = db.getValuesFromColumn($"select CONCAT(surname,' ', name,' ', patronymic) from clients where id={clientId};")[0];
                //var owner_id = dataGridView1.SelectedCells[0].Value;
                //ownerTb.Text = db.getValuesFromColumn($"select concat(surname,' ', name,' ', patronymic) from clients where id={owner_id};")[0];
            } else
            {
                this.Text = "Редактирование сделки";
                
                dateTimePicker1.Enabled = false;
                

                //db.FillDGV(dataGridView1, $"select id, photo, square as 'Площадь', rooms as 'Кол-во комнат', price as 'Цена', owner_id as 'Владелец' from estate where id={estateId};");
                //dataGridView1.Columns["photo"].Visible = false;
                //dataGridView1.Columns["id"].Visible = false;
                //dataGridView1.Columns["Владелец"].Visible = false;
                //db.setUpDgvImages(dataGridView1, "Фото объекта");

                //ownerTb.Text = db.getValuesFromColumn($"select (select concat(surname,' ', name,' ', patronymic) from clients where id=owner_id) from estate where id={estateId};")[0];
                //ownerId = db.getIntValuesFromColumn($"select (select id from clients where id=owner_id) from estate where id={estateId};")[0];

                //dateTimePicker1.Value = db.getDateValuesFromColumn($"select transaction_date from deals where id={clientId};")[0];
                //db.FillLabel(clientFIO, $"select (select CONCAT(surname, ' ', name, ' ',patronymic) from clients where id=client) from deals where id={clientId};");
                //db.FillLabel(employeeFIO, $"select (select CONCAT(surname,' ', name, ' ', patronymic) from employees where id=employees) from deals where id={clientId};");
                //type.Text = db.getValuesFromColumn($"select type from deals where id={clientId};")[0].ToString();
                //status.Text = db.getValuesFromColumn($"select status from deals where id={clientId};")[0].ToString();
            }
        }

        private void fillDgv()
        {
            db.FillDGV(clientsDgv, $"SELECT id, concat(surname, ' ', name, ' ', patronymic) as 'ФИО' FROM db17.clients where type ='Покупатель' or type ='Арендатель';");
            clientsDgv.Columns[0].Visible = false;
            db.FillDGV(objectDgv, $"SELECT objectid, object_type.type as 'Тип объекта', square as 'Площадь', rooms as 'Км.', price as 'Цена', photo FROM db17.object JOIN object_type ON object_type.id=object.object_type;");
            objectDgv.Columns[0].Visible = false;
            objectDgv.Columns[2].Width = 40;
            objectDgv.Columns[3].Width = 40;
            objectDgv.Columns[4].Width = 100;
            db.setUpDgvImages(objectDgv, "Фото объекта");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var estateId = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            //try
            //{
            //    db.executeNonQuery($"INSERT INTO `db17`.`deals` (`client`, `estate`, `employees`, `type`, `transaction_date`, `status`) VALUES ('{clientId}', '{estateId}', '{db.getIntValuesFromColumn($"select id from employees where login='{empLogin}'")[0]}', '{type.Text}', '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}', '{status.Text}')");
            //    MessageBox.Show("Сделка создана!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    if (type.Text == "Аренда")
            //    {
            //        db.executeNonQuery($"UPDATE `db17`.`estate` SET `status` = '{"Арендовано"}' WHERE (`id` = '{estateId}');");
            //    } else if (type.Text == "Покупка")
            //    {
            //        db.executeNonQuery($"UPDATE `db17`.`estate` SET `status` = '{"Продан"}' WHERE (`id` = '{estateId}');");
            //    }
            //}
            //catch (Exception exc)
            //{
            //    MessageStore.somethingWentWrongMessage();
            //}

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                db.FillDGV(clientsDgv, $"SELECT id, concat(surname, ' ', name, ' ', patronymic) as 'ФИО' FROM db17.clients where Surname like \"{textBox1.Text}%\" and (type ='Покупатель' or type ='Арендатель');");
            }
            else if (textBox1.Text.Length == 0)
            {
                db.FillDGV(clientsDgv, $"SELECT id, concat(surname, ' ', name, ' ', patronymic) as 'ФИО' FROM db17.clients where type ='Покупатель' or type ='Арендатель';");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputFieldCorrection.ruLettersField(e);
        }
    }
}
