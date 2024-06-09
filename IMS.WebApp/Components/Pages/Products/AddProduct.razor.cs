using IMS.CoreBusiness;
using Microsoft.AspNetCore.Components.Forms;

namespace IMS.WebApp.Components.Pages.Products
{
    public partial class AddProduct
    {
        private Product product = new Product();

        private List<IBrowserFile> loadedFiles = new();

        private async Task SaveProduct()
        {
            await AddProductUseCase.ExecuteAsync(product);
            NavigationManager.NavigateTo("/products");
        }

        private void Cancel()
        {
            NavigationManager.NavigateTo("/products");
        }

        private async Task LoadFiles(InputFileChangeEventArgs e)
        {

            loadedFiles.Clear();

            var file = e.File;
            if (file != null)
            {
                var buffer = new byte[file.Size];
                await file.OpenReadStream().ReadAsync(buffer);
                product.ImgUrl = $"data:{file.ContentType};base64,{Convert.ToBase64String(buffer)}";
            }

        }


    }
}
