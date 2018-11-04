using System.Collections.Generic;
using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IOrderService
    {
        Order AddToOrder(string accountId, IEnumerable<Product> products);
        Order AddToOrder(string accountId, int productId);
        Order FindLastOrder(string accountId);
        IEnumerable<Order> FindOrders(string accountId);
    }
}