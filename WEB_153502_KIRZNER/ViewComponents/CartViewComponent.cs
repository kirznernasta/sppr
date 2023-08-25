using System;
using Microsoft.AspNetCore.Mvc;

namespace WEB_153502_KIRZNER.ViewComponents
{
	public class CartViewComponent : ViewComponent
	{
        public IViewComponentResult Invoke()
        {
           
            return View();
        }
    }
}

