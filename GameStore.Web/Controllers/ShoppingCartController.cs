using GameStore.Data.Models;
using GameStore.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameStore.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartsService _shoppingCartsService;
        private readonly UserManager<Account> userManager;

        public ShoppingCartController(UserManager<Account> userManager, IShoppingCartsService shoppingCartsService)
        {
            this._shoppingCartsService = shoppingCartsService;
            this.userManager = userManager;
        }
        public async  Task<IActionResult> Index()
        {
            var userId =  userManager.GetUserId(this.User);
            var cart = await _shoppingCartsService.GetUserCart(userId);
                
            return View();
        }
    }
}