using ApnaBazaar.Database;
using ApnaBazaar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApnaBazaar.Services
{
	public class CategoriesService
	{
		#region Singleton
		public static CategoriesService Instance {
			get
			{
				if (instance == null)
				{
					instance = new CategoriesService();
				}
				return instance;
			}
         }	
		private static CategoriesService instance { get; set; }

		private CategoriesService()
		{

		}
		#endregion
		public Category GetSpecificCategory(int Id)
		{
			using (var context = new ApnaBazaarContext())
			{
				return context.Categories.Find(Id);
			}
		}


		public int GetCategoriesCount(string search)
		{
			using (var context = new ApnaBazaarContext())
			{
				if (!String.IsNullOrEmpty(search))
				{
					return context.Categories
						.Where(category => category.Name != null && category.Name.ToUpper().Contains(search.ToUpper()))
						.OrderBy(category => category.Name)
						.Count();

				}
				else
				{
					return context.Categories.Count();
				}
				
			}
		}

		public List<Category> GetCategories(string search,int pageNo)
		{
			//int pageSize = 3; // will done by using configuration

			int pageSize = ConfigurationService.Instance.GetNormalPageSizeConfiguration();
			using (var context = new ApnaBazaarContext())
			{
				if (!String.IsNullOrEmpty(search))
				{
					return context.Categories
						.Where(category => category.Name != null && category.Name.ToUpper().Contains(search.ToUpper()))
						.OrderBy(category => category.Name)
						.Skip((pageNo - 1) * pageSize)
						.Take(pageSize)
						.Include(x => x.Products)
						.ToList();

				}
				else
				{
					return context.Categories
						.OrderBy(category => category.Name)
						.Skip((pageNo - 1) * pageSize)
						.Take(pageSize)
						.Include(x => x.Products)
						.ToList();

				}

			}

			/*var context = new ApnaBazaarContext();
			return context.Products.ToList();*/

		}

		public List<Category> GetCategories()
		{
			using (var context = new ApnaBazaarContext())
			{
				return context.Categories.ToList();
			}
		}

		public List<Category> GetFeaturedCategories()
		{
			using (var context = new ApnaBazaarContext())
			{
				return context.Categories.Where( x => x.IsFeatured).ToList();
			}
		}
		public void SaveCategory(Category category)
		{
			using (var context = new ApnaBazaarContext())
			{
				context.Categories.Add(category);
				context.SaveChanges();
			}
		}

		public void UpdateCategory(Category category)
		{
			using (var context = new ApnaBazaarContext())
			{
				context.Entry(category).State = System.Data.Entity.EntityState.Modified;
				context.SaveChanges();
			}
		}

		public void DeleteCategory(int Id)
		{
			using (var context = new ApnaBazaarContext())
			{
				//context.Entry(category).State = System.Data.Entity.EntityState.Modified;
				var category = context.Categories.Where(x=>x.ID == Id).Include(p=>p.Products).FirstOrDefault();

				//First Delete All The Products in the Category
				//then Delete the Category
				context.Products.RemoveRange(category.Products);
	            context.Categories.Remove(category);

				context.SaveChanges();
			}
		}
		public List<Category> GetCategor()
		{
			using (var context = new ApnaBazaarContext())
			{
				return context.Categories.ToList();
			}
		}
	}
}