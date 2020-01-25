using ApnaBazaar.Database;
using ApnaBazaar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApnaBazaar.Services
{
	public class CategoriesService
	{
		public Category GetSpecificCategory(int Id)
		{
			using (var context = new ApnaBazaarContext())
			{
				return context.Categories.Find(Id);
			}
		}
		public List<Category> GetCategories()
		{
			using (var context = new ApnaBazaarContext())
			{
				return context.Categories.ToList();
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
				var category = context.Categories.Find(Id);
				context.Categories.Remove(category);
				context.SaveChanges();
			}
		}
	}
}