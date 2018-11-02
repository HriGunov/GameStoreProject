using System;
using System.Collections.Generic;
using System.Text;
using GameStore.Data.Models;
using System.Linq;

namespace GameStore.Web.Models.ProductsViewModels
{
    public class ProductListingViewModel
    {
        public ProductListingViewModel(Product product)
        {
            this.Id = product.Id;
            this.ImageName = product.ProductImageName;
            this.Name = product.Name;
            this.Description = product.Description;
            this.Genres = product.Genre.Select(g => g.Name).ToList();
            this.Price = product.Price;
        }

        public int Id { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Genres { get; set; } = new List<string>();
        public decimal Price { get; set; }
    }
}