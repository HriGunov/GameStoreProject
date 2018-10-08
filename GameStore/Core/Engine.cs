using GameStore.Commands;
using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Services;
using System;

namespace GameStore.Core
{
    public class Engine : IEngine
    {
        private readonly ICommandManager commandManager;
        private readonly IGameStoreContext gameStoreContext;

        public Engine(IGameStoreContext gameStoreContext, ICommandManager commandManager)
        {
            this.gameStoreContext = gameStoreContext;
            this.commandManager = commandManager;

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

            commandManager.Execute("SiGnUp");

            string input;
            do
            {
                input = Console.ReadLine();
                commandManager.Execute("Login test1 test1");
            } while (input != "end");
        }
    }
}