using IMS.CoreBusiness;
using Microsoft.JSInterop;

namespace IMS.WebApp.Components.Pages.Reports
{
    public partial class ProductTransactionReport
    {
        private string prodName = string.Empty;
        private DateTime? dateFrom;
        private DateTime? dateTo;
        private int activityTypeId = 2;

        private IEnumerable<ProductTransaction>? productTransactions;

        private async Task SearchProducts()
        {
            ProductTransactionType? prodType = (ProductTransactionType)activityTypeId;

            productTransactions = await SearchProductTransactionUseCase.ExecuteAsync(prodName, dateFrom, dateTo, prodType);
        }

        private void PrintReport()
        {
            JSRuntime.InvokeVoidAsync("print");
        }
    }
}
