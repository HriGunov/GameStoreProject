using GameStore.Services.Abstract;
using GameStore.Web.Models.ProductsViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public IActionResult Index()
        {
            List<ProductListingViewModel> productListings = new List<ProductListingViewModel>();
            var latestProducts = productsService.SkipAndTakeLatestProducts(10);
            foreach (var product in latestProducts)
            {
                productListings.Add(new ProductListingViewModel(product));
            }

            return View(new ProductsPageViewModel(productListings));
        }

        public IActionResult Details(int id)
        {
            var product = this.productsService.FindProductById(id);

            var viewModel = new ProductListingViewModel(product);

            return View(viewModel);
        }
    }
}