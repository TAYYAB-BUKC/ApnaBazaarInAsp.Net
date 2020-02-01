using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApnaBazaar.Entities
{
	public class Category : BaseEntity
	{
		//use Inheritance for similiar Properties
		
		public List<Product> Products { get; set; }
		public bool IsFeatured { get; set; }



	}
}
