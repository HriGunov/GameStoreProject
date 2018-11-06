using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IOrderService
    {
        Task<Order> AddToOrderAsync(string accountId, IEnumerable<Product> products);
        Task<Order> AddToOrderAsync(string accountId, int productId);
        Task<Order> FindLastOrderAsync(string accountId);
        Task<IEnumerable<Order>> FindOrdersAsync(string accountId);
    }
}