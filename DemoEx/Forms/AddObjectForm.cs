using DB;
using System;
using System.Windows.Forms;

namespace DemoEx.Forms
{
    public partial class AddObjectForm : Form
    {
        private Db db = new Db(Connection.connectionString);
        public AddObjectForm()
        {
            InitializeComponent();
        }

        private void AddObjectForm_Load(object sender, EventArgs e)
        {
            owner_cb.DropDownStyle = ComboBoxStyle.DropDownList;    
            owner_cb.Items.AddRange(db.getValuesFromColumn("SELECT concat(surname,' ', name, ' ', patronymic) FROM db17.clients where type='Продавец' or type='Арендодатель';").ToArray());
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.executeNonQuery($"INSERT INTO `db17`.`object` (`object_type`, `owner_id`, `address`, `square`, `cadastral`, `rooms`, `price`, `photo`, `status`) VALUES ('1', '1', 'f', 'f', 'f', '3', 'f', 'f', 'f');");
        }
    }
}
