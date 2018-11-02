using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Web.Models.ProductsViewModels
{
    public class ProductListingViewModel
    {
        public string ImageName { get; set; }
        public string Name { get; set; }        
        public decimal Price { get; set; }
    }
}
