using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelajarNextJsBackEnd.Entities
{
    public class ApplicationDbContext : DbContext, IDataProtectionKeyContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();

        public DbSet<Restaurant> Restaurants => Set<Restaurant>();

        public DbSet<FoodItem> FoodItems => Set<FoodItem>();

        public DbSet<Cart> Carts => Set<Cart>();

        public DbSet<CartDetail> CartDetails => Set<CartDetail>();

        public DbSet<DataProtectionKey> DataProtectionKeys => Set<DataProtectionKey>();
    }
}
