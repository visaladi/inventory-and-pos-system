using IMS.CoreBusiness;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCoreSqlServer
{
    public class IMSContext: DbContext
    {
        public IMSContext(DbContextOptions<IMSContext> options): base(options)
        {
            
        }

        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<ProductTransaction> ProductTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductInventory>().HasKey(pi => new { pi.ProductId, pi.InventoryId });

            modelBuilder.Entity<ProductInventory>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductInventories)
                .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity<ProductInventory>()
                .HasOne(pi => pi.Inventory)
                .WithMany(p => p.ProductInventories)
                .HasForeignKey(pi => pi.InventoryId);

            //seeding data
            modelBuilder.Entity<Inventory>().HasData(
                new Inventory { InventoryID = 1, InventoryName = "Bike Seat", Quantity = 10, Price = 2 },
                new Inventory { InventoryID = 2, InventoryName = "Bike Body", Quantity = 10, Price = 15 },
                new Inventory { InventoryID = 3, InventoryName = "Bike Wheels", Quantity = 20, Price = 8 },
                new Inventory { InventoryID = 4, InventoryName = "Bike Pedals", Quantity = 20, Price = 1 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product() { ProductID = 1, ProductName = "Bike", Quantity = 10, Price = 150 },
                new Product() { ProductID = 2, ProductName = "Car", Quantity = 5, Price = 25000 }
            );

            modelBuilder.Entity<ProductInventory>().HasData(
                new ProductInventory { ProductId = 1, InventoryId = 1, InventoryQuantity = 1 }, // Seats for a Bike
                new ProductInventory { ProductId = 1, InventoryId = 2, InventoryQuantity = 1 }, // Body for aa Bike
                new ProductInventory { ProductId = 2, InventoryId = 3, InventoryQuantity = 2 }, // Wheels for a Car
                new ProductInventory { ProductId = 2, InventoryId = 4, InventoryQuantity = 2 }  // Pedels for a Car
            );
        }
    }
}
