using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Data.Models;
using GameStore.Services.Abstract;
using System.Collections.Generic;

namespace GameStore.Commands
{
    class BuyProductCommand : ICommand
    {
        private readonly IEngine engine;
        private readonly IProductsService productsService;
        private readonly IShoppingCartsService shoppingCartsService;
        private readonly IConsoleManager consoleManager;
        private readonly ISaveContextService saveContextService;

        public BuyProductCommand(IEngine engine, IProductsService productsService, IShoppingCartsService shoppingCartsService, IConsoleManager consoleManager,
            ISaveContextService  saveContextService)
        {
            this.engine = engine;
            this.productsService = productsService;
            this.shoppingCartsService = shoppingCartsService;
            this.consoleManager = consoleManager;
            this.saveContextService = saveContextService;
        }
        public string Execute(List<string> parameters)
        {
            if (engine.CurrentUser == null|| engine.CurrentUser.IsGuest == true)
            {
                return "You need to be logged in to add products to shopping cart.";
               
            }

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

            if (productFound == null)
            {
                return "No product with that name found.";
            }

            var cartProducts = new ShoppingCartProducts() { ShoppingCartId = engine.CurrentUser.ShoppingCart.Id, ShoppingCart = engine.CurrentUser.ShoppingCart, Product = productFound,ProductId = productFound.Id,  };
            engine.CurrentUser.ShoppingCart.ShoppingCartProducts.Add(cartProducts);
            saveContextService.SaveChanges();

            return $"{productFound.Name} has been added to shoping cart.";
        }
    }
}
