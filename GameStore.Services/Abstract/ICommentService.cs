using GameStore.Data.Models;

namespace GameStore.Services
{
    public interface ICommentService
    {
        Comment AddCommentToProduct(int productId, string commentorId, string commentText);
        void RemoveCommentsFromAccount(string accountId);
        void RemoveCommentsFromProduct(int productId);
    }
}