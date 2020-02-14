using ApnaBazaar.Entities;
using ApnaBazaar.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApnaBazaar.Web.ViewModels
{
	public class SearchOrderViewModel
	{
		public List<Order> Orders { get; set; }
		public int? PageNo { get; set; }
		public string SearchUserID { get; set; }
		public string Status { get; set; }
		public Pager Pager { get; set; }
	}

	public class OrderDetailViewModel
	{
		public Order Order { get; set; }
		public ApplicationUser OrderBy { get; set; }
		public List<string> AvailableStatuses { get; set; }
	}
}