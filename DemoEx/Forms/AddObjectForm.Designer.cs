namespace DemoEx.Forms
{
    partial class AddObjectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddObjectForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.owner_cb = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.address_tb = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.object_type_cb = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.square_tb = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cadastral_tb = new System.Windows.Forms.MaskedTextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.price_tb = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rooms_tb = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.status_cb = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(264, 218);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.SkyBlue;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(12, 238);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(264, 51);
            this.button3.TabIndex = 5;
            this.button3.Text = "Загрузить фото";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.owner_cb);
            this.groupBox7.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.groupBox7.ForeColor = System.Drawing.Color.Black;
            this.groupBox7.Location = new System.Drawing.Point(285, 3);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox7.Size = new System.Drawing.Size(577, 91);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Владелец";
            // 
            // owner_cb
            // 
            this.owner_cb.FormattingEnabled = true;
            this.owner_cb.Location = new System.Drawing.Point(6, 35);
            this.owner_cb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.owner_cb.Name = "owner_cb";
            this.owner_cb.Size = new System.Drawing.Size(562, 40);
            this.owner_cb.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.address_tb);
            this.groupBox1.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(285, 100);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox1.Size = new System.Drawing.Size(577, 91);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Адрес";
            // 
            // address_tb
            // 
            this.address_tb.Location = new System.Drawing.Point(9, 38);
            this.address_tb.MaxLength = 100;
            this.address_tb.Name = "address_tb";
            this.address_tb.Size = new System.Drawing.Size(558, 39);
            this.address_tb.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.object_type_cb);
            this.groupBox2.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(285, 198);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox2.Size = new System.Drawing.Size(285, 91);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Тип объекта";
            // 
            // object_type_cb
            // 
            this.object_type_cb.FormattingEnabled = true;
            this.object_type_cb.Location = new System.Drawing.Point(9, 35);
            this.object_type_cb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.object_type_cb.Name = "object_type_cb";
            this.object_type_cb.Size = new System.Drawing.Size(267, 40);
            this.object_type_cb.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.square_tb);
            this.groupBox3.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(577, 198);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox3.Size = new System.Drawing.Size(285, 91);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Площадь";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "кв.м";
            // 
            // square_tb
            // 
            this.square_tb.Location = new System.Drawing.Point(10, 36);
            this.square_tb.MaxLength = 4;
            this.square_tb.Name = "square_tb";
            this.square_tb.Size = new System.Drawing.Size(198, 39);
            this.square_tb.TabIndex = 1;
            this.square_tb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.square_tb_KeyPress);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cadastral_tb);
            this.groupBox4.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.groupBox4.ForeColor = System.Drawing.Color.Black;
            this.groupBox4.Location = new System.Drawing.Point(12, 301);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox4.Size = new System.Drawing.Size(312, 91);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Кадастровый номер";
            // 
            // cadastral_tb
            // 
            this.cadastral_tb.Location = new System.Drawing.Point(9, 41);
            this.cadastral_tb.Mask = "99:99:999999:99";
            this.cadastral_tb.Name = "cadastral_tb";
            this.cadastral_tb.Size = new System.Drawing.Size(294, 39);
            this.cadastral_tb.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.price_tb);
            this.groupBox5.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.groupBox5.ForeColor = System.Drawing.Color.Black;
            this.groupBox5.Location = new System.Drawing.Point(336, 301);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox5.Size = new System.Drawing.Size(312, 91);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Цена";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(242, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "руб.";
            // 
            // price_tb
            // 
            this.price_tb.Location = new System.Drawing.Point(9, 41);
            this.price_tb.MaxLength = 10;
            this.price_tb.Name = "price_tb";
            this.price_tb.Size = new System.Drawing.Size(225, 39);
            this.price_tb.TabIndex = 1;
            this.price_tb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.price_tb_KeyPress);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rooms_tb);
            this.groupBox6.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.groupBox6.ForeColor = System.Drawing.Color.Black;
            this.groupBox6.Location = new System.Drawing.Point(660, 301);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox6.Size = new System.Drawing.Size(202, 91);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Кол-во комнат";
            // 
            // rooms_tb
            // 
            this.rooms_tb.Location = new System.Drawing.Point(9, 43);
            this.rooms_tb.MaxLength = 2;
            this.rooms_tb.Name = "rooms_tb";
            this.rooms_tb.Size = new System.Drawing.Size(183, 39);
            this.rooms_tb.TabIndex = 2;
            this.rooms_tb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rooms_tb_KeyPress);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.status_cb);
            this.groupBox8.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.groupBox8.ForeColor = System.Drawing.Color.Black;
            this.groupBox8.Location = new System.Drawing.Point(12, 392);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox8.Size = new System.Drawing.Size(405, 91);
            this.groupBox8.TabIndex = 7;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Статус";
            // 
            // status_cb
            // 
            this.status_cb.FormattingEnabled = true;
            this.status_cb.Location = new System.Drawing.Point(6, 35);
            this.status_cb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.status_cb.Name = "status_cb";
            this.status_cb.Size = new System.Drawing.Size(390, 40);
            this.status_cb.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SkyBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(427, 424);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(212, 43);
            this.button1.TabIndex = 11;
            this.button1.Text = "Добавить";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.SteelBlue;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(650, 424);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(212, 43);
            this.button2.TabIndex = 12;
            this.button2.Text = "Выйти";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // AddObjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(872, 491);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddObjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление объекта";
            this.Load += new System.EventHandler(this.AddObjectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox owner_cb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox address_tb;
        private System.Windows.Forms.ComboBox object_type_cb;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox square_tb;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.MaskedTextBox cadastral_tb;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox price_tb;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox rooms_tb;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox status_cb;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label2;
    }
}