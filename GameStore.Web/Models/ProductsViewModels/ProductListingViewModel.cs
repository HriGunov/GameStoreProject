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
            this.Publisher = product.Publisher;
            this.Genres = product.Genre.Select(g => g.Name).ToList();
            this.Price = product.Price;
            this.CreatedOn = product.CreatedOn;
            this.LastPurchased = product.LastPurchased;
            this.IsDeleted = product.IsDeleted;
            this.Comments = product.Comments.ToList();
        }

        public int Id { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastPurchased { get; set; }
        public bool IsDeleted { get; set; }
    }
}