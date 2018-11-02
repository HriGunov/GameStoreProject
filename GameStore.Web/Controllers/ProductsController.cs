using GameStore.Services.Abstract;
using GameStore.Web.Models.ProductsViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            var latestProducts = productsService.SkipAndTakeLatestProducts(0, 9);
            foreach (var product in latestProducts)
            {
                productListings.Add(new ProductListingViewModel()
                {
                    Name = product.Name,
                    Price = product.Price,
                    ImageName = product.ProductImageName
                });
            }
            return View(new ProductsPageViewModel()
            {
                ProductsToList = productListings
            });
            
        }
    }
}