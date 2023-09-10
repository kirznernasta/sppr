using System;
using Microsoft.AspNetCore.Mvc;
using WEB_153502_KIRZNER.Domain.Entities;

namespace WEB_153502_KIRZNER.ViewComponents
{
	public class CartViewComponent : ViewComponent
	{
        public Cart Cart { get; set; }

        public CartViewComponent(Cart cart)
        {
            Cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            return View(Cart);
        }
    }
}

