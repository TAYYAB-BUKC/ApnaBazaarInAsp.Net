﻿using ApnaBazaar.Entities;
using ApnaBazaar.Web.Models;
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

		public ApplicationUser User { get; set; }

	}

	public class CartViewModel
	{
		public List<Product> CartProducts { get; set; }

		public List<int> CartProductIds { get; set; }
		
		public Pager Pager { get; set; }
		
		public string SearchTerm { get; set; }
		public string UserId { get; set; }
		public List<int> WishedProductsIds { get; set; }
	}






	public class ShopViewModel
	{
		public int MaximumPrice { get; set; }

		public List<Product> Products { get; set; }

		public List<Category> FeaturedCategories { get; set; }

		public int? SortBy { get; set; }
		public int? CategoryID { get; set; }

		public Pager Pager { get; set; }

		public string SearchTerm { get; set; }
		public string UserId { get; set; }
	}


	public class FilterProductViewModel
	{
		public List<Product> Products { get; set; }
		public Pager Pager { get; set; }
		public int? SortBy { get; set; }
		public int? CategoryID { get; set; }
		public string SearchTerm { get; set; }
		public string UserId { get; set; }
		public List<int> WishedProductsIds { get; set; }
	}
}