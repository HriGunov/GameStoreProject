using GameStore.Data.Models;
using GameStore.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameStore.Web.Controllers
{
    [Route("[Controller]")]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartsService _shoppingCartsService;
        private readonly IProductsService _productsService;
        private readonly UserManager<Account> userManager;
         
        public ShoppingCartController(UserManager<Account> userManager, IShoppingCartsService shoppingCartsService,IProductsService productsService)
        {
            this._shoppingCartsService = shoppingCartsService;
            this._productsService = productsService;
            this.userManager = userManager;
          

        }
        public async  Task<IActionResult> Index()
        {
            
            return View();
        }
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Add(int id)
        {
            try
            {
            if (!await  _shoppingCartsService.ProductExistsInCartAsync(id, userManager.GetUserId(this.User)))
            {
                await _shoppingCartsService.AddToCartAsync(id, userManager.GetUserId(this.User)); 
            }
                var swap = id;
                return RedirectToAction("Details","Products", new { id = swap }); 
            }
            catch (System.Exception)
            { 
                return Redirect("Error");
            } 
        }
    }
}