using ApnaBazaar.Entities;
using ApnaBazaar.Services;
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
			return View();
		}
		[HttpPost]
		public ActionResult Create(Product product)
		{
			productService.SaveProduct(product);
			return RedirectToAction("ProductTable");
		}

	}
}