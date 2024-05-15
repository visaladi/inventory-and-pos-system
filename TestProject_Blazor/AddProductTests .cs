//using Bunit;
//using IMS.CoreBusiness;
//using IMS.Plugins.EFCoreSqlServer;
//using IMS.UseCases.PluginInterfaces;
//using IMS.UseCases.Products;
//using IMS.UseCases.Products.interfaces;
//using IMS.WebApp.Components.Pages.Products;
//using Microsoft.Extensions.DependencyInjection;
//using Xunit;

//namespace TestProject_Blazor
//{
//    public class AddProductTests : TestContext
//    {
//        [Fact]
//        public void CanAddProduct()
//        {
//            // Arrange
//            var product = new Product
//            {
//                ProductName = "Test Product",
//                Quantity = 10,
//                Price = 20.0,
//                ImgUrl = "test_image_url"
//            };

//            var productRepository = new MockProductRepository(); // Implement a mock repository or use a real one if suitable for your test environment
//            Services.AddSingleton<IProductRepository>(productRepository);
//            Services.AddSingleton<IAddProductUseCase, AddProductUseCase>();

//            var cut = RenderComponent<AddProduct>();

//            // Act
//            //cut.Find("#name").Change(product.ProductName);
//            //cut.Find("#quantity").Change(product.Quantity.ToString());
//            //cut.Find("#price").Change(product.Price.ToString());
//            //cut.Find("#image").Change(product.ImgUrl);
//            //cut.Find("form").Submit();
//            // Act
//            var nameInput = cut.Find("#name");
//            nameInput.SetAttribute("value", product.ProductName);
//            nameInput.Change("Test");

//            var quantityInput = cut.Find("#quantity");
//            quantityInput.SetAttribute("value", product.Quantity.ToString());
//            quantityInput.Change("Test");

//            var priceInput = cut.Find("#price");
//            priceInput.SetAttribute("value", product.Price.ToString());
//            priceInput.Change("Test");

//            var imageInput = cut.Find("#image");
//            imageInput.SetAttribute("value", product.ImgUrl);
//            imageInput.Change("Test");

//            cut.Find("form").Submit();


//            // Assert
//            Assert.Single(productRepository.AddedProducts); // Ensure that the product was added to the repository
//            Assert.Equal(product, productRepository.AddedProducts[0]); // Ensure that the correct product was added
//            // You can add more assertions based on your requirements
//        }
//    }

//    // Implement a mock repository for testing purposes
//    public class MockProductRepository : IProductRepository
//    {
//        public List<Product> AddedProducts { get; } = new List<Product>();

//        public Task AddProductAsync(Product product)
//        {
//            AddedProducts.Add(product);
//            return Task.CompletedTask;
//        }

//        public Task<Product?> GetProductByIdAsync(int productId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
//        {
//            throw new NotImplementedException();
//        }

//        public Task UpdateProductAsync(Product product)
//        {
//            throw new NotImplementedException();
//        }

//        // Implement other methods as needed for your test cases
//    }
//}



//using Bunit;
//using IMS.CoreBusiness;
//using IMS.UseCases.Products;
//using IMS.UseCases.Products.interfaces;
//using IMS.WebApp.Components.Pages.Products;
//using Microsoft.AspNetCore.Components.Forms;
//using Microsoft.Extensions.DependencyInjection;
//using Moq;
//using Xunit;

//namespace TestProject_Blazor
//{
//    public class AddProductTests : TestContext
//    {
//        [Fact]
//        public void TestAddProductPage()
//        {
//            // Arrange
//            var product = new Product();
//            var addProductUseCaseMock = new Mock<IAddProductUseCase>();
//            addProductUseCaseMock.Setup(x => x.ExecuteAsync(It.IsAny<Product>())).Verifiable();

//            Services.AddSingleton<IAddProductUseCase>(addProductUseCaseMock.Object);

//            var cut = RenderComponent<AddProduct>();

//            // Act
//            cut.Find("#name").Change("Test Product");
//            cut.Find("#quantity").Change("10");
//            cut.Find("#price").Change("20.5");

//            // Simulate file upload
//            var file = new Mock<IBrowserFile>();
//            file.Setup(f => f.ContentType).Returns("image/jpeg");
//            file.Setup(f => f.Size).Returns(1000); // Set file size as needed

//            cut.Find("#file").Change(file.Object);

//            cut.Find("form").Submit();

//            // Assert
//            addProductUseCaseMock.Verify(x => x.ExecuteAsync(It.Is<Product>(p =>
//                p.ProductName == "Test Product" &&
//                p.Quantity == 10 &&
//                p.Price == 20.5 &&
//                p.ImgUrl != null)), Times.Once);

//            // You can add more assertions based on your requirements
//        }
//    }
//}

