using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections.MainWindowSections;
using GameStore.Exceptions;
using System.Collections.Generic;
using System.Linq;
using GameStore.Services.Abstract;

namespace GameStore.Commands
{
    class ViewCartCommand : ICommand
    {
        private readonly IEngine engine;
        private readonly IProductsSection productsSection;
        private readonly IConsoleManager consoleManager;
        private readonly IShoppingCartsService shoppingCartService;

        public ViewCartCommand(IEngine engine, IProductsSection productsSection, IConsoleManager consoleManager, IShoppingCartsService shoppingCartService)
        {
            this.engine = engine;
            this.productsSection = productsSection;
            this.consoleManager = consoleManager;
            this.shoppingCartService = shoppingCartService;
        }

        public string Execute(List<string> parameters)
        {
            try
            {
                if (engine.CurrentUser == null || engine.CurrentUser.IsGuest)
                    return "You need to be logged in to add view your shopping cart.";

                var tempCart = shoppingCartService.GetUserCart(engine.CurrentUser);

                if (tempCart.ShoppingCartProducts == null || !tempCart.ShoppingCartProducts.Any())
                    return "You need to have products in your shopping cart.";

                engine.MainSection = productsSection;
                productsSection.ChangeTitle("Shopping Cart");

                productsSection.UpdateProducts(tempCart.ShoppingCartProducts.Select(cart => cart.Product));
                return "Viewing cart";
            }
            catch (UserException e)
            {
                return e.Message;
            }
        }
    }
}