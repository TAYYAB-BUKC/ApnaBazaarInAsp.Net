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
	public class ProductService
	{
		#region Singleton
		public static ProductService Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new ProductService();
				}
				return instance;
			}
		}
		private static ProductService instance { get; set; }

		private ProductService()
		{

		}
		#endregion
		public Product GetSpecificProduct(int Id)
		{
			/*			using (var context = new ApnaBazaarContext())
						{		
							return context.Products.Find(Id);
						}
			*/
			var context = new ApnaBazaarContext();
			return context.Products.Find(Id);
		}

		public List<Product> GetListOfProduct(List<int> Ids)
		{
			using (var context = new ApnaBazaarContext())
			{
				return context.Products.Where(product => Ids.Contains(product.ID)).ToList();

			}


		}

		public List<Product> GetProducts(int pageNo)
		{
			int pageSize = 5;
			using (var context = new ApnaBazaarContext())
			{
				return context.Products.OrderBy(product => product.Name).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();
			}

			/*var context = new ApnaBazaarContext();
			return context.Products.ToList();*/

		}
		public void SaveProduct(Product product)
		{
			using (var context = new ApnaBazaarContext())
			{
				context.Entry(product.Category).State = System.Data.Entity.EntityState.Unchanged;
				context.Products.Add(product);
				context.SaveChanges();
			}
		}

		public void UpdateProduct(Product product)
		{
			using (var context = new ApnaBazaarContext())
			{
				context.Entry(product).State = System.Data.Entity.EntityState.Modified;
				//var pro = context.Products.Find(product.ID);
				//context.Entry(pro).State = System.Data.Entity.EntityState.Modified;
				context.SaveChanges();
			}
		}

		public void DeleteProduct(int Id)
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
