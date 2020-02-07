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
		/*protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>()
				.Property(p => p.Name)
				.HasMaxLength(50)
				.IsRequired();
		}*/

		public DbSet<Category> Categories { get; set; }

		public DbSet<Product> Products { get; set; }


		public DbSet<Configuration> Configurations { get; set; }

	}
}
