﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using DB;
using System.Configuration;
using System.Threading;
using System.Text.RegularExpressions;
using DemoEx.utility;
using DemoEx.Forms;


namespace DemoEx
{
    public partial class Form1 : Form
    {
        private Db db = new Db(Connection.connectionString);
        private int counter = 0;
        public Form1()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            captcha.Visible = false;
            pwdTb.PasswordChar = '·';
            captchaPicture.Image = db.createImageForCaptcha(captchaPicture.Width, captchaPicture.Height, 4);

        }

        private void loginTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsNumber(ch) && !char.IsLetter(ch) && !char.IsControl(ch) && ch != '_') 
            {
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                pwdTb.PasswordChar = '\0';
            } else
            {
                pwdTb.PasswordChar = '·';
            }
        }


        private void loginTb_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            InputFieldCorrection.engLettersNumbersField(e);
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
           var result = MessageStore.applicationExitConfirmationMessage();
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            try
            {
                if (counter >= 1)
                {
                    captcha.Visible = true;
                }

                if (loginTb.Text.Length == 0 || pwdTb.Text.Length == 0)
                {
                    MessageStore.notAllFieldsFilledMessage();
                    return;
                }
                                
                if (loginTb.Text == "admin" && pwdTb.Text == "admin")
                {
                    new AdminForm().ShowDialog();
                    return;
                } else
                {
                    string login = loginTb.Text;
                    string pwd = pwdTb.Text;
                    string pwdFromDb = db.getValuesFromColumn($"select password from employees where login='{login}';")[0];
                    if (db.getHashFromPassword(pwd) == pwdFromDb)
                    {
                        int post = Convert.ToInt32(db.getIntValuesFromColumn($"select post from employees where login='{login}';")[0]);
                        loginTb.Clear();
                        pwdTb.Clear();
                        captcha.Visible = false;
                        new MainForm(login, post).ShowDialog();
                    }
                    else
                    {
                        MessageStore.loginErrorMessage();
                        counter++;
                        return;
                    }
                }
                
            } catch(Exception ex)
            {
                //MessageStore.somethingWentWrongMessage();
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (captchaInputText.Text == db.getCaptchaText())
            {
                captcha.Visible = false;
                captchaPicture.Image = db.createImageForCaptcha(captchaPicture.Width, captchaPicture.Height, 4);
            }
            else
            {
                MessageStore.captchaErrorMessage();
            }
            captchaInputText.Clear();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            captchaPicture.Image = db.createImageForCaptcha(captchaPicture.Width, captchaPicture.Height, 4);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
