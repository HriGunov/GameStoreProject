using GameStore.Data.Context.Abstract;
using GameStore.Data.Models;
using GameStore.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Services
{
    public class CommentService : ICommentService
    {
        private readonly IGameStoreContext storeContext;
        private readonly IAccountsService accountsService;
        private readonly IProductsService productsService;

        public CommentService(IGameStoreContext storeContext, IAccountsService accountsService,IProductsService productsService)
        {
            this.storeContext = storeContext ?? throw new ArgumentNullException(nameof(storeContext));
            this.accountsService = accountsService;
            this.productsService = productsService;
        }

        public Comment AddCommentToProduct(string productName,string username,string commentText)
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
            var newComment = new Comment() { Account = commentor, Text = commentText, TimeStamp = DateTime.Now, IsDeleted = false };
            productToBeCommentedTo.Comments.Add(newComment);
            storeContext.SaveChanges();
            return newComment;
        }

        public void RemoveCommentsFromProduct(string productName)
        {
            var product = productsService.FindProduct(productName);
            if (product == null)
            {
                throw new ArgumentException("Could not find product");
            }
            foreach (var comment in product.Comments)
            {
                comment.IsDeleted = true;
            }
        }
    }
}
