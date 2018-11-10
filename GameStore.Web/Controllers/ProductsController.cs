using GameStore.Data.Models;
using GameStore.Services.Abstract;
using GameStore.Web.Models.ProductsViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly UserManager<Account> _userManager;
        private readonly IProductsService _productsService;
        private readonly ICommentService _commentService;
        private readonly IAccountsService _accountsService;

        public ProductsController(UserManager<Account> userManager, IProductsService productsService, ICommentService commentService,IAccountsService accountsService)
        {
            this._userManager = userManager;
            this._productsService = productsService;
            this._commentService = commentService;
            this._accountsService = accountsService;
        }

        public async Task<IActionResult> Index(string search)
        {
            IEnumerable<Product> latestProducts;

            if (search != null)
            {
                latestProducts = await _productsService.SkipAndTakeLatestProductsAsync(10, x => x.Name.Contains(search));
                if (latestProducts == null || !latestProducts.Any())
                {
                    latestProducts = await _productsService.SkipAndTakeLatestProductsAsync(10);
                    TempData["Message"] = $"No products found matching your search ({search}).";
                }
            }
            else
            {
                latestProducts = await _productsService.SkipAndTakeLatestProductsAsync(10);
            }
            
            var productListings = new List<ProductListingViewModel>();
            foreach (var product in latestProducts)
            {
                productListings.Add(new ProductListingViewModel(product));
            }

            return View(productListings);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await this._productsService.FindProductAsync(id);
            var viewModel = new ProductListingViewModel(product);
            viewModel.Comments =  await _commentService.GetCommentsFromProductAsync(id);

            foreach (var comment in viewModel.Comments)
            {
                
                comment.Account  = await _accountsService.FindAccountAsync(comment.AccountId);
            }

            return View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> AddComment(AddCommentViewModel comment)
        {
            await _commentService.AddCommentToProductAsync(comment.ProductId, _userManager.GetUserId(this.User), comment.Text);

            return RedirectToAction("Details", "Products", new { id = comment.ProductId });
        }
    }
}