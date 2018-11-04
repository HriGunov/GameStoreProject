using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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
            this.IsOnSale = product.IsOnSale;
        }

        public int Id { get; set; }
        [Display(Name = "Image Name")]
        public string ImageName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [StringLength(512)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.Text)]
        public string Publisher { get; set; }
        public IEnumerable<string> Genres { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? LastPurchased { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsOnSale { get; set; }
    }
}