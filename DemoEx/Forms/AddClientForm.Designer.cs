
namespace DemoEx
{
    partial class AddClientForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.surname = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.passport = new System.Windows.Forms.MaskedTextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.phone = new System.Windows.Forms.MaskedTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.name = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pat = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.type = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.address = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.surname);
            this.groupBox1.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(15, 3);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox1.Size = new System.Drawing.Size(355, 113);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Фамилия";
            // 
            // surname
            // 
            this.surname.BackColor = System.Drawing.Color.White;
            this.surname.ForeColor = System.Drawing.Color.Black;
            this.surname.Location = new System.Drawing.Point(6, 47);
            this.surname.Margin = new System.Windows.Forms.Padding(4);
            this.surname.MaxLength = 15;
            this.surname.Name = "surname";
            this.surname.Size = new System.Drawing.Size(339, 39);
            this.surname.TabIndex = 0;
            this.surname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.passport);
            this.groupBox4.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.groupBox4.ForeColor = System.Drawing.Color.Black;
            this.groupBox4.Location = new System.Drawing.Point(15, 456);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox4.Size = new System.Drawing.Size(355, 113);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Паспортные данные";
            // 
            // passport
            // 
            this.passport.BackColor = System.Drawing.Color.White;
            this.passport.ForeColor = System.Drawing.Color.Black;
            this.passport.Location = new System.Drawing.Point(10, 49);
            this.passport.Margin = new System.Windows.Forms.Padding(4);
            this.passport.Mask = "0000 999999";
            this.passport.Name = "passport";
            this.passport.Size = new System.Drawing.Size(335, 39);
            this.passport.TabIndex = 0;
            this.passport.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.maskedTextBox1_KeyPress);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dateTimePicker1);
            this.groupBox5.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.groupBox5.ForeColor = System.Drawing.Color.Black;
            this.groupBox5.Location = new System.Drawing.Point(382, 3);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox5.Size = new System.Drawing.Size(355, 113);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Дата рождения";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarForeColor = System.Drawing.Color.White;
            this.dateTimePicker1.CalendarMonthBackground = System.Drawing.Color.Black;
            this.dateTimePicker1.CalendarTitleBackColor = System.Drawing.SystemColors.ControlText;
            this.dateTimePicker1.CalendarTitleForeColor = System.Drawing.Color.Black;
            this.dateTimePicker1.Location = new System.Drawing.Point(10, 47);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(341, 39);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SkyBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(382, 459);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(355, 51);
            this.button1.TabIndex = 4;
            this.button1.Text = "Создать";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.phone);
            this.groupBox6.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.groupBox6.ForeColor = System.Drawing.Color.Black;
            this.groupBox6.Location = new System.Drawing.Point(382, 118);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox6.Size = new System.Drawing.Size(355, 113);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Номер телефона";
            // 
            // phone
            // 
            this.phone.BackColor = System.Drawing.Color.White;
            this.phone.ForeColor = System.Drawing.Color.Black;
            this.phone.Location = new System.Drawing.Point(10, 49);
            this.phone.Margin = new System.Windows.Forms.Padding(4);
            this.phone.Mask = "+7 (999) 999 99 99";
            this.phone.Name = "phone";
            this.phone.Size = new System.Drawing.Size(331, 39);
            this.phone.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.name);
            this.groupBox2.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(15, 118);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox2.Size = new System.Drawing.Size(355, 113);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Имя";
            // 
            // name
            // 
            this.name.BackColor = System.Drawing.Color.White;
            this.name.ForeColor = System.Drawing.Color.Black;
            this.name.Location = new System.Drawing.Point(6, 44);
            this.name.Margin = new System.Windows.Forms.Padding(4);
            this.name.MaxLength = 15;
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(339, 39);
            this.name.TabIndex = 0;
            this.name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress_1);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pat);
            this.groupBox3.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(15, 230);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox3.Size = new System.Drawing.Size(355, 113);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Отчество";
            // 
            // pat
            // 
            this.pat.BackColor = System.Drawing.Color.White;
            this.pat.ForeColor = System.Drawing.Color.Black;
            this.pat.Location = new System.Drawing.Point(6, 51);
            this.pat.Margin = new System.Windows.Forms.Padding(4);
            this.pat.MaxLength = 15;
            this.pat.Name = "pat";
            this.pat.Size = new System.Drawing.Size(339, 39);
            this.pat.TabIndex = 0;
            this.pat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress_1);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.type);
            this.groupBox7.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.groupBox7.ForeColor = System.Drawing.Color.Black;
            this.groupBox7.Location = new System.Drawing.Point(382, 230);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.groupBox7.Size = new System.Drawing.Size(355, 113);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Тип клиента";
            // 
            // type
            // 
            this.type.FormattingEnabled = true;
            this.type.Location = new System.Drawing.Point(6, 49);
            this.type.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(336, 40);
            this.type.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.SteelBlue;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.button2.Location = new System.Drawing.Point(382, 518);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(355, 51);
            this.button2.TabIndex = 5;
            this.button2.Text = "Выйти";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.address);
            this.groupBox9.Location = new System.Drawing.Point(15, 352);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(723, 93);
            this.groupBox9.TabIndex = 18;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Адрес";
            // 
            // address
            // 
            this.address.Location = new System.Drawing.Point(6, 38);
            this.address.MaxLength = 100;
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(706, 39);
            this.address.TabIndex = 0;
            // 
            // AddClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(750, 586);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 18F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Name = "AddClientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление клиентов";
            this.Load += new System.EventHandler(this.RegistrationForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox surname;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.MaskedTextBox passport;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.MaskedTextBox phone;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox pat;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox type;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TextBox address;
    }
}