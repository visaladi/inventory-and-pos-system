namespace IMS.WebApp.Components.Pages.Products
{
    public partial class ProductList
    {
        private string searchTerm = string.Empty;

        private void OnSearchProduct(string searchTerm)
        {
            this.searchTerm = searchTerm;
        }

        private void AddProduct()
        {
            NavigationManager.NavigateTo("/addproduct");
        }
    }
}
