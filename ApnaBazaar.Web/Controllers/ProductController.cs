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
		ProductService productService = new ProductService();
		CategoriesService categoriesService = new CategoriesService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult ProductTable(string search)
		{
			var products = productService.GetProducts();
			if (!String.IsNullOrEmpty(search))
			{
			    products = products.Where(product => product.Name.Contains(search)).ToList();
			}
			return PartialView(products);
		}


		[HttpGet]
		public ActionResult Create()
		{
			var categories = categoriesService.GetCategories();

			return PartialView(categories);
		}
		[HttpPost]
		public ActionResult Create(CategoryViewModel categoryViewModel)
		{
			var newProduct = new Product{ Name = categoryViewModel.Name,Description=categoryViewModel.Description,Price=categoryViewModel.Price };
			//newProduct.ID = categoryViewModel.Category;	
			newProduct.Category = categoriesService.GetSpecificCategory(categoryViewModel.Category);
			productService.SaveProduct(newProduct);
			return RedirectToAction("ProductTable");
		}


		[HttpGet]
		public ActionResult Edit(int Id)
		{
			var product = productService.GetSpecificProduct(Id);

			return PartialView(product);
		}
		[HttpPost]
		public ActionResult Edit(ForProductUpdateViewModel model)
		{
			var newProduct = new Product { ID = model.Id, Name = model.Name, Description = model.Description, Price = model.Price };
			//newProduct.ID = categoryViewModel.Category;	
			newProduct.Category = categoriesService.GetSpecificCategory(model.Category);

			productService.UpdateProduct(newProduct);
			return RedirectToAction("ProductTable");
		}

		public JsonResult ForEdit()
		{
			JsonResult jsonResult = new JsonResult();
			jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

			var categories = categoriesService.GetCategories();
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
			productService.DeleteProduct(Id);
			return RedirectToAction("ProductTable");
		}

	}
}