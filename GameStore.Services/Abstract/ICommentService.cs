using GameStore.Data.Models;
using System.Threading.Tasks;

namespace GameStore.Services.Abstract
{
    public interface ICommentService
    {
        Task<Comment> AddCommentToProduct(int productId, string commentorId, string commentText);
        Task RemoveCommentsFromAccount(string accountId);
        Task RemoveCommentsFromProduct(int productId);
    }
}