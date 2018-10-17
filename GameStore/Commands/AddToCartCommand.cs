using System.Collections.Generic;
using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Services.Abstract;

namespace GameStore.Commands
{
    internal class AddToCartCommand : ICommand
    {
        private readonly IConsoleManager consoleManager;
        private readonly IEngine engine;
        private readonly IProductsService productsService;
        private readonly IShoppingCartsService shoppingCartsService;

        public AddToCartCommand(IEngine engine, IProductsService productsService,
            IShoppingCartsService shoppingCartsService, IConsoleManager consoleManager)
        {
            this.engine = engine;
            this.productsService = productsService;
            this.shoppingCartsService = shoppingCartsService;
            this.consoleManager = consoleManager;
        }

        public string Execute(List<string> parameters)
        {
            if (engine.CurrentUser == null || engine.CurrentUser.IsGuest)
                return "You need to be logged in to add products to shopping cart.";

            string nameOfProduct;

            if (parameters.Count >= 1)
            {
                nameOfProduct = string.Join(' ', parameters);
            }
            else
            {
                consoleManager.LogMessage("Enter the name of the product you want to buy.");
                nameOfProduct = consoleManager.ListenForCommand();
            }

            var productFound = productsService.FindProduct(nameOfProduct);

            if (productFound == null)
                return "No product found to add...";

            shoppingCartsService.AddToCart(productFound, engine.CurrentUser);

            return $"({productFound.Name}) has been added to your shopping cart.";
        }
    }
}