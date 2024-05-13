using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Domain.Abstract;

namespace WebStore.WebUI.Controllers
{
    public class Admin5Controller : Controller
    {
        IWebRepository repository;

        public Admin5Controller(IWebRepository repo)
        {
            repository = repo;
        }


        public ViewResult Index5()
        {
            return View(repository.Seller);
        }
    }
}