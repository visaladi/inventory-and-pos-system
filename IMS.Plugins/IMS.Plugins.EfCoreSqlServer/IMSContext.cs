using IMS.CoreBusiness;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EfCoreSqlServer
{
    public class IMSContext : DbContext
    {

        public IMSContext(DbContextOptions<IMSContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTransaction> ProductTransactions { get; set; }
        public DbSet<Cart> Carts { get; set; } // New DbSet for Carts
        public DbSet<CartItem> CartItems { get; set; } // New DbSet for CartItems


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.Items) // Define one-to-many relationship between Carts and CartItems
                .WithOne(ci => ci.Cart) // Specify that each CartItem has one corresponding Cart
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product) // Define many-to-one relationship between CartItems and Products
                .WithMany()
                .HasForeignKey(ci => ci.ProductId);


        }

    }
}
