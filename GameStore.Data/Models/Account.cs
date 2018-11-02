using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using GameStore.Data.Models.Abstract;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Data.Models
{
    public class Account : IdentityUser, IAuditable, IDeletable
    {
        public string AvatarImageName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
         
        public string CreditCard { get; set; }
        public string DeletedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        public ICollection<Order> OrderProducts { get; set; } = new List<Order>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; } = new ShoppingCart();

        public bool IsDeleted { get; set; } = false;
        public bool IsGuest { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
    }
}