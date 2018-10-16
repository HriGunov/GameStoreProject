using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Data.Context.Abstract;
using GameStore.Data.Models;
using GameStore.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGameStoreContext storeContext;

        public OrderService(IGameStoreContext storeContext)
        {
            this.storeContext = storeContext ?? throw new ArgumentNullException(nameof(storeContext));
        }

        public Order AddToOrder(Account account, Product product)
        {
            var tempOrder = CreateOrder(account);

            var newOrderProducts = new OrderProducts
            {
                OrderId = tempOrder.Id,
                ProductId = product.Id
            };
            storeContext.OrdersProducts.Add(newOrderProducts);
            storeContext.SaveChanges();
            return FindLastOrder(account);
        }

        public Order AddToOrder(Account account, IEnumerable<Product> product)
        {
            CreateOrder(account);

            foreach (var tempProduct in product)
            {
                var tempOrder = FindLastOrder(account);
                if (tempOrder.OrderProducts.Any(p => p.ProductId == tempProduct.Id)) continue;

                var newOrderProducts = new OrderProducts
                {
                    OrderId = tempOrder.Id,
                    ProductId = tempProduct.Id
                };
                storeContext.OrdersProducts.Add(newOrderProducts);
            }

            storeContext.SaveChanges();
            return FindLastOrder(account);
        }

        public IEnumerable<Order> FindOrders(Account account)
        {
            return GetOrders().Where(o => o.AccountId == account.Id);
        }

        public Order FindLastOrder(Account account)
        {
            return GetOrders().LastOrDefault(o => o.AccountId == account.Id);
        }

        /// <summary>
        ///     Create a new blank order for the given account.
        ///     For 'private' usage.
        /// </summary>
        /// <param name="account">Account (type)</param>
        /// <returns></returns>
        private Order CreateOrder(Account account)
        {
            var newOrder = new Order
            {
                AccountId = account.Id,
                OrderTimestamp = DateTime.Now
            };
            storeContext.Orders.Add(newOrder);
            storeContext.SaveChanges();

            return FindLastOrder(account);
        }

        private IEnumerable<Order> GetOrders()
        {
            return storeContext.Orders
                .Include(a => a.Account)
                .Include(op => op.OrderProducts)
                .ThenInclude(o => o.Order)
                .Include(op => op.OrderProducts)
                .ThenInclude(p => p.Product)
                .ToList();
        }
    }
}