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

        //[HttpGet]
        //public async Task<IActionResult> Search(string term)
        //{
        //    var matches = _context.App
        //        .Where(p => p.Name.Contains(term))
        //        .ToList();

        //    return new JsonResult(matches);
        //}
    }
}