using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using DB;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Word.Application;
using System.Runtime.InteropServices;

namespace DemoEx.data
{   
    public static class DocumentHelper
    {
        private static Db db = new Db(Connection.connectionString);

        public static void generateSalesContract(string filePath,
            string seller,
            string buyer,
            string propertyDescription,
            decimal price,
            DateTime contractDate,
            string additionalTerms = "")
        {
            Application wordApp = null;
            Document doc = null;
            
            try
            {
                wordApp = new Application { Visible = false };
                doc = wordApp.Documents.Add();
                
                // Настройка документа
                doc.PageSetup.Orientation = WdOrientation.wdOrientPortrait;
                doc.PageSetup.TopMargin = wordApp.CentimetersToPoints(2.5f);
                doc.PageSetup.BottomMargin = wordApp.CentimetersToPoints(2.5f);
                doc.PageSetup.LeftMargin = wordApp.CentimetersToPoints(3f);
                doc.PageSetup.RightMargin = wordApp.CentimetersToPoints(1.5f);

                // Добавляем заголовок
                AddTitle(doc, "ДОГОВОР КУПЛИ-ПРОДАЖИ НЕДВИЖИМОСТИ");
                
                // Добавляем дату (выравнивание по правому краю)
                AddRightAlignedParagraph(doc, $"г. {contractDate:dd.MM.yyyy}");

                // Раздел 1 - Предмет договора
                AddHeading(doc, "1. ПРЕДМЕТ ДОГОВОРА");
                AddNormalParagraph(doc, $"1.1. Продавец обязуется передать в собственность Покупателя, а Покупатель обязуется принять и оплатить следующий объект недвижимости: {propertyDescription}.");

                // Раздел 2 - Стороны договора
                AddHeading(doc, "2. СТОРОНЫ ДОГОВОРА");
                AddNormalParagraph(doc, $"2.1. Продавец: {seller}.");
                AddNormalParagraph(doc, $"2.2. Покупатель: {buyer}.");

                // Раздел 3 - Цена и порядок расчетов
                AddHeading(doc, "3. ЦЕНА И ПОРЯДОК РАСЧЕТОВ");
                AddNormalParagraph(doc, $"3.1. Цена объекта недвижимости составляет {price:N2} ({"рублей".ToUpper()}).");
                AddNormalParagraph(doc, "3.2. Оплата производится в полном объеме в течение 5 банковских дней с момента подписания настоящего договора.");

                // Раздел 4 - Дополнительные условия (если есть)
                if (!string.IsNullOrWhiteSpace(additionalTerms))
                {
                    AddHeading(doc, "4. ДОПОЛНИТЕЛЬНЫЕ УСЛОВИЯ");
                    AddNormalParagraph(doc, additionalTerms);
                }

                // Раздел 5 - Подписи сторон
                AddHeading(doc, "5. ПОДПИСИ СТОРОН");
                AddSignaturesTable(doc, seller, buyer);

                // Сохраняем документ
                doc.SaveAs2(filePath, WdSaveFormat.wdFormatDocumentDefault);
            }
            finally
            {
                // Закрываем документ и приложение Word
                if (doc != null)
                {
                    doc.Close();
                    Marshal.ReleaseComObject(doc);
                }
                
                if (wordApp != null)
                {
                    wordApp.Quit();
                    Marshal.ReleaseComObject(wordApp);
                }
            }
        }

