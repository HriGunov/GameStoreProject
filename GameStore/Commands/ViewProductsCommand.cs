using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections.MainWindowSections;
using GameStore.Data.Models;
using GameStore.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStore.Commands
{
    class ViewProductsCommand : ICommand
    {
        private readonly IEngine engine;
        private readonly IProductsSection productsSection;
        private readonly IProductsService productsService;

        public ViewProductsCommand(IEngine engine,IProductsSection productsSection, IProductsService productsService)
        {
            this.engine = engine;
            this.productsSection = productsSection;
            this.productsService = productsService;
        }
        public string Execute(List<string> parameters)
        {
            //get all
            IEnumerable<Product> products = productsService.GetProducts(); 
            if (products == null || products.Count()==0 )
            {
                return "No products are currently available.";
            }
            
            productsSection.UpdateProducts(products);
            engine.MainSection = productsSection;

            return $"Products {products.Count()} found.";
        }
    }
}
