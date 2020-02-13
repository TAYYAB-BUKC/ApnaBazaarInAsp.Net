﻿using ApnaBazaar.Entities;
using ApnaBazaar.Services;
using ApnaBazaar.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApnaBazaar.Web.Controllers
{
	public class ShopController : Controller
	{

		private ApplicationSignInManager _signInManager;
		private ApplicationUserManager _userManager;


		public ApplicationSignInManager SignInManager
		{
			get
			{
				return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
			}
			private set
			{
				_signInManager = value;
			}
		}

		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}



		//ProductService productService = new ProductService();
		CheckoutViewModel checkoutViewModel = new CheckoutViewModel();
		// GET: Shop

		public ActionResult Index(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
		{
			pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

			int pageSize = ConfigurationService.Instance.GetShopPageSizeConfiguration();

			ShopViewModel model = new ShopViewModel();

			model.SearchTerm = searchTerm;

			model.FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories();

			model.MaximumPrice = ProductService.Instance.GetMaximumPrice();


			int totalShopRecords = ProductService.Instance.ShopProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);

			model.Products = ProductService.Instance.ShopProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo, pageSize);

			model.SortBy = sortBy;

			model.CategoryID = categoryID;

			model.Pager = new Pager(totalShopRecords, pageNo, pageSize);

			return View(model);
		}

		public ActionResult FilterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
		{
			pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

			int pageSize = ConfigurationService.Instance.GetShopPageSizeConfiguration();


			FilterProductViewModel model = new FilterProductViewModel();

			model.SearchTerm = searchTerm;

			model.SortBy = sortBy;

			model.CategoryID = categoryID;


			int totalShopRecords = ProductService.Instance.ShopProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);

			model.Pager = new Pager(totalShopRecords, pageNo, pageSize);


			model.Products = ProductService.Instance.ShopProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo, pageSize);

			return PartialView(model);
		}

		[Authorize]
		public ActionResult Checkout()
		{
			var cartProductCookoies = Request.Cookies["CartProducts"];

			if (cartProductCookoies != null && !string.IsNullOrEmpty(cartProductCookoies.Value))
			{
				//var cpc = cartProductCookoies.Value;

				//var ids = cpc.Split('-');

				//var pids = ids.Select(x => int.Parse(x)).ToList();

				checkoutViewModel.CartProductIds = cartProductCookoies.Value.Split('-').Select(x => int.Parse(x)).ToList();

				//GetListOfProduct
				checkoutViewModel.CartProducts = ProductService.Instance.GetListOfProduct(checkoutViewModel.CartProductIds);

				checkoutViewModel.User = UserManager.FindById(User.Identity.GetUserId());

			}
			return View(checkoutViewModel);
		}

		public JsonResult PlaceOrder(string products)
		{
			JsonResult result = new JsonResult();
			result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
			if (!string.IsNullOrEmpty(products))
			{

				var productQuantities = products.Split('-').Select(p => int.Parse(p)).ToList();

				var boughtProducts = ProductService.Instance.GetListOfProduct(productQuantities.Distinct().ToList());

				Order newOrder = new Order();
				newOrder.UserId = User.Identity.GetUserId();
				newOrder.OrderedAt = DateTime.Now;
				newOrder.Status = "Pending";
				newOrder.TotalAmount = boughtProducts.Sum(x => x.Price * productQuantities.Where(productId => productId == x.ID).Count());

				newOrder.OrderItems = new List<OrderItem>();
				newOrder.OrderItems.AddRange(boughtProducts.Select(x => new OrderItem() { ProductId = x.ID, Quantity = productQuantities.Where(productId => productId == x.ID).Count() }));

				int rowsAffected = ShopService.Instance.SaveOrder(newOrder);


				result.Data = new { Success = true, TotalRows = rowsAffected };

			}
			else
			{
				result.Data = new { Success = false };

			}

			return result;
		}
	}
}