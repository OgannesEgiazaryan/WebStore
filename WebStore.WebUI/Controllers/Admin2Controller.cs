using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Domain.Abstract;

namespace WebStore.WebUI.Controllers
{
    public class Admin2Controller : Controller
    {
        IWebRepository repository;

        public Admin2Controller(IWebRepository repo)
        {
            repository = repo;
        }


        public ViewResult Index2()
        {
            return View(repository.Event);
        }
    }
}