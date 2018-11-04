using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IOrderService
    {
        Task<Order> AddToOrder(string accountId, IEnumerable<Product> products);
        Task<Order> AddToOrder(string accountId, int productId);
        Task<Order> FindLastOrder(string accountId);
        Task<IEnumerable<Order>> FindOrders(string accountId);
    }
}