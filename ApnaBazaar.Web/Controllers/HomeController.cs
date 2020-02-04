using ApnaBazaar.Services;
using ApnaBazaar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApnaBazaar.Web.Controllers
{
	public class HomeController : Controller
	{
		HomeViewModel homeViewModel = new HomeViewModel();
		//CategoriesService categoriesService = new CategoriesService();
		public ActionResult Index()
		{
			homeViewModel.FeaturedCategories = CategoriesService.Instance.GetCategories();

			return View(homeViewModel);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}