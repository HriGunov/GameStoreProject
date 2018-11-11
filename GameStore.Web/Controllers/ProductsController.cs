using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Data.Models;
using GameStore.Services.Abstract;
using GameStore.Web.Models.ProductsViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace GameStore.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly ICommentService _commentService;
        private readonly IProductsService _productsService;
        private readonly UserManager<Account> _userManager;

        public ProductsController(UserManager<Account> userManager, IProductsService productsService,
            ICommentService commentService, IAccountsService accountsService)
        {
            _userManager = userManager;
            _productsService = productsService;
            _commentService = commentService;
            _accountsService = accountsService;
        }

        [TempData] public string StatusMessage { get; set; }

        public async Task<IActionResult> Index(string search, int? page)
        {
            IEnumerable<Product> latestProducts;
            var pageIndex = (page ?? 1) - 1; //MembershipProvider expects a 0 for the first page 
            const int pageSize = 3; // Hardcoded view products count

            if (search != null)
            {
                latestProducts =
                    await _productsService.SkipAndTakeLatestProductsAsync(10, x => x.Name.Contains(search));
                if (latestProducts == null || !latestProducts.Any())
                {
                    latestProducts = await _productsService.SkipAndTakeLatestProductsAsync(10);
                    StatusMessage = $"No products found matching your search ({search}).";
                }
            }
            else
            {
                latestProducts = await _productsService.SkipAndTakeLatestProductsAsync(10);
            }

            var productListings = latestProducts.Select(product => new ProductListingViewModel(product)).ToList();

            var currentPage = productListings.ToPagedList(pageIndex + 1, pageSize);
            var productsAsIPagedList =
                new StaticPagedList<ProductListingViewModel>(currentPage, pageIndex + 1, pageSize,
                    productListings.Count);
            ViewBag.OnePageOfProducts = productsAsIPagedList;

            return View(productsAsIPagedList);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productsService.FindProductAsync(id);
            var viewModel = new ProductListingViewModel(product)
            {
                Comments = await _commentService.GetCommentsFromProductAsync(id)
            };

            foreach (var comment in viewModel.Comments)
                comment.Account = await _accountsService.FindAccountAsync(comment.AccountId);

            if (TempData != null && (string) TempData["StatusMessage"] != "")
                StatusMessage = (string) TempData["StatusMessage"];

            return View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> AddComment(AddCommentViewModel comment)
        {
            await _commentService.AddCommentToProductAsync(comment.ProductId, _userManager.GetUserId(User),
                comment.Text);

            return RedirectToAction("Details", "Products", new {id = comment.ProductId});
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveComment(int id, int productId)
        {
            var comment = await _commentService.RemoveComment(id);
            StatusMessage = $"Successfully removed {comment.Account.UserName}'s comment.";
            return RedirectToAction("Details", "Products", new {id = productId});
        }
    }
}