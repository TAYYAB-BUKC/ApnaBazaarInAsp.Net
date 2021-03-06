﻿using ApnaBazaar.Database;
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

		public List<Order> SearchOrder(string searchUserId, int? pageNo, int pageSize)
		{
			using (var context = new ApnaBazaarContext())
			{
				var orders = context.Orders.ToList();

				if (!string.IsNullOrEmpty(searchUserId))
				{
					orders = orders.Where(o => o.UserId.ToUpper().Contains(searchUserId.ToUpper()) || o.Status.ToLower().Contains(searchUserId.ToLower())).ToList();
				}

				return orders.Skip((pageNo.Value - 1) * pageSize).Take(pageSize).ToList();

			}
		}

		public int SearchOrderCount(string searchUserId)
		{
			using (var context = new ApnaBazaarContext())
			{
				var orders = context.Orders.ToList();

				if (!string.IsNullOrEmpty(searchUserId))
				{
					orders = orders.Where(o => o.UserId.ToUpper().Contains(searchUserId.ToUpper()) || o.Status.ToLower().Contains(searchUserId.ToLower())).ToList();
				}


				return orders.Count;

			}
		}

		public int ShowUserOrderCount(string UserId, string SearchTerm)
		{
			using (var context = new ApnaBazaarContext())
			{
				var orders = context.Orders.Where(o=>o.UserId == UserId).ToList();

				if (!string.IsNullOrEmpty(SearchTerm))
				{
					orders = orders.Where(o => o.Status.ToLower().Contains(SearchTerm.ToLower())).ToList();
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

		public List<Order> ShowUserOrder(string SearchTerm, string status,int? pageNo, int pageSize)
		{
			using (var context = new ApnaBazaarContext())
			{
				var orders = context.Orders.ToList();

				if (!string.IsNullOrEmpty(SearchTerm))
				{
					orders = orders.Where(o => o.UserId.ToUpper().Contains(SearchTerm.ToUpper())).ToList();
				}
				if (!string.IsNullOrEmpty(status))
				{
					orders = orders.Where(o => o.Status.ToUpper().Contains(status.ToUpper())).ToList();
				}


				return orders.Skip((pageNo.Value - 1) * pageSize).Take(pageSize).ToList();
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
