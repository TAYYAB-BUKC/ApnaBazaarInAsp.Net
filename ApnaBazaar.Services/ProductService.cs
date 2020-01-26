using ApnaBazaar.Database;
using ApnaBazaar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApnaBazaar.Services
{
	public class ProductService
	{
		public Product GetSpecificProduct(int Id)
		{
			using (var context = new ApnaBazaarContext())
			{
				return context.Products.Find(Id);
			}
		}
		public List<Product> GetProducts()
		{
			using (var context = new ApnaBazaarContext())
			{
				return context.Products.ToList();
			}
		}
		public void SaveProduct(Product product)
		{
			using (var context = new ApnaBazaarContext())
			{
				context.Products.Add(product);
				context.SaveChanges();
			}
		}

		public void UpdateProduct(Product product)
		{
			using (var context = new ApnaBazaarContext())
			{
				context.Entry(product).State = System.Data.Entity.EntityState.Modified;
				context.SaveChanges();
			}
		}

		public void DeleteCategory(int Id)
		{
			using (var context = new ApnaBazaarContext())
			{
				//context.Entry(category).State = System.Data.Entity.EntityState.Modified;
				var product = context.Products.Find(Id);
				context.Products.Remove(product);
				context.SaveChanges();
			}
		}
	}
}
