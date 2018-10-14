using System.Collections.Generic;
using System.Linq;
using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Data.Models;
using GameStore.Exceptions;
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

            var tempProductsList = new List<Product>();

            if (parameters.Count >= 1)
            {
                foreach (var param in parameters) tempProductsList.Add(productsService.FindProduct(param));
            }
            else
            {
                consoleManager.LogMessage("Enter the name of the product you want to buy.");
                tempProductsList.Add(productsService.FindProduct(consoleManager.ListenForCommand()));
            }

            if (!tempProductsList.Any())
                return "No products found to add...";

            try
            {
                shoppingCartsService.AddToCart(tempProductsList, engine.CurrentUser);
            }
            catch (UserException e )
            {

                return e.Message;
            }
            
            return $"({string.Join(", ", tempProductsList.Select(p => p.Name))}) has been added to your shopping cart.";
        }
    }
}