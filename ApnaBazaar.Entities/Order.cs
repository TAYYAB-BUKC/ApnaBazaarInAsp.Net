using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApnaBazaar.Entities
{
	public class Order
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public DateTime OrderedAt { get; set; }
		public decimal TotalAmount { get; set; }
		public string Status { get; set; }

		public List<OrderItem> OrderItems { get; set; }

	}
}
