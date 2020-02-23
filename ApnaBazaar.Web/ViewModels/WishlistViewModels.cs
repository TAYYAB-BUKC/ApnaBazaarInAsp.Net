using ApnaBazaar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApnaBazaar.Web.ViewModels
{
	public class ShowWishlistViewModel
	{
		public List<Wishlist> Wishlists { get; set; }
		public Pager Pager { get; set; }
		public string SearchTerm { get; set; }


	}
}