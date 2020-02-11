using ApnaBazaar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApnaBazaar.Web.ViewModels
{
	public class CategoryViewModel
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int Category { get; set; }
		public string Imagepath { get; set; }
	}

	public class CategorySearchViewModel
	{
		public List<Category> Categories { get; set; }
		public string SearchTerm { get; set; }

		public Pager Pager { get; set; }
		

	}
}