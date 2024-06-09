using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Reports.interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.Reports
{
    public class SearchProductTransactionUseCase : ISearchProductTransactionUseCase
    {
        private readonly IProductTransactionRepository productTransactionRepository;
        private readonly UserManager<IdentityUser> userManager;

        public SearchProductTransactionUseCase(
            IProductTransactionRepository productTransactionRepository, UserManager<IdentityUser> userManager)
        {
            this.productTransactionRepository = productTransactionRepository;
            this.userManager = userManager;
        }
        public async Task<IEnumerable<ProductTransaction>> ExecuteAsync(
            string sellOrder,
            string productName,
            DateTime? dateFrom,
            DateTime? dateTo,
            ProductTransactionType? transactionType,
            string userEmail)
        {

            List<string> userIds = null;
            if (!string.IsNullOrWhiteSpace(userEmail))
            {
                var users = await userManager.Users
                    .Where(u => u.Email.Contains(userEmail))
                    .ToListAsync();

                userIds = users.Select(u => u.Id).ToList();
            }

            return await productTransactionRepository.GetProductTransactionsAsync(sellOrder, productName, dateFrom, dateTo, transactionType, userIds);

        }
    }
}
