using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApnaBazaar.Entities
{
	public class Product : BaseEntity
	{
		//use Inheritance for similiar Properties
		public decimal Price { get; set; }
		public Category Category { get; set; }

	}
}
