using System.Collections.Generic;
using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Services.Abstract;

namespace GameStore.Commands
{
    public class RemoveGenreCommand : ICommand
    {
        private readonly IConsoleManager consoleManager;
        private readonly IEngine engine;
        private readonly IProductsService productService;

        public RemoveGenreCommand(IEngine engine, IConsoleManager consoleManager,
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

            consoleManager.LogMessage("=== Removing Genre ===", true);

            consoleManager.LogMessage("Enter Product Name", true);
            var tempProduct = consoleManager.ListenForCommand();
            consoleManager.LogMessage(tempProduct);

            consoleManager.LogMessage("Enter Genre", true);
            var tempGenre = consoleManager.ListenForCommand();
            consoleManager.LogMessage(tempGenre);

            // Removing the product
            productService.RemoveGenreFromProduct(tempGenre, productService.FindProduct(tempProduct));

            return $"Genre {tempGenre} from {tempProduct} has been removed successfully.";
        }
    }
}