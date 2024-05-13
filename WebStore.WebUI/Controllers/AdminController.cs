    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Domain.Abstract;
using WebStore.Domain.Concrete;
using WebStore.Domain.Entities;

namespace WebStore.WebUI.Controllers
{

    [Authorize]
    public class AdminController : Controller
    {
        IWebRepository repository;
        EFDbContext _context;

        public AdminController(IWebRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.App);
        }

        public ViewResult Index1()
        {
            return View(repository.Category);
        }

        public ViewResult Index2()
        {
            return View(repository.Event);
        }

        public ViewResult Index3()
        {
            return View(repository.Order);
        }

        public ViewResult Index4()
        {
            return View(repository.Review);
        }

        public ViewResult Index5()
        {
            return View(repository.Seller);
        }

        public ViewResult Edit(int softID)
        {
            SoftWares soft = repository.App
                .FirstOrDefault(g => g.ID_SoftWare == softID);

            // Подготовка списков для DropDownList
            ViewBag.Categories = repository.Category
                .Select(x => new { ID_Category = x.ID_Category, Name = x.Name });

            ViewBag.Sellers = repository.Seller
                .Select(x => new { ID_Seller = x.ID_Seller, Name = x.Name });

            ViewBag.Events = repository.Event
               .Select(x => new { Event_ID = x.Event_ID, Name = x.Event_Name });


            return View(soft);
        }

        [HttpPost]
        public ActionResult Edit(SoftWares software, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    software.Image_Mime_Type = image.ContentType;
                    software.Photo1 = new byte[image.ContentLength];
                    image.InputStream.Read(software.Photo1, 0, image.ContentLength);
                }
                repository.SaveSoft(software);
                TempData["message"] = $"Изменения в программном обеспечении \"{software.Name}\" были сохранены";
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так с данными
                return View(software);
            }
        }

        public ViewResult Create()
        {
            // Подготовка списков для DropDownList
            ViewBag.Categories = repository.Category
                .Select(x => new { ID_Category = x.ID_Category, Name = x.Name });

            ViewBag.Sellers = repository.Seller
                .Select(x => new { ID_Seller = x.ID_Seller, Name = x.Name });

            ViewBag.Events = repository.Event
              .Select(x => new { ID_Seller = x.Event_ID, Name = x.Event_Name });

            return View("Edit", new SoftWares());
        }

        [HttpPost]
        public ActionResult Delete(int ID_SoftWare)
        {
            // Проверка на наличие связанных заказов
            var relatedOrders = _context.Order.Any(o => o.Order_Soft_ID == ID_SoftWare);
            if (relatedOrders)
            {
                // Возвращаем ошибку или сообщение, что удаление невозможно
                TempData["error"] = $"Невозможно удалить программное обеспечение, так как существуют связанные заказы.";
                return RedirectToAction("Index");
            }

            SoftWares deletedSoftWare = repository.DeleteSoft(ID_SoftWare);
            if (deletedSoftWare != null)
            {
                TempData["message"] = $"Программное обеспечение \"{deletedSoftWare.Name}\" было удалено";
            }
            return RedirectToAction("Index");
        }

    }
}