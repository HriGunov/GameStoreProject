﻿using System.Collections.Generic;

namespace GameStore.Data.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public ICollection<ShoppingCartProducts> ShoppingCartProducts { get; set; } = new List<ShoppingCartProducts>();
    }
}