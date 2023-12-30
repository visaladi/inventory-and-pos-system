using IMS.CoreBusiness;
using IMS.Plugins.EFCoreSqlServer;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMSContext db;

        public ProductEFCoreRepository(IMSContext db)
        {
            this.db = db;
        }

        public async Task AddProductAsync(Product product)
        {
            this.db.Products.Add(product);

            //if(product.ProductInventories != null &&
            //   product.ProductInventories.Count > 0)
            //{
            //    foreach(var prodInv in  product.ProductInventories)
            //    {
            //        if(prodInv.Inventory != null)
            //        {
            //            this.db.Entry(prodInv.Inventory).State = EntityState.Unchanged;
            //        }
            //    }
            //}

            FLagInventoryUnchanges(product, this.db);
            await this.db.SaveChangesAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await this.db.Products.Include(x => x.ProductInventories)
                .ThenInclude(x => x.Inventory)
                .FirstOrDefaultAsync(x => x.ProductID == productId);
        }
        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            return await this.db.Products.Where(
                x => x.ProductName.ToLower().IndexOf(name.ToLower()) >= 0).ToListAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            var prod = await this.db.Products
                .Include(x => x.ProductInventories)
                .FirstOrDefaultAsync(x => x.ProductID == product.ProductID);

            if (prod != null)
            {
                prod.ProductName = product.ProductName;
                prod.Price = product.Price;
                prod.Quantity = product.Quantity;
                prod.ProductInventories = product.ProductInventories;

                FLagInventoryUnchanges(product, this.db);

                await this.db.SaveChangesAsync();
            }
        }

        private void FLagInventoryUnchanges(Product product, IMSContext db)
        {
            if (product.ProductInventories != null &&
                               product.ProductInventories.Count > 0)
            {
                foreach (var prodInv in product.ProductInventories)
                {
                    if (prodInv.Inventory != null)
                    {
                        this.db.Entry(prodInv.Inventory).State = EntityState.Unchanged;
                    }
                }
            }
        }
    }
}
