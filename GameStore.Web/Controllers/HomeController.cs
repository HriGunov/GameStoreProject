using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Services.Abstract;
using GameStore.Web.Models;
using GameStore.Web.Models.ProductsViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public async Task<IActionResult> Index()
        {
            var productsCarousel = await productsService.SkipAndTakeLatestProductsAsync(4);
            var viewModel = productsCarousel.Select(p => new ProductListingViewModel(p)).ToList();
            return View(viewModel);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Products()
        {
            return RedirectToAction("Index", "Products");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}