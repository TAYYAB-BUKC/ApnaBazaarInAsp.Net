using ApnaBazaar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApnaBazaar.Database
{
	public class ApnaBazaarContext : DbContext, IDisposable
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

		public DbSet<ApnaBazaarConfiguration> Configurations { get; set; }

		public DbSet<Order> Orders { get; set; }

		public DbSet<OrderItem> OrderItems { get; set; }

		public DbSet<Review> Reviews { get; set; }

	}

}
