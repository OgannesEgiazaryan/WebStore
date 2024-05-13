using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;
using WebStore.WebUI.Models;


namespace WebStore.WebUI.Controllers
{
    public class WebController : Controller
    {
        private IWebRepository repository;
        public int pageSize = 4;


        public WebController(IWebRepository repo)
        {
            repository = repo;
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
    }

}