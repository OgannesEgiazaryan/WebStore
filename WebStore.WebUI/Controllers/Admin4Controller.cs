using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Domain.Abstract;

namespace WebStore.WebUI.Controllers
{
    public class Admin4Controller : Controller
    {
        IWebRepository repository;

        public Admin4Controller(IWebRepository repo)
        {
            repository = repo;
        }


        public ViewResult Index4()
        {
            return View(repository.Review);
        }
    }
}