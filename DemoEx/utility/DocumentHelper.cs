using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using DB;
using System.Windows.Forms;

namespace DemoEx.data
{   
    public static class DocumentHelper
    {
        private static Db db = new Db();

        public static void createPurchaseDocument(int dealId, int ownerId, int estateId)
        {
            db.setConnectionStr(Connection.connectionString);
            string type = db.getValuesFromColumn($"select type from deals where id={dealId};")[0];

            var clientId = db.getIntValuesFromColumn($"select client from deals where id={dealId};")[0];

            var clientFIO = db.getValuesFromColumn($"select CONCAT(surname, ' ', name, ' ',patronymic) from clients where id={clientId};")[0].ToString();
            var ownerFIO = db.getValuesFromColumn($"select CONCAT(surname, ' ', name, ' ',patronymic) from clients where id={ownerId};")[0].ToString();
            var clientPassport = db.getValuesFromColumn($"select passport from clients where id={clientId};")[0].ToString();
            var ownerPassport = db.getValuesFromColumn($"select passport from clients where id={ownerId};")[0].ToString();
            var clientBirth = db.getDateValuesFromColumn($"select birth from clients where id={clientId};")[0];
            var ownerBirth = db.getDateValuesFromColumn($"select birth from clients where id={ownerId};")[0];
            var clientAddress = db.getValuesFromColumn($"select address from clients where id={clientId};")[0].ToString();
            var ownerAddress = db.getValuesFromColumn($"select address from clients where id={ownerId};")[0].ToString();

            var estateAddress = db.getValuesFromColumn($"select address from estate where id={estateId};")[0].ToString();
            var estateCadastral = db.getValuesFromColumn($"select cadastral from estate where id={estateId};")[0].ToString();
            var estateRooms = db.getIntValuesFromColumn($"select rooms from estate where id={estateId};")[0];
            var estateSquare = db.getIntValuesFromColumn($"select square from estate where id={estateId};")[0];
            var estatePrice = db.getIntValuesFromColumn($"select price from estate where id={estateId};")[0];

            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            Document wordDoc = wordApp.Documents.Add();
            wordApp.Visible = true;


            Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
            titleParagraph.Range.Text = "Договор купли-продажи квартиры";
            titleParagraph.Range.Font.Name = "Times New Roman";
            titleParagraph.Range.Font.Size = 24;
            titleParagraph.Range.Font.Bold = 1;
            titleParagraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            titleParagraph.Range.InsertParagraphAfter();

            Paragraph bodyParagraph = wordDoc.Content.Paragraphs.Add();
            bodyParagraph.Range.Text = $"{ownerFIO} (Ф.И.О), паспорт серии {ownerPassport.Split(' ')[0]} № {ownerPassport.Split(' ')[1]}, дата рождения {ownerBirth.ToString("D")}, зарегистрирован по адресу: {ownerAddress}, именуемый в дальнейшем \"Продавец\", с одной стороны и {clientFIO} (Ф.И.О), паспорт серии {clientPassport.Split(' ')[0]} № {clientPassport.Split(' ')[1]}, дата рождения {clientBirth.ToString("D")}, зарегистрирован по адресу: {clientAddress}, именуемый в дальнейшем \"Покупатель\", с другой стороны, далее совместно именуемые \"Стороны\", заключили настоящий договор о нижеследующем: ";
            bodyParagraph.Range.Font.Name = "Times New Roman";
            bodyParagraph.Range.Font.Size = 16;
            bodyParagraph.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph.Range.Font.Bold = 0;
            bodyParagraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph.Range.InsertParagraphAfter();

            Paragraph subjectTitleParagraph = wordDoc.Content.Paragraphs.Add();
            subjectTitleParagraph.Range.Text = "1. Предмет договора";
            subjectTitleParagraph.Range.Font.Name = "Times New Roman";
            subjectTitleParagraph.Range.Font.Size = 20;
            subjectTitleParagraph.Range.Font.Bold = 1;
            subjectTitleParagraph.Range.ParagraphFormat.SpaceBefore = 24;
            subjectTitleParagraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            subjectTitleParagraph.Range.InsertParagraphAfter();

            Paragraph bodyParagraph2 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph2.Range.Text = $"1.1 ПРОДАВЕЦ продает, а ПОКУПАТЕЛЬ покупает в собственность за цену и на условиях установленных настоящим Договором, Квартиру (в дальнейшем именуемую \"Квартира\"), имеющую кадастровый номер {estateCadastral}, находящуюся на этаже многоквартирного жилого дома.";
            bodyParagraph2.Range.Font.Name = "Times New Roman";
            bodyParagraph2.Range.Font.Size = 16;
            bodyParagraph2.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph2.Range.Font.Bold = 0;
            bodyParagraph2.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph2.Range.InsertParagraphAfter();

            Paragraph bodyParagraph3 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph3.Range.Text = $"Квартира расположена по адресу: {estateAddress}.";
            bodyParagraph3.Range.Font.Name = "Times New Roman";
            bodyParagraph3.Range.Font.Size = 16;
            bodyParagraph3.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph3.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph3.Range.InsertParagraphAfter();

            Paragraph bodyParagraph4 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph4.Range.Text = $"Квартира имеет следующие характеристики: Квартира состоит из {estateRooms} комнат, общая площадь Квартиры {estateSquare} кв.м.";
            bodyParagraph4.Range.Font.Name = "Times New Roman";
            bodyParagraph4.Range.Font.Size = 16;
            bodyParagraph4.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph4.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph4.Range.InsertParagraphAfter();

            Paragraph subjectTitleParagraph2 = wordDoc.Content.Paragraphs.Add();
            subjectTitleParagraph2.Range.Text = "2. Передача квартиры";
            subjectTitleParagraph2.Range.Font.Name = "Times New Roman";
            subjectTitleParagraph2.Range.Font.Size = 20;
            subjectTitleParagraph2.Range.Font.Bold = 1;
            subjectTitleParagraph2.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            subjectTitleParagraph2.Range.InsertParagraphAfter();

            Paragraph bodyParagraph5 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph5.Range.Text = $"2.1 Переход права собственности на Квартиру от Продавца к Покупателю подлежит государственной регистрации в Едином государственном реестре недвижимости в порядке, установленном законодательством Российской Федерации. Покупателем становится собственником Квартиры с момента государственной регистрации.";
            bodyParagraph5.Range.Font.Name = "Times New Roman";
            bodyParagraph5.Range.Font.Size = 16;
            bodyParagraph5.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph5.Range.Font.Bold = 0;
            bodyParagraph5.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph5.Range.InsertParagraphAfter();

            Paragraph bodyParagraph6 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph6.Range.Text = $"2.2 Квартира должна быть передана Продавцом в фактическое владение Покупателя в течение 7 календарных дней с момента заключения настоящего Договора.";
            bodyParagraph6.Range.Font.Name = "Times New Roman";
            bodyParagraph6.Range.Font.Size = 16;
            bodyParagraph6.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph6.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph6.Range.InsertParagraphAfter();

            Paragraph bodyParagraph7 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph7.Range.Text = $"При передаче Квартиры Продавец обязан передать Покупателю также всю имеющуюся техническую и иную документацию на Квартиру и находящееся в ней оборудование, а также документацию и предметы, связанные с владением, эксплуатацией и использованием Квартиры (ключи, документы и т.п.)";
            bodyParagraph7.Range.Font.Name = "Times New Roman";
            bodyParagraph7.Range.Font.Size = 16;
            bodyParagraph7.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph7.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph7.Range.InsertParagraphAfter();

            Paragraph bodyParagraph8 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph8.Range.Text = $"Передача Квартиры оформляется Актом приема-передачи.";
            bodyParagraph8.Range.Font.Name = "Times New Roman";
            bodyParagraph8.Range.Font.Size = 16;
            bodyParagraph8.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph8.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph8.Range.InsertParagraphAfter();

            Paragraph subjectTitleParagraph3 = wordDoc.Content.Paragraphs.Add();
            subjectTitleParagraph3.Range.Text = "3. Цена Квартиры. Порядок расчетов.";
            subjectTitleParagraph3.Range.Font.Name = "Times New Roman";
            subjectTitleParagraph3.Range.Font.Size = 20;
            subjectTitleParagraph3.Range.Font.Bold = 1;
            subjectTitleParagraph3.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            subjectTitleParagraph3.Range.InsertParagraphAfter();

            Paragraph bodyParagraph9 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph9.Range.Text = $"3.1 Стороны пришли к соглашению, что цена, за которую Квартира продается и которую Покупатель обязан уплатить Продавцу, составляет {estatePrice} рублей.";
            bodyParagraph9.Range.Font.Name = "Times New Roman";
            bodyParagraph9.Range.Font.Size = 16;
            bodyParagraph9.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph9.Range.Font.Bold = 0;
            bodyParagraph9.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph9.Range.InsertParagraphAfter();

            Paragraph bodyParagraph10 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph10.Range.Text = $"3.2 В подтверждение получения денежных средств в сумме, указанной в п. 3.2 настоящего Договора, Продавец передает Покупателю расписку в получении соответствующей суммы. ";
            bodyParagraph10.Range.Font.Name = "Times New Roman";
            bodyParagraph10.Range.Font.Size = 16;
            bodyParagraph10.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph10.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph10.Range.InsertParagraphAfter();

            Paragraph subjectTitleParagraph4 = wordDoc.Content.Paragraphs.Add();
            subjectTitleParagraph4.Range.Text = "4. Заверения Сторон.";
            subjectTitleParagraph4.Range.Font.Name = "Times New Roman";
            subjectTitleParagraph4.Range.Font.Size = 20;
            subjectTitleParagraph4.Range.Font.Bold = 1;
            subjectTitleParagraph4.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            subjectTitleParagraph4.Range.InsertParagraphAfter();

            Paragraph bodyParagraph11 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph11.Range.Font.Bold = 0;
            bodyParagraph11.Range.Font.Size = 16;
            bodyParagraph11.Range.Text = $"4.1. Продавец гарантирует и заверяет, что: ";
            bodyParagraph11.Range.Font.Name = "Times New Roman";
            bodyParagraph11.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph11.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph11.Range.InsertParagraphAfter();

            Paragraph bodyParagraph12 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph12.Range.Font.Size = 16;
            bodyParagraph12.Range.Font.Bold = 0;
            bodyParagraph12.Range.Text = $"4.1.1. Квартира принадлежит Продавцу на праве собственности.";
            bodyParagraph12.Range.Font.Name = "Times New Roman";
            bodyParagraph12.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph11.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph12.Range.InsertParagraphAfter();

            Paragraph bodyParagraph13 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph13.Range.Font.Size = 16;
            bodyParagraph13.Range.Font.Bold = 0;
            bodyParagraph13.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph13.Range.Text = $"4.1.2. Квартира не обременена правами других лиц, в залоге, в споре, под арестом или под запретом не находится, не продана и не обещана быть проданной третьим лицам, не имеет каких-либо иных обременений.";
            bodyParagraph13.Range.Font.Name = "Times New Roman";
            bodyParagraph13.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph13.Range.InsertParagraphAfter();

            Paragraph bodyParagraph14 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph14.Range.Font.Size = 16;
            bodyParagraph14.Range.Font.Bold = 0;
            bodyParagraph14.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph14.Range.Text = $"4.1.3. Квартира не имеет существенных недостатков или скрытых дефектов, которые могут в значительной степени повлиять на возможность пользования Квартирой и на ее эксплуатационные характеристики.";
            bodyParagraph14.Range.Font.Name = "Times New Roman";
            bodyParagraph14.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph14.Range.InsertParagraphAfter();

            Paragraph bodyParagraph15 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph15.Range.Font.Size = 16;
            bodyParagraph15.Range.Font.Bold = 0;
            bodyParagraph15.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph15.Range.Text = $"4.1.4. Предоставленные Продавцом Выписка из Единого государственного реестра недвижимости, а также иные документы и информация являются достоверными.";
            bodyParagraph15.Range.Font.Name = "Times New Roman";
            bodyParagraph15.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph15.Range.InsertParagraphAfter();

            Paragraph bodyParagraph16 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph16.Range.Font.Size = 16;
            bodyParagraph16.Range.Font.Bold = 0;
            bodyParagraph16.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph16.Range.Text = $"4.2. Покупатель гарантирует и заверяет, что: Покупатель удовлетворен состоянием Квартиры, каких либо дефектов и недостатков, о которых Покупателю не было сообщено, Покупателем не обнаружено.\r\nНа основании изложенного в настоящем пункте Договора Покупатель принял решение о приобретении Квартиры на условиях, установленных настоящим Договором.";
            bodyParagraph16.Range.Font.Name = "Times New Roman";
            bodyParagraph16.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph16.Range.InsertParagraphAfter();

            Paragraph bodyParagraph17 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph17.Range.Font.Size = 16;
            bodyParagraph17.Range.Font.Bold = 0;
            bodyParagraph17.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph17.Range.Text = $"4.2.1. До заключения настоящего Договора Покупатель визуально осмотрел Квартиру, ознакомился с ее основными конструктивными и техническими элементами и особенностями, а также с ее эксплуатационным и техническим состоянием.";
            bodyParagraph17.Range.Font.Name = "Times New Roman";
            bodyParagraph17.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph17.Range.InsertParagraphAfter();

            Paragraph bodyParagraph18 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph18.Range.Font.Size = 16;
            bodyParagraph18.Range.Font.Bold = 0;
            bodyParagraph18.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph18.Range.Text = $"Покупатель удовлетворен состоянием Квартиры, каких либо дефектов и недостатков, о которых Покупателю не было сообщено, Покупателем не обнаружено. ";
            bodyParagraph18.Range.Font.Name = "Times New Roman";
            bodyParagraph18.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph18.Range.InsertParagraphAfter();

            Paragraph bodyParagraph19 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph19.Range.Font.Size = 16;
            bodyParagraph19.Range.Font.Bold = 0;
            bodyParagraph19.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph19.Range.Text = $"На основании изложенного в настоящем пункте Договора Покупатель принял решение о приобретении Квартиры на условиях, установленных настоящим Договором.";
            bodyParagraph19.Range.Font.Name = "Times New Roman";
            bodyParagraph19.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph19.Range.InsertParagraphAfter();


            wordDoc.Close();
            wordApp.Quit();
        }

