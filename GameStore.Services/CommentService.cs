using GameStore.Data.Context.Abstract;
using GameStore.Data.Models;
using GameStore.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Services
{
    public class CommentService : ICommentService
    {
        private readonly IGameStoreContext storeContext;
        private readonly IAccountsService accountsService;
        private readonly IProductsService productsService;

        public CommentService(IGameStoreContext storeContext, IAccountsService accountsService, IProductsService productsService)
        {
            this.storeContext = storeContext ?? throw new ArgumentNullException(nameof(storeContext));
            this.accountsService = accountsService;
            this.productsService = productsService;
        }

        public Comment AddCommentToProduct(string productName, string username, string commentText)
        {
            var commentor = accountsService.FindAccount(username);
            if (commentor == null)
            {
                throw new ArgumentException("Could not find commentor");
            }
            var productToBeCommentedTo = productsService.FindProduct(productName);
            if (productToBeCommentedTo == null)
            {
                throw new ArgumentException("Could not find product");
            }
            if (productToBeCommentedTo.Comments.Any(x => x.AccountId == commentor.Id && x.Text == commentText))
            {
                throw new ArgumentException("Cannot add duplicate comments.");
            }
            var newComment = new Comment { AccountId = commentor.Id, Text = commentText, ProductId = productToBeCommentedTo.Id, TimeStamp = DateTime.Now, IsDeleted = false };
            storeContext.Comments.Add(newComment);
            storeContext.SaveChanges();
            return newComment;
        }

        public void RemoveCommentsFromProduct(string productName)
        {
            var product = productsService.FindProduct(productName);
            if (product == null)
            {
                throw new ArgumentException("Could not find product.");
            }
            foreach (var comment in product.Comments)
            {
                comment.IsDeleted = true;
            }
            storeContext.SaveChanges();
        }

        public void RemoveCommentsFromAccount(Account account)
        {
            var tempAccount = accountsService.FindAccount(account.Username);
            if (tempAccount == null)
            {
                throw new ArgumentException("Could not find account.");
            }
            foreach (var comment in tempAccount.Comments)
            {
                // TODO: Will change it to Flag later...
                storeContext.Accounts.Include(c => c.Comments).ToList().Single(a => a.Username == account.Username).Comments.Remove(comment);
            }
            storeContext.SaveChanges();
        }
    }
}