using ApnaBazaar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApnaBazaar.Web.ViewModels
{
	public class ConfigurationSearchViewModel
	{
		public int PageNo { get; set; }
		public List<ApnaBazaarConfiguration> Configurations { get; set; }
		public string SearchTerm { get; set; }
		public Pager Pager { get; set; }
	}


	public class ConfiguraionCreateViewModel
	{
		public string Key { get; set; }
		public string Value { get; set; }

	}





}