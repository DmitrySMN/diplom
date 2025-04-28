using System;
using System.Windows.Forms;
using DB;
using DemoEx.Forms;
using DemoEx.utility;

namespace DemoEx
{
    public partial class MainForm : Form
    {
        private Db db = new Db(Connection.connectionString);
        private string login;
        private int post;
        private int currentInfo = 1;
        private int offset = 0;
        private int page = 1;
        private int clientsPageCount = 0;

        public MainForm(string login, int post)
        {
            this.login = login;
            this.post = post;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            int totalRecords = db.getIntValuesFromColumn("select count(*) from clients;")[0];
            clientsPageCount = (totalRecords % 10 == 0) ? totalRecords / 10 : totalRecords / 10 + 1;
            label1.Text = $"{page}/{clientsPageCount}";
            objectsDGV.RowTemplate.Height = 85;
            clientDGV.RowTemplate.Height = 82;
            dealsDGV.RowTemplate.Height = 85;
            employeeDGV.RowTemplate.Height = 85;

            fillAllDgv();

            clientDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            objectsDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            employeeDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dealsDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            clientSortField.DropDownStyle = ComboBoxStyle.DropDownList;
            clientSortField.Items.Add("По умолчанию");
            clientSortField.Items.Add("Покупатели");
            clientSortField.Items.Add("Продавцы");
            clientSortField.Items.Add("Арендодатели");
            clientSortField.Items.Add("Арендатели");

            clientSortField.SelectedIndex = 0;
        }

        private void fillAllDgv()
        {
            db.FillDGV(clientDGV, $"SELECT id, concat(Surname,' ', Name,' ', Patronymic) as 'ФИО', passport as 'Паспорт', address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' FROM db17.clients limit 10 offset {offset};");
            db.FillDGV(objectsDGV, $"SELECT objectid, object_type.type as 'Тип объекта', concat(clients.surname,' ', clients.name, ' ', clients.patronymic) as 'Владелец', address as 'Адрес', square as 'Площадь', cadastral as 'Кадастровый ном.', rooms as 'Кол-во комнат', price as 'Цена', photo as 'Фото', status as 'Статус'\r\nFROM db17.object\r\nJOIN object_type ON object_type.id=object.object_type\r\nJOIN clients ON clients.id=object.owner_id;");
            db.FillDGV(employeeDGV, $"SELECT id, login as 'Логин', password as 'Пароль', concat(Surname, ' ', Name, ' ', Patronymic) as 'ФИО', passport as 'Паспорт', birth as 'Дата рождения', phone_number as 'Номер телефона', address as 'Адрес', post as 'Должность' FROM db17.employees;");
            db.FillDGV(dealsDGV, $"SELECT * FROM db17.deals;");

            clientDGV.Columns[0].Visible = false;
            objectsDGV.Columns[0].Visible = false;
            employeeDGV.Columns[0].Visible = false;
            dealsDGV.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AddClientForm().ShowDialog();
            fillAllDgv();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new AddObjectForm().ShowDialog();
            fillAllDgv();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new AddEmployee().ShowDialog();
            fillAllDgv();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            fillAllDgv();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (post == 1)
            {
                if (e.TabPage == tabPage2 || e.TabPage == tabPage4 || e.TabPage == tabPage5)
                {
                    e.Cancel = true;
                }
            }
            else if (post == 2) {
                if (e.TabPage == tabPage4)
                {
                    e.Cancel = true;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                db.FillDGV(clientDGV, $"SELECT id, concat(Surname,' ', Name,' ', Patronymic) as 'ФИО', passport as 'Паспорт', address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' FROM db17.clients WHERE Surname LIKE '{textBox1.Text}%';");
            }
            else if( textBox1.Text.Length == 0)
            {
                fillAllDgv();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputFieldCorrection.ruLettersField(e);
        }

        private void clientSortField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clientSortField.SelectedIndex == 0)
            {
                fillAllDgv();
            } else if (clientSortField.SelectedIndex == 1)
            {
                db.FillDGV(clientDGV, $"SELECT id, concat(Surname,' ', Name,' ', Patronymic) as 'ФИО', passport as 'Паспорт', address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' FROM db17.clients WHERE type='Покупатель';");
            }
            else if (clientSortField.SelectedIndex == 2)
            {
                db.FillDGV(clientDGV, $"SELECT id, concat(Surname,' ', Name,' ', Patronymic) as 'ФИО', passport as 'Паспорт', address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' FROM db17.clients WHERE type='Продавец';");
            }
            else if (clientSortField.SelectedIndex == 3)
            {
                db.FillDGV(clientDGV, $"SELECT id, concat(Surname,' ', Name,' ', Patronymic) as 'ФИО', passport as 'Паспорт', address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' FROM db17.clients WHERE type='Арендодатель';");
            }
            else
            {
                db.FillDGV(clientDGV, $"SELECT id, concat(Surname,' ', Name,' ', Patronymic) as 'ФИО', passport as 'Паспорт', address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' FROM db17.clients WHERE type='Арендатель';");
            }
        }

