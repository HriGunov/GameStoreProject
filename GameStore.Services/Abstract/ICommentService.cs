using GameStore.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStore.Services.Abstract
{
    public interface ICommentService
    {
        Task<Comment> AddCommentToProductAsync(int productId, string commentorId, string commentText);
        Task RemoveCommentsFromAccountAsync(string accountId);
        Task RemoveCommentsFromProductAsync(int productId);
        Task<IEnumerable<Comment>> GetCommentsByUserAsync(string accountId);
        Task<IEnumerable<Comment>> GetCommentsFromProductAsync(int productId);
    }
}