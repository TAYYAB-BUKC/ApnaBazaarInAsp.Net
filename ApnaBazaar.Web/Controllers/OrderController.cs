using ApnaBazaar.Services;
using ApnaBazaar.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApnaBazaar.Web.Controllers
{
    public class OrderController : Controller
    {
		private ApplicationSignInManager _signInManager;
		private ApplicationUserManager _userManager;


		public ApplicationSignInManager SignInManager
		{
			get
			{
				return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
			}
			private set
			{
				_signInManager = value;
			}
		}

		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}
		// GET: Order
		public ActionResult Search()
        {
			//int pageSize = ConfigurationService.Instance.GetNormalPageSizeConfiguration();
			
			//SearchOrderViewModel model = new SearchOrderViewModel();

			//var totalRecords = OrderService.Instance.SearchOrderCount(searchUserId);

			//model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

			//model.SearchUserID = searchUserId;
			
			//model.Pager = new Pager(totalRecords, pageNo, pageSize);

			//model.Orders = OrderService.Instance.SearchOrder(model.SearchUserID, model.PageNo.Value, pageSize);

			return View();
        }

		public ActionResult OrderTable(string searchUserId, int? pageNo)
		{
			int pageSize = ConfigurationService.Instance.GetNormalPageSizeConfiguration();

			SearchOrderViewModel model = new SearchOrderViewModel();

			var totalRecords = OrderService.Instance.SearchOrderCount(searchUserId);

			model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

			model.SearchUserID = searchUserId;

			model.Pager = new Pager(totalRecords, pageNo, pageSize);

			model.Orders = OrderService.Instance.SearchOrder(model.SearchUserID, model.PageNo.Value, pageSize);

			return PartialView("_OrderTable", model);
		}




		public ActionResult ShowUserOrders()
		{
			//int pageSize = ConfigurationService.Instance.GetNormalPageSizeConfiguration();

			//ShowUserOrderViewModel model = new ShowUserOrderViewModel();

			//model.User = UserManager.FindById(User.Identity.GetUserId());

			//model.SearchTerm = SearchTerm;
			
			//var totalRecords = OrderService.Instance.ShowUserOrderCount(model.User.Id, model.SearchTerm);

			//model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

			//model.Pager = new Pager(totalRecords, pageNo, pageSize);


			//model.Orders = OrderService.Instance.ShowUserOrder(model.User.Id, model.SearchTerm,model.PageNo.Value, pageSize);

			return View();
		}

		public ActionResult ShowUserOrdersPartial(string SearchTerm, int? pageNo)
		{
			int pageSize = ConfigurationService.Instance.GetNormalPageSizeConfiguration();

			ShowUserOrderViewModel model = new ShowUserOrderViewModel();

			model.User = UserManager.FindById(User.Identity.GetUserId());

			model.SearchTerm = SearchTerm;

			var totalRecords = OrderService.Instance.ShowUserOrderCount(model.User.Id, model.SearchTerm);

			model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

			model.Pager = new Pager(totalRecords, pageNo, pageSize);


			model.Orders = OrderService.Instance.ShowUserOrder(model.User.Id, model.SearchTerm, model.PageNo.Value, pageSize);

			return PartialView("_ShowUserOrdersPartial", model);
		}


		public ActionResult Details(int orderID)
		{
			
			OrderDetailViewModel model = new OrderDetailViewModel();

			model.Order = OrderService.Instance.GetOrderByID(orderID);

			if (model.Order != null)
			{
			model.OrderBy = UserManager.FindById(model.Order.UserId);
			}

			model.AvailableStatuses = new List<string>() { "Pending", "In Progress", "Delivered" };	

			return View(model);
		}

		public ActionResult UserDetails(int orderID)
		{

			OrderDetailViewModel model = new OrderDetailViewModel();

			model.Order = OrderService.Instance.GetOrderByID(orderID);

			if (model.Order != null)
			{
				model.OrderBy = UserManager.FindById(model.Order.UserId);
			}



			return View(model);
		}



		public JsonResult UpdateStatus(string status,int orderID)
		{

			JsonResult result = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet };

			result.Data = new { Success = OrderService.Instance.UpdateStatus(status, orderID) };
			
			return result;
		}


	}
}


