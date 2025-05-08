using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DB;
using DemoEx.Forms;
using DemoEx.utility;
using MySqlX.XDevAPI;

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
        private int objectPage = 1;
        private int dealsPage = 1;
        private int employeesPage = 1;

        private int clientsPageCount = 0;
        private int objectPageCount = 0;
        private int dealsPageCount = 0;
        private int employeesPageCount = 0;

        public MainForm(string login, int post)
        {
            this.login = login;
            this.post = post;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                fillAllDgv();

                string userFullName = db.getValuesFromColumn($"select concat(surname, ' ', name) from employees where login='{login}';")[0];
                this.Text = $"Главное меню - {userFullName}";

                objectsDGV.RowTemplate.Height = 85;
                clientDGV.RowTemplate.Height = 82;
                dealsDGV.RowTemplate.Height = 85;
                employeeDGV.RowTemplate.Height = 85;


                clientDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                objectsDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                employeeDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dealsDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                clientsFilter.DropDownStyle = ComboBoxStyle.DropDownList;
                objectsFilter.DropDownStyle = ComboBoxStyle.DropDownList;
                dealsFilter.DropDownStyle = ComboBoxStyle.DropDownList;
                employeeFilter.DropDownStyle = ComboBoxStyle.DropDownList;

                clientSort.DropDownStyle = ComboBoxStyle.DropDownList;
                objectsSort.DropDownStyle = ComboBoxStyle.DropDownList;
                dealsSort.DropDownStyle = ComboBoxStyle.DropDownList;
                employeeSort.DropDownStyle = ComboBoxStyle.DropDownList;

                clientsFilter.Items.Add("По умолчанию");
                clientsFilter.Items.Add("Покупатели");
                clientsFilter.Items.Add("Продавцы");
                clientsFilter.Items.Add("Арендодатели");
                clientsFilter.Items.Add("Арендатели");

                objectsFilter.Items.Add("По умолчанию");
                objectsFilter.Items.Add("Квартира");
                objectsFilter.Items.Add("Дом");
                objectsFilter.Items.Add("Складское помещ.");
                objectsFilter.Items.Add("Офис");

                dealsFilter.Items.Add("По умолчанию");
                dealsFilter.Items.Add("Новые");
                dealsFilter.Items.Add("Подтвержденные");
                dealsFilter.Items.Add("Отмененные");
                dealsFilter.Items.Add("Завершенные");

                employeeFilter.Items.Add("По умолчанию");
                employeeFilter.Items.Add("Риелторы");
                employeeFilter.Items.Add("Менеджеры");
                employeeFilter.Items.Add("Администраторы");

                objectsSort.Items.Add("По умолчанию");
                objectsSort.Items.Add("Цена (возр.)");
                objectsSort.Items.Add("Цена (убыв.)");

                clientSort.Items.Add("По умолчанию");
                clientSort.Items.Add("Дата рождения (возр.)");
                clientSort.Items.Add("Дата рождения (убыв.)");

                dealsSort.Items.Add("По умолчанию");
                dealsSort.Items.Add("Дата заключения (возр.)");
                dealsSort.Items.Add("Дата заключения (убыв.)");

                employeeSort.Items.Add("По умолчанию");
                employeeSort.Items.Add("Дата рождения (возр.)");
                employeeSort.Items.Add("Дата рождения (убыв.)");

                clientsFilter.SelectedIndex = 0;
                objectsFilter.SelectedIndex = 0;
                dealsFilter.SelectedIndex = 0;
                employeeFilter.SelectedIndex = 0;
                objectsSort.SelectedIndex = 0;
                clientSort.SelectedIndex = 0;
                dealsSort.SelectedIndex = 0;
                employeeSort.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageStore.somethingWentWrongMessage();
            }
            
        }

        private void loadPagination()
        {
            int totalRecords = db.getIntValuesFromColumn("select count(*) from clients;")[0];
            clientsPageCount = (totalRecords % 10 == 0) ? totalRecords / 10 : totalRecords / 10 + 1;
            label1.Text = $"{page}/{clientsPageCount}";

            int totalObjectRecords = db.getIntValuesFromColumn("select count(*) from object;")[0];
            objectPageCount = (totalObjectRecords % 10 == 0) ? totalObjectRecords / 10 : totalObjectRecords / 10 + 1;
            objectPaginationLabel.Text = $"{objectPage}/{objectPageCount}";

            int totalDealsRecords = db.getIntValuesFromColumn("select count(*) from deals;")[0];
            dealsPageCount = (totalDealsRecords % 10 == 0) ? totalDealsRecords / 10 : totalDealsRecords / 10 + 1;
            dealsPaginationLabel.Text = $"{dealsPage}/{dealsPageCount}";

            int totalEmployeesRecords = db.getIntValuesFromColumn("select count(*) from employees;")[0];
            employeesPageCount = (totalEmployeesRecords % 10 == 0) ? totalEmployeesRecords / 10 : totalEmployeesRecords / 10 + 1;
            employeesPaginationLabel.Text = $"{employeesPage}/{employeesPageCount}";
        }

        private void fillAllDgv(int objectType = 0, int objectSortIndex = 0)
        {
            db.FillDGV(clientDGV, $"SELECT id, concat(Surname,' ', Name,' ', Patronymic) as 'ФИО', passport as 'Паспорт', address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' FROM db17.clients limit 10 offset {offset};");

            if (objectType == 0)
            { 
                db.FillDGV(objectsDGV, $"SELECT objectid, object_type.type as 'Тип объекта', concat(clients.surname,' ', clients.name, ' ', clients.patronymic) as 'Владелец', object_address as 'Адрес', square as 'Площадь', cadastral as 'Кадаст. ном.', rooms as 'Кол-во комнат', price as 'Цена', photo, status as 'Статус'\r\nFROM db17.object\r\nJOIN object_type ON object_type.id=object.object_type\r\nJOIN clients ON clients.id=object.owner_id;");
            }
            else
            {
                db.FillDGV(objectsDGV, $"SELECT objectid, object_type.type as 'Тип объекта', concat(clients.surname,' ', clients.name, ' ', clients.patronymic) as 'Владелец', object_address as 'Адрес', square as 'Площадь', cadastral as 'Кадаст. ном.', rooms as 'Кол-во комнат', price as 'Цена', photo, status as 'Статус'\r\nFROM db17.object\r\nJOIN object_type ON object_type.id=object.object_type\r\nJOIN clients ON clients.id=object.owner_id WHERE object_type='{objectType}';");
            }

            if (objectSortIndex == 1)
            {
                db.FillDGV(objectsDGV, $"SELECT objectid, object_type.type as 'Тип объекта', concat(clients.surname,' ', clients.name, ' ', clients.patronymic) as 'Владелец', object_address as 'Адрес', square as 'Площадь', cadastral as 'Кадаст. ном.', rooms as 'Кол-во комнат', price as 'Цена', photo, status as 'Статус'\r\nFROM db17.object\r\nJOIN object_type ON object_type.id=object.object_type\r\nJOIN clients ON clients.id=object.owner_id order by price desc;");
            }
            else if (objectSortIndex == 2)
            {
                db.FillDGV(objectsDGV, $"SELECT objectid, object_type.type as 'Тип объекта', concat(clients.surname,' ', clients.name, ' ', clients.patronymic) as 'Владелец', object_address as 'Адрес', square as 'Площадь', cadastral as 'Кадаст. ном.', rooms as 'Кол-во комнат', price as 'Цена', photo, status as 'Статус'\r\nFROM db17.object\r\nJOIN object_type ON object_type.id=object.object_type\r\nJOIN clients ON clients.id=object.owner_id order by price;");
            }
            else
            {
                db.FillDGV(objectsDGV, $"SELECT objectid, object_type.type as 'Тип объекта', concat(clients.surname,' ', clients.name, ' ', clients.patronymic) as 'Владелец', object_address as 'Адрес', square as 'Площадь', cadastral as 'Кадаст. ном..', rooms as 'Кол-во комнат', price as 'Цена', photo, status as 'Статус'\r\nFROM db17.object\r\nJOIN object_type ON object_type.id=object.object_type\r\nJOIN clients ON clients.id=object.owner_id");

            }

            db.FillDGV(employeeDGV, $"SELECT id, login as 'Логин', password as 'Пароль', concat(Surname, ' ', Name, ' ', Patronymic) as 'ФИО', passport as 'Паспорт', birth as 'Дата рождения', phone_number as 'Номер телефона', address as 'Адрес', posts.post as 'Должность' FROM db17.employees join posts on employees.post=posts.postId;\r\n");
            db.FillDGV(dealsDGV, $@"SELECT dealId, concat(clients.surname, ' ', clients.name, ' ', clients.patronymic) as 'Клиент', object.cadastral as 'Объект', concat(employees.surname, ' ', employees.name, ' ', employees.patronymic) as 'Риелтор', deals.type as 'Тип', transaction_date as 'Дата заключения', deals.status as 'Статус'
                                    FROM db17.deals
                                    join clients on deals.client = clients.id
                                    join object on deals.object = object.objectId
                                    join employees on deals.employees = employees.id;
                                    ");

            clientDGV.Columns[0].Visible = false;
            objectsDGV.Columns[0].Visible = false;
            employeeDGV.Columns[0].Visible = false;
            dealsDGV.Columns[0].Visible = false;

            db.setUpDgvImages(objectsDGV, "Фото объекта");

            foreach (DataGridViewRow row in objectsDGV.Rows)
            {
                if (row.Cells[9].Value.ToString() == "В продаже")
                {
                    row.Cells[9].Style.BackColor = Color.LightGreen;
                }
                else if (row.Cells[9].Value.ToString() == "Продан")
                {
                    row.Cells[9].Style.BackColor = Color.LightPink;

                }
                else
                {
                    row.Cells[9].Style.BackColor = Color.LightYellow;
                }
            }

            loadPagination();
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
            new AddDealForm(login).ShowDialog();
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
            if (clientsFilter.SelectedIndex == 0)
            {
                fillAllDgv();
            } else if (clientsFilter.SelectedIndex == 1)
            {
                db.FillDGV(clientDGV, $"SELECT id, concat(Surname,' ', Name,' ', Patronymic) as 'ФИО', passport as 'Паспорт', address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' FROM db17.clients WHERE type='Покупатель';");
            }
            else if (clientsFilter.SelectedIndex == 2)
            {
                db.FillDGV(clientDGV, $"SELECT id, concat(Surname,' ', Name,' ', Patronymic) as 'ФИО', passport as 'Паспорт', address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' FROM db17.clients WHERE type='Продавец';");
            }
            else if (clientsFilter.SelectedIndex == 3)
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
            int objectTypeId = objectsFilter.SelectedIndex;
            fillAllDgv(objectTypeId);
        }

        private void clearParameters()
        {
            clientsFilter.SelectedIndex = 0;
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

        private void редактироватьДанныеОбъектаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AddObjectForm(Convert.ToInt32(objectsDGV.SelectedRows[0].Cells[0].Value)).ShowDialog();
            fillAllDgv();
        }

        private void objectsDGV_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                objectsDGV.ClearSelection();
                objectsDGV[e.ColumnIndex, e.RowIndex].Selected = true;
            }
        }

        private void dealsDGV_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dealsDGV.ClearSelection();
                dealsDGV[e.ColumnIndex, e.RowIndex].Selected = true;
            }
        }

        private void employeeDGV_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                employeeDGV.ClearSelection();
                employeeDGV[e.ColumnIndex, e.RowIndex].Selected = true;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillAllDgv();
            clientsFilter.SelectedIndex = 0;
            objectsFilter.SelectedIndex = 0;
            dealsFilter.SelectedIndex = 0;
            employeeFilter.SelectedIndex = 0;
            objectsSort.SelectedIndex = 0;
            clientSort.SelectedIndex = 0;
            dealsSort.SelectedIndex = 0;
            employeeSort.SelectedIndex = 0;
        }

        private void objectsSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sortIndex = objectsSort.SelectedIndex;
            fillAllDgv(0, sortIndex);
        }

        private void экспортДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                db.exportDataFromTableToCSV("clients");
                MessageStore.successExportMessage();
            }
            catch (Exception ex)
            {
                MessageStore.somethingWentWrongMessage();
            }
        }

        private void импортДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog1.Title = "Выберите CSV-файл";
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    openFileDialog1.OpenFile();
                    db.importDataFromCSVToTable(openFileDialog1.FileName, "clients");
                    MessageStore.successImportMessage();
                    fillAllDgv();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new AddDealForm(login, Convert.ToInt32(Convert.ToInt32(clientDGV.SelectedRows[0].Cells[0].Value))).ShowDialog(this);
            fillAllDgv();
        }

        private void изменитьДанныеОСделкеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dealsDGV.SelectedRows[0].Cells[6].Value.ToString() != "Завершена")
            {
                new AddDealForm(login, Convert.ToInt32(dealsDGV.SelectedRows[0].Cells[0].Value)).ShowDialog();
            } else
            {
                MessageStore.canNotEditDealMessage();
            }
            fillAllDgv();
        }

        private void создатьДокументыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dealsDGV.SelectedRows[0].Cells[6].Value.ToString() != "Завершена" || dealsDGV.SelectedRows[0].Cells[6].Value.ToString() != "Подтверждена")
            {
                new CreateDocumentForm(Convert.ToInt32(dealsDGV.SelectedRows[0].Cells[0].Value)).ShowDialog();
            }
            else
            {
                MessageBox.Show("Для данное сделки пока нельзя создать документы");
            }
        }

        private void просмотрДокументовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string docsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Документы по сделкам");

            if (!Directory.Exists(docsFolder))
            {
                Directory.CreateDirectory(docsFolder);
            }

            Process.Start("explorer.exe", docsFolder);
        }

        private void dealsSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            int dealSortIndex = dealsSort.SelectedIndex;

            if (dealSortIndex == 0)
            {
                fillAllDgv();
            }
            else if (dealSortIndex == 1)
            {
                db.FillDGV(dealsDGV, $@"SELECT dealId, concat(clients.surname, ' ', clients.name, ' ', clients.patronymic) as 'Клиент', object.cadastral as 'Объект', concat(employees.surname, ' ', employees.name, ' ', employees.patronymic) as 'Риелтор', deals.type as 'Тип', transaction_date as 'Дата заключения', deals.status as 'Статус'
                                    FROM db17.deals
                                    join clients on deals.client = clients.id
                                    join object on deals.object = object.objectId
                                    join employees on deals.employees = employees.id
                                    order by deals.transaction_date desc;");
            } else
            {
                db.FillDGV(dealsDGV, $@"SELECT dealId, concat(clients.surname, ' ', clients.name, ' ', clients.patronymic) as 'Клиент', object.cadastral as 'Объект', concat(employees.surname, ' ', employees.name, ' ', employees.patronymic) as 'Риелтор', deals.type as 'Тип', transaction_date as 'Дата заключения', deals.status as 'Статус'
                                    FROM db17.deals
                                    join clients on deals.client = clients.id
                                    join object on deals.object = object.objectId
                                    join employees on deals.employees = employees.id
                                    order by deals.transaction_date;");
            }
        }

        private void dealsFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            int dealFilterIndex = dealsFilter.SelectedIndex;

            if (dealFilterIndex == 0)
            {
                fillAllDgv();
            }
            else if (dealFilterIndex == 1)
            {
                db.FillDGV(dealsDGV, $@"SELECT dealId, concat(clients.surname, ' ', clients.name, ' ', clients.patronymic) as 'Клиент', object.cadastral as 'Объект', concat(employees.surname, ' ', employees.name, ' ', employees.patronymic) as 'Риелтор', deals.type as 'Тип', transaction_date as 'Дата заключения', deals.status as 'Статус'
                                    FROM db17.deals
                                    join clients on deals.client = clients.id
                                    join object on deals.object = object.objectId
                                    join employees on deals.employees = employees.id
                                    where deals.status='Новая';"); 
            }
            else if (dealFilterIndex == 2)
            {
                db.FillDGV(dealsDGV, $@"SELECT dealId, concat(clients.surname, ' ', clients.name, ' ', clients.patronymic) as 'Клиент', object.cadastral as 'Объект', concat(employees.surname, ' ', employees.name, ' ', employees.patronymic) as 'Риелтор', deals.type as 'Тип', transaction_date as 'Дата заключения', deals.status as 'Статус'
                                    FROM db17.deals
                                    join clients on deals.client = clients.id
                                    join object on deals.object = object.objectId
                                    join employees on deals.employees = employees.id
                                    where deals.status='Подтверждена';");
            }
            else if (dealFilterIndex == 3)
            {
                db.FillDGV(dealsDGV, $@"SELECT dealId, concat(clients.surname, ' ', clients.name, ' ', clients.patronymic) as 'Клиент', object.cadastral as 'Объект', concat(employees.surname, ' ', employees.name, ' ', employees.patronymic) as 'Риелтор', deals.type as 'Тип', transaction_date as 'Дата заключения', deals.status as 'Статус'
                                    FROM db17.deals
                                    join clients on deals.client = clients.id
                                    join object on deals.object = object.objectId
                                    join employees on deals.employees = employees.id
                                    where deals.status='Отменена';");
            }
            else
            {
                db.FillDGV(dealsDGV, $@"SELECT dealId, concat(clients.surname, ' ', clients.name, ' ', clients.patronymic) as 'Клиент', object.cadastral as 'Объект', concat(employees.surname, ' ', employees.name, ' ', employees.patronymic) as 'Риелтор', deals.type as 'Тип', transaction_date as 'Дата заключения', deals.status as 'Статус'
                                    FROM db17.deals
                                    join clients on deals.client = clients.id
                                    join object on deals.object = object.objectId
                                    join employees on deals.employees = employees.id
                                    where deals.status='Завершена';");
            }
        }

        private void employeeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (employeeFilter.SelectedIndex == 0)
            {
                db.FillDGV(employeeDGV, $"SELECT id, login as 'Логин', password as 'Пароль', concat(Surname, ' ', Name, ' ', Patronymic) as 'ФИО', passport as 'Паспорт', birth as 'Дата рождения', phone_number as 'Номер телефона', address as 'Адрес', posts.post as 'Должность' FROM db17.employees join posts on employees.post=posts.postId;");
            } else if (employeeFilter.SelectedIndex == 1)
            {
                db.FillDGV(employeeDGV, $"SELECT id, login as 'Логин', password as 'Пароль', concat(Surname, ' ', Name, ' ', Patronymic) as 'ФИО', passport as 'Паспорт', birth as 'Дата рождения', phone_number as 'Номер телефона', address as 'Адрес', posts.post as 'Должность' FROM db17.employees join posts on employees.post=posts.postId where employees.post=2;");
            }
            else if (employeeFilter.SelectedIndex == 2)
            {
                db.FillDGV(employeeDGV, $"SELECT id, login as 'Логин', password as 'Пароль', concat(Surname, ' ', Name, ' ', Patronymic) as 'ФИО', passport as 'Паспорт', birth as 'Дата рождения', phone_number as 'Номер телефона', address as 'Адрес', posts.post as 'Должность' FROM db17.employees join posts on employees.post=posts.postId where employees.post=1;");
            }
            else
            {
                db.FillDGV(employeeDGV, $"SELECT id, login as 'Логин', password as 'Пароль', concat(Surname, ' ', Name, ' ', Patronymic) as 'ФИО', passport as 'Паспорт', birth as 'Дата рождения', phone_number as 'Номер телефона', address as 'Адрес', posts.post as 'Должность' FROM db17.employees join posts on employees.post=posts.postId where employees.post=3;");
            }
        }

        private void clientSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clientSort.SelectedIndex == 0)
            {
                fillAllDgv();
            }
            else if (clientSort.SelectedIndex == 1)
            {
                db.FillDGV(clientDGV, $"SELECT id, concat(Surname,' ', Name,' ', Patronymic) as 'ФИО', passport as 'Паспорт', address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' FROM db17.clients order by birth limit 10;");
            }
            else if (clientSort.SelectedIndex == 2)
            {
                db.FillDGV(clientDGV, $"SELECT id, concat(Surname,' ', Name,' ', Patronymic) as 'ФИО', passport as 'Паспорт', address as 'Адрес', birth as 'Дата рождения', phone_number as 'Номер телефона', type as 'Тип' FROM db17.clients order by birth desc limit 10;");
            }
            
        }

        private void employeeSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (employeeSort.SelectedIndex == 0)
            {
                db.FillDGV(employeeDGV, $"SELECT id, login as 'Логин', password as 'Пароль', concat(Surname, ' ', Name, ' ', Patronymic) as 'ФИО', passport as 'Паспорт', birth as 'Дата рождения', phone_number as 'Номер телефона', address as 'Адрес', posts.post as 'Должность' FROM db17.employees join posts on employees.post=posts.postId;");
            }
            else if (employeeSort.SelectedIndex == 1)
            {
                db.FillDGV(employeeDGV, $"SELECT id, login as 'Логин', password as 'Пароль', concat(Surname, ' ', Name, ' ', Patronymic) as 'ФИО', passport as 'Паспорт', birth as 'Дата рождения', phone_number as 'Номер телефона', address as 'Адрес', posts.post as 'Должность' FROM db17.employees join posts on employees.post=posts.postId order by birth;");
            }
            else if (employeeSort.SelectedIndex == 2)
            {
                db.FillDGV(employeeDGV, $"SELECT id, login as 'Логин', password as 'Пароль', concat(Surname, ' ', Name, ' ', Patronymic) as 'ФИО', passport as 'Паспорт', birth as 'Дата рождения', phone_number as 'Номер телефона', address as 'Адрес', posts.post as 'Должность' FROM db17.employees join posts on employees.post=posts.postId order by birth desc;");
            }
            
        }

        private void импортДанныхToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog1.Title = "Выберите CSV-файл";
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    openFileDialog1.OpenFile();
                    db.importDataFromCSVToTable(openFileDialog1.FileName, "employees");
                    MessageStore.successImportMessage();
                    fillAllDgv();
                }
                catch (Exception ex)
                {
                    MessageStore.somethingWentWrongMessage();
                }
            }
        }

        private void импортДанныхToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog1.Title = "Выберите CSV-файл";
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    openFileDialog1.OpenFile();
                    db.importDataFromCSVToTable(openFileDialog1.FileName, "object");
                    MessageStore.successImportMessage();
                    fillAllDgv();
                }
                catch (Exception ex)
                {
                    MessageStore.somethingWentWrongMessage();
                }
            }
        }

        private void импортДанныхToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog1.Title = "Выберите CSV-файл";
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    openFileDialog1.OpenFile();
                    db.importDataFromCSVToTable(openFileDialog1.FileName, "deals");
                    MessageStore.successImportMessage();
                    fillAllDgv();
                }
                catch (Exception ex)
                {
                    MessageStore.somethingWentWrongMessage();
                }
            }
        }
    }
}
