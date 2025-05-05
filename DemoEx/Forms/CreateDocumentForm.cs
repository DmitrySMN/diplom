using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoEx.Forms
{
    public partial class CreateDocumentForm : Form
    {
        private int dealId; 
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
    }
}
