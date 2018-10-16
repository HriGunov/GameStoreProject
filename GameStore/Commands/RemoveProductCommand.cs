using System.Collections.Generic;
using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Services.Abstract;

namespace GameStore.Commands
{
    public class RemoveProductCommand : ICommand
    {
        private readonly IConsoleManager consoleManager;
        private readonly IEngine engine;
        private readonly IProductsService productService;

        public RemoveProductCommand(IEngine engine, IConsoleManager consoleManager,
            IProductsService productService)
        {
            this.engine = engine;
            this.consoleManager = consoleManager;
            this.productService = productService;
        }

        public string Execute(List<string> parameters)
        {
            if (engine.CurrentUser == null)
                return "You must be logged in...";

            if (!engine.CurrentUser.IsAdmin)
                return "You don't have enough permissions.";

            consoleManager.LogMessage("=== Removing Product ===", true);

            consoleManager.LogMessage("Enter Name", true);
            var tempProduct = consoleManager.ListenForCommand();
            consoleManager.LogMessage(tempProduct);

            // Removing the product
            productService.RemoveProduct(tempProduct);

            return $"Product {tempProduct} has been removed successfully.";
        }
    }
}