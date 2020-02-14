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
	public class OrderService
	{
		#region Singleton
		public static OrderService Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new OrderService();
				}
				return instance;
			}
		}
		private static OrderService instance { get; set; }

		private OrderService()
		{

		}
		#endregion

		public List<Order> SearchOrder(string searchUserId, string status, int? pageNo, int pageSize)
		{
			using (var context = new ApnaBazaarContext())
			{
				var orders = context.Orders.ToList();

				if (!string.IsNullOrEmpty(searchUserId))
				{
					orders = orders.Where(o => o.UserId.ToUpper().Contains(searchUserId.ToUpper())).ToList();
				}

				if (!string.IsNullOrEmpty(status))
				{
					orders = orders.Where(o => o.Status.ToLower().Contains(status.ToLower())).ToList();
				}

				return orders.Skip((pageNo.Value - 1) * pageSize).Take(pageSize).ToList();

			}
		}

		public int SearchOrderCount(string searchUserId, string status)
		{
			using (var context = new ApnaBazaarContext())
			{
				var orders = context.Orders.ToList();

				if (!string.IsNullOrEmpty(searchUserId))
				{
					orders = orders.Where(o => o.UserId.ToUpper().Contains(searchUserId.ToUpper())).ToList();
				}

				if (!string.IsNullOrEmpty(status))
				{
					orders = orders.Where(o => o.Status.ToLower().Contains(status.ToLower())).ToList();
				}

				return orders.Count;

			}
		}

		public Order GetOrderByID(int orderID)
		{
			using (var context = new ApnaBazaarContext())
			{
				return context.Orders.Where(o=>o.Id == orderID).Include(o=>o.OrderItems).Include("OrderItems.Product").FirstOrDefault();
			}
		}

		public bool UpdateStatus(string status, int orderID)
		{
			using (var context = new ApnaBazaarContext())
			{
				var order = context.Orders.Find(orderID);
				order.Status = status;

				context.Entry(order).State = EntityState.Modified;
				return context.SaveChanges() > 0;
			}
		}
	}
}
