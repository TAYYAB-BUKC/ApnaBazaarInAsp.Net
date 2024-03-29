﻿using ApnaBazaar.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApnaBazaar.Web.ViewModels
{
	public class UserProfileViewModel
	{
		public ApplicationUser User { get; set; }
	}

	public class UserViewModel
	{
		public List<ApplicationUser> Users { get; set; }

		public string SearchTerm { get; set; }
		public Pager Pager { get; set; }
	}

	public class UserUpdateViewModel
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }

		public string Image { get; set; }

	}

	public class ReviewViewModel
	{
		public string UserId { get; set; }

		public string Comment { get; set; }

		public float Rating { get; set; }

		public int ProductID { get; set; }
	}


	public class ShowReviewViewModel
	{
		public ApplicationUser ReviewBy { get; set; }

		public string Comment { get; set; }

		public float Rating { get; set; }

	}






	public class Pager
	{
		public Pager(int totalItems, int? page, int pageSize)
		{
			if (pageSize == 0) pageSize = 10;

			var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
			var currentPage = page != null ? (int)page : 1;
			var startPage = currentPage - 5;
			var endPage = currentPage + 4;
			if (startPage <= 0)
			{
				endPage -= (startPage - 1);
				startPage = 1;
			}
			if (endPage > totalPages)
			{
				endPage = totalPages;
				if (endPage > 10)
				{
					startPage = endPage - 9;
				}
			}

			TotalItems = totalItems;
			CurrentPage = currentPage;
			PageSize = pageSize;
			TotalPages = totalPages;
			StartPage = startPage;
			EndPage = endPage;
		}

		public int TotalItems { get; private set; }
		public int CurrentPage { get; private set; }
		public int PageSize { get; private set; }
		public int TotalPages { get; private set; }
		public int StartPage { get; private set; }
		public int EndPage { get; private set; }
	}
}