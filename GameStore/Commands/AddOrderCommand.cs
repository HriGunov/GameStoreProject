using System.Collections.Generic;
using System.Linq;
using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Data.Models;
using GameStore.Services.Abstract;

namespace GameStore.Commands
{
    public class AddOrderCommand : ICommand
    {
        private readonly IAccountsService accountsService;
        private readonly IConsoleManager consoleManager;
        private readonly IEngine engine;
        private readonly IOrderService orderService;
        private readonly IProductsService productService;

        public AddOrderCommand(IEngine engine, IConsoleManager consoleManager, IOrderService orderService,
            IProductsService productService, IAccountsService accountsService)
        {
            this.engine = engine;
            this.consoleManager = consoleManager;
            this.orderService = orderService;
            this.productService = productService;
            this.accountsService = accountsService;
        }

        public string Execute(List<string> parameters)
        {
            if (engine.CurrentUser == null)
                return "You must be logged in...";

            if (parameters.Count < 2)
                return "You're missing arguments (User) or (Product/s).";

            var user = accountsService.FindAccount(parameters[0]);

            if (user == null)
                return $"No User ({parameters[0]}) found...";

            var tempCollection = new List<Product>();

            if (parameters.Count >= 2)
            {
                foreach (var tempProduct in parameters.Skip(1))
                    productService.GetProducts().ToList().ForEach(p =>
                    {
                        if (p.Name == tempProduct)
                            tempCollection.Add(p);
                    });
            }
            else
            {
                var tempProduct = productService.FindProduct(parameters[1]);
                if (tempProduct != null)
                    tempCollection.Add(tempProduct);
            }

            if (!tempCollection.Any())
                return "No products found...";

            var tempOrder = orderService.AddToOrder(user, tempCollection);

            return
                $"Successfully made an order containing the following products: {string.Join(", ", tempCollection.Select(p => p.Name).Distinct())}";
        }
    }
}