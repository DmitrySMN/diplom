using System;
using System.Windows.Forms;
using DB;
using DemoEx.utility;

namespace DemoEx
{
    public partial class MainForm : Form
    {
        private Db db = new Db(Connection.connectionString);
        private string login;
        private int post;
        private int currentInfo = 1;
        public MainForm(string login, int post)
        {
            this.login = login;
            this.post = post;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            clientDGV.RowTemplate.Height = 85;
            objectsDGV.RowTemplate.Height = 85;
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
            clientSortField.Items.Add("Арендатели");

            clientSortField.SelectedIndex = 0;

            clientDGV.Columns[0].Visible = false;
            objectsDGV.Columns[0].Visible = false;
            employeeDGV.Columns[0].Visible = false;
            dealsDGV.Columns[0].Visible = false;
        }

        private void fillAllDgv()
        {
            db.FillDGV(clientDGV, $"SELECT id, concat(Surname,' ', Name,' ', Patronymic) as 'ФИО', passport as 'Паспорт', address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' FROM db17.clients");
            db.FillDGV(objectsDGV, $"SELECT * FROM db17.object;");
            db.FillDGV(employeeDGV, $"SELECT id, login as 'Логин', password as 'Пароль', concat(Surname, ' ', Name, ' ', Patronymic) as 'ФИО', passport as 'Паспорт', birth as 'Дата рождения', phone_number as 'Номер телефона', address as 'Адрес', post as 'Должность', photo FROM db17.employees;");
            db.FillDGV(dealsDGV, $"SELECT * FROM db17.deals;");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AddClientForm().ShowDialog();
            fillAllDgv();
        }

