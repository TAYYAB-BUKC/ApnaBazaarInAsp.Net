using ApnaBazaar.Database;
using ApnaBazaar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ApnaBazaar.Services
{
	public class WishlistService
	{
		#region Singleton
		public static WishlistService Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new WishlistService();
				}
				return instance;
			}
		}
		private static WishlistService instance { get; set; }

		private WishlistService()
		{

		}
		#endregion

		public bool SaveToWishlist(Wishlist wishlist)
		{
			using (var context = new ApnaBazaarContext())
			{
				context.Wishlists.Add(wishlist);
				return context.SaveChanges() > 0;
			}
		}


		public bool RemoveFromWishlist(int ProdcutId,string UserId)
		{
			using (var context = new ApnaBazaarContext())
			{
				//context.Entry(category).State = System.Data.Entity.EntityState.Modified;
				var wishlist = context.Wishlists.Where(w=>w.ProductId == ProdcutId && w.UserID == UserId).FirstOrDefault();
				context.Wishlists.Remove(wishlist);
				return context.SaveChanges() > 0;
			}
		}

		public List<Wishlist> GetWishlist(string UserId)
		{
			using (var context = new ApnaBazaarContext())
			{
				return context.Wishlists.Where(w => w.UserID == UserId).ToList();
			}
		}


		public int GetWishlistProductCount(string userId, string searchTerm)
		{
			using (var context = new ApnaBazaarContext())
			{
				var wishlists = context.Wishlists.Include(x=>x.Product).ToList();

				if (!string.IsNullOrEmpty(userId))
				{
					wishlists = wishlists.Where(w => w.UserID.ToUpper().Contains(userId.ToUpper())).ToList();
				}

				if (!string.IsNullOrEmpty(searchTerm))
				{
					wishlists = wishlists.Where(w => w.Product.Name.ToUpper().Contains(searchTerm.ToUpper())).ToList();
				}

				return wishlists.Count;
			}

		}

		public List<Wishlist> GetWishlistProduct(string userId, string searchTerm, int? pageNo, int pageSize)
		{
			using (var context = new ApnaBazaarContext())
			{
				var wishlists = context.Wishlists.Include(x => x.Product).ToList();



				if (!string.IsNullOrEmpty(userId))
				{
					wishlists = wishlists.Where(w => w.UserID.ToUpper().Contains(userId.ToUpper())).ToList();
				}

				if (!string.IsNullOrEmpty(searchTerm))
				{
					wishlists = wishlists.Where(w => w.Product.Name.ToUpper().Contains(searchTerm.ToUpper())).ToList();
				}

				return wishlists.Skip((pageNo.Value - 1) * pageSize).Take(pageSize).ToList();
			}
		}

		public List<int> GetWishlistProducts(string UserId)
		{
			using (var context = new ApnaBazaarContext())
			{
				var wishedProducts = context.Wishlists.Where(w => w.UserID == UserId).ToList();
				return wishedProducts.Select(c => c.ProductId).ToList();
			}
		}
	}
}
