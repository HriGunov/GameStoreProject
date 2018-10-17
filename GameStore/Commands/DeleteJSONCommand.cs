
using GameStore.Commands.Abstract;
using GameStore.Services.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameStore.Commands
{
    class DeleteJSONCommand : ICommand
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
                var testJson =  File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "NewProducts.json");
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
