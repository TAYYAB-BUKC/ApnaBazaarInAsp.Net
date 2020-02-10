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
	[Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
		//CategoriesService categoriesService = new CategoriesService();

		// GET: Category
		[HttpGet]
		public ActionResult Index()
		{
			var categories = CategoriesService.Instance.GetFeaturedCategories();
			return View(categories);
		}

		public ActionResult CategoriesTable(string search, int? pageNo)
		{
			int pageSize = 3;

			pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

			CategorySearchViewModel model = new CategorySearchViewModel();

			model.SearchTerm = search;

			var totalRecords = CategoriesService.Instance.GetCategoriesCount(search);
			
            model.Categories = CategoriesService.Instance.GetCategories(search,pageNo.Value);

			if (model.Categories != null)
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
            return PartialView();
        }
		[HttpPost]
		public ActionResult Create(Category category)
		{
			CategoriesService.Instance.SaveCategory(category);
			return RedirectToAction("CategoriesTable");
		}

		[HttpGet]
		public ActionResult Edit(int Id)
		{
			var category = CategoriesService.Instance.GetSpecificCategory(Id);
			return PartialView(category);
		}
		[HttpPost]
		public ActionResult Edit(Category category)
		{
			CategoriesService.Instance.UpdateCategory(category);
			return RedirectToAction("CategoriesTable");
		}

		//[HttpGet]
		//public ActionResult Delete(int Id)
		//{
		//	var category = categoriesService.GetSpecificCategory(Id);
		//	return View(category);
		//}
		[HttpPost]
		public ActionResult Delete(Category category)
		{
			CategoriesService.Instance.DeleteCategory(category.ID);
			return RedirectToAction("CategoriesTable");
		}
	}
}