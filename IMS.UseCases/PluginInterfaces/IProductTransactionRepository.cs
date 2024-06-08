using IMS.CoreBusiness;
using IMS.CoreBusiness.DTO;

namespace IMS.UseCases.PluginInterfaces
{
    public interface IProductTransactionRepository
    {
        Task<IEnumerable<ProductTransaction>> GetProductTransactionsAsync(
            string productName,
            DateTime? dateFrom,
            DateTime? dateTo,
            ProductTransactionType? transactionType,
            List<string> userIds);

        Task SellProductAsync(ProductTransaction transaction);
    }
}