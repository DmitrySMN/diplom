using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DemoEx
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "CSV files (*.csv)|*.csv|XML files (*.xml)|*.xml";

            tables.DropDownStyle = ComboBoxStyle.DropDownList;
            tables.Items.Add("Клиенты");
            tables.Items.Add("Объекты");
            tables.Items.Add("Сотрудники");
            tables.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string pathToFile = openFileDialog1.FileName; // full path
                importCsvToDatabase(pathToFile);
            }
            else
            {
                MessageBox.Show("Выберите файл!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void importCsvToDatabase(string filePath)
        {
            string con = Connection.connectionString;
            using (MySqlConnection connection = new MySqlConnection(con))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var reader = new StreamReader(filePath))
                        {
                            string headerLine = reader.ReadLine();
                            string[] headers = headerLine.Split(';');
                            while (!reader.EndOfStream)
                            {
                                string line = reader.ReadLine();
                                string[] values = line.Split(';');
                                
                                switch (tables.Text)
                                {
                                    case "Клиенты":
                                        string sql = "INSERT INTO `clients` (" + string.Join(",", headers) + ") VALUES (" + string.Join(",", values.Select(v => "'" + MySqlHelper.EscapeString(v) + "'")) + ")";
                                        using (MySqlCommand command = new MySqlCommand(sql, connection, transaction))
                                        {
                                            command.ExecuteNonQuery();
                                        }
                                        break;
                                    case "Объекты":
                                        string sql1 = "INSERT INTO `estate` (" + string.Join(",", headers) + ") VALUES (" + string.Join(",", values.Select(v => "'" + MySqlHelper.EscapeString(v) + "'")) + ")";
                                        using (MySqlCommand command = new MySqlCommand(sql1, connection, transaction))
                                        {
                                            command.ExecuteNonQuery();
                                        }
                                        break;
                                    case "Сотрудники":
                                        string sql2 = "INSERT INTO `employees` (" + string.Join(",", headers) + ") VALUES (" + string.Join(",", values.Select(v => "'" + MySqlHelper.EscapeString(v) + "'")) + ")";
                                        using (MySqlCommand command = new MySqlCommand(sql2, connection, transaction))
                                        {
                                            command.ExecuteNonQuery();
                                        }
                                        break;
                                }
                            }
                        }
                        transaction.Commit();
                        MessageBox.Show("Данные успешно импортированы.");
                    } catch (Exception excc)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Ошибка импорта данных: " + excc.Message);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы точно хотите восстановить базу данных?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
       
            if (result == DialogResult.Yes)
            {
                try
                {
                    restoreDataBase();
                    MessageBox.Show("Восстановление успешно выполнено!", "Восстановление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception excc)
                {
                    MessageBox.Show("Восстановление не удалось", "Восстановление", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else
            {

            }
        }

        private void restoreDataBase()
        {
            string connectionString = Connection.connectionString;
            string sqlFilePath = @".\data\restore_dump.sql";

            string sqlScript = File.ReadAllText(sqlFilePath);

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;

                string[] sqlCommands = sqlScript.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string commandText in sqlCommands)
                {
                    if (!string.IsNullOrWhiteSpace(commandText))
                    {
                        command.CommandText = commandText;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
