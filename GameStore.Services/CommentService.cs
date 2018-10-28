using System;
using System.Linq;
using GameStore.Data.Context.Abstract;
using GameStore.Data.Models;
using GameStore.Exceptions;
using GameStore.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Services
{
    public class CommentService : ICommentService
    {
        private readonly IAccountsService accountsService;
        private readonly IProductsService productsService;
        private readonly IGameStoreContext storeContext;

        public CommentService(IGameStoreContext storeContext, IAccountsService accountsService,
            IProductsService productsService)
        {
            this.storeContext = storeContext ?? throw new ArgumentNullException(nameof(storeContext));
            this.accountsService = accountsService;
            this.productsService = productsService;
        }

        public Comment AddCommentToProduct(string productName, string username, string commentText)
        {
            var commentor = accountsService.FindAccount(username, true);
            if (commentor == null) throw new UserException("Could not find the user who commented...");
            var productToBeCommentedTo = productsService.FindProduct(productName);
            if (productToBeCommentedTo == null) throw new UserException("Could not find product...");
            if (productToBeCommentedTo.Comments.Any(x => x.AccountId == int.Parse(commentor.Id) && x.Text == commentText))
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

            productToBeCommentedTo.Comments.Add(newComment);
            storeContext.Products.Update(productToBeCommentedTo);
            storeContext.Comments.Add(newComment);
            storeContext.SaveChanges();
            return newComment;
        }

        public void RemoveCommentsFromProduct(string productName)
        {
            var product = productsService.FindProduct(productName);
            if (product == null) throw new UserException("Could not find product.");
            foreach (var comment in product.Comments) comment.IsDeleted = true;
            storeContext.SaveChanges();
        }

        public void RemoveCommentsFromAccount(Account account)
        {
            var tempAccount = accountsService.FindAccount(account.UserName, true);
            if (tempAccount == null) throw new UserException("Could not find account.");
            foreach (var comment in tempAccount.Comments)
                // TODO: Will change it to Flag later...
                storeContext.Accounts.Include(c => c.Comments).ToList().Single(a => a.UserName == account.UserName)
                    .Comments.Remove(comment);
            storeContext.SaveChanges();
        }
    }
}