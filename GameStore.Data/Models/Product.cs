using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Data.Models
{
    class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public int GenreTypeId { get; set; }
        public GenreType Genre { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsOnSale { get; set; }


    }
}
