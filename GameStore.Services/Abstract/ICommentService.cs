using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface ICommentService
    {
        Comment AddCommentToProduct(int productId, string commentorId, string commentText);
        void RemoveCommentsFromAccount(string accountId);
        void RemoveCommentsFromProduct(int productId);
    }
}