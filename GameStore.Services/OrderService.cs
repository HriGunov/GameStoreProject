using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly GameStoreContext storeContext;

        public OrderService(GameStoreContext storeContext)
        {
            this.storeContext = storeContext ?? throw new ArgumentNullException(nameof(storeContext));
        }
         
        public Order AddToOrder(string accountId, int productId)
        {
            var tempOrder = CreateOrder(accountId);

            var newOrderProducts = new OrderProducts
            {
                OrderId = tempOrder.Id,
                ProductId = productId
            };
            storeContext.OrdersProducts.Add(newOrderProducts);
            storeContext.SaveChanges();
            return tempOrder;
        }

        public Order AddToOrder(string accountId, IEnumerable<Product> products)
        {
            CreateOrder(accountId);
            Order tempOrder = null;
            foreach (var tempProduct in products)
            {

                tempOrder = FindLastOrder(accountId);
                if (tempOrder.OrderProducts.Any(p => p.ProductId == tempProduct.Id)) continue;

                var newOrderProducts = new OrderProducts
                {
                    OrderId = tempOrder.Id,
                    ProductId = tempProduct.Id
                };
                storeContext.OrdersProducts.Add(newOrderProducts);
            }

            storeContext.SaveChanges();
            return tempOrder;
        }

        public IEnumerable<Order> FindOrders(string accountId)
        {
            return storeContext.Orders.Where(o => o.AccountId == accountId).ToList();
        }

        public Order FindLastOrder(string accountId)
        {
            return storeContext.Orders.LastOrDefault(o => o.AccountId == accountId);
        }

        /// <summary>
        ///     Create a new blank order for the given account.
        ///     For 'private' usage.
        /// </summary>
        /// <param name="account">Account (type)</param>
        /// <returns></returns>
        private Order CreateOrder(string accountId)
        {
            var newOrder = new Order
            {
                AccountId = accountId,
                OrderTimestamp = DateTime.Now
            };
            storeContext.Orders.Add(newOrder);
            storeContext.SaveChanges();

            return newOrder;
        }
  
    }
}