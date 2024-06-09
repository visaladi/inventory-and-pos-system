using IMS.CoreBusiness;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace IMS.WebApp.Components.Pages.Reports
{
    public partial class ProductTransactionReport
    {
        private string sellOrder = string.Empty;
        private string prodName = string.Empty;
        private DateTime? dateFrom;
        private DateTime? dateTo;
        private int activityTypeId = 2;
        private string selectedUserEmail = string.Empty;
        private IEnumerable<ProductTransaction>? productTransactions;
        private List<string> userEmails = new List<string>();
        private Dictionary<string, string> userEmailsDictionary = new Dictionary<string, string>();

        protected override async Task OnInitializedAsync()
        {
            userEmails = await LoadUserEmails();
            await SearchProducts();
        }

        private async Task<List<string>> LoadUserEmails()
        {
            var users = await UserManager.Users.ToListAsync();
            return users.Select(u => u.Email).ToList();
        }

        private async Task<string> GetUserEmail(string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);
            return user?.Email ?? "Unknown";
        }

        private async Task SearchProducts()
        {
            ProductTransactionType? prodType = (ProductTransactionType)activityTypeId;
            productTransactions = await SearchProductTransactionUseCase.ExecuteAsync(sellOrder, prodName, dateFrom, dateTo, prodType, selectedUserEmail);

            userEmailsDictionary.Clear();
            foreach (var transaction in productTransactions)
            {
                if (!userEmailsDictionary.ContainsKey(transaction.UserId))
                {
                    var email = await GetUserEmail(transaction.UserId);
                    userEmailsDictionary[transaction.UserId] = email;
                }
            }
        }

    }
}
