
//using IMS.CoreBusiness;
//using IMS.WebApp.Components.Pages.Products;
//using Microsoft.AspNetCore.Components;
//using Microsoft.Extensions.DependencyInjection;
//using Xunit;
//using Bunit;

//namespace TestProject_Blazor
//{
//    public class Test_AddProduct
//    {
//        [Fact]
//        public void Should_Render_AddProduct_Page()
//        {
//            using var ctx = new TestContext();

//            // Add any required services here (e.g., Mock IProductRepository)
//            // ctx.Services.AddScoped<IProductRepository>(mock => new Mock<IProductRepository>().Object);

//            var cut = ctx.RenderComponent<AddProduct>();

//            // Assert component renders correctly
//            Assert.NotNull(cut.Instance);
//            Assert.Contains("Add Product", cut.Markup);
//        }

//        [Fact]
//        public void Should_Validate_Product_Name_OnSubmit()
//        {
//            using var ctx = new TestContext();

//            var cut = ctx.RenderComponent<AddProduct>();

//            // Simulate user input (empty product name)
//            //cut.Instance.product.ProductName = "";
//            cut.Instance.SetProduct(new Product { ProductName = "" });

//            cut.SetParametersAndRender();

//            // Submit the form
//            cut.Find("button[type=submit]").Click();

//            // Assert validation error message is displayed
//            Assert.Contains("The Product Name field is required.", cut.Markup);
//        }

//        [Fact]
//        public void Should_Validate_Quantity_OnSubmit()
//        {
//            using var ctx = new TestContext();

//            var cut = ctx.RenderComponent<AddProduct>();

//            // Simulate user input (negative quantity)
//            cut.Instance.product.Quantity = -1;
//            cut.SetParametersAndRender();

//            // Submit the form
//            cut.Find("button[type=submit]").Click();

//            // Assert validation error message is displayed
//            Assert.Contains("Must be greater than or equal to 0", cut.Markup);
//        }

//        [Fact]
//        public void Should_Validate_Price_OnSubmit()
//        {
//            using var ctx = new TestContext();

//            var cut = ctx.RenderComponent<AddProduct>();

//            // Simulate user input (price less than minimum)
//            cut.Instance.product.Price = 4.99;
//            cut.SetParametersAndRender();

//            // Submit the form
//            cut.Find("button[type=submit]").Click();

//            // Assert validation error message is displayed
//            Assert.Contains("Must be greater than or equal to 5", cut.Markup);
//        }

//        // Add more tests for other functionalities (e.g., image upload, successful submit)
//    }
//}
