using System.Collections.Generic;
using System.Linq;
using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections.MainWindowSections;
using GameStore.Services.Abstract;

namespace GameStore.Commands
{
    public class ViewProductsCommand : ICommand
    {
        private readonly IEngine engine;
        private readonly IProductsSection productsSection;
        private readonly IProductsService productsService;

        public ViewProductsCommand(IEngine engine, IProductsSection productsSection, IProductsService productsService)
        {
            this.engine = engine;
            this.productsSection = productsSection;
            this.productsService = productsService;
        }

        public string Execute(List<string> parameters)
        {
            // Get All Products
            var products = productsService.GetProducts();
            if (products == null || !products.Any()) return "No products are currently available.";

            productsSection.UpdateProducts(products);
            engine.MainSection = productsSection;

            return $"{products.Count()} Products found.";
        }
    }
}