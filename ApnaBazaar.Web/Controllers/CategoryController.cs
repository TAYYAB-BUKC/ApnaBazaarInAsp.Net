﻿using ApnaBazaar.Entities;
using ApnaBazaar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApnaBazaar.Web.Controllers
{
    public class CategoryController : Controller
    {
		CategoriesService categoriesService = new CategoriesService();

		// GET: Category


		[HttpGet]
		public ActionResult Index()
		{
			var categories = categoriesService.GetCategories();
			return View(categories);
		}

		[HttpGet]
        public ActionResult Create()
        {
            return View();
        }
		[HttpPost]
		public ActionResult Create(Category category)
		{
			categoriesService.SaveCategory(category);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Edit(int Id)
		{
			var category = categoriesService.GetSpecificCategory(Id);
			return View(category);
		}
		[HttpPost]
		public ActionResult Edit(Category category)
		{
			categoriesService.UpdateCategory(category);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Delete(int Id)
		{
			var category = categoriesService.GetSpecificCategory(Id);
			return View(category);
		}
		[HttpPost]
		public ActionResult Delete(Category category)
		{
			categoriesService.DeleteCategory(category.ID);
			return RedirectToAction("Index");
		}
	}
}