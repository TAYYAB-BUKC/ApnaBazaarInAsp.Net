using ApnaBazaar.Services;
using ApnaBazaar.Web.ViewModels;
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
			return PartialView("_Products",model);
		}
	}
}