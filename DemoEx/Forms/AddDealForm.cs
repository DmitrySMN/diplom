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
            clientsDgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            objectDgv.RowTemplate.Height = 70;
            clientsDgv.RowTemplate.Height = 70;
            objectDgv.MultiSelect = false;
            objectDgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            clientsDgv.DefaultCellStyle.SelectionBackColor = Color.OldLace;
            objectDgv.DefaultCellStyle.SelectionBackColor = Color.OldLace;

            objectDgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            clientsDgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dealStatusCb.DropDownStyle = ComboBoxStyle.DropDownList;
            dealStatusCb.Items.Add("Новая");

            if (dealId == 0)
            {
                employeeFIO.Text = db.getValuesFromColumn($"SELECT concat(surname, ' ', name, ' ', patronymic) FROM db17.employees where login='{empLogin}';")[0].ToString();
                fillDgv();
                dealStatusCb.SelectedIndex = 0;
            } else
            {
                Text = "Редактирование сделки";
                
                addButton.Text = "Редактировать";

                dateTimePicker1.Enabled = false;
                dealStatusCb.Items.Add("Ожидание");
                dealStatusCb.Items.Add("Подтверждена");
                dealStatusCb.Items.Add("Завершена");
                dealStatusCb.Items.Add("Отменена");

                textBox1.Enabled = false;

                employeeFIO.Text = db.getValuesFromColumn($"SELECT concat(employees.surname, ' ', employees.name, ' ', employees.patronymic) FROM db17.deals \r\njoin employees on deals.employees=employees.id\r\nwhere dealId={dealId};")[0];

                db.FillDGV(clientsDgv, $"SELECT concat(clients.surname, ' ', clients.name, ' ', clients.patronymic) as 'ФИО', birth as 'Дата рождения' FROM db17.deals \r\njoin clients on deals.client=clients.id\r\nwhere dealId={dealId};");
                db.FillDGV(objectDgv, $"SELECT (select type from object_type where id=object.object_type) as 'Тип', object.cadastral as 'Кадастровый номер' FROM db17.deals \r\njoin object on deals.object=object.objectId\r\nwhere dealId={dealId};");
                dealStatusCb.SelectedItem = db.getValuesFromColumn($"SELECT status FROM db17.deals where dealId={dealId};")[0];
            }
        }

        private void fillDgv()
        {
            db.FillDGV(clientsDgv, $"SELECT id, concat(surname, ' ', name, ' ', patronymic) as 'ФИО', birth as 'Дата рождения' FROM db17.clients where type ='Покупатель' or type ='Арендатель';");
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
            try
            {
                if (dealId == 0)
                {
                    db.executeNonQuery($@"INSERT INTO `db17`.`deals` (`client`, `object`, `employees`, `type`, `transaction_date`, `status`) VALUES
                    ('{Convert.ToInt32(clientsDgv.SelectedCells[0].Value)}',
                    '{Convert.ToInt32(objectDgv.SelectedRows[0].Cells[0].Value)}',
                    '{db.getIntValuesFromColumn($"SELECT id FROM db17.employees where login='{empLogin}';")[0]}',
                    'Аренда',
                    '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}',
                    'Новая');");
                    MessageStore.addDealMessage();
                } else
                {
                    db.executeNonQuery($"UPDATE `db17`.`deals` SET `status` = '{dealStatusCb.Text}' WHERE (`dealId` = '{dealId}');");
                    MessageStore.editDealMessage();
                    Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
