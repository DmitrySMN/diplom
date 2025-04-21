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
        private int clientId;
        private int edit;
        private string estateType;
        private int estateId;
        private int ownerId;
        public AddDealForm(string empLogin, int clientId, int edit = 0)
        {
            InitializeComponent();
            this.empLogin = empLogin;
            this.clientId = clientId;
            this.edit = edit;
        }

        private void AddDealForm_Load(object sender, EventArgs e)
        {

            dataGridView1.RowTemplate.Height = 70;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(246, 246, 246);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;

            type.DropDownStyle = ComboBoxStyle.DropDownList;
            status.DropDownStyle = ComboBoxStyle.DropDownList;
            ownerTb.Enabled = false;
            type.Items.Clear();
            type.Items.Add("Аренда");
            type.Items.Add("Покупка");
            type.SelectedIndex = 0;

            status.Items.Clear();
            status.Items.Add("Новая");
            status.SelectedIndex = 0;

            if (edit == 0)
            {
                editbutton.Visible = false;
                addButton.Visible = true;

                string type = db.getValuesFromColumn($"select type from clients where id={clientId};")[0].ToString();

                switch (type)
                {
                    case "Покупатель":
                        db.FillDGV(dataGridView1, "select id, photo, square as 'Площадь', rooms as 'Кол-во комнат', price as 'Цена', owner_id as 'Владелец' from estate where status='В продаже';");
                        dataGridView1.Columns["photo"].Visible = false;
                        dataGridView1.Columns["id"].Visible = false;
                        dataGridView1.Columns["Владелец"].Visible = false;
                        db.setUpDgvImages(dataGridView1, "Фото объекта");
                        break;
                    case "Арендатель":
                        db.FillDGV(dataGridView1, "select id, photo, square as 'Площадь', rooms as 'Кол-во комнат', price as 'Цена', owner_id as 'Владелец' from estate where status='Сдается в аренду';");
                        dataGridView1.Columns["photo"].Visible = false;
                        dataGridView1.Columns["id"].Visible = false;
                        dataGridView1.Columns["Владелец"].Visible = false;
                        db.setUpDgvImages(dataGridView1, "Фото объекта");
                        break;
                }

                

                var dealNumber = db.getIntValuesFromColumn("select id from deals ORDER BY id DESC LIMIT 1;")[0] + 1;
                numberLabel.Text += " " + dealNumber.ToString();
                db.FillLabel(employeeFIO, $"select CONCAT(surname,' ', name,' ', patronymic) from employees where login='{empLogin}';");
                clientFIO.Text = db.getValuesFromColumn($"select CONCAT(surname,' ', name,' ', patronymic) from clients where id={clientId};")[0];
                var owner_id = dataGridView1.SelectedCells[0].Value;
                ownerTb.Text = db.getValuesFromColumn($"select concat(surname,' ', name,' ', patronymic) from clients where id={owner_id};")[0];
            } else
            {
                this.Text = "Редактирование сделки";
                editbutton.Visible = true;
                dateTimePicker1.Enabled = false;
                numberLabel.Text +=" " + clientId.ToString();
                status.Items.Add("Завершена");
                status.Items.Add("Не состоялась");

                estateId = db.getIntValuesFromColumn($"select estate from deals where id={clientId};")[0];

                db.FillDGV(dataGridView1, $"select id, photo, square as 'Площадь', rooms as 'Кол-во комнат', price as 'Цена', owner_id as 'Владелец' from estate where id={estateId};");
                dataGridView1.Columns["photo"].Visible = false;
                dataGridView1.Columns["id"].Visible = false;
                dataGridView1.Columns["Владелец"].Visible = false;
                db.setUpDgvImages(dataGridView1, "Фото объекта");

                ownerTb.Text = db.getValuesFromColumn($"select (select concat(surname,' ', name,' ', patronymic) from clients where id=owner_id) from estate where id={estateId};")[0];
                ownerId = db.getIntValuesFromColumn($"select (select id from clients where id=owner_id) from estate where id={estateId};")[0];

                dateTimePicker1.Value = db.getDateValuesFromColumn($"select transaction_date from deals where id={clientId};")[0];
                db.FillLabel(clientFIO, $"select (select CONCAT(surname, ' ', name, ' ',patronymic) from clients where id=client) from deals where id={clientId};");
                db.FillLabel(employeeFIO, $"select (select CONCAT(surname,' ', name, ' ', patronymic) from employees where id=employees) from deals where id={clientId};");
                type.Text = db.getValuesFromColumn($"select type from deals where id={clientId};")[0].ToString();
                status.Text = db.getValuesFromColumn($"select status from deals where id={clientId};")[0].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var owner_id = dataGridView1.SelectedCells[0].Value;
            ownerTb.Text = db.getValuesFromColumn($"select concat(surname,' ', name,' ', patronymic) from clients where id={owner_id};")[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var estateId = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            try
            {
                db.executeNonQuery($"INSERT INTO `db17`.`deals` (`client`, `estate`, `employees`, `type`, `transaction_date`, `status`) VALUES ('{clientId}', '{estateId}', '{db.getIntValuesFromColumn($"select id from employees where login='{empLogin}'")[0]}', '{type.Text}', '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}', '{status.Text}')");
                MessageBox.Show("Сделка создана!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (type.Text == "Аренда")
                {
                    db.executeNonQuery($"UPDATE `db17`.`estate` SET `status` = '{"Арендовано"}' WHERE (`id` = '{estateId}');");
                } else if (type.Text == "Покупка")
                {
                    db.executeNonQuery($"UPDATE `db17`.`estate` SET `status` = '{"Продан"}' WHERE (`id` = '{estateId}');");
                }
            }
            catch (Exception exc)
            {
                MessageStore.somethingWentWrongMessage();
            }

        }

        private void editbutton_Click(object sender, EventArgs e)
        {
            try
            {
                db.executeNonQuery($"update deals set type='{type.Text}', status='{status.Text}' where id={clientId};");
                MessageBox.Show("Данные о сделке успешно изменены!", "Информация",MessageBoxButtons.OK, MessageBoxIcon.Information);
                var estateId = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
                if (type.Text == "Аренда")
                {
                    db.executeNonQuery($"UPDATE `db17`.`estate` SET `status` = '{"Арендован"}' WHERE (`id` = '{estateId}');");
                }
                else if (type.Text == "Покупка")
                {
                    db.executeNonQuery($"UPDATE `db17`.`estate` SET `status` = '{"Продан"}' WHERE (`id` = '{estateId}');");
                }

                if (status.Text == "Завершена")
                {
                    var result = MessageBox.Show("Создать документ по данной сделке?", "Создание докумета", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (type.Text == "Покупка")
                        {
                            DocumentHelper.createPurchaseDocument(clientId, ownerId, estateId);

                        }
                        else
                        {
                            DocumentHelper.createRentDocument(clientId, ownerId, estateId);

                        }
                    }
                }
            } catch (Exception xe)
            {
                MessageStore.somethingWentWrongMessage();
            }
        }
    }
}
