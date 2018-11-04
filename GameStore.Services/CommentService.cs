using System;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Exceptions;
using GameStore.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Services
{
    public class CommentService : ICommentService
    {
        private readonly GameStoreContext storeContext;

        public CommentService(GameStoreContext storeContext)
        {
            this.storeContext = storeContext ?? throw new ArgumentNullException(nameof(storeContext));
        }

        public async Task<Comment> AddCommentToProduct(int productId, string commentorId, string commentText)
        {
            var commentor = await storeContext.Accounts.Include(acc => acc.Comments).Where(acc => acc.Id == commentorId)
                .SingleAsync();
            if (commentor == null) throw new UserException("Could not find the user who commented...");

            var productToBeCommentedTo = await storeContext.Products.Include(prod => prod.Comments)
                .Where(prod => prod.Id == productId).SingleAsync();
            if (productToBeCommentedTo == null) throw new UserException("Could not find product...");

            if (productToBeCommentedTo.Comments.Any(
                x => x.AccountId == int.Parse(commentor.Id) && x.Text == commentText))
                throw new UserException("Cannot add duplicate comments...");

            var newComment = new Comment
            {
                AccountId = int.Parse(commentor.Id),
                Text = commentText,
                ProductId = productToBeCommentedTo.Id,
                TimeStamp = DateTime.Now,
                IsDeleted = false
            };
            commentor.Comments.Add(newComment);
            storeContext.Accounts.Update(commentor);

            /*  Ako ne raboti mahni komentara
            productToBeCommentedTo.Comments.Add(newComment);
            storeContext.Products.Update(productToBeCommentedTo);
            storeContext.Comments.Add(newComment);*/
            await storeContext.SaveChangesAsync();
            return newComment;
        }

        public async Task RemoveCommentsFromProduct(int productId)
        {
            var product = await storeContext.Products.Include(prod => prod.Comments).Where(prod => prod.Id == productId)
                .SingleAsync();
            if (product == null) throw new UserException("Could not find product.");

            foreach (var comment in product.Comments) comment.IsDeleted = true;
            storeContext.Update(storeContext.Products);

            await storeContext.SaveChangesAsync();
        }

        public async Task RemoveCommentsFromAccount(string accountId)
        {
            var tempAccount = await storeContext.Accounts.Include(acc => acc.Comments).Where(acc => acc.Id == accountId)
                .SingleAsync();

            if (tempAccount == null) throw new UserException("Could not find account.");
            foreach (var comment in tempAccount.Comments)
                comment.IsDeleted = true;
            storeContext.Update(storeContext.Accounts);
            await storeContext.SaveChangesAsync();
        }
    }
}