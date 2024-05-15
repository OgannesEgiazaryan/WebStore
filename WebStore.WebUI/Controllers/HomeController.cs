using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebStore.Domain.Abstract;
using WebStore.Domain.Concrete;

namespace WebStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IWebRepository repository;
        EFDbContext _context;

        public HomeController(IWebRepository repo, EFDbContext context)
        {
            repository = repo;
            _context = context;
        }

        public ActionResult _Layout()
        {
            // You can add any logic needed to prepare the homepage view.
            return View();
        }

        // Optionally, add other actions as needed for other pages.
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}