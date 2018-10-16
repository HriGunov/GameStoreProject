using System.Collections.Generic;
using System.Linq;
using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Data.Models;
using GameStore.Services.Abstract;

namespace GameStore.Commands
{
    public class CheckoutCommand : ICommand
    {
        private readonly IAccountsService accountsService;
        private readonly IConsoleManager consoleManager;
        private readonly IEngine engine;
        private readonly IOrderService orderService;
        private readonly IShoppingCartsService shoppingCartService;

        public CheckoutCommand(IEngine engine, IConsoleManager consoleManager, IOrderService orderService,
            IProductsService productService, IAccountsService accountsService,
            IShoppingCartsService shoppingCartService)
        {
            this.engine = engine;
            this.consoleManager = consoleManager;
            this.orderService = orderService;
            this.accountsService = accountsService;
            this.shoppingCartService = shoppingCartService;
        }

        public string Execute(List<string> parameters)
        {
            if (engine.CurrentUser == null)
                return "You must be logged in...";

            var user = engine.CurrentUser;

            if (!user.ShoppingCart.ShoppingCartProducts.Any())
                return "No products found in your cart...";

            var tempCollection = new List<Product>();

            foreach (var product in user.ShoppingCart.ShoppingCartProducts)
                if (product != null)
                    tempCollection.Add(product.Product);

            if (!tempCollection.Any())
                return "No products found in your shopping cart.";

            var tempOrder = orderService.AddToOrder(user, tempCollection);

            consoleManager.LogMessage(
                $"Successfully made an order containing the following products: {string.Join(", ", tempCollection.Select(p => p.Name).Distinct())}");

            string cardNumber;
            if (string.IsNullOrEmpty(user.CreditCard))
            {
                consoleManager.LogMessage("Please enter your credit card's last four digits/letters");
                cardNumber = consoleManager.ListenForCommand();
                consoleManager.LogMessage(new string('*', cardNumber.Length));
                accountsService.AddCreditCard(cardNumber, user);
            }
            else
            {
                cardNumber = user.CreditCard;
            }

            consoleManager.LogMessage(
                $"Your credit card was charged {tempCollection.Sum(p => p.Price).ToString("0.00")} BGN");
            consoleManager.LogMessage($"Order {orderService.FindLastOrder(user).Id} has been completed.");
            shoppingCartService.ClearUserCart(user);

            return "";
        }
    }
}