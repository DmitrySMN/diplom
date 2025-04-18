using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoEx.utility
{
    public static class InputFieldCorrection
    {
        public static void engLettersNumbersField(KeyPressEventArgs e)
        {
            string s = e.KeyChar.ToString();
            if (!Regex.Match(s, @"^[a-zA-Z]+$|[\b]|[_]|[0-9]").Success)
            {
                e.Handled = true;
            }
        }

        public static void ruLettersField(KeyPressEventArgs e)
        {
            string s = e.KeyChar.ToString();
            if (!Regex.Match(s, @"[а-яА-Я]|[\b]|[-]").Success)
            {
                e.Handled = true;
            }
        }

        public static void ruLettersNumbersField(KeyPressEventArgs e)
        {
            string s = e.KeyChar.ToString();
            if (!Regex.Match(s, @"[а-яА-Я]|[\b]|[-]|[.]|[,]|[0-9]").Success)
            {
                e.Handled = true;
            }
        }

        public static void numbersField(KeyPressEventArgs e)
        {
            string s = e.KeyChar.ToString();
            if (!Regex.Match(s, @"[0-9]").Success)
            {
                e.Handled = true;
            }
        }
    }
}
