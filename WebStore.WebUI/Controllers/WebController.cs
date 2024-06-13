using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebStore.Domain.Abstract;
using WebStore.Domain.Concrete;
using WebStore.Domain.Entities;
using WebStore.WebUI.Models;


namespace WebStore.WebUI.Controllers
{
    public class WebController : Controller
    {
        private IWebRepository repository;
        public int pageSize = 4;
        EFDbContext _context;

        public WebController(IWebRepository repo, EFDbContext context)
        {
            repository = repo;
            _context = context;
        }

        public ViewResult WebSummary()
        {
            return View();
        }

        public ViewResult List(string category, int page = 1)
        {

            ViewBag.Categorys = repository.Category.ToList();

            // Получаем ID категории на основе имени категории
            int? categoryID = category != null ?
                repository.Category.FirstOrDefault(c => c.Name == category)?.ID_Category : null;



            SoftsListViewModel model = new SoftsListViewModel
            {
                App = repository.App
                    .Where(s => categoryID == null || s.ID_Category == categoryID)
                    .OrderBy(soft => soft.ID_SoftWare)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = categoryID == null ?
                        repository.App.Count() :
                        repository.App.Count(s => s.ID_Category == categoryID)
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public FileContentResult GetImage(int softId)
        {
            SoftWares soft = repository.App
                .FirstOrDefault(g => g.ID_SoftWare == softId);

            if (soft != null)
            {
                return File(soft.Photo1, soft.Image_Mime_Type);
            }
            else
            {
                return null;
            }
        }

        public ViewResult List1(string events, int page = 1)
        {

            ViewBag.Events = repository.Event.ToList();

            // Получаем ID категории на основе имени категории
            int? event_ID = 1;


            SoftsListViewModel model = new SoftsListViewModel
            {
                App = repository.App
                    .Where(s => event_ID == null || s.ID_Event == event_ID)
                    .OrderBy(soft => soft.ID_SoftWare)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = event_ID == null ?
                        repository.App.Count() :
                        repository.App.Count(s => s.ID_Category == event_ID)
                },
                CurrentCategory = events
            };
            return View(model);
        }

        public ActionResult Details(int softId)
{
    var product = repository.App.FirstOrDefault(p => p.ID_SoftWare == softId);
    if (product != null)
    {
        var bestOffers = repository.App
            .Where(p => p.ID_Event == product.ID_Event && p.ID_SoftWare != softId)
            .Select(p => new BestOfferViewModel
            {
                Software = p,
                AverageRating = p.Review.Any() ? p.Review.Average(r => r.Rating) : 0,
                ReviewCount = p.Review.Count()
            })
            .ToList();

        ViewBag.BestOffers = bestOffers;

        ViewBag.ReviewCount = product.Review.Count();
        ViewBag.AverageRating = product.Review.Any() ? product.Review.Average(r => r.Rating) : 0;

        // Calculate counts for each star rating
        ViewBag.RatingCounts = new Dictionary<int, int>
        {
            { 5, product.Review.Count(r => r.Rating == 5) },
            { 4, product.Review.Count(r => r.Rating == 4) },
            { 3, product.Review.Count(r => r.Rating == 3) },
            { 2, product.Review.Count(r => r.Rating == 2) },
            { 1, product.Review.Count(r => r.Rating == 1) }
        };

        return View(product);
    }
    return HttpNotFound();
}


        [HttpPost]
        public ActionResult AddReview(int softId, string userName, string userEmail, int rating, string reviewText)
        {
            var product = repository.App.FirstOrDefault(p => p.ID_SoftWare == softId);
            if (product != null)
            {
                var review = new Reviews
                {
                    Date = DateTime.Now,
                    Text = reviewText,
                    Rating = rating,
                    User = userName,
                    ID_Software = softId
                };

                product.Review.Add(review);
                repository.SaveReview(review);

                return RedirectToAction("Details", new { softId = softId });
            }
            return HttpNotFound();
        }

       


    }

}