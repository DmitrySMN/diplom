using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoEx.utility
{
    public static class Messages
    {

        public static void loginErrorMessage()
        {
            MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void applicationExitConfirmationMessage()
        {
            DialogResult res = MessageBox.Show("Вы уверенны что хотите, выйти из приложения?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                return;
            }
        }
    }
}