        public static void generateTransferDocument(
            string filePath,
            string seller,
            string buyer,
            string propertyDescription,
            DateTime contractDate,
            DateTime actDate,
            string propertyCondition = "удовлетворительное",
            string additionalTerms = "")
        {
            Application wordApp = null;
            Document doc = null;

            try
            {
                wordApp = new Application { Visible = false };
                doc = wordApp.Documents.Add();

                // Настройка документа
                doc.PageSetup.Orientation = WdOrientation.wdOrientPortrait;
                doc.PageSetup.TopMargin = wordApp.CentimetersToPoints(2.5f);
                doc.PageSetup.BottomMargin = wordApp.CentimetersToPoints(2.5f);
                doc.PageSetup.LeftMargin = wordApp.CentimetersToPoints(3f);
                doc.PageSetup.RightMargin = wordApp.CentimetersToPoints(1.5f);

                // Добавляем заголовок
                AddTitle(doc, "АКТ ПРИЕМА-ПЕРЕДАЧИ НЕДВИЖИМОСТИ");

                // Добавляем дату (выравнивание по правому краю)
                AddRightAlignedParagraph(doc, $"г. {actDate:dd.MM.yyyy}");

                // Преамбула
                AddNormalParagraph(doc, $"Мы, нижеподписавшиеся:");
                AddNormalParagraph(doc, $"Продавец: {seller}");
                AddNormalParagraph(doc, $"Покупатель: {buyer}");
                AddNormalParagraph(doc, $"составили настоящий акт о нижеследующем:");

                // Раздел 1 - Предмет акта
                AddHeading(doc, "1. ПРЕДМЕТ АКТА");
                AddNormalParagraph(doc, $"1.1. Продавец передал, а Покупатель принял объект недвижимости по адресу: {propertyDescription}.");
                AddNormalParagraph(doc, $"1.2. Указанный объект недвижимости был продан на основании договора купли-продажи от {contractDate:dd.MM.yyyy} года.");

                // Раздел 2 - Состояние объекта
                AddHeading(doc, "2. СОСТОЯНИЕ ОБЪЕКТА");
                AddNormalParagraph(doc, $"2.1. На момент передачи объект недвижимости находится в {propertyCondition} состоянии.");
                AddNormalParagraph(doc, "2.2. Коммуникации и инженерные системы функционируют нормально.");

                // Раздел 3 - Дополнительные условия (если есть)
                if (!string.IsNullOrWhiteSpace(additionalTerms))
                {
                    AddHeading(doc, "3. ДОПОЛНИТЕЛЬНЫЕ УСЛОВИЯ");
                    AddNormalParagraph(doc, additionalTerms);
                }

                // Раздел 4 - Заключение
                AddHeading(doc, "4. ЗАКЛЮЧЕНИЕ");
                AddNormalParagraph(doc, "4.1. Покупатель претензий к состоянию передаваемого объекта недвижимости не имеет.");
                AddNormalParagraph(doc, "4.2. Настоящий акт составлен в двух экземплярах, имеющих одинаковую юридическую силу.");

                // Подписи сторон
                AddHeading(doc, "ПОДПИСИ СТОРОН");
                AddSignaturesTable(doc, seller, buyer);

                // Сохраняем документ
                doc.SaveAs2(filePath, WdSaveFormat.wdFormatDocumentDefault);
            }
            finally
            {
                // Закрываем документ и приложение Word
                if (doc != null)
                {
                    doc.Close();
                    Marshal.ReleaseComObject(doc);
                }

                if (wordApp != null)
                {
                    wordApp.Quit();
                    Marshal.ReleaseComObject(wordApp);
                }
            }
        }


        private static void AddTitle(Document doc, string text)
        {
            Paragraph title = doc.Content.Paragraphs.Add();
            title.Range.Text = text;
            title.Range.Font.Name = "Arial";
            title.Range.Font.Size = 16;
            title.Range.Font.Bold = 1;
            title.Format.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            title.Range.InsertParagraphAfter();
        }

        private static void AddHeading(Document doc, string text)
        {
            Paragraph heading = doc.Content.Paragraphs.Add();
            heading.Range.Text = text;
            heading.Range.Font.Name = "Arial";
            heading.Range.Font.Size = 14;
            heading.Range.Font.Bold = 1;
            heading.Format.SpaceAfter = 6;
            heading.Format.SpaceBefore = 12;
            heading.Range.InsertParagraphAfter();
        }

        private static void AddNormalParagraph(Document doc, string text)
        {
            Paragraph paragraph = doc.Content.Paragraphs.Add();
            paragraph.Range.Text = text;
            paragraph.Range.Font.Name = "Times New Roman";
            paragraph.Range.Font.Size = 12;
            paragraph.Format.SpaceAfter = 6;
            paragraph.Format.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            paragraph.Range.InsertParagraphAfter();
        }

        private static void AddRightAlignedParagraph(Document doc, string text)
        {
            Paragraph paragraph = doc.Content.Paragraphs.Add();
            paragraph.Range.Text = text;
            paragraph.Range.Font.Name = "Times New Roman";
            paragraph.Range.Font.Size = 12;
            paragraph.Format.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
            paragraph.Range.InsertParagraphAfter();
        }

        private static void AddSignaturesTable(Document doc, string seller, string buyer)
        {
            // Создаем таблицу 2x4 (2 колонки, 4 строки)
            Table table = doc.Tables.Add(
                doc.Content.Paragraphs[doc.Content.Paragraphs.Count].Range,
                NumRows: 4,
                NumColumns: 2);

            // Настройка таблицы
            table.Borders.Enable = 1;
            table.AllowAutoFit = true;
            table.Range.ParagraphFormat.SpaceAfter = 0;

            // Заполняем таблицу
            table.Cell(1, 1).Range.Text = "Продавец:";
            table.Cell(1, 2).Range.Text = "Покупатель:";

            table.Cell(2, 1).Range.Text = seller;
            table.Cell(2, 2).Range.Text = buyer;

            table.Cell(3, 1).Range.Text = "_________________________";
            table.Cell(3, 2).Range.Text = "_________________________";

            table.Cell(4, 1).Range.Text = "(подпись)";
            table.Cell(4, 2).Range.Text = "(подпись)";

            // Форматирование таблицы
            foreach (Row row in table.Rows)
            {
                foreach (Cell cell in row.Cells)
                {
                    cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    cell.Range.Font.Name = "Times New Roman";
                    cell.Range.Font.Size = 12;
                }
            }
        }
    }
}
