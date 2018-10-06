using System;
using GameStore.Commands;
using GameStore.Data.Context;
using GameStore.Services;

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

        public void Run()
        {
            /*var accounts = new AccountsService(gameStoreContext);
            var products = new ProductsService(gameStoreContext);
            var shoppingCarts = new ShoppingCartsService(gameStoreContext);
            products.AddProduct("Banana", "Golden Banana to Eat", 10);
            var tempProduct = products.FindProducts("Banana");
            var tempAccount = accounts.FindAccount("hrigunov");
            var tempAccount2 = accounts.FindAccount("dngrozdanov");
            var tempCart = shoppingCarts.AddToCart(tempProduct, tempAccount);
            var tempCart2 = shoppingCarts.AddToCart(tempProduct, tempAccount2);*/

            string input;
            while ((input = Console.ReadLine()) != "end")
            {
                var commandExecute = commandManager.Execute(input);
            }
        }
    }
}