using IMS.CoreBusiness;
using IMS.CoreBusiness.DTO;
using IMS.Plugins.EfCoreSqlServer;
using IMS.Plugins.EFCoreSqlServer;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Plugins.EFCoreSqlServer
{
    public class ProductEFCoreRepository : IProductRepository
    {
        private readonly IDbContextFactory<IMSContext> contextFactory;

        public ProductEFCoreRepository(IDbContextFactory<IMSContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task AddProductAsync(Product product)
        {
            using var db = this.contextFactory.CreateDbContext();

            db.Products.Add(new Product
            {
                ProductName = product.ProductName,
                Price = product.Price,
                Quantity = product.Quantity,
                ImgUrl = product.ImgUrl
            });

            await db.SaveChangesAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            using var db = contextFactory.CreateDbContext();
            return await Task.FromResult(db.Products.FirstOrDefault(x => x.ProductID == productId));
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            using var db = contextFactory.CreateDbContext();
            return await db.Products.Where(x => x.ProductName.ToLower()
                .IndexOf(name.ToLower()) >= 0).ToListAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            using var db = contextFactory.CreateDbContext();
            var prod = await db.Products.FirstOrDefaultAsync(x => x.ProductID == product.ProductID);

            if (prod != null)
            {
                prod.ProductName = product.ProductName;
                prod.Price = product.Price;
                prod.Quantity = product.Quantity;
                prod.ImgUrl = product.ImgUrl;

                //prod.BranchQty = product.BranchQty;                             // Added this new line

                await db.SaveChangesAsync();
            }
        }



    }
}
