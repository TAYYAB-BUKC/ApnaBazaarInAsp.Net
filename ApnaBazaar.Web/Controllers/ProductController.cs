using ApnaBazaar.Entities;
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
    public class ProductController : Controller
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
		//CategoriesService categoriesService = new CategoriesService();
		// GET: Product
		public ActionResult Index()
        {
            return View();
        }

		public ActionResult ProductTable(string search, int? pageNo)
		{

			int pageSize = ConfigurationService.Instance.GetNormalPageSizeConfiguration();

			ProductSearchViewModel model = new ProductSearchViewModel();

			model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

			//if (pageNo.HasValue)
			//{
			//	if (pageNo.Value > 0)
			//	{
			//		model.PageNo = pageNo.Value;
			//	}
			//	else
			//	{
			//		model.PageNo = pageNo.Value;
			//	}
			//}
			//else
			//{
			//	model.PageNo = pageNo.Value;
			//}

			model.SearchTerm = search;

			var totalRecords = ProductService.Instance.GetProductsCount(search);

			model.Products = ProductService.Instance.GetSearchProducts(search, model.PageNo);

			if (model.Products != null)
			{
				model.Pager = new Pager(totalRecords, pageNo, pageSize);

				return PartialView(model);
			}
			else
			{
				return HttpNotFound();
			}

		}

		[HttpGet]
		public ActionResult Create()
		{
			var categories = CategoriesService.Instance.GetCategories();

			return PartialView(categories);

		}
		
		[HttpPost]
		public ActionResult Create(CategoryViewModel categoryViewModel)
		{
			var newProduct = new Product{ Name = categoryViewModel.Name,Description=categoryViewModel.Description,Price=categoryViewModel.Price , Imagepath = categoryViewModel.Imagepath};
			//newProduct.ID = categoryViewModel.Category;	
			newProduct.Category = CategoriesService.Instance.GetSpecificCategory(categoryViewModel.Category);
			ProductService.Instance.SaveProduct(newProduct);
			return RedirectToAction("ProductTable");
		}


		[HttpGet]
		public ActionResult Edit(int Id)
		{
			var product = ProductService.Instance.GetSpecificProduct(Id);

			return PartialView(product);
		}


		[HttpPost]
		public ActionResult Edit(ForProductUpdateViewModel model)
		{
			var newProduct = new Product { ID = model.Id, Name = model.Name, Description = model.Description, Price = model.Price, Imagepath = model.Imagepath};
			newProduct.Category = null;	
			newProduct.CategoryID = model.Category;

			ProductService.Instance.UpdateProduct(newProduct);
			return RedirectToAction("ProductTable");
		}

		public JsonResult ForEdit()
		{
			JsonResult jsonResult = new JsonResult();
			jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

			var categories = CategoriesService.Instance.GetCategories();
			try
			{
				List<string> categoriesNames= new List<string>();
				List<int> categoriesIDs = new List<int>();

				foreach (var category in categories)
				{
					categoriesIDs.Add(category.ID);
					categoriesNames.Add(category.Name);
				}
				jsonResult.Data = new { CategoryIds = categoriesIDs, CategoryNames = categoriesNames };
			}
			catch (Exception ex)
			{
				jsonResult.Data = new { Success = false, Message = ex.Message };
			}

			return jsonResult;
		}
	

		[HttpPost]
		public ActionResult Delete(int Id)
		{
			ProductService.Instance.DeleteProduct(Id);
			return RedirectToAction("ProductTable");
		}

		[HttpGet]
		public ActionResult Details(int Id)
		{
			ProductDetailViewModel model = new ProductDetailViewModel { product = ProductService.Instance.GetProductWithReviews(Id) };

			int counter = 0;
			string[] names = new string[model.product.Reviews.Count];
			string[] ImagePaths = new string[model.product.Reviews.Count];


			foreach (var user in model.product.Reviews)
			{
				var UserIDs = UserManager.FindById(user.UserId);
				names[counter] = UserIDs.Name;
				ImagePaths[counter] = UserIDs.ProfileImage;
			}

			model.ReviewBy = names;
			model.ReviewByImage = ImagePaths;

			return View(model);
		}
	}
}