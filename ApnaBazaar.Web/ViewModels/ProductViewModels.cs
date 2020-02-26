using ApnaBazaar.Entities;
using ApnaBazaar.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApnaBazaar.Web.ViewModels
{
	public class ProductDetailViewModel
	{


		public Product product { get; set; }
		public string[] ReviewBy { get; set; }
		public string[] ReviewByImage { get; set; }
		public bool isWished { get;  set; }
	}
}