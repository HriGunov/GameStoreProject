using System.Collections.Generic;
using System.Linq;
using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Data.Models;
using GameStore.Services.Abstract;

namespace GameStore.Commands
{
    public class AddProductCommand : ICommand
    {
        private readonly IConsoleManager consoleManager;
        private readonly IEngine engine;
        private readonly IProductsService productService;

        public AddProductCommand(IEngine engine, IConsoleManager consoleManager,
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

            consoleManager.LogMessage("=== Adding Product ===", true);

            consoleManager.LogMessage("Enter Name", true);
            var productName = consoleManager.ListenForCommand();
            consoleManager.LogMessage(productName);

            consoleManager.LogMessage("Enter Description", true);
            var productDescription = consoleManager.ListenForCommand();
            consoleManager.LogMessage(productDescription);

            consoleManager.LogMessage("Enter Price", true);
            var productPrice = decimal.Parse(consoleManager.ListenForCommand());
            consoleManager.LogMessage(productPrice.ToString("0.00"));

            consoleManager.LogMessage("Enter Genres (Seperated by comma)", true);
            var tempGenres = consoleManager.ListenForCommand().Trim();
            var tempGenresSplit = tempGenres.Split(",").ToList();
            var productGenres = new List<Genre>();
            foreach (var genre in tempGenresSplit) productGenres.Add(new Genre {Name = genre});
            consoleManager.LogMessage(tempGenres);

            // Adding the product
            productService.AddProduct(productName, productDescription, productPrice, productGenres);

            return $"Product {productName} has been added successfully.";
        }
    }
}