using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface ICommentService
    {
        Comment AddCommentToProduct(string productName, string username, string commentText);
        void RemoveCommentsFromProduct(string productName);
        void RemoveCommentsFromAccount(Account account);
    }
}