using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using GameStore.Data.Models;

namespace GameStore.Web.Areas.Administration.Models
{
    public class ProductsViewModel
    {
        public ProductsViewModel()
        {
        }

        public ProductsViewModel(Product product)
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
            IsOnSale = product.IsOnSale;
        }

        public int Id { get; set; }

        [Display(Name = "Image Name")] public string ImageName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [StringLength(512)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Text)] public string Publisher { get; set; }

        public IEnumerable<string> Genres { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        [DataType(DataType.DateTime)] public DateTime? LastPurchased { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsOnSale { get; set; }
    }
}