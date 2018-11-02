using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Web.Models.ProductsViewModels
{
    public class ProductsPageViewModel
    {
        public ProductsPageViewModel(IEnumerable<ProductListingViewModel> productListings)
        {
            this.ProductsList = productListings;
        }

        public IEnumerable<ProductListingViewModel> ProductsList { get; set; } = new List<ProductListingViewModel>();
    }
}