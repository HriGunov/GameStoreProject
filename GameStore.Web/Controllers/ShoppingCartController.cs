using System;
using System.Threading.Tasks;
using GameStore.Data.Models;
using GameStore.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    [Route("[Controller]")]
    public class ShoppingCartController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly IShoppingCartsService _shoppingCartsService;
        private readonly UserManager<Account> userManager;

        public ShoppingCartController(UserManager<Account> userManager, IShoppingCartsService shoppingCartsService,
            IProductsService productsService)
        {
            _shoppingCartsService = shoppingCartsService;
            _productsService = productsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Route("[Action]/{id}")]
        public async Task<IActionResult> Add(int id)
        {
            try
            {
                if (!await _shoppingCartsService.ProductExistsInCartAsync(id, userManager.GetUserId(User)))
                    await _shoppingCartsService.AddToCartAsync(id, userManager.GetUserId(User));
                var swap = id;
                return RedirectToAction("Details", "Products", new {id = swap});
            }
            catch (Exception)
            {
                return Redirect("Error");
            }
        }
    }
}