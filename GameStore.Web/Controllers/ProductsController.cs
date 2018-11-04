using GameStore.Services.Abstract;
using GameStore.Web.Models.ProductsViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public async Task<IActionResult> Index()
        {
            var productListings = new List<ProductListingViewModel>();
            var latestProducts = await productsService.SkipAndTakeLatestProductsAsync(10);
            foreach (var product in latestProducts)
            {
                productListings.Add(new ProductListingViewModel(product));
            }

            return View(productListings);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await this.productsService.FindProductAsync(id);

            var viewModel = new ProductListingViewModel(product);

            return View(viewModel);
        }
    }
}