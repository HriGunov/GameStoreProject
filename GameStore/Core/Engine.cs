using GameStore.Commands;
using GameStore.Core.Abstract;
using GameStore.Data.Context;
using GameStore.Data.Context.Abstract;
using GameStore.Data.Models;
using GameStore.Services;
using GameStore.Services.Abstract;
using System;

namespace GameStore.Core
{
    public class Engine : IEngine
    {
        private readonly ICommandManager commandManager;
        private readonly ICommentService commentService;
        private readonly IGameStoreContext gameStoreContext;

        public Engine(IGameStoreContext gameStoreContext, ICommandManager commandManager, ICommentService commentService)
        {
            this.gameStoreContext = gameStoreContext;
            this.commandManager = commandManager;
            this.commentService = commentService;
        }

        public Account CurrentUser { get; set; }

        public void Run()
        {
            var accounts = new AccountsService(gameStoreContext);
            var products = new ProductsService(gameStoreContext);
            var shoppingCarts = new ShoppingCartsService(gameStoreContext);
            /*   //products.AddProduct("Normal Banana", "Normal Banana to Eat", 2);
               //accounts.AddAccount("Danail", "Grozdanov", "dngrozdanov", "dntest", true);
               //accounts.AddAccount("Hristo", "Gunov", "hrigunov", "hritest", true);
               //accounts.AddAccount("Guest", "Guest", "guest", "guest", false, true);
               var tempProduct = products.FindProducts("Normal Banana");
               var tempAccount = accounts.FindAccount("hrigunov");
               var tempAccount2 = accounts.FindAccount("dngrozdanov");
               var tempCart = shoppingCarts.AddToCart(tempProduct, tempAccount);
               var tempCart2 = shoppingCarts.AddToCart(tempProduct, tempAccount2);
               */
          //  commentService.AddCommentToProduct("Banana", "hrigunov", "Top KeK");
            string line;
            while ((line = Console.ReadLine()) != "end")
            {
                commandManager.Execute(line);
            }
        }
    }
}