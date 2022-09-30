using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Data
{
    public class OnlineShopDbContext : IdentityDbContext<AppUser>
    {
        public OnlineShopDbContext(DbContextOptions<OnlineShopDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<CartItem>();

            modelBuilder.Entity<Purchases>().HasKey(p => new { p.UserId, p.ProductId });
        }

        public DbSet<Page> Pages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> Cart { get; set; }
        public DbSet<Purchases> Purchases { get; set; }

    }
}
