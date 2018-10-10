using GameStore.Commands;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections;
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
        private IConsoleManager consoleManager;
        private readonly IGameStoreContext gameStoreContext;

        public Engine(IGameStoreContext gameStoreContext, ICommandManager commandManager,
            ICommentService commentService)
        {
            this.gameStoreContext = gameStoreContext;
            this.commandManager = commandManager;
            this.commentService = commentService;
          
        }

        public Account CurrentUser { get; set; }

        public void Run()
        {
            consoleManager = new ConsoleManager(this);
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
            //var comment = commentService.AddCommentToProduct("Banana", "dngrozdanov", "Top KeK");
            //commentService.RemoveCommentsFromAccount(accounts.FindAccount("hrigunov"));
            // var tempProduct = products.FindProduct("Banana");
            // var tempAccount = accounts.FindAccount("hrigunov");
            // var tempAccount2 = accounts.FindAccount("dngrozdanov");
            //var tempCart = shoppingCarts.AddToCart(tempProduct, tempAccount);
            //var tempCart2 = shoppingCarts.AddToCart(tempProduct, tempAccount2);
            //var acc = accounts.FindAccount("dngrozdanov");
            // var acc2 = accounts.FindAccount("hrigunov");
            var mockAcc = new Account() { FirstName = "Hristo", LastName = "Gunov", Username = "hrigunov", Password = "hritest", IsGuest = false };
            string line;
            int counter = 0;
            while ((line = consoleManager.ListenForCommand()) != "end")
            {
                consoleManager.SetText(line, counter,0);

                var nameSection = new TopLeftCornerUserSection(consoleManager,0,0);
                nameSection.ImprintOnConsoleMatrix(mockAcc);

                consoleManager.Print();

                // commandManager.Execute(line);
                counter++;
            }
        }
    }
}