using ApnaBazaar.Database;
using ApnaBazaar.Entities;
using ApnaBazaar.Services;
using ApnaBazaar.Web.Models;
using ApnaBazaar.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApnaBazaar.Web.Controllers
{
    public class SharedController : Controller
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

		public JsonResult UploadImage()
		{
			JsonResult result = new JsonResult();
			result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

			try
			{
				var file = Request.Files[0];

				var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

				var path = Path.Combine(Server.MapPath("~/content/UploadImages/"), fileName);

				file.SaveAs(path);

				result.Data = new { Success = true, ImageURL = string.Format("/content/UploadImages/{0}",fileName) };

				//var newImage = new Image() { Name = fileName };

				//if (ImagesService.Instance.SaveNewImage(newImage))
				//{
				//    result.Data = new { Success = true, Image = fileName, File = newImage.ID, ImageURL = string.Format("{0}{1}", Variables.ImageFolderPath, fileName) };
				//}
				//else
				//{
				//    result.Data = new { success = false, Message = new HttpStatusCodeResult(500) };
				//}
			}
			catch (Exception ex)
			{
				result.Data = new { Success = false, Message = ex.Message };
			}

			return result;
		}

		[HttpGet]
		public ActionResult UserProfile()
		{
			UserProfileViewModel model = new UserProfileViewModel();

			var loggedInUser = UserManager.FindById(User.Identity.GetUserId());

			model.User = loggedInUser;

			return View(model);
		}
		
		public JsonResult UserProfile(UserUpdateViewModel model)
		{
			JsonResult result = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet };

			var loggedInUser = UserManager.FindById(User.Identity.GetUserId());

				loggedInUser.Name = model.Name;
				loggedInUser.Email = model.Email;
				loggedInUser.Address = model.Address;
			    loggedInUser.ProfileImage = model.Image;

				IdentityResult isUpdated = UserManager.Update(loggedInUser);

				result.Data = isUpdated.Succeeded ? new { Success = true } : new { Success = false };
			
			return result;
		}

		public ActionResult Contact()
		{
			return View();
		}


		public ActionResult FAQ()
		{
			return View();
		}

		public ActionResult ComingSoon()
		{
			return View();
		}

		public JsonResult AddReview(ReviewViewModel model)
		{
			JsonResult result = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet };


			var loggedInUser = UserManager.FindById(User.Identity.GetUserId());


			Review newReview = new Review { UserId = loggedInUser.Id, Name = loggedInUser.Name, Image = loggedInUser.ProfileImage,  Comment = model.Comment, Rating = model.Rating, ProductId = model.ProductID };

			result.Data = ProductService.Instance.SaveReview(newReview) > 0 ? new { Success = true } : new { Success = false };

			return result;
		}

		public MvcHtmlString ReturnUser(string Email)
		{
			var reviewBy = UserManager.FindByEmail(Email);

			return new MvcHtmlString(reviewBy.Name);
		}

		public ActionResult ShowAllUsers()
		{
			return View();
		}

		public ActionResult ShowAllUsersPartial(string SearchTerm, int? pageNo)
		{
			int pageSize = ConfigurationService.Instance.GetNormalPageSizeConfiguration();

			pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

			UserViewModel model = new UserViewModel();

			var totalRecords = 0;
			using (var context = new ApplicationDbContext())
			{
				var Users = context.Users.ToList();

				if (!string.IsNullOrEmpty(SearchTerm))
				{
					Users = Users.Where(u => u.Name.ToUpper().Contains(SearchTerm.ToUpper())).ToList();
				}

				totalRecords = Users.Count;
				//return orders.Skip((pageNo.Value - 1) * pageSize).Take(pageSize).ToList();
	             model.Users =  Users.Skip((pageNo.Value - 1) * pageSize).Take(pageSize).ToList();
			}

			model.SearchTerm = SearchTerm;

			model.Pager = new Pager(totalRecords, pageNo, pageSize);

			return PartialView("_ShowAllUsersPartial", model);
		}


	}
}