        private void button2_Click(object sender, EventArgs e)
        {
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
            else
            {
                employeeDGV.Columns.Remove("Фото");
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

        //        foreach (DataGridViewRow row in dataGridView1.Rows)
        //        {
        //            if (row.Cells[6].Value.ToString() == "Новая")
        //            {
        //                row.Cells[6].Style.BackColor = Color.LightYellow;
        //            }
        //            else if (row.Cells[6].Value.ToString() == "Завершена")
        //            {
        //                row.Cells[6].Style.BackColor = Color.LightGreen;

        //            }
        //            else
        //            {
        //                row.Cells[6].Style.BackColor = Color.LightPink;
        //            }
        //        }

        //        pathString.Text = "/ Сделки / Список";
        //        var count = db.getIntValuesFromColumn("select count(*) from deals;")[0];
        //        label7.Text = count.ToString();
        //        currentInfo = 3;
        //        searchTextBox.Visible = false;
        //        sort.Visible = false;
        //        button1.Text = "Редактировать";

        //        filter.Items.Clear();

        //        filter.Items.Add("По умолчанию   ");
        //        filter.Items.Add("Новые");
        //        filter.Items.Add("Завершенные");
        //        filter.Items.Add("Не состоявшиеся");

        //        sort.Items.Clear();

        //        sort.Items.Add("По умолчанию    ");

        //        filter.SelectedIndex = 0;
        //        sort.SelectedIndex = 0;
        //    }

        //    private void label3_Click_1(object sender, EventArgs e)
        //    {
        //        button1.Text = "Добавить";
        //        sort.Visible = true;
        //        contextMenuStrip1.Items["toolStripMenuItem1"].Visible = true;
        //        contextMenuStrip1.Items["toolStripMenuItem2"].Visible = true;
        //        contextMenuStrip1.Items["toolStripMenuItem3"].Visible = false;
        //        contextMenuStrip1.Items["toolStripMenuItem4"].Visible = false;
        //        contextMenuStrip1.Items["удалитьСотрудникаToolStripMenuItem"].Visible = false;

        //        if (dataGridView1.Columns["Фото объекта"] == null)
        //        {

        //        } else
        //        {
        //            dataGridView1.Columns.Remove("Фото объекта");
        //        }

        //        db.FillDGV(dataGridView1, "select id as 'ID', Surname as 'Фамилия', Name as 'Имя', Patronymic as 'Отчество', passport as 'Паспортные данные', address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' from clients;");
        //        pathString.Text = "/ Клиенты / Список";
        //        var count = db.getIntValuesFromColumn("select count(*) from clients;")[0];
        //        label7.Text = count.ToString();
        //        currentInfo = 1;
        //        searchTextBox.Visible = true;

        //        filter.Items.Clear();

        //        filter.Items.Add("По умолчанию");
        //        filter.Items.Add("Покупатели");
        //        filter.Items.Add("Продавцы");
        //        filter.Items.Add("Арендатели");
        //        filter.Items.Add("Арендодатели");

        //        sort.Items.Clear();

        //        sort.Items.Add("По умолчанию");
        //        sort.Items.Add("По фамилии А-Я");
        //        sort.Items.Add("По фамилии Я-А");

        //        filter.SelectedIndex = 0;
        //        sort.SelectedIndex = 0;
        //    }

        //    private void label4_Click(object sender, EventArgs e)
        //    {
        //        button1.Text = "Добавить";
        //        sort.Visible = true;
        //        contextMenuStrip1.Items["toolStripMenuItem1"].Visible = false;
        //        contextMenuStrip1.Items["toolStripMenuItem2"].Visible = false;
        //        contextMenuStrip1.Items["toolStripMenuItem3"].Visible = true;
        //        contextMenuStrip1.Items["удалитьСотрудникаToolStripMenuItem"].Visible = false;

        //        if (dataGridView1.Columns["Фото объекта"] == null)
        //        {

        //        }
        //        else
        //        {
        //            dataGridView1.Columns.Remove("Фото объекта");
        //        }
        //        db.FillDGV(dataGridView1, $"select id, (select type from estate_type where id=estate_type) as 'Тип объекта', (select concat(Surname, Name, Patronymic) from clients where id=owner_id) as 'Владелец', address as 'Адрес', square as 'Площадь', cadastral as 'Кадастровый номер', rooms as 'Кол-во комнат', price as 'Цена', photo, status as 'Статус' from estate;");
        //        dataGridView1.Columns["id"].Visible = false;
        //        dataGridView1.Columns["photo"].Visible = false;
        //        db.setUpDgvImages(dataGridView1, "Фото объекта");
        //        dataGridView1.Columns[1].Width = 150;
        //        dataGridView1.Columns[4].Width = 120;
        //        dataGridView1.Columns[5].Width = 150;
        //        dataGridView1.Columns[6].Width = 170;
        //        dataGridView1.Columns["Фото объекта"].Width = 120;
        //        pathString.Text = "/ Объекты / Список";
        //        var count = db.getIntValuesFromColumn("select count(*) from estate;")[0];
        //        label7.Text = count.ToString();
        //        currentInfo = 2;
        //        searchTextBox.Visible = false;

        //        filter.Items.Clear();

        //        filter.Items.Add("По умолчанию ");
        //        filter.Items.Add("Квартиры");
        //        filter.Items.Add("Дома");
        //        filter.Items.Add("Котеджи");
        //        filter.Items.Add("Склады");
        //        filter.Items.Add("Офисы");

        //        sort.Items.Clear();

        //        sort.Items.Add("По умолчанию  ");
        //        sort.Items.Add("По убыванию цены");
        //        sort.Items.Add("По возрастанию цены");

        //        filter.SelectedIndex = 0;
        //        sort.SelectedIndex = 0;

        //    }

        //    private void panel3_Paint(object sender, PaintEventArgs e)
        //    {

        //    }

        //    private void button1_Click_2(object sender, EventArgs e)
        //    {
        //        switch (currentInfo)
        //        {
        //            case 1:
        //                new AddClientForm(0).ShowDialog();
        //                db.FillDGV(dataGridView1, "select id as 'ID', Surname as 'Фамилия', Name as 'Имя', Patronymic as 'Отчество', passport as 'Паспортные данные',address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' from clients;");
        //                break;
        //            case 2:
        //                new AddEstateForm(0).ShowDialog();
        //                if (dataGridView1.Columns["Фото объекта"] == null)
        //                {

        //                }
        //                else
        //                {
        //                    dataGridView1.Columns.Remove("Фото объекта");
        //                }
        //                db.FillDGV(dataGridView1, $"select id, (select type from estate_type where id=estate_type) as 'Тип объекта', (select concat(Surname, Name, Patronymic) from clients where id=owner_id) as 'Владелец', address as 'Адрес', square as 'Площадь',cadastral as 'Кадастровый номер', rooms as 'Кол-во комнат', price as 'Цена', photo, status as 'Статус' from estate;");
        //                db.setUpDgvImages(dataGridView1, "Фото объекта");
        //                break;
        //            case 3:
        //                if (dataGridView1.SelectedRows[0].Cells[6].Value.ToString() != "Новая")
        //                {
        //                    MessageBox.Show("Данная сделка завершена, её нельзя изменить!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                } else
        //                {
        //                    new AddDealForm(login, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value), 1).ShowDialog();
        //                    db.FillDGV(dataGridView1, "select id, (select concat(Surname, Name, Patronymic) from clients where id=client) as 'Клиент', (select address from estate where id=estate) as 'Объект', (select concat(Surname,' ', Name, ' ', Patronymic) from employees where id=employees) as 'Риелтор', type as 'Тип', transaction_date as 'Дата заключения', status as 'Статус' from deals;");
        //                    foreach (DataGridViewRow row in dataGridView1.Rows)
        //                    {
        //                        if (row.Cells[6].Value.ToString() == "Новая")
        //                        {
        //                            row.Cells[6].Style.BackColor = Color.LightYellow;
        //                        }
        //                        else if (row.Cells[6].Value.ToString() == "Завершена")
        //                        {
        //                            row.Cells[6].Style.BackColor = Color.LightGreen;

        //                        }
        //                        else
        //                        {
        //                            row.Cells[6].Style.BackColor = Color.LightPink;
        //                        }
        //                    }
        //                }
        //                break;
        //            case 4:
        //                new AddEmployee(0).ShowDialog();
        //                db.FillDGV(dataGridView1, "select id, login as 'Логин', password as 'Пароль', surname as 'Фамилия', name as 'Имя', patronymic as 'Отчество', passport as 'Паспортные данные', birth as 'Дата рождения', phone_number as 'Номер телефона', address as 'Адрес', (select post from posts where posts.id=db17.employees.post) as 'Должность' from employees;");
        //                break;
        //        }
        //    }


        //    private void label8_Click(object sender, EventArgs e)
        //    {
        //        Close();
        //    }

        //    private void pictureBox6_Click_1(object sender, EventArgs e)
        //    {
        //        Close();
        //    }

        //    private void textBox1_TextChanged(object sender, EventArgs e)
        //    {
        //        filter.SelectedIndex = 0;
        //        sort.SelectedIndex = 0;
        //        switch (currentInfo)
        //        {
        //            case 1:
        //                db.FillDGV(dataGridView1, $"select id as 'ID', Surname as 'Фамилия', Name as 'Имя', Patronymic as 'Отчество', passport as 'Паспортные данные',address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' from clients where surname like '{searchTextBox.Text}%';");
        //                break;
        //            case 2:
        //                break;
        //            case 4:
        //                db.FillDGV(dataGridView1, $"select id, login as 'Логин', password as 'Пароль', surname as 'Фамилия', name as 'Имя', patronymic as 'Отчество', passport as 'Паспортные данные', birth as 'Дата рождения', phone_number as 'Номер телефона', address as 'Адрес', (select post from posts where posts.id=db17.employees.post) as 'Должность' from employees where surname like '{searchTextBox.Text}%';");
        //                break;
        //        }
        //    }

        //    private void filter_SelectedIndexChanged(object sender, EventArgs e)
        //    {

        //        sort.SelectedIndex = 0;
        //        searchTextBox.Clear();
        //        switch (filter.Text)
        //        {
        //            case "По умолчанию":
        //                db.FillDGV(dataGridView1, "select id as 'ID', Surname as 'Фамилия', Name as 'Имя', Patronymic as 'Отчество', passport as 'Паспортные данные',address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' from clients;");
        //                break;
        //            case "Покупатели":
        //                db.FillDGV(dataGridView1, "select id as 'ID', Surname as 'Фамилия', Name as 'Имя', Patronymic as 'Отчество', passport as 'Паспортные данные',address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' from clients where type='Покупатель';");
        //                break;
        //            case "Продавцы":
        //                db.FillDGV(dataGridView1, "select id as 'ID', Surname as 'Фамилия', Name as 'Имя', Patronymic as 'Отчество', passport as 'Паспортные данные',address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' from clients where type='Продавец';");
        //                break;
        //            case "Арендатели":
        //                db.FillDGV(dataGridView1, "select id as 'ID', Surname as 'Фамилия', Name as 'Имя', Patronymic as 'Отчество', passport as 'Паспортные данные',address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' from clients where type='Арендатель';");
        //                break;
        //            case "Арендодатели":
        //                db.FillDGV(dataGridView1, "select id as 'ID', Surname as 'Фамилия', Name as 'Имя', Patronymic as 'Отчество', passport as 'Паспортные данные',address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' from clients where type='Арендодатель';");
        //                break;
        //            case "По умолчанию ":
        //                db.FillDGV(dataGridView1, $"select id, (select type from estate_type where id=estate_type) as 'Тип объекта', (select concat(Surname, Name, Patronymic) from clients where id=owner_id) as 'Владелец', address as 'Адрес', square as 'Площадь',cadastral as 'Кадастровый номер', rooms as 'Кол-во комнат', price as 'Цена', photo, status as 'Статус' from estate;");
        //                dataGridView1.Columns.Remove("Фото объекта");
        //                dataGridView1.Columns["photo"].Visible = false;
        //                db.setUpDgvImages(dataGridView1, "Фото объекта");
        //                break;
        //            case "Квартиры":
        //                db.FillDGV(dataGridView1, "select id, (select type from estate_type where id=estate_type) as 'Тип объекта', (select concat(Surname, Name, Patronymic) from clients where id=owner_id) as 'Владелец', address as 'Адрес', square as 'Площадь',cadastral as 'Кадастровый номер', rooms as 'Кол-во комнат', price as 'Цена', photo, status as 'Статус' from estate where estate_type=1;");
        //                dataGridView1.Columns.Remove("Фото объекта");
        //                dataGridView1.Columns["photo"].Visible = false;
        //                db.setUpDgvImages(dataGridView1, "Фото объекта");
        //                break;
        //            case "Дома":
        //                db.FillDGV(dataGridView1, "select id, (select type from estate_type where id=estate_type) as 'Тип объекта', (select concat(Surname, Name, Patronymic) from clients where id=owner_id) as 'Владелец', address as 'Адрес', square as 'Площадь',cadastral as 'Кадастровый номер', rooms as 'Кол-во комнат', price as 'Цена', photo, status as 'Статус' from estate where estate_type=2;");
        //                dataGridView1.Columns.Remove("Фото объекта");
        //                dataGridView1.Columns["photo"].Visible = false;
        //                db.setUpDgvImages(dataGridView1, "Фото объекта");
        //                break;
        //            case "Котеджи":
        //                db.FillDGV(dataGridView1, "select id, (select type from estate_type where id=estate_type) as 'Тип объекта', (select concat(Surname, Name, Patronymic) from clients where id=owner_id) as 'Владелец', address as 'Адрес', square as 'Площадь',cadastral as 'Кадастровый номер', rooms as 'Кол-во комнат', price as 'Цена', photo, status as 'Статус' from estate where estate_type=3;");
        //                dataGridView1.Columns.Remove("Фото объекта");
        //                dataGridView1.Columns["photo"].Visible = false;
        //                db.setUpDgvImages(dataGridView1, "Фото объекта");

        //                break;
        //            case "Склады":
        //                db.FillDGV(dataGridView1, "select (select type from estate_type where id=estate_type) as 'Тип объекта', (select concat(Surname, Name, Patronymic) from clients where id=owner_id) as 'Владелец', address as 'Адрес', square as 'Площадь',cadastral as 'Кадастровый номер', rooms as 'Кол-во комнат', price as 'Цена', photo, status as 'Статус' from estate where estate_type=4;");
        //                dataGridView1.Columns.Remove("Фото объекта");
        //                dataGridView1.Columns["photo"].Visible = false;
        //                db.setUpDgvImages(dataGridView1, "Фото объекта");

        //                break;
        //            case "Офисы":
        //                db.FillDGV(dataGridView1, "select (select type from estate_type where id=estate_type) as 'Тип объекта', (select concat(Surname, Name, Patronymic) from clients where id=owner_id) as 'Владелец', address as 'Адрес', square as 'Площадь',cadastral as 'Кадастровый номер', rooms as 'Кол-во комнат', price as 'Цена', photo, status as 'Статус' from estate where estate_type=5;");
        //                dataGridView1.Columns.Remove("Фото объекта");
        //                dataGridView1.Columns["photo"].Visible = false;
        //                db.setUpDgvImages(dataGridView1, "Фото объекта");
        //                break;
        //            case ("По умолчанию   "):
        //                db.FillDGV(dataGridView1, "select id, (select concat(Surname, Name, Patronymic) from clients where id=client) as 'Клиент', (select address from estate where id=estate) as 'Объект', (select concat(Surname,' ', Name, ' ', Patronymic) from employees where id=employees) as 'Риелтор', type as 'Тип', transaction_date as 'Дата заключения', status as 'Статус' from deals;");
        //                foreach (DataGridViewRow row in dataGridView1.Rows)
        //                {
        //                    if (row.Cells[6].Value.ToString() == "Новая")
        //                    {
        //                        row.Cells[6].Style.BackColor = Color.LightYellow;
        //                    }
        //                    else if (row.Cells[6].Value.ToString() == "Завершена")
        //                    {
        //                        row.Cells[6].Style.BackColor = Color.LightGreen;

        //                    }
        //                    else
        //                    {
        //                        row.Cells[6].Style.BackColor = Color.LightPink;
        //                    }
        //                }
        //                break;
        //            case ("Новые"):
        //                db.FillDGV(dataGridView1, "select id, (select concat(Surname, Name, Patronymic) from clients where id=client) as 'Клиент', (select address from estate where id=estate) as 'Объект', (select concat(Surname,' ', Name, ' ', Patronymic) from employees where id=employees) as 'Риелтор', type as 'Тип', transaction_date as 'Дата заключения', status as 'Статус' from deals where status='Новая';");
        //                foreach (DataGridViewRow row in dataGridView1.Rows)
        //                {
        //                    if (row.Cells[6].Value.ToString() == "Новая")
        //                    {
        //                        row.Cells[6].Style.BackColor = Color.LightYellow;
        //                    }
        //                    else if (row.Cells[6].Value.ToString() == "Завершена")
        //                    {
        //                        row.Cells[6].Style.BackColor = Color.LightGreen;

        //                    }
        //                    else
        //                    {
        //                        row.Cells[6].Style.BackColor = Color.LightPink;
        //                    }
        //                }
        //                break;
        //            case ("Завершенные"):
        //                db.FillDGV(dataGridView1, "select id, (select concat(Surname, Name, Patronymic) from clients where id=client) as 'Клиент', (select address from estate where id=estate) as 'Объект', (select concat(Surname,' ', Name, ' ', Patronymic) from employees where id=employees) as 'Риелтор', type as 'Тип', transaction_date as 'Дата заключения', status as 'Статус' from deals where status='Завершена';");
        //                foreach (DataGridViewRow row in dataGridView1.Rows)
        //                {
        //                    if (row.Cells[6].Value.ToString() == "Новая")
        //                    {
        //                        row.Cells[6].Style.BackColor = Color.LightYellow;
        //                    }
        //                    else if (row.Cells[6].Value.ToString() == "Завершена")
        //                    {
        //                        row.Cells[6].Style.BackColor = Color.LightGreen;

        //                    }
        //                    else
        //                    {
        //                        row.Cells[6].Style.BackColor = Color.LightPink;
        //                    }
        //                }
        //                break;
        //            case ("Не состоявшиеся"):
        //                db.FillDGV(dataGridView1, "select id, (select concat(Surname, Name, Patronymic) from clients where id=client) as 'Клиент', (select address from estate where id=estate) as 'Объект', (select concat(Surname,' ', Name, ' ', Patronymic) from employees where id=employees) as 'Риелтор', type as 'Тип', transaction_date as 'Дата заключения', status as 'Статус' from deals where status='Не состоялась';");
        //                foreach (DataGridViewRow row in dataGridView1.Rows)
        //                {
        //                    if (row.Cells[6].Value.ToString() == "Новая")
        //                    {
        //                        row.Cells[6].Style.BackColor = Color.LightYellow;
        //                    }
        //                    else if (row.Cells[6].Value.ToString() == "Завершена")
        //                    {
        //                        row.Cells[6].Style.BackColor = Color.LightGreen;

        //                    }
        //                    else
        //                    {
        //                        row.Cells[6].Style.BackColor = Color.LightPink;
        //                    }
        //                }
        //                break;
        //            case "Администраторы":
        //                db.FillDGV(dataGridView1, "select id, login as 'Логин', password as 'Пароль', surname as 'Фамилия', name as 'Имя', patronymic as 'Отчество', passport as 'Паспортные данные', birth as 'Дата рождения', phone_number as 'Номер телефона', address as 'Адрес', (select post from posts where posts.id=db17.employees.post) as 'Должность' from employees where post=1;");
        //                break;
        //            case "Риелторы":
        //                db.FillDGV(dataGridView1, "select id, login as 'Логин', password as 'Пароль', surname as 'Фамилия', name as 'Имя', patronymic as 'Отчество', passport as 'Паспортные данные', birth as 'Дата рождения', phone_number as 'Номер телефона', address as 'Адрес', (select post from posts where posts.id=db17.employees.post) as 'Должность' from employees where post=2;");
        //                break;
        //            case "По умолчанию     ":
        //                db.FillDGV(dataGridView1, "select id, login as 'Логин', password as 'Пароль', surname as 'Фамилия', name as 'Имя', patronymic as 'Отчество', passport as 'Паспортные данные', birth as 'Дата рождения', phone_number as 'Номер телефона', address as 'Адрес', (select post from posts where posts.id=db17.employees.post) as 'Должность' from employees;");
        //                break;
        //        }
        //    }

        //    private void sort_SelectedIndexChanged(object sender, EventArgs e)
        //    {
        //        filter.SelectedIndex = 0;
        //        searchTextBox.Clear();
        //        switch (sort.Text)
        //        {
        //            case "По умолчанию":
        //                db.FillDGV(dataGridView1, "select id as 'ID', Surname as 'Фамилия', Name as 'Имя', Patronymic as 'Отчество', passport as 'Паспортные данные',address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' from clients;");
        //                break;
        //            case "По фамилии А-Я":
        //                db.FillDGV(dataGridView1, "select id as 'ID', Surname as 'Фамилия', Name as 'Имя', Patronymic as 'Отчество', passport as 'Паспортные данные',address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' from clients order by surname;");
        //                break;
        //            case "По фамилии Я-А":
        //                db.FillDGV(dataGridView1, "select id as 'ID', Surname as 'Фамилия', Name as 'Имя', Patronymic as 'Отчество', passport as 'Паспортные данные',address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' from clients order by surname desc;");
        //                break;
        //            case "По умолчанию  ":
        //                db.FillDGV(dataGridView1, "select id, (select type from estate_type where id=estate_type) as 'Тип объекта', (select concat(Surname, Name, Patronymic) from clients where id=owner_id) as 'Владелец', address as 'Адрес', square as 'Площадь',cadastral as 'Кадастровый номер', rooms as 'Кол-во комнат', price as 'Цена', photo, status as 'Статус' from estate;");
        //                dataGridView1.Columns.Remove("Фото объекта");
        //                dataGridView1.Columns["photo"].Visible = false;
        //                db.setUpDgvImages(dataGridView1, "Фото объекта");
        //                break;
        //            case "По возрастанию цены":
        //                db.FillDGV(dataGridView1, "select id, (select type from estate_type where id=estate_type) as 'Тип объекта', (select concat(Surname, Name, Patronymic) from clients where id=owner_id) as 'Владелец', address as 'Адрес', square as 'Площадь',cadastral as 'Кадастровый номер', rooms as 'Кол-во комнат', price as 'Цена', photo, status as 'Статус' from estate order by price;");
        //                dataGridView1.Columns.Remove("Фото объекта");
        //                dataGridView1.Columns["photo"].Visible = false;
        //                db.setUpDgvImages(dataGridView1, "Фото объекта");
        //                break;
        //            case "По убыванию цены":
        //                db.FillDGV(dataGridView1, "select id, (select type from estate_type where id=estate_type) as 'Тип объекта', (select concat(Surname, Name, Patronymic) from clients where id=owner_id) as 'Владелец', address as 'Адрес', square as 'Площадь',cadastral as 'Кадастровый номер', rooms as 'Кол-во комнат', price as 'Цена', photo, status as 'Статус' from estate order by price desc;");
        //                dataGridView1.Columns.Remove("Фото объекта");
        //                dataGridView1.Columns["photo"].Visible = false;
        //                db.setUpDgvImages(dataGridView1, "Фото объекта");
        //                break;
        //        }
        //    }

        //    private void dataGridView1_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        //    {
        //        if (e.ColumnIndex != -1 && e.RowIndex != -1)
        //        {
        //            contextMenuStrip1.Show(Cursor.Position);
        //        }
        //    }

        //    private void toolStripMenuItem1_Click(object sender, EventArgs e)
        //    {
        //        new AddDealForm(login, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())).ShowDialog();
        //    }

        //    private void employeeLabel_Click(object sender, EventArgs e)
        //    {
        //        button1.Text = "Добавить";
        //        currentInfo = 4;
        //        sort.Visible = true;
        //        filter.Visible = true;
        //        searchTextBox.Visible = true;

        //        filter.Items.Clear();
        //        searchTextBox.Clear();

        //        filter.Items.Add("По умолчанию     ");
        //        filter.Items.Add("Администраторы");
        //        filter.Items.Add("Риелторы");


        //        filter.SelectedIndex = 0;

        //        if (dataGridView1.Columns["Фото объекта"] == null)
        //        {

        //        }
        //        else
        //        {
        //            dataGridView1.Columns.Remove("Фото объекта");
        //        }

        //        pathString.Text = "/ Сделки / Сотрудники";

        //        var count = db.getIntValuesFromColumn("select count(*) from employees;")[0];
        //        label7.Text = count.ToString();

        //        contextMenuStrip1.Items["toolStripMenuItem1"].Visible = false;
        //        contextMenuStrip1.Items["toolStripMenuItem2"].Visible = false;
        //        contextMenuStrip1.Items["toolStripMenuItem3"].Visible = false;
        //        contextMenuStrip1.Items["toolStripMenuItem4"].Visible = true;
        //        contextMenuStrip1.Items["удалитьСотрудникаToolStripMenuItem"].Visible = true;
        //        db.FillDGV(dataGridView1, "select id, login as 'Логин', password as 'Пароль', surname as 'Фамилия', name as 'Имя', patronymic as 'Отчество', passport as 'Паспортные данные', birth as 'Дата рождения', phone_number as 'Номер телефона', address as 'Адрес', (select post from posts where posts.id=db17.employees.post) as 'Должность' from employees;");
        //        dataGridView1.Columns["id"].Visible = false;
        //    }

        //    private void toolStripMenuItem2_Click(object sender, EventArgs e)
        //    {
        //        new AddClientForm(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())).ShowDialog();
        //        filter.SelectedIndex = 0;
        //        sort.SelectedIndex = 0;
        //        searchTextBox.Clear();
        //        db.FillDGV(dataGridView1, $"select id as 'ID', Surname as 'Фамилия', Name as 'Имя', Patronymic as 'Отчество', passport as 'Паспортные данные', birth as 'Дата рождения',address as 'Адрес', phone_number as 'Номер телефона', type as 'Тип' from clients where surname like '{searchTextBox.Text}%';");

        //    }

        //    private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        //    {
        //        if (e.Button == MouseButtons.Right)
        //        {
        //            dataGridView1.ClearSelection();
        //            dataGridView1[e.ColumnIndex, e.RowIndex].Selected = true;
        //            if (currentInfo == 1)
        //            {
        //                if (dataGridView1.SelectedRows[0].Cells[8].Value.ToString() == "Продавец" || dataGridView1.SelectedRows[0].Cells[8].Value.ToString() == "Арендодатель")
        //                {
        //                    contextMenuStrip1.Items["toolStripMenuItem1"].Visible = false;
        //                }
        //                else
        //                {
        //                    contextMenuStrip1.Items["toolStripMenuItem1"].Visible = true;
        //                }
        //            }
        //        }
        //    }

        //    private void toolStripMenuItem3_Click(object sender, EventArgs e)
        //    {
        //        new AddEstateForm(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())).ShowDialog();
        //        filter.SelectedIndex = 0;
        //        sort.SelectedIndex = 0;
        //        searchTextBox.Clear();
        //        db.FillDGV(dataGridView1, $"select id, (select type from estate_type where id=estate_type) as 'Тип объекта', (select concat(Surname, Name, Patronymic) from clients where id=owner_id) as 'Владелец', address as 'Адрес', square as 'Площадь',cadastral as 'Кадастровый номер', rooms as 'Кол-во комнат', price as 'Цена', photo, status as 'Статус' from estate;");
        //        dataGridView1.Columns.Remove("Фото объекта");
        //        db.setUpDgvImages(dataGridView1, "Фото объекта");
        //    }

        //    private void toolStripMenuItem4_Click(object sender, EventArgs e)
        //    {
        //        try
        //        {
        //            new AddEmployee(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())).ShowDialog();
        //            filter.SelectedIndex = 0;
        //            sort.SelectedIndex = 0;
        //            searchTextBox.Clear();
        //            db.FillDGV(dataGridView1, "select id, login as 'Логин', password as 'Пароль', surname as 'Фамилия', name as 'Имя', patronymic as 'Отчество', passport as 'Паспортные данные', birth as 'Дата рождения', phone_number as 'Номер телефона', address as 'Адрес', (select post from posts where posts.id=db17.employees.post) as 'Должность' from employees;");

        //            var count = db.getIntValuesFromColumn("select count(*) from employees;")[0];
        //            label7.Text = count.ToString();
        //        } catch (Exception exx)
        //        {
        //            MessageBox.Show("Что-то пошло не так!");
        //        }
        //    }

    }
}
