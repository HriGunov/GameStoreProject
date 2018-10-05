﻿using System.Collections.Generic;

namespace GameStore.Data.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        // SHA-256
        public string Password { get; set; }
        public string CreditCard { get; set; }

        ICollection<Comment> Comments { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsAdmin { get; set; }
    }
}