using ApnaBazaar.Database;
using ApnaBazaar.Entities;
using System;
using System.Collections.Generic;
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
		public Configuration GetConfiguration(string key)
		{
			using (var context = new ApnaBazaarContext())
			{
				return context.Configurations.Where(configuration => configuration.Key == key).FirstOrDefault();
				return context.Configurations.FirstOrDefault(configuration => configuration.Key == key);
				return context.Configurations.Find(key);

			}
		}

	}
}
