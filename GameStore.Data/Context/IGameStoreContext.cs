using GameStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Data.Context
{
    public interface IGameStoreContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<ShoppingCart> ShoppingCarts { get; set; }
        DbSet<Order> Orders { get; set; }
        int SaveChanges();

    }
}
