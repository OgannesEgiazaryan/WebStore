using WebStore.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;
using WebStore.WebUI.Models;

namespace WebStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IWebRepository repository;
        private IOrderProcessor orderProcessor;


        public CartController(IWebRepository repo, IOrderProcessor processor)
        {
            repository = repo;
            orderProcessor = processor;

        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int softId, string returnUrl)
        {
            SoftWares soft = repository.App
                .FirstOrDefault(g => g.ID_SoftWare == softId);

            if (soft != null)
            {
                cart.AddItem(soft, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int softId, string returnUrl)
        {
            SoftWares soft = repository.App
                .FirstOrDefault(g => g.ID_SoftWare == softId);

            if (soft != null)
            {
                cart.RemoveLine(soft);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}