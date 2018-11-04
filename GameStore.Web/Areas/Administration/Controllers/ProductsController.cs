using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Web.Areas.Administration.Models;
using Microsoft.AspNetCore.Authorization;

namespace GameStore.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly GameStoreContext _context;

        public ProductsController(GameStoreContext context)
        {
            _context = context;
        }

        [TempData]
        public string StatusMessage { get; set; }

        // GET: Administration/Products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            var viewModel = new List<ProductsViewModel>();

            foreach (var product in products)
            {
                viewModel.Add(new ProductsViewModel(product));
            }

            return View(viewModel);
        }

        // GET: Administration/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var newProduct = new ProductsViewModel(product);

            return View(newProduct);
        }

        // GET: Administration/Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administration/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductsViewModel product)
        {
            if (ModelState.IsValid)
            {
                var newModel = new Product
                {
                    Name = product.Name,
                    Description = product.Description,
                    Publisher = product.Publisher,
                    Price = product.Price,
                    CreatedOn = DateTime.Now,
                    IsDeleted = product.IsDeleted,
                    IsOnSale = product.IsOnSale
                };

                _context.Add(newModel);
                await _context.SaveChangesAsync();
                this.StatusMessage = $"Successfully added {newModel.Name}";
                return RedirectToAction(nameof(Index));
            }
            this.StatusMessage = "Error: Something went wrong...";
            return View(product);
        }

        // GET: Administration/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var newProduct = new ProductsViewModel(product);

            return View(newProduct);
        }

        // POST: Administration/Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductsViewModel product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var newModel = new Product()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Publisher = product.Publisher,
                        Price = product.Price,
                        ProductImageName = product.ImageName,
                        CreatedOn = product.CreatedOn,
                        LastPurchased = product.LastPurchased,
                        IsDeleted = product.IsDeleted,
                        IsOnSale = product.IsOnSale
                    };

                    _context.Update(newModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                this.StatusMessage = $"Successfully modified {product.Name}";
                return RedirectToAction(nameof(Index));
            }
            this.StatusMessage = "Error: Something went wrong...";
            return View(product);
        }

        // GET: Administration/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var newProduct = new ProductsViewModel(product);

            return View(newProduct);
        }

        // POST: Administration/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            var productName = product.Name;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            this.StatusMessage = $"Successfully deleted {productName}";

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
