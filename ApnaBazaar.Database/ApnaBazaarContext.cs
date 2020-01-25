using ApnaBazaar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApnaBazaar.Database
{
	public class ApnaBazaarContext : DbContext
	{
		public ApnaBazaarContext() : base("ApnaBazaarConnectionString")
		{

		}
		public DbSet<Category> Categories { get; set; }

		public DbSet<Product> Products { get; set; }
		
	}
}
