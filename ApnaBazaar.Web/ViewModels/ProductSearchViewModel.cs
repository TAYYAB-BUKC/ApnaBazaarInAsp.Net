using ApnaBazaar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApnaBazaar.Web.ViewModels
{
	public class ProductSearchViewModel
	{
		public int PageNo { get; set; }
		public List<Product> Products { get; set; }
		public string SearchTerm { get; set; }
		public Pager Pager { get; set; }
	}
}