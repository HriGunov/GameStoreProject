using System.Collections.Generic;
using System.Linq;
using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections.MainWindowSections.Abstract;
using GameStore.Exceptions;
using GameStore.Services.Abstract;

namespace GameStore.Commands
{
    internal class ViewCartCommand : ICommand
    {
        private readonly IEngine engine;
        private readonly IProductsSection productsSection;
        private readonly IShoppingCartsService shoppingCartService;

        public ViewCartCommand(IEngine engine, IProductsSection productsSection,
            IShoppingCartsService shoppingCartService)
        {
            this.engine = engine;
            this.productsSection = productsSection;
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
                if (parameters.Count >= 1) productsSection.SetPageTo(int.Parse(parameters[0]));
                return "Viewing cart";
            }
            catch (UserException e)
            {
                return e.Message;
            }
        }
    }
}