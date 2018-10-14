using System.Collections.Generic;
using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Data.Models;
using GameStore.Services.Abstract;

namespace GameStore.Commands
{
    internal class BuyProductCommand : ICommand
    {
        private readonly IConsoleManager consoleManager;
        private readonly IEngine engine;
        private readonly IProductsService productsService;
        private readonly ISaveContextService saveContextService;
        private readonly IShoppingCartsService shoppingCartsService;

        public BuyProductCommand(IEngine engine, IProductsService productsService,
            IShoppingCartsService shoppingCartsService, IConsoleManager consoleManager,
            ISaveContextService saveContextService)
        {
            this.engine = engine;
            this.productsService = productsService;
            this.shoppingCartsService = shoppingCartsService;
            this.consoleManager = consoleManager;
            this.saveContextService = saveContextService;
        }

        public string Execute(List<string> parameters)
        {
            if (engine.CurrentUser == null || engine.CurrentUser.IsGuest)
                return "You need to be logged in to add products to shopping cart.";

            var productName = "";
            if (parameters.Count == 1)
            {
                productName = parameters[0];
            }
            else
            {
                consoleManager.LogMessage("Enter name of product you want to buy.");
                productName = consoleManager.ListenForCommand();
            }

            var productFound = productsService.FindProduct(productName);

            if (productFound == null) return "No product with that name found.";

             var cartProducts = new ShoppingCartProducts
             {
                 ShoppingCartId = engine.CurrentUser.ShoppingCart.Id, ShoppingCart = engine.CurrentUser.ShoppingCart,
                 Product = productFound, ProductId = productFound.Id
             };

             shoppingCartsService.AddToCart(productFound,engine.CurrentUser);
             

            return $"{productFound.Name} has been added to shoping cart.";
        }
    }
}