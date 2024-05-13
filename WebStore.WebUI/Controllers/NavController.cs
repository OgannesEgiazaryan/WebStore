using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebStore.Domain.Abstract;
using WebStore.Domain.Concrete;
using WebStore.Domain.Entities;

namespace WebStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        EFDbContext context;
        private IWebRepository repository;

        public NavController(IWebRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.Categorys = repository.Category; // Добавляем список категорий в ViewBag


            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Category
                .Select(game => game.Name)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);


        }

    }
}