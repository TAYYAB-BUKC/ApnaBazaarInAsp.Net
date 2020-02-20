using ApnaBazaar.Entities;
using ApnaBazaar.Services;
using ApnaBazaar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApnaBazaar.Web.Controllers
{
    public class ConfigurationController : Controller
    {
        // GET: Configuration
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult ConfigurationTable(string search, int? pageNo)
		{

			int pageSize = ConfigurationService.Instance.GetNormalPageSizeConfiguration();

			ConfigurationSearchViewModel model = new ConfigurationSearchViewModel();

			model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

			model.SearchTerm = search;

			var totalRecords = ConfigurationService.Instance.GetConfigurationsCount(search);

			model.Configurations = ConfigurationService.Instance.GetSearchConfigurations(search, model.PageNo);

			if (model.Configurations != null)
			{
				model.Pager = new Pager(totalRecords, pageNo, pageSize);

				return PartialView("_ConfigurationTable", model);
			}
			else
			{
				return HttpNotFound();
			}

		}

		[HttpPost]
		public ActionResult Create(ConfiguraionCreateViewModel model)
		{
			var configuration = new ApnaBazaarConfiguration { Key = model.Key, Value = model.Value};
			//newProduct.ID = categoryViewModel.Category;	
			ConfigurationService.Instance.SaveConfiguration(configuration);
			return RedirectToAction("ConfigurationTable");
		}

		

		[HttpPost]
		public ActionResult Edit(ConfiguraionCreateViewModel model)
		{
			var configuration = new ApnaBazaarConfiguration { Key = model.Key, Value = model.Value };
			
			ConfigurationService.Instance.UpdateConfiguration(configuration);
			return RedirectToAction("ConfigurationTable");
		}

		[HttpPost]
		public ActionResult Delete(string key)
		{
			ConfigurationService.Instance.DeleteConfiguration(key);
			return RedirectToAction("ConfigurationTable");
		}



	}
}