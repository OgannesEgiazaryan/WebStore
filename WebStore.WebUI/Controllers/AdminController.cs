    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Domain.Abstract;
using WebStore.Domain.Concrete;
using WebStore.Domain.Entities;
using WebStore.WebUI.Models;

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


        public ViewResult Index6()
        {
            var allApps = repository.App.Select(app => new AppViewModel
            {
                ID_SoftWare = app.ID_SoftWare,
                Name = app.Name,
                Price = app.Price,
                Count_Sale = app.Count_Sale,
                LastSaleDate = null // This will be populated for recent sales only
            }).ToList();

            var totalSales = allApps.Sum(app => app.Count_Sale);
            var totalRevenue = allApps.Sum(app => app.Price * app.Count_Sale);

            var last30Days = DateTime.Now.AddDays(-30);
            var recentOrders = repository.Order
                                         .Where(o => o.Order_Date >= last30Days)
                                         .GroupBy(o => o.Order_Soft_ID)
                                         .Select(g => new
                                         {
                                             ID_SoftWare = g.Key,
                                             Count_Sale = g.Count(),
                                             LastSaleDate = g.Max(o => o.Order_Date)
                                         })
                                         .ToList();

            var recentApps = recentOrders
                              .Join(repository.App,
                                    r => r.ID_SoftWare,
                                    app => app.ID_SoftWare,
                                    (r, app) => new AppViewModel
                                    {
                                        ID_SoftWare = app.ID_SoftWare,
                                        Name = app.Name,
                                        Price = app.Price,
                                        Count_Sale = r.Count_Sale,
                                        LastSaleDate = r.LastSaleDate
                                    })
                              .ToList();

            var recentTotalSales = recentApps.Sum(app => app.Count_Sale);
            var recentTotalRevenue = recentApps.Sum(app => app.Price * app.Count_Sale);

            ViewBag.TotalSales = totalSales;
            ViewBag.TotalRevenue = totalRevenue;
            ViewBag.RecentTotalSales = recentTotalSales;
            ViewBag.RecentTotalRevenue = recentTotalRevenue;
            ViewBag.RecentApps = recentApps;

            return View(allApps);
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
        public ActionResult Edit(SoftWares software, HttpPostedFileBase image = null, HttpPostedFileBase Image1 = null, HttpPostedFileBase Image2 = null, HttpPostedFileBase Image3 = null)
        {
            // Подготовка списков для DropDownList
            ViewBag.Categories = repository.Category
                .Select(x => new { ID_Category = x.ID_Category, Name = x.Name });

            ViewBag.Sellers = repository.Seller
                .Select(x => new { ID_Seller = x.ID_Seller, Name = x.Name });

            ViewBag.Events = repository.Event
               .Select(x => new { Event_ID = x.Event_ID, Name = x.Event_Name });

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
               .Select(x => new { Event_ID = x.Event_ID, Name = x.Event_Name });

            return View("Edit", new SoftWares());
        }

        [HttpPost]
        public ActionResult Delete(int ID_SoftWare)
        {
            try
            {
                SoftWares deletedSoftWare = repository.DeleteSoft(ID_SoftWare);
                if (deletedSoftWare != null)
                {
                    TempData["message"] = $"Программное обеспечение \"{deletedSoftWare.Name}\" было удалено";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                // Возвращаем ошибку или сообщение, что удаление невозможно
                    TempData["error"] = $"Невозможно удалить программное обеспечение, так как существуют связанные отзывы.";
                    return RedirectToAction("Index");
            }

        }

        public ViewResult Edit1(int categoryID)
        {
            Categorys category = repository.Category
                .FirstOrDefault(g => g.ID_Category == categoryID);

            return View(category);
        }

        [HttpPost]
        public ActionResult Edit1(Categorys category, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    category.ImageMimeType = image.ContentType;
                    category.Photo = new byte[image.ContentLength];
                    image.InputStream.Read(category.Photo, 0, image.ContentLength);
                }
                repository.SaveCat(category);
                TempData["message"] = $"Изменения в категории \"{category.Name}\" были сохранены";
                return RedirectToAction("Index1");
            }
            else
            {
                // Что-то не так с данными
                return View(category);
            }
        }

        public ViewResult Create1()
        {

            return View("Edit1", new Categorys());
        }

        [HttpPost]
        public ActionResult Delete1(int ID_Category)
        {
      
            try
            {
                Categorys deletedCategory = repository.DeleteCat(ID_Category);
                if (deletedCategory != null)
                {
                    TempData["message"] = $"Категория \"{deletedCategory.Name}\" была удалено";
                }
                return RedirectToAction("Index1");
            }
            catch
            {
                TempData["error"] = $"Невозможно удалить категорию, так как существуют связанные программы.";
                return RedirectToAction("Index1");
            }
       
        }

        public ViewResult Edit2(int eventID)
        {
            Events event_ = repository.Event
                .FirstOrDefault(g => g.Event_ID == eventID);

            return View(event_);
        }

        [HttpPost]
        public ActionResult Edit2(Events event_)
        {
            if (ModelState.IsValid)
            {
                repository.SaveEvent(event_);
                TempData["message"] = $"Изменения в событии \"{event_.Event_Name}\" были сохранены";
                return RedirectToAction("Index2");
            }
            else
            {
                // Что-то не так с данными
                return View(event_);
            }
        }

        public ViewResult Create2()
        {

            return View("Edit2", new Events());
        }

        [HttpPost]
        public ActionResult Delete2(int Event_ID)
        {
            try
            {
                Events deletedEvent = repository.DeleteEvent(Event_ID);
                if (deletedEvent != null)
                {
                    TempData["message"] = $"Событие \"{deletedEvent.Event_Name}\" было удалено";
                }
                return RedirectToAction("Index2");
            }
            catch
            {
                TempData["error"] = $"Невозможно удалить событие, так как существуют связанные программы.";
                return RedirectToAction("Index2");
            }
        
        }

        public ViewResult Edit3(int orderID)
        {


            Orders order = repository.Order
                .FirstOrDefault(g => g.Order_ID == orderID);

            ViewBag.Softwares = repository.App
    .Select(x => new { ID_SoftWare = x.ID_SoftWare, Name = x.Name });

            return View(order);
        }

        [HttpPost]
        public ActionResult Edit3(Orders order)
        {
            if (ModelState.IsValid)
            {
                repository.SaveOrder(order);
                TempData["message"] = $"Изменения в заказе от \"{order.Order_Name}\" были сохранены";
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так с данными
                return View(order);
            }
        }

        public ViewResult Create3()
        {
            // Подготовка списков для DropDownList
            ViewBag.Softwares = repository.App
.Select(x => new { ID_SoftWare = x.ID_SoftWare, Name = x.Name });

            return View("Edit3", new Orders());
        }

        [HttpPost]
        public ActionResult Delete3(int Order_ID)
        {
            try
            {
                Orders deletedOrder = repository.DeleteOrder(Order_ID);
                if (deletedOrder != null)
                {
                    TempData["message"] = $"Заказ от \"{deletedOrder.Order_Name}\" был удален";
                }
                return RedirectToAction("Index3");
            }
            catch
            {
                TempData["error"] = $"Невозможно удалить заказ, так как существуют связанные программы.";
                return RedirectToAction("Index3");
            }

      
        }

        public ViewResult Edit4(int reviewID)
        {
            Reviews review = repository.Review
                .FirstOrDefault(g => g.ID_Review == reviewID);

            ViewBag.Softwares = repository.App
.Select(x => new { ID_SoftWare = x.ID_SoftWare, Name = x.Name });

            return View(review);
        }

        [HttpPost]
        public ActionResult Edit4(Reviews review)
        {
            if (ModelState.IsValid)
            {
                repository.SaveReview(review);
                TempData["message"] = $"Изменения в отзыве от \"{review.User}\" были сохранены";
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так с данными
                return View(review);
            }
        }

        public ViewResult Create4()
        {
            // Подготовка списков для DropDownList
            ViewBag.Softwares = repository.App.Select(x => new { ID_SoftWare = x.ID_SoftWare, Name = x.Name });

            return View("Edit4", new Reviews());
        }

        [HttpPost]
        public ActionResult Delete4(int ID_Review)
        {
            try
            {
                Reviews deletedReview = repository.DeleteReview(ID_Review);
                if (deletedReview != null)
                {
                    TempData["message"] = $"Отзыв от \"{deletedReview.User}\" был удален";
                }
                return RedirectToAction("Index4");
            }
            catch
            {
                TempData["error"] = $"Невозможно удалить отзыв, так как существуют связанные программы.";
                return RedirectToAction("Index4");
            }
        }

        public ViewResult Edit5(int sellerID)
        {
            Sellers seller = repository.Seller
                .FirstOrDefault(g => g.ID_Seller == sellerID);

            return View(seller);
        }

        [HttpPost]
        public ActionResult Edit5(Sellers seller, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    seller.ImageMimeType = image.ContentType;
                    seller.Photo = new byte[image.ContentLength];
                    image.InputStream.Read(seller.Photo, 0, image.ContentLength);
                }
                repository.SaveSeller(seller);
                TempData["message"] = $"Изменения в продавце \"{seller.Name}\" были сохранены";
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так с данными
                return View(seller);
            }
        }

        public ViewResult Create5()
        {

            return View("Edit5", new Sellers());
        }

        [HttpPost]
        public ActionResult Delete5(int ID_Seller)
        {
            try
            {
                Sellers deletedSeller = repository.DeleteSeller(ID_Seller);
                if (deletedSeller != null)
                {
                    TempData["message"] = $"Издатель \"{deletedSeller.Name}\" была удалено";
                }
                return RedirectToAction("Index5");
            }
            catch
            {
                // Возвращаем ошибку или сообщение, что удаление невозможно
                TempData["error"] = $"Невозможно удалить продавца, так как существуют связанные программы.";
                return RedirectToAction("Index5");
            }
       
        }

    }
}