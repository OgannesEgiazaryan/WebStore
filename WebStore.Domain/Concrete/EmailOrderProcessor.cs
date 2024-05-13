using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;
using WebStore.Domain.Concrete;

namespace WebStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "orders@example.com";
        public string MailFromAddress = "gamestore@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\web_store_emails";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        private EFDbContext context = new EFDbContext();

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            foreach (var line in cart.Lines)
            {
                var order = new Orders
                {
                    Order_Name = shippingInfo.Name,
                    Order_Email = shippingInfo.Line1,
                    Order_Soft_ID = line.Soft.ID_SoftWare,
                    Order_Quantity = line.Quantity
                };

                context.Order.Add(order);
            }

            context.SaveChanges();

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod
                        = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Новый заказ обработан")
                    .AppendLine("---")
                    .AppendLine("Товары:");

                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                StringBuilder result = new StringBuilder(16);

                for (int i = 0; i < 16; i++)
                {
                    if (i > 0 && i % 4 == 0)
                        result.Append('-');
                    result.Append(chars[random.Next(chars.Length)]);
                }

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Soft.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (итого: {2:c}",
                        line.Quantity, line.Soft.Name, subtotal);
                    for (int i = 0; i < line.Quantity; i++)
                    {
                        body.AppendLine($"\nКлюч для активации {i + 1}: {result}");
                    }
                    body.AppendLine("\n---");
                }
                DateTime Date = DateTime.Now;
                body.AppendFormat("\nОбщая стоимость: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("\n---")
                    .AppendLine("Покупка:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine("Дата:")
                    .AppendLine(Date.ToString())
                    //.AppendLine(shippingInfo.Line1)
                    //.AppendLine(shippingInfo.Line2 ?? "")
                    //.AppendLine(shippingInfo.Line3 ?? "")
                    //.AppendLine(shippingInfo.City)
                    //.AppendLine(shippingInfo.Country)
                    .AppendLine("---")
                    .AppendLine("Электронный кассовый чек:")
                    .AppendLine("ФП Д999999")
                    .AppendLine("ФД 999999")
                    .AppendLine("ФПК 999999")
                    .AppendLine("ИНН 777777777777")
                    .AppendLine("Спасибо за покупку!");

                //.AppendFormat("Подарочная упаковка: {0}",
                //    shippingInfo.GiftWrap ? "Да" : "Нет");

                MailAddress from = new MailAddress("workcollege111222@gmail.com", "SoftClub"); //пароль к gmail *MqMHasKjdA*
                MailAddress to = new MailAddress(shippingInfo.Line1);
                MailMessage mailMessage = new MailMessage(from, to);
                mailMessage.Subject = "Поздравляем с покупкой приложения в магазине WebStore";
                mailMessage.Body = body.ToString();
                mailMessage.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(from.Address, "ygut vtsf okwn ylfu");

                //MailMessage mailMessage = new MailMessage(
                //                       emailSettings.MailFromAddress,	// От кого
                //                       emailSettings.Line1,		// Кому
                //                       "Новый заказ отправлен!",		// Тема
                //                       body.ToString()); 				// Тело письма

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                //smtpClient.Send(mailMessage);
                smtp.Send(mailMessage);
            }
        }
    }
}