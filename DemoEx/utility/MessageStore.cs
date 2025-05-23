﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoEx.utility
{
    public static class MessageStore
    {
        //Общие
        public static void somethingWentWrongMessage() {
            MessageBox.Show("Что-то пошло не так!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void fieldsFilledIncorrectMessage()
        {
            MessageBox.Show("Поля заполнены некорректно!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        public static void notAllFieldsFilledMessage()
        {
            MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Авторизация
        public static void loginErrorMessage()
        {
            MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult applicationExitConfirmationMessage()
        {
            return MessageBox.Show("Вы уверенны что хотите, выйти из приложения?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static void captchaErrorMessage()
        {
            MessageBox.Show("Каптча введена неверно! Система заблокирована на 10 секунд.", "Каптча", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //Сделки
        public static void dealCannotChangeMessage() {
            MessageBox.Show("Данная сделка завершена, её нельзя изменить!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void addDealMessage()
        {
            MessageBox.Show("Сделка успешна создана!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void editDealMessage()
        {
            MessageBox.Show("Данные о сделке успешно изменены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void canNotEditDealMessage()
        {
            MessageBox.Show("Эта сделка завершена, ее нельзя изменить!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult createDealDocumentlMessage()
        {
            return MessageBox.Show("Создать документ по данной сделке?", "Создание докумета", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        }

        //Клиенты
        public static void clientDataEditedMessage() {
            MessageBox.Show("Данные о клиенте успешно изменены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void clientAddedMessage() {
            MessageBox.Show("Клиент успешно добавлен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult deleteClientConfirmationMessage()
        {
            return MessageBox.Show("Удалить данные клиента?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        }

        //Работники
        public static void employeeAddedMessage()
        {
            MessageBox.Show("Данные о сотруднике успешно изменены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        public static void employeeLoginErrorMessage()
        {
            MessageBox.Show("Сотрудник с таким логином уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void addEmployeeMessage()
        {
            MessageBox.Show("Сотрудник успешно добавлен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Объекты
        public static void editObjectMessage()
        {
            MessageBox.Show("Информаци об объекте успешно отредактирована!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void addObjectMessage()
        {
            MessageBox.Show("Объект успешно добавлен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult deleteObjectConfirmationMessage()
        {
            return MessageBox.Show("Удалить объект?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        //Импорт Экспорт
        public static void successExportMessage()
        {
            MessageBox.Show("Данные успешно экспортированы!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void successImportMessage()
        {
            MessageBox.Show("Данные успешно импортированы!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Создание документов
        public static void successCreateSalesDocument(string path)
        {
            MessageBox.Show($"Документ купли-продажи создан и сохранен по пути\n{path}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void successCreateTransferDocument(string path)
        {
            MessageBox.Show($"Документ приема-передачи создан и сохранен по пути\n{path}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void successCreateObjectDocument(string path)
        {
            MessageBox.Show($"Отчет по объекту создан и сохранен по пути\n{path}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
