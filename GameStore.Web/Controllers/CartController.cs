using System;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Data.Models;
using GameStore.Services.Abstract;
using GameStore.Web.Models.ProductsViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly IShoppingCartsService _shoppingCartsService;
        private readonly UserManager<Account> userManager;

        public CartController(UserManager<Account> userManager, IShoppingCartsService shoppingCartsService,
            IProductsService productsService)
        {
            _shoppingCartsService = shoppingCartsService;
            _productsService = productsService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var shoppingCart = await _shoppingCartsService.GetUserCartAsync(userManager.GetUserId(User));
            var viewProducts = shoppingCart.ShoppingCartProducts
                .Select(product => new ProductListingViewModel(product.Product)).ToList();
            return View(viewProducts);
        }

        [Authorize]
        public async Task<IActionResult> Add(int id)
        {
            try
            {
                if (!await _shoppingCartsService.ProductExistsInCartAsync(id, userManager.GetUserId(User)))
                {
                    await _shoppingCartsService.AddToCartAsync(id, userManager.GetUserId(User));
                    TempData["StatusMessage"] = "Successfully added to your cart.";
                }
                else
                {
                    TempData["StatusMessage"] = "Error: You already have this product in your cart.";
                }

                var swap = id;
                return RedirectToAction("Details", "Products", new {id = swap});
            }
            catch (Exception)
            {
                return Redirect("Error");
            }
        }

        [Authorize]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var product = await _shoppingCartsService.RemoveProductCartAsync(userManager.GetUserId(User), id);
            TempData["StatusMessage"] = $"Successfully removed ({product.Name}) from your cart.";
            return RedirectToAction("Index", "Cart");
        }
    }
}