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
		//ProductService productService = new ProductService();
		CheckoutViewModel checkoutViewModel = new CheckoutViewModel();
		// GET: Shop

		public ActionResult Index(string searchTerm,int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy)
		{
			ShopViewModel model = new ShopViewModel();

			model.FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories();

			model.MaximumPrice = ProductService.Instance.GetMaximumPrice();

			model.Products = ProductService.Instance.ShopProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);

			model.SortBy = sortBy;
			
			return View(model);
		}

		public ActionResult FilterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy)
		{
			FilterProductViewModel model = new FilterProductViewModel();

			model.Products = ProductService.Instance.ShopProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);

			return PartialView("_FilterProducts", model);
		}

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
				checkoutViewModel.CartProducts = ProductService.Instance.GetListOfProduct(checkoutViewModel.CartProductIds);
			}
			return View(checkoutViewModel);
        }
    }
}