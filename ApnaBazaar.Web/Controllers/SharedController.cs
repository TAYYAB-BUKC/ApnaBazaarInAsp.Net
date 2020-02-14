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

		public ActionResult UserProfile()
		{
			UserProfileViewModel model = new UserProfileViewModel();

			var loggedInUser = UserManager.FindById(User.Identity.GetUserId());

			model.User = loggedInUser;

			return View(model);
		}
	}
}