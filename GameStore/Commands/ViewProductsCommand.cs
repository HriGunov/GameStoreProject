using System.Collections.Generic;
using System.Linq;
using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections.MainWindowSections.Abstract;
using GameStore.Data.Models;
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
            var products = productsService.GetProducts().Where(product => product.IsDeleted ==false);


            if (products == null || !products.Any())
            {
                productsSection.UpdateProducts(new List<Product>());
                engine.MainSection = productsSection;
                productsSection.ChangeTitle("Products");
                return "No products are currently available."; 
            }

            productsSection.UpdateProducts(products);
            if (parameters.Count>= 1)
            {
                productsSection.SetPageTo(int.Parse(parameters[0]));
            }
            engine.MainSection = productsSection;
            productsSection.ChangeTitle("Products");
            return $"{products.Count()} Products found.";
        }
    }
}