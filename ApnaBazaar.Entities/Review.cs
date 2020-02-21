using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApnaBazaar.Entities
{
	public class Review
	{
		public int ID { get; set; }

		public string UserId { get; set; }

		public string Name { get; set; }

		public string Image { get; set; }

		public int ProductId { get; set; }

		public virtual Product Product { get; set; }

		public string Comment { get; set; }

		public float Rating { get; set; }

	}
}
