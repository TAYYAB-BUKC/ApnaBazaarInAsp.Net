using ApnaBazaar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApnaBazaar.Web.ViewModels
{
	public class CheckoutViewModel
	{
		public List<Product> CartProducts { get; set; }

		public List<int> CartProductIds { get; set; }

	}


	public class ShopViewModel
	{
		public int MaximumPrice { get; set; }

		public List<Product> Products { get; set; }

		public List<Category> FeaturedCategories { get; set; }

		public int? SortBy { get; set; }
	}


	public class FilterProductViewModel
	{
		public List<Product> Products { get; set; }
	}
}