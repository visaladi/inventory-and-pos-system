namespace IMS.WebApp.Components.Pages.Products
{
    public partial class ProductList
    {
        // private List<Product> products = new List<Product>();

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
