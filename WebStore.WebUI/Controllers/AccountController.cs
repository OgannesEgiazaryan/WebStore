using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebStore.Domain.Entities;
using WebStore.WebUI.Infrastructure.Abstract;
using WebStore.WebUI.Models;
using System.Text;
using WebStore.Domain.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.WebUI.Controllers
{
    public class AccountController : Controller
    {

        EFDbContext _context = new EFDbContext();


        IAuthProvider authProvider;
        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин или пароль");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult History()
        {
            return View();
        }



        public ActionResult Main()
        {
            var sliderItems = _context.App.Take(5).ToList();
            var rightColumnItems = _context.App.Where(s => s.ID_Event == 3).ToList();
            var topSelles = _context.App.Where(t => t.ID_Event == 4).ToList();
            //var twoBlock = _context.App.Take(2).ToList();
            var twoBlock = _context.App.OrderByDescending(t => t.ID_SoftWare).Take(2).ToList();

            // Add logging or breakpoints here to inspect sliderItems and rightColumnItems

            var viewModel = new SoftsListViewModel
            {
                SliderItems = sliderItems,
                RightColumnItems = rightColumnItems,
                TopSelles = topSelles,
                TwoBlocks = twoBlock
            };

            return View(viewModel);
        }

        // Действие для отправки истории покупок на почту
        [HttpPost]
        public ActionResult SendPurchaseHistory(ShippingDetails2 model)
        {
            // Ваша логика отправки письма с историей покупок
            if (ModelState.IsValid)
            {
                // Логика для отправки письма на почту model.Email
                SendEmailWithPurchaseHistory(model.Line1);

                // Редирект на другую страницу после успешной отправки
                return RedirectToAction("HistorySent");
            }

            // Если модель не валидна, возвращаем ту же страницу с ошибками
            return View("History", model);
        }
        private void SendEmailWithPurchaseHistory(string email)
        {
            // Получаем заказы по заданной почте
            List<Orders> orders = GetOrdersByEmail(email);

            // Если нет заказов, можно отправить сообщение о том, что нет покупок
            if (orders.Count == 0)
            {
                SendNoPurchasesEmail(email);
                return;
            }

            StringBuilder body = new StringBuilder();
            body.AppendLine("Информация о ваших покупках:");

            foreach (var order in orders)
            {
                body.AppendLine($"ID программы: {order.Software.Name}");
                body.AppendLine($"Дата заказа: {order.Order_Date}");
                body.AppendLine($"Дата заказа: {order.Order_Key}");
                body.AppendLine("------------------------------------");
            }

            MailAddress from = new MailAddress("workcollege111222@gmail.com", "SoftClub");
            MailAddress to = new MailAddress(email);
            MailMessage mailMessage = new MailMessage(from, to);
            mailMessage.Subject = "История покупок приложений в магазине WebStore";
            mailMessage.Body = body.ToString();
            mailMessage.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(from.Address, "ygut vtsf okwn ylfu"); // ваш пароль для доступа к Gmail

            try
            {
                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            {
                // Обработка ошибок при отправке письма
                throw ex;
            }
        }

        // Метод для получения заказов по электронной почте
        private List<Orders> GetOrdersByEmail(string email)
        {
         
                return _context.Order.Where(o => o.Order_Email == email).ToList();
           
        }

        // Метод для отправки письма, если нет покупок
        private void SendNoPurchasesEmail(string email)
        {
            MailAddress from = new MailAddress("workcollege111222@gmail.com", "SoftClub");
            MailAddress to = new MailAddress(email);
            MailMessage mailMessage = new MailMessage(from, to);
            mailMessage.Subject = "Информация о покупках";
            mailMessage.Body = "У вас пока нет совершенных покупок.";
            mailMessage.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(from.Address, "ygut vtsf okwn ylfu"); // ваш пароль для доступа к Gmail

            try
            {
                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            {
                // Обработка ошибок при отправке письма
                throw ex;
            }
        }

        // Действие для отображения страницы после успешной отправки
        public ActionResult HistorySent()
        {
            return View();
        }
    }
}