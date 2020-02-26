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
    public class WidgetController : Controller
    {
		// GET: Widget

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

		public ActionResult Products(bool isLatestProducts,int? categoryID)
        {
			ProductsWidgetViewModels model = new ProductsWidgetViewModels();

			model.isLatestProducts = isLatestProducts;
			model.categoryID = categoryID;
			if (model.isLatestProducts)
			{
				model.LatestProducts = ProductService.Instance.GetLatestProducts(4);
			}
			else if (categoryID.HasValue && categoryID.Value > 0)
			{
				model.LatestProducts = ProductService.Instance.GetProductsByCategory(categoryID.Value,4);
			}
			else
			{
				model.LatestProducts = ProductService.Instance.GetProducts(1,8);
			}

			model.UserId = User.Identity.GetUserId();


			model.WishedProductsIds = WishlistService.Instance.GetWishlistProducts(model.UserId);


			return PartialView("_Products",model);
		}
	}
}