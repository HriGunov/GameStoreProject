using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Data.Models;

namespace GameStore.Web.Models.ProductsViewModels
{
    public class ProductListingViewModel
    {
        public ProductListingViewModel(Product product)
        {
            Id = product.Id;
            ImageName = product.ProductImageName;
            Name = product.Name;
            Description = product.Description;
            Publisher = product.Publisher;
            Genres = product.Genre.Select(g => g.Name).ToList();
            Price = product.Price;
            CreatedOn = product.CreatedOn;
            LastPurchased = product.LastPurchased;
            IsDeleted = product.IsDeleted;
            Comments = product.Comments.ToList();
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