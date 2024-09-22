using e_commerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace e_commerce.Context
{
    public class StoreContext:IdentityDbContext<ApplicationUser>
    {

        public StoreContext()
        {
            
        }
        public StoreContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CartItems>().HasKey(x => new { x.CartId, x.ProductId });
            builder.Entity<FavItem>().HasKey(x => new { x.FavoriteId, x.ProductId });

            base.OnModelCreating(builder);
        }
        public DbSet<Product> Products { get; set; }
       public DbSet<ProductBrand> ProductBrands { get; set; }
       public DbSet<ProductType> ProductTypes { get; set; }
       public DbSet<UserCart> UserCarts { get; set; }
       public DbSet<CartItems> CartItems { get; set; }
       public DbSet<Order> Orders { get; set; }
       public DbSet<OrderItem> OrderItems { get; set; }
       public DbSet<PaymentOrder> PaymentOrders { get; set; }
        public DbSet<Favorite> Favorites { get; set;}
        public DbSet<FavItem> FavItems { get; set;}
    }
}
