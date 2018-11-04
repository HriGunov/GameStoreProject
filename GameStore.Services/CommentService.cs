using System;
using System.Linq;
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

        public Comment AddCommentToProduct(int productId, string commentorId, string commentText)
        {
            var commentor = storeContext.Accounts.Include(acc => acc.Comments).Where(acc => acc.Id == commentorId)
                .Single();
            if (commentor == null) throw new UserException("Could not find the user who commented...");

            var productToBeCommentedTo = storeContext.Products.Include(prod => prod.Comments)
                .Where(prod => prod.Id == productId).Single();
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
            storeContext.SaveChanges();
            return newComment;
        }

        public void RemoveCommentsFromProduct(int productId)
        {
            var product = storeContext.Products.Include(prod => prod.Comments).Where(prod => prod.Id == productId)
                .Single();
            if (product == null) throw new UserException("Could not find product.");

            foreach (var comment in product.Comments) comment.IsDeleted = true;
            storeContext.Update(storeContext.Products);

            storeContext.SaveChanges();
        }

        public void RemoveCommentsFromAccount(string accountId)
        {
            var tempAccount = storeContext.Accounts.Include(acc => acc.Comments).Where(acc => acc.Id == accountId)
                .Single();

            if (tempAccount == null) throw new UserException("Could not find account.");
            foreach (var comment in tempAccount.Comments)
                comment.IsDeleted = true;
            storeContext.Update(storeContext.Accounts);
            storeContext.SaveChanges();
        }
    }
}