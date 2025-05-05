using DemoEx.data;
using DemoEx.utility;
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
using DB;

namespace DemoEx.Forms
{
    public partial class CreateDocumentForm : Form
    {
        private int dealId;
        private Db db = new Db(Connection.connectionString);
        public CreateDocumentForm(int dealId = 0)
        {
            InitializeComponent();
            this.dealId = dealId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CreateDocumentForm_Load(object sender, EventArgs e)
        {
            groupBox1.Text = $"Сделка №{dealId}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DocumentHelper.generateSalesContract(
                    filePath: $@"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Документы по сделкам")}\{dealId}_Документ_купли_продажи.docx",
                    buyer: db.getValuesFromColumn($"SELECT concat(clients.surname, ' ', clients.name, ' ', clients.patronymic) FROM db17.deals join clients on deals.client=clients.id where dealId={dealId};")[0],
                    seller: db.getValuesFromColumn($"SELECT (select concat(surname,' ', name,' ', patronymic) from clients where id=object.owner_id) FROM db17.deals join object on deals.object=object.objectid where dealId={dealId};")[0],
                    propertyDescription: $"{db.getValuesFromColumn($"SELECT (select type from object_type where id=object.object_type) FROM db17.deals join object on deals.object=object.objectid where dealid={dealId};")[0]} по адресу: {db.getValuesFromColumn($"SELECT object.object_address FROM db17.deals join object on deals.object=object.objectid where dealid={dealId};")[0]}",
                    price: db.getIntValuesFromColumn($"SELECT object.price FROM db17.deals join object on deals.object=object.objectid where dealid={dealId};")[0],
                    contractDate: db.getDateValuesFromColumn($"SELECT transaction_date FROM db17.deals where dealId={dealId};")[0]
                    );
                MessageBox.Show("Документ создан!");
            } catch (Exception ex)
            {
                MessageStore.somethingWentWrongMessage();
            }
        }
    }
}
