using IMS.CoreBusiness;
using IMS.CoreBusiness.DTO;
using IMS.UseCases.Activities.Interfaces;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.Activities
{
    public class SellProductUseCase : ISellProductUseCase
    {
        private readonly IProductTransactionRepository productTransactionRepository;
        private readonly IProductRepository productRepository;

        public SellProductUseCase(
            IProductTransactionRepository productTransactionRepository,
            IProductRepository productRepository)
        {
            this.productTransactionRepository = productTransactionRepository;
            this.productRepository = productRepository;
        }

        public async Task ExecuteAsync(string salesOrderNumber, Product product, int quantity, double unitPrice, string userid, string useremail)
        {
            var prod = await productRepository.GetProductByIdAsync(product.ProductID);
            if (prod != null)
            {
                prod.Quantity -= quantity;
                await productRepository.UpdateProductAsync(prod);
            }

            // To add Sold Quantity & Total Price to the Report
            var transaction = new ProductTransaction
            {
                ActivityType = ProductTransactionType.SellProduct,
                SONumber = salesOrderNumber,
                ProductId = product.ProductID,
                QauntityBefore = product.Quantity,
                QauntityAfter = product.Quantity - quantity,
                TransactionDate = DateTime.Now,
                UnitPrice = unitPrice,
                SoldQuantity = quantity,
                TotalPrice = quantity * unitPrice,

                UserId = userid // Store user information
            };

            await productTransactionRepository.SellProductAsync(transaction);

        }
    }
}
