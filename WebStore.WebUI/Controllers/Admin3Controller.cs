using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Domain.Abstract;

namespace WebStore.WebUI.Controllers
{
    public class Admin3Controller : Controller
    {
        IWebRepository repository;

        public Admin3Controller(IWebRepository repo)
        {
            repository = repo;
        }


        public ViewResult Index3()
        {
            return View(repository.Order);
        }
    }
}