        private void создатьЗапросToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void изменитьДанныеКлиентаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AddClientForm(Convert.ToInt32(clientDGV.SelectedRows[0].Cells[0].Value)).ShowDialog();
            fillAllDgv();
        }

        private void удалитьКлиентаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageStore.deleteClientConfirmationMessage();

            if (result == DialogResult.Yes)
            {
                db.executeNonQuery($"DELETE FROM `db17`.`clients` WHERE (`id` = '{Convert.ToInt32(clientDGV.SelectedRows[0].Cells[0].Value)}');");
            }

            fillAllDgv();
        }

        private void удалитьСотрудникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageStore.deleteClientConfirmationMessage();

            if (result == DialogResult.Yes)
            {
                db.executeNonQuery($"DELETE FROM `db17`.`employees` WHERE (`id` = '{Convert.ToInt32(clientDGV.SelectedRows[0].Cells[0].Value)}');");
            }

            fillAllDgv();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            nextClientPage();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            prevClientPage();
        }

        private void nextClientPage() 
        {
            if (page < clientsPageCount)
            {
                offset += 10;
                clearParameters();
                fillAllDgv();
                page += 1;
                label1.Text = $"{page}/{clientsPageCount}";
            }
        }

        private void prevClientPage()
        {
            if (page > 1)
            {
                offset -= 10;
                clearParameters();
                fillAllDgv();
                page -= 1;
                label1.Text = $"{page}/{clientsPageCount}";
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void clearParameters()
        {
            clientSortField.SelectedIndex = 0;
            textBox1.Clear();
        }

        private void clientDGV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (clientDGV.Columns[e.ColumnIndex].Name == "ФИО" && e.Value != null)
            {
                string[] spitedFIO = e.Value.ToString().Split(' ');
                int surnamelen = spitedFIO[0].Length / 2; 
                int namelen = spitedFIO[1].Length / 2; 
                int patlen = spitedFIO[2].Length / 2; 
                string maskedsur = spitedFIO[0].Substring(0, surnamelen) + new string('*', spitedFIO[0].Length - surnamelen);
                string maskedname = spitedFIO[1].Substring(0, namelen) + new string('*', spitedFIO[1].Length - namelen);
                string maskedpat = spitedFIO[2].Substring(0, patlen) + new string('*', spitedFIO[2].Length - patlen);

                e.Value = maskedsur + maskedname + maskedpat;
            }
        }

        private void clientDGV_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                clientDGV.ClearSelection();
                clientDGV[e.ColumnIndex, e.RowIndex].Selected = true;
            }
        }

        private void удалитьОбъектToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageStore.deleteObjectConfirmationMessage();
            if (result  == DialogResult.Yes)
            {
                db.executeNonQuery($"DELETE FROM `db17`.`object` WHERE (`objectid` = '{Convert.ToInt32(objectsDGV.SelectedRows[0].Cells[0].Value)}');");
            }
            fillAllDgv();
        }
    }
}
