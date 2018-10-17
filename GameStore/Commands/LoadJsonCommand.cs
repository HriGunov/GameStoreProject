using System;
using System.Collections.Generic;
using System.IO;
using GameStore.Commands.Abstract;
using GameStore.Services.Abstract;

namespace GameStore.Commands
{
    internal class LoadJSONCommand : ICommand
    {
        private readonly IProductsService productsService;

        public LoadJSONCommand(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public string Execute(List<string> parameters)
        {
            try
            {
                var testJson = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "NewProducts.json");
                productsService.LoadProductsLoadedFromJSON(testJson);
            }
            catch (Exception)
            {
                return "Loading of products has failed.";
            }

            return "Loading of products has succeeded.";
        }
    }
}