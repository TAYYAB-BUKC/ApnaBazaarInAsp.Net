using ApnaBazaar.Entities;
using ApnaBazaar.Services;
using ApnaBazaar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApnaBazaar.Web.Controllers
{
    public class ProductController : Controller
    {
		//ProductService productService = new ProductService();
		//CategoriesService categoriesService = new CategoriesService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult ProductTable(string search,int? pageNo)
		{
			ProductSearchViewModel model = new ProductSearchViewModel();
			
            model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 :1;

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


			model.Products = ProductService.Instance.GetProducts(model.PageNo);

			model.SearchTerm = search;
			if (!String.IsNullOrEmpty(model.SearchTerm))
			{
				model.Products = model.Products.Where(product => product.Name.ToLower().Contains(model.SearchTerm.ToLower())).ToList();
			}
			return PartialView(model);
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
			var newProduct = new Product{ Name = categoryViewModel.Name,Description=categoryViewModel.Description,Price=categoryViewModel.Price };
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
			//newProduct.ID = categoryViewModel.Category;	
			newProduct.Category = CategoriesService.Instance.GetSpecificCategory(model.Category);

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
			ProductDetailViewModel model = new ProductDetailViewModel { product = ProductService.Instance.GetSpecificProduct(Id) };

			return View(model);
		}



	}
}