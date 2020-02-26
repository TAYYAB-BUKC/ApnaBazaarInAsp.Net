using ApnaBazaar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApnaBazaar.Web.ViewModels
{
	public class ProductsWidgetViewModels
	{
		public List<Product> LatestProducts { get; set; }
		public bool isLatestProducts { get; set; }
		public int? categoryID { get; set; }
		public string UserId { get; set; }
		public List<int> WishedProductsIds { get; set; }
	}
}