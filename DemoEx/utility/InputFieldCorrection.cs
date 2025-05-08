using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DemoEx.utility
{
    public static class InputFieldCorrection
    {
        public static void engLettersNumbersField(KeyPressEventArgs e)
        {
            string s = e.KeyChar.ToString();
            if (!Regex.Match(s, @"^[a-zA-Z]+$|[\b]|[0-9]").Success && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        public static void engLoginField(KeyPressEventArgs e)
        {
            string s = e.KeyChar.ToString();
            if (!Regex.Match(s, @"^[a-zA-Z]+$|[\b]|[_]|[0-9]").Success && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        public static void ruLettersField(KeyPressEventArgs e)
        {
            string s = e.KeyChar.ToString();
            if (!Regex.Match(s, @"[а-яА-Я]|[\b]").Success && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        public static void ruLettersAndDash(KeyPressEventArgs e)
        {
            string s = e.KeyChar.ToString();
            if (!Regex.Match(s, @"[а-яА-Я]|[\b]|[-]").Success && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        public static void ruAddressField(KeyPressEventArgs e)
        {
            string s = e.KeyChar.ToString();
            if (!Regex.Match(s, @"[а-яА-Я]|[\b]|[-]|[.]|[,]").Success && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        public static void ruLettersNumbersField(KeyPressEventArgs e)
        {
            string s = e.KeyChar.ToString();
            if (!Regex.Match(s, @"[а-яА-Я]|[\b]|[-]|[.]|[,]|[0-9]").Success && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        public static void numbersField(KeyPressEventArgs e)
        {
            string s = e.KeyChar.ToString();
            if (!Regex.Match(s, @"[0-9]").Success && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        public static void toUpperFirstLetter(System.Windows.Forms.TextBox textBoxName)
        {
            if (!string.IsNullOrEmpty(textBoxName.Text))
            { 
                int cursorPosition = textBoxName.SelectionStart;

                // Преобразуем первую букву в заглавную, остальные оставляем как есть
                textBoxName.Text = char.ToUpper(textBoxName.Text[0]) +
                                (textBoxName.Text.Length > 1 ? textBoxName.Text.Substring(1) : "");

                // Восстанавливаем позицию курсора
                textBoxName.SelectionStart = cursorPosition;
            }
        }
    }
}
