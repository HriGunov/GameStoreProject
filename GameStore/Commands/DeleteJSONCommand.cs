using System;
using System.Collections.Generic;
using System.IO;
using GameStore.Commands.Abstract;
using GameStore.Services.Abstract;

namespace GameStore.Commands
{
    internal class DeleteJSONCommand : ICommand
    {
        private readonly IProductsService productsService;

        public DeleteJSONCommand(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public string Execute(List<string> parameters)
        {
            try
            {
                var testJson = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "NewProducts.json");
                productsService.DeleteProductsLoadedFromJSON(testJson);
            }
            catch (Exception)
            {
                return "Removing of products has failed.";
            }

            return "Removing of products has succeeded.";
        }
    }
}