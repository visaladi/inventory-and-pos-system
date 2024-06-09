using IMS.CoreBusiness;
using IMS.CoreBusiness.DTO;
using IMS.Plugins.EfCoreSqlServer;
using IMS.Plugins.EFCoreSqlServer;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Plugins.EFCoreSqlServer
{
    public class ProductTransactionEFCoreRepository : IProductTransactionRepository
    {
        private readonly IProductRepository productRepository;
        private readonly IDbContextFactory<IMSContext> contextFactory;

        public ProductTransactionEFCoreRepository(
            IProductRepository productRepository,
            IDbContextFactory<IMSContext> contextFactory)
        {
            using var db = contextFactory.CreateDbContext();
            this.productRepository = productRepository;
            this.contextFactory = contextFactory;
        }

        public async Task SellProductAsync(ProductTransaction transaction)
        {
            using var db = contextFactory.CreateDbContext();
            db.ProductTransactions.Add(transaction);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductTransaction>> GetProductTransactionsAsync(
            string sellOrder, string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType, List<string> userIds)
        {
            using var db = contextFactory.CreateDbContext();
            var query = db.ProductTransactions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(sellOrder))
            {
                query = query.Where(pt => pt.SONumber.Contains(sellOrder));
            }
            if (!string.IsNullOrWhiteSpace(productName))
            {
                query = query.Where(pt => pt.Product.ProductName.Contains(productName));
            }
            if (dateFrom.HasValue)
            {
                query = query.Where(pt => pt.TransactionDate >= dateFrom.Value);
            }
            if (dateTo.HasValue)
            {
                query = query.Where(pt => pt.TransactionDate <= dateTo.Value);
            }
            if (transactionType.HasValue)
            {
                query = query.Where(pt => pt.ActivityType == transactionType.Value);
            }


            if (userIds != null && userIds.Any())
            {
                query = query.Where(pt => userIds.Contains(pt.UserId));
            }

            return await query.Include(pt => pt.Product).ToListAsync();

        }
    }
}
