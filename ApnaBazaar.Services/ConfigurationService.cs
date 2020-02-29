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
	public class ConfigurationService
	{
		#region Singleton
		public static ConfigurationService Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new ConfigurationService();
				}
				return instance;
			}
		}
		private static ConfigurationService instance { get; set; }

		private ConfigurationService()
		{

		}
		#endregion
		public ApnaBazaarConfiguration GetConfiguration(string key)
		{
			using (var context = new ApnaBazaarContext())
			{
				var config = context.Configurations.Where(configuration => configuration.Key == key).FirstOrDefault();
				return config != null ? config : new ApnaBazaarConfiguration();
				return context.Configurations.FirstOrDefault(configuration => configuration.Key == key);
				return context.Configurations.Find(key);

			}
		}

		public List<ApnaBazaarConfiguration> GetSearchConfigurations(string search, int pageNo)
		{
			int pageSize = GetNormalPageSizeConfiguration();
			using (var context = new ApnaBazaarContext())
			{
				if (!String.IsNullOrEmpty(search))
				{
					return context.Configurations
						.Where(configuration => configuration.Key != null && configuration.Key.ToUpper().Contains(search.ToUpper()))
						.OrderBy(configuration => configuration.Key)
						.Skip((pageNo - 1) * pageSize)
						.Take(pageSize)
						.ToList();

				}
				else
				{
					return context.Configurations
						.OrderBy(configuration => configuration.Key)
						.Skip((pageNo - 1) * pageSize)
						.Take(pageSize)
						.ToList();
				}
			}
		}

		public void SaveConfiguration(ApnaBazaarConfiguration configuration)
		{
			using (var context = new ApnaBazaarContext())
			{
				context.Configurations.Add(configuration);
				context.SaveChanges();
			}
		}

		public int GetConfigurationsCount(string search)
		{
			using (var context = new ApnaBazaarContext())
			{
				if (!String.IsNullOrEmpty(search))
				{
					return context.Configurations
						.Where(configuration => configuration.Key != null && configuration.Key.ToUpper().Contains(search.ToUpper()))
						.OrderBy(product => product.Key)
						.Count();
				}
				else
				{
					return context.Configurations.Count();
				}
			}
		}

		public void DeleteConfiguration(string key)
		{
			using (var context = new ApnaBazaarContext())
			{
				//context.Entry(category).State = System.Data.Entity.EntityState.Modified;
				var configuration = context.Configurations.Find(key);
				context.Configurations.Remove(configuration);
				context.SaveChanges();
			}
		}

		public void UpdateConfiguration(ApnaBazaarConfiguration configuration)
		{
			using (var context = new ApnaBazaarContext())
			{
				context.Entry(configuration).State = EntityState.Modified;
				context.SaveChanges();
			}
		}

		public int GetNormalPageSizeConfiguration()
		{
			using (var context = new ApnaBazaarContext())
			{
				var pageSize = context.Configurations.Find("ListingPageSize");
				return pageSize != null ? int.Parse(pageSize.Value) : 5;
			}
		}

		public int GetShopPageSizeConfiguration()
		{
			using (var context = new ApnaBazaarContext())
			{
				var pageSize = context.Configurations.Find("ShopPageSize");
				return pageSize != null ? int.Parse(pageSize.Value) : 12;
			}
		}




	}
}
