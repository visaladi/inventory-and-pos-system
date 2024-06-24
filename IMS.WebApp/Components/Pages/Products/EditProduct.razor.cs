using IMS.CoreBusiness;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace IMS.WebApp.Components.Pages.Products
{
    public partial class EditProduct
    {
        [Parameter]
        public int ProdId { get; set; }

        private Product? product = new Product();

        private List<IBrowserFile> loadedFiles = new();

        protected override async Task OnParametersSetAsync()
        {
            this.product = await ViewProductByIdUseCase.ExecuteAsync(this.ProdId);
        }

        private async Task SaveProduct()
        {
            if (this.product != null)
            {
                await EditProductUseCase.ExecuteAsync(this.product);
            }
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