        public static void createRentDocument(int dealId, int ownerId, int estateId)
        {
            db.setConnectionStr(Connection.connectionString);
            string type = db.getValuesFromColumn($"select type from deals where id={dealId};")[0];

            var clientId = db.getIntValuesFromColumn($"select client from deals where id={dealId};")[0];

            var clientFIO = db.getValuesFromColumn($"select CONCAT(surname, ' ', name, ' ',patronymic) from clients where id={clientId};")[0].ToString();
            var ownerFIO = db.getValuesFromColumn($"select CONCAT(surname, ' ', name, ' ',patronymic) from clients where id={ownerId};")[0].ToString();
            var clientPassport = db.getValuesFromColumn($"select passport from clients where id={clientId};")[0].ToString();
            var ownerPassport = db.getValuesFromColumn($"select passport from clients where id={ownerId};")[0].ToString();
            var clientBirth = db.getDateValuesFromColumn($"select birth from clients where id={clientId};")[0];
            var ownerBirth = db.getDateValuesFromColumn($"select birth from clients where id={ownerId};")[0];
            var clientAddress = db.getValuesFromColumn($"select address from clients where id={clientId};")[0].ToString();
            var ownerAddress = db.getValuesFromColumn($"select address from clients where id={ownerId};")[0].ToString();

            var estateAddress = db.getValuesFromColumn($"select address from estate where id={estateId};")[0].ToString();
            var estateCadastral = db.getValuesFromColumn($"select cadastral from estate where id={estateId};")[0].ToString();
            var estateRooms = db.getIntValuesFromColumn($"select rooms from estate where id={estateId};")[0];
            var estateSquare = db.getIntValuesFromColumn($"select square from estate where id={estateId};")[0];
            var estatePrice = db.getIntValuesFromColumn($"select price from estate where id={estateId};")[0];

            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            Document wordDoc = wordApp.Documents.Add();
            wordApp.Visible = true;

            Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
            titleParagraph.Range.Text = "Договор аренды жилого помещения";
            titleParagraph.Range.Font.Name = "Times New Roman";
            titleParagraph.Range.Font.Size = 24;
            titleParagraph.Range.Font.Bold = 1;
            titleParagraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            titleParagraph.Range.InsertParagraphAfter();

            Paragraph bodyParagraph = wordDoc.Content.Paragraphs.Add();
            bodyParagraph.Range.Text = $"{ownerFIO} (Ф.И.О), паспорт серии {ownerPassport.Split(' ')[0]} № {ownerPassport.Split(' ')[1]}, дата рождения {ownerBirth.ToString("D")}, зарегистрирован по адресу: {ownerAddress}, именуемый в дальнейшем \"Арендодатель\", с одной стороны и {clientFIO} (Ф.И.О), паспорт серии {clientPassport.Split(' ')[0]} № {clientPassport.Split(' ')[1]}, дата рождения {clientBirth.ToString("D")}, зарегистрирован по адресу: {clientAddress}, именуемый в дальнейшем \"Арендатель\", с другой стороны, далее совместно именуемые \"Стороны\", заключили настоящий договор о нижеследующем: ";
            bodyParagraph.Range.Font.Name = "Times New Roman";
            bodyParagraph.Range.Font.Size = 16;
            bodyParagraph.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph.Range.Font.Bold = 0;
            bodyParagraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph.Range.InsertParagraphAfter();

            Paragraph subjectTitleParagraph2 = wordDoc.Content.Paragraphs.Add();
            subjectTitleParagraph2.Range.Text = "1. Предмет договора. ";
            subjectTitleParagraph2.Range.Font.Name = "Times New Roman";
            subjectTitleParagraph2.Range.Font.Size = 20;
            subjectTitleParagraph2.Range.Font.Bold = 1;
            subjectTitleParagraph2.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            subjectTitleParagraph2.Range.InsertParagraphAfter();

            Paragraph bodyParagraph1 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph1.Range.Text = $"1.1. Арендодатель предоставляет Арендатору за плату во временное владение и пользование жилое помещение, далее именуемое «Жилое помещение», на условиях настоящего Договора.";
            bodyParagraph1.Range.Text = $"Передаваемое во временное владение и пользование Жилое помещение расположено по адресу: {estateAddress} и имеет кадастровый номер {estateCadastral}.";
            bodyParagraph1.Range.Font.Name = "Times New Roman";
            bodyParagraph1.Range.Font.Size = 16;
            bodyParagraph1.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph1.Range.Font.Bold = 0;
            bodyParagraph1.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph1.Range.InsertParagraphAfter();

            Paragraph bodyParagraph2 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph2.Range.Font.Size = 16;
            bodyParagraph2.Range.Font.Bold = 0;
            bodyParagraph2.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph2.Range.Text = $"Жилое помещение имеет следующие характеристики: Жилое помещение состоит из {estateRooms} комнат, общая площадь Жилого помещения {estateSquare} кв. м";
            bodyParagraph2.Range.Font.Name = "Times New Roman";
            bodyParagraph2.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph2.Range.InsertParagraphAfter();

            Paragraph bodyParagraph3 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph3.Range.Font.Size = 16;
            bodyParagraph3.Range.Font.Bold = 0;
            bodyParagraph3.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph3.Range.Text = $"1.2. Жилое помещение не обременено правами других лиц, в залоге, в споре, под арестом или под запретом не находится, не имеет каких-либо иных обременений.";
            bodyParagraph3.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph3.Range.InsertParagraphAfter();

            Paragraph subjectTitleParagraph3 = wordDoc.Content.Paragraphs.Add();
            subjectTitleParagraph3.Range.Text = "2. Обязанности Сторон.";
            subjectTitleParagraph3.Range.Font.Name = "Times New Roman";
            subjectTitleParagraph3.Range.Font.Size = 20;
            subjectTitleParagraph3.Range.Font.Bold = 1;
            subjectTitleParagraph3.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            subjectTitleParagraph3.Range.InsertParagraphAfter();

            Paragraph bodyParagraph4 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph4.Range.Font.Size = 16;
            bodyParagraph4.Range.Font.Bold = 0;
            bodyParagraph4.Range.Text = $"2.1. Арендодатель обязан.";
            bodyParagraph4.Range.Font.Name = "Times New Roman";
            bodyParagraph4.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph4.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph4.Range.InsertParagraphAfter();

            Paragraph bodyParagraph5= wordDoc.Content.Paragraphs.Add();
            bodyParagraph5.Range.Font.Size = 16;
            bodyParagraph5.Range.Font.Bold = 0;
            bodyParagraph5.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph5.Range.Text = $"2.1.1. В течение 7 календарных дней с момента заключения настоящего Договора передать по Акту приема-передачи Жилое помещение Арендателю.";
            bodyParagraph5.Range.Text = "Одновременно с передачей Жилого помещения передать Нанимателю имущество, находящееся в Жилом помещении, ключи от Жилого помещения, иные предметы, связанные с владением, эксплуатацией и использованием Жилого помещения, а также всю необходимую документацию на Жилое помещение и находящееся в нем оборудование, в т.ч. документацию и информацию, необходимую для осуществления коммунальных платежей.Перечень имущества, передаваемого Нанимателю вместе с Жилым помещением, указывается в Акте приема-передачи Жилого помещения.";
            bodyParagraph5.Range.Font.Name = "Times New Roman";
            bodyParagraph5.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph5.Range.InsertParagraphAfter();

            Paragraph bodyParagraph6 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph6.Range.Font.Size = 16;
            bodyParagraph6.Range.Font.Bold = 0;
            bodyParagraph6.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph6.Range.Text = $"2.1.2. Не препятствовать Нанимателю в правомерном использовании Жилого помещения в соответствии с условиями настоящего Договора.";
            bodyParagraph6.Range.Font.Name = "Times New Roman";
            bodyParagraph6.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph6.Range.InsertParagraphAfter();

            Paragraph bodyParagraph7 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph7.Range.Font.Size = 16;
            bodyParagraph7.Range.Font.Bold = 0;
            bodyParagraph7.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph7.Range.Text = $"2.1.3. Принять Жилое помещение от Нанимателя по Акту приема-передачи после прекращения действия настоящего Договора.";
            bodyParagraph7.Range.Font.Name = "Times New Roman";
            bodyParagraph7.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph7.Range.InsertParagraphAfter();

            Paragraph bodyParagraph8 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph8.Range.Font.Size = 16;
            bodyParagraph8.Range.Font.Bold = 0;
            bodyParagraph8.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph8.Range.Text = $"2.1.4. Выполнять иные обязанности, вытекающие из настоящего Договора, при этом действовать разумно и добросовестно в целях обеспечения достижения результатов, ожидаемых Сторонами при заключении настоящего Договора.";
            bodyParagraph8.Range.Font.Name = "Times New Roman";
            bodyParagraph8.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph8.Range.InsertParagraphAfter();

            Paragraph bodyParagraph9 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph9.Range.Font.Size = 16;
            bodyParagraph9.Range.Font.Bold = 0;
            bodyParagraph9.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph9.Range.Text = $"2.2. Арендатель обязан.";
            bodyParagraph9.Range.Font.Name = "Times New Roman";
            bodyParagraph9.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph9.Range.InsertParagraphAfter();

            Paragraph bodyParagraph10 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph10.Range.Font.Size = 16;
            bodyParagraph10.Range.Font.Bold = 0;
            bodyParagraph10.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph10.Range.Text = $"2.2.1. В течение 7 календарных дней с момента заключения настоящего Договора принять от Наймодателя по Акту приема-передачи Жилое помещение, имущество и документацию.";
            bodyParagraph10.Range.Font.Name = "Times New Roman";
            bodyParagraph10.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph10.Range.InsertParagraphAfter();

            Paragraph bodyParagraph11 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph11.Range.Font.Size = 16;
            bodyParagraph11.Range.Font.Bold = 0;
            bodyParagraph11.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph11.Range.Text = $"2.2.2. Своевременно и в полном объеме вносить плату за пользование Жилым помещением, за коммунальные и прочие услуги, платежи в счет долевого участия в расходах по содержанию дома и придомовой территории, а также осуществлять другие платежи в соответствии с условиями настоящего Договора, дополнительных соглашений и приложений к нему.";
            bodyParagraph11.Range.Font.Name = "Times New Roman";
            bodyParagraph11.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph11.Range.InsertParagraphAfter();

            Paragraph bodyParagraph12 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph12.Range.Font.Size = 16;
            bodyParagraph12.Range.Font.Bold = 0;
            bodyParagraph12.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph12.Range.Text = $"2.2.3. Обеспечить сохранность Жилого помещения и находящегося в нем имущества и оборудования. Не допускать их разрушения или повреждения.";
            bodyParagraph12.Range.Font.Name = "Times New Roman";
            bodyParagraph12.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph12.Range.InsertParagraphAfter();

            Paragraph bodyParagraph13 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph13.Range.Font.Size = 16;
            bodyParagraph13.Range.Font.Bold = 0;
            bodyParagraph13.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph13.Range.Text = $"2.2.4. Соблюдать правила проживания в жилых помещениях, правила пожарной безопасности, санитарно-технических норм, требования по охране окружающей среды, правила эксплуатации установленного в Жилом помещении санитарно-технического и инженерного оборудования.Не допускать нарушения прав и законных интересов соседей.";
            bodyParagraph13.Range.Font.Name = "Times New Roman";
            bodyParagraph13.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph13.Range.InsertParagraphAfter();

            Paragraph bodyParagraph14 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph14.Range.Font.Size = 16;
            bodyParagraph14.Range.Font.Bold = 0;
            bodyParagraph14.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph14.Range.Text = $"2.2.5. Предоставлять возможность Наймодателю или уполномоченным им лицам, а также представителям городских служб проверять техническое состояние Жилого помещения и расположенного в нем оборудования, а также соблюдение условий его использования.";
            bodyParagraph14.Range.Font.Name = "Times New Roman";
            bodyParagraph14.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph14.Range.InsertParagraphAfter();

            Paragraph bodyParagraph15 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph15.Range.Font.Size = 16;
            bodyParagraph15.Range.Font.Bold = 0;
            bodyParagraph15.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph15.Range.Text = $"2.2.6. Освободить Помещение и передать его Арендодателю по Акту приема-передачи в течение _____________________________ после истечения срока пользования Жилым помещением, или в течение ____________________________ с момента досрочного расторжения настоящего Договора.";
            bodyParagraph15.Range.Font.Name = "Times New Roman";
            bodyParagraph15.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph15.Range.InsertParagraphAfter();

            Paragraph bodyParagraph16 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph16.Range.Font.Size = 16;
            bodyParagraph16.Range.Font.Bold = 0;
            bodyParagraph16.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph16.Range.Text = $"Жилое помещение передается вместе с имуществом, которое находилось в Жилом помещении в момент его принятия Арендателем, и указанном в Акте приема-передачи, а также с произведенными улучшениями, неотделимыми без вреда для конструкций Жилого помещения.";
            bodyParagraph16.Range.Font.Name = "Times New Roman";
            bodyParagraph16.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph16.Range.InsertParagraphAfter();

            Paragraph bodyParagraph17 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph17.Range.Font.Size = 16;
            bodyParagraph17.Range.Font.Bold = 0;
            bodyParagraph17.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph17.Range.Text = $"До передачи (возврата) Жилого помещения Арендодателю Арендатель обязан привести Жилое помещение в первоначальное (исходное) состояние (за исключением произведенных в соответствии с условиями настоящего Договора улучшений), за свой счет провести текущий ремонт Жилого помещения и устранить все повреждения, образовавшиеся в процессе его эксплуатации, либо оплатить Арендодателю стоимость не произведенного текущего ремонта.";
            bodyParagraph17.Range.Font.Name = "Times New Roman";
            bodyParagraph17.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph17.Range.InsertParagraphAfter();

            Paragraph subjectTitleParagraph4 = wordDoc.Content.Paragraphs.Add();
            subjectTitleParagraph4.Range.Text = "3. Платежи и расчеты по договору.";
            subjectTitleParagraph4.Range.Font.Name = "Times New Roman";
            subjectTitleParagraph4.Range.Font.Size = 20;
            subjectTitleParagraph4.Range.Font.Bold = 1;
            subjectTitleParagraph4.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            subjectTitleParagraph4.Range.InsertParagraphAfter();

            Paragraph bodyParagraph18 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph18.Range.Font.Size = 16;
            bodyParagraph18.Range.Font.Bold = 0;
            bodyParagraph18.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph18.Range.Text = $"Стороны устанавливают следующий порядок расчетов между сторонами по настоящему Договору: 3.1.Ежемесячная плата за пользование Жилым помещением устанавливается в сумме {estatePrice} рублей.";
            bodyParagraph18.Range.Font.Name = "Times New Roman";
            bodyParagraph18.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph18.Range.InsertParagraphAfter();

            Paragraph bodyParagraph19 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph19.Range.Font.Size = 16;
            bodyParagraph19.Range.Font.Bold = 0;
            bodyParagraph19.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph19.Range.Text = $"3.1.Ежемесячная плата за пользование Жилым помещением устанавливается в сумме {estatePrice} рублей.";
            bodyParagraph19.Range.Font.Name = "Times New Roman";
            bodyParagraph19.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph19.Range.InsertParagraphAfter();

            Paragraph bodyParagraph20 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph20.Range.Font.Size = 16;
            bodyParagraph20.Range.Font.Bold = 0;
            bodyParagraph20.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph20.Range.Text = $"3.2. Плата за пользование Жилым помещением, установленная п. 3.1 настоящего Договора, рассчитывается с момента передачи Жилого помещения Нанимателю по Акту приема-передачи и уплачивается Нанимателем ежемесячно до ____________________ числа текущего (оплачиваемого) месяца путем передачи Нанимателем Наймодателю наличных денежных средств. Каждая передача денежных средств подтверждается распиской, которую Наймодатель передает Нанимателю.";
            bodyParagraph20.Range.Font.Name = "Times New Roman";
            bodyParagraph20.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph20.Range.InsertParagraphAfter();

            Paragraph bodyParagraph21 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph21.Range.Font.Size = 16;
            bodyParagraph21.Range.Font.Bold = 0;
            bodyParagraph21.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph21.Range.Text = $"3.3. Стоимость коммунальных услуг (электроэнергия, горячее и холодное водоснабжение, канализация, природный газ, телефон, иные коммунальные услуги и услуги, связанные с эксплуатацией и обслуживанием Жилого помещения и многоквартирного дома, в котором Жилое помещение расположено, и т.д.) не включается в стоимость платы за пользование Жилым помещением, указанную в п. 3.1 настоящего Договора и оплачивается Нанимателем дополнительно и самостоятельно на основании показаний приборов учета либо платежных документов организаций, предоставляющих указанные услуги, в порядке и в сроки, установленные требованиями нормативных правовых актов и требованиями организаций, предоставляющих соответствующие услуги.";
            bodyParagraph21.Range.Font.Name = "Times New Roman";
            bodyParagraph21.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph21.Range.InsertParagraphAfter();

            Paragraph subjectTitleParagraph5 = wordDoc.Content.Paragraphs.Add();
            subjectTitleParagraph5.Range.Text = "4. Срок владения и пользования Жилым помещением. Срок действия договора.";
            subjectTitleParagraph5.Range.Font.Name = "Times New Roman";
            subjectTitleParagraph5.Range.Font.Size = 20;
            subjectTitleParagraph5.Range.Font.Bold = 1;
            subjectTitleParagraph5.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            subjectTitleParagraph5.Range.InsertParagraphAfter();

            Paragraph bodyParagraph22 = wordDoc.Content.Paragraphs.Add();
            bodyParagraph22.Range.Font.Size = 16;
            bodyParagraph22.Range.Font.Bold = 0;
            bodyParagraph22.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            bodyParagraph22.Range.Text = $"4.1. Жилое помещение передается во временное владение и пользование Нанимателю на срок с момента передачи Жилого помещения Нанимателю по Акту приема-передачи до _________________________________ .";
            bodyParagraph22.Range.Font.Name = "Times New Roman";
            bodyParagraph22.Range.ParagraphFormat.SpaceBefore = 24;
            bodyParagraph22.Range.InsertParagraphAfter();

            wordDoc.Close();
            wordApp.Quit();
        }
    }
}
