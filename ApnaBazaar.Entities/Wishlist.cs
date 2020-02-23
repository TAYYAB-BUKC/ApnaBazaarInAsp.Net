using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApnaBazaar.Entities
{
	public class Wishlist
	{
		public int Id { get; set; }
		public string UserID { get; set; }
		public int ProductId { get; set; }
		public virtual Product Product { get; set; }

	}
}
