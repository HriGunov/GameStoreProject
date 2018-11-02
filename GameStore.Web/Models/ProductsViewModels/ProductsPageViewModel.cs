using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Web.Models.ProductsViewModels
{
    public class ProductsPageViewModel
    {
        public IEnumerable<ProductListingViewModel> ProductsToList { get; set; } = new List<ProductListingViewModel>();
    }
}
