using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections.MainWindowSections;
using GameStore.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Commands
{
    class ViewCartCommand : ICommand
    {
        private readonly IEngine engine;
        private readonly IProductsSection productsSection;
        private readonly IConsoleManager consoleManager;

        public ViewCartCommand(IEngine engine, IProductsSection productsSection, IConsoleManager consoleManager)
        {
            this.engine = engine;
            this.productsSection = productsSection;
            this.consoleManager = consoleManager;
        }

        public string Execute(List<string> parameters)
        {
            try
            {


                if (engine.CurrentUser == null || engine.CurrentUser.IsGuest)
                    return "You need to be logged in to add view your shopping cart.";
                engine.MainSection = productsSection;
                productsSection.ChnageTitle("Shopping Cart");

                productsSection.UpdateProducts(engine.CurrentUser.ShoppingCart.ShoppingCartProducts.Select(cart => cart.Product));
                return "Viewing cart";
            }
            catch (UserException e)
            {

                return e.Message;
            }
        }
    }
}
