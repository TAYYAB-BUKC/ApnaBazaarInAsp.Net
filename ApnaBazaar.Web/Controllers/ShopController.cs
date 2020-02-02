using ApnaBazaar.Services;
using ApnaBazaar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApnaBazaar.Web.Controllers
{
    public class ShopController : Controller
    {
		ProductService productService = new ProductService();
		CheckoutViewModel checkoutViewModel = new CheckoutViewModel();
		// GET: Shop
		public ActionResult Checkout()
        {
			var cartProductCookoies = Request.Cookies["CartProducts"];

			if (cartProductCookoies != null)
			{
				//var cpc = cartProductCookoies.Value;

				//var ids = cpc.Split('-');

				//var pids = ids.Select(x => int.Parse(x)).ToList();

				checkoutViewModel.CartProductIds = cartProductCookoies.Value.Split('-').Select(x => int.Parse(x)).ToList();

				//GetListOfProduct
				checkoutViewModel.CartProducts = productService.GetListOfProduct(checkoutViewModel.CartProductIds);
			}
			return View(checkoutViewModel);
        }
    }
}