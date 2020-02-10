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

		public ActionResult Index(string searchTerm,int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
		{
			pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

			int pageSize = 10;

			ShopViewModel model = new ShopViewModel();

			model.FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories();

			model.MaximumPrice = ProductService.Instance.GetMaximumPrice();

			model.Products = ProductService.Instance.ShopProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy,pageNo,pageSize);

			model.SortBy = sortBy;

			model.CategoryID = categoryID;

			int totalShopRecords = ProductService.Instance.ShopProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
			
            model.Pager = new Pager(totalShopRecords,pageNo);
			
			return View(model);
		}

		public ActionResult FilterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
		{
			pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

			int pageSize = 10;

			FilterProductViewModel model = new FilterProductViewModel();

			int totalShopRecords = ProductService.Instance.ShopProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);

			model.Pager = new Pager(totalShopRecords, pageNo);


			model.Products = ProductService.Instance.ShopProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo, pageSize);

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