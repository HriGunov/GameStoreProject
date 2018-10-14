using System.Collections.Generic;
using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IOrderService
    {
        Order AddToOrder(Account account, Product product);
        Order AddToOrder(Account account, IEnumerable<Product> product);
        IEnumerable<Order> FindOrders(Account account);
    }
}