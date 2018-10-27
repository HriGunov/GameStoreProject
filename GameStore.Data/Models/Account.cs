using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Data.Models
{
    public class Account : IdentityUser
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        // SHA-512
        public string Password { get; set; }
        public string CreditCard { get; set; }
        public string DeletedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<Order> OrderProducts { get; set; } = new List<Order>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public bool IsDeleted { get; set; } = false;
        public bool IsGuest { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
    }
}