using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Comment>> GetCommentsFromProductAsync(int productId)
        {
            var productComments = await storeContext.Comments.Where(c => c.ProductId == productId && !c.IsDeleted).ToListAsync();
            return productComments;
        }
        public async Task<IEnumerable<Comment>> GetCommentsByUserAsync(string accountId)
        {
            var account = await storeContext.Accounts.Include(acc => acc.Comments).FirstOrDefaultAsync(acc => acc.Id == accountId);
            return account.Comments;
        }
        public async Task<Comment> AddCommentToProductAsync(int productId, string commentorId, string commentText)
        {
            var commentor = await storeContext.Accounts.Include(acc => acc.Comments).Where(acc => acc.Id == commentorId)
                .SingleAsync();
            if (commentor == null) throw new UserException("Could not find the user who commented...");

            var productToBeCommentedTo = await storeContext.Products.Include(prod => prod.Comments)
                .Where(prod => prod.Id == productId).SingleAsync();
            if (productToBeCommentedTo == null) throw new UserException("Could not find product...");

            if (productToBeCommentedTo.Comments.Any(
                x => x.AccountId == commentor.Id  && x.Text == commentText))
                throw new UserException("Cannot add duplicate comments...");

            var newComment = new Comment
            {
                AccountId = commentor.Id,
                Text = commentText,
                ProductId = productToBeCommentedTo.Id,
                TimeStamp = DateTime.Now,
                IsDeleted = false
            };
            commentor.Comments.Add(newComment);
            storeContext.Accounts.Update(commentor);

            /*  If it doesn't work, remove the comment.
            productToBeCommentedTo.Comments.Add(newComment);
            storeContext.Products.Update(productToBeCommentedTo);
            storeContext.Comments.Add(newComment);*/
            await storeContext.SaveChangesAsync();
            return newComment;
        }

        public async Task RemoveCommentsFromProductAsync(int productId)
        {
            var product = await storeContext.Products.Include(prod => prod.Comments).Where(prod => prod.Id == productId)
                .SingleAsync();
            if (product == null) throw new UserException("Could not find product.");

            foreach (var comment in product.Comments) comment.IsDeleted = true; 

            await storeContext.SaveChangesAsync();
        }

        public async Task RemoveCommentsFromAccountAsync(string accountId)
        {
            var tempAccount = await storeContext.Accounts.Include(acc => acc.Comments).Where(acc => acc.Id == accountId)
                .SingleAsync();

            if (tempAccount == null) throw new UserException("Could not find account.");
            foreach (var comment in tempAccount.Comments)
                comment.IsDeleted = true;
            
            await storeContext.SaveChangesAsync();
        }

        public async Task<Comment> RemoveComment(int id)
        {
            var comment = await storeContext.Comments.Include(a => a.Account).SingleAsync(c => c.Id == id);
            var returnComment = comment;
            storeContext.Comments.Remove(comment);

            await storeContext.SaveChangesAsync();

            return returnComment;
        }
    }
}