using AngleSharp.Html.Dom;
using Bunit;
using IMS.WebApp.Components.Pages.Products;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TestProject_Blazor;
using IMS.UseCases.Products.interfaces;

public class Test01_ProductList : TestContext
{
    private readonly Mock<NavigationManagerWrapper> _navigationManagerMock;
    private readonly Mock<IViewProductsByNameUseCase> _viewProductsByNameUseCaseMock;

    public Test01_ProductList()
    {
        //Services.AddSingleton(_navigationManagerMock = new Mock<NavigationManager>());
        //Services.AddSingleton(_viewProductsByNameUseCaseMock = new Mock<IViewProductsByNameUseCase>());
        _navigationManagerMock = new Mock<NavigationManagerWrapper>();
        _viewProductsByNameUseCaseMock = new Mock<IViewProductsByNameUseCase>();
        Services.AddSingleton(_navigationManagerMock.Object);
        Services.AddSingleton(_viewProductsByNameUseCaseMock.Object);
    }
    [Fact]
    public void ClickingAddProductButton_ShouldNavigateToAddProductPage()
    {
        // - Perplexity
        // Arrange
        //Services.AddSingleton<NavigationManager>(_navigationManagerMock.Object);
        //Services.AddSingleton<IViewProductsByNameUseCase>(_viewProductsByNameUseCaseMock.Object);
        var component = RenderComponent<ProductList>();

        // Act
        var addProductButton = component.Find("button");
        addProductButton.Click();

        // Assert 
        //_navigationManagerMock.Verify(nm => nm.NavigateTo("/addproduct"), Times.Once);
        _navigationManagerMock.Verify(nm => nm.NavigateTo("/addproduct", It.IsAny<bool>()), Times.Once);
        //_navigationManagerMock.Verify(nm => nm.NavigateTo("/addproduct", false), Times.Once);

    }
}



public class NavigationManagerWrapper : NavigationManager
{
    public virtual void NavigateTo(string uri, bool forceLoad)
    {
        base.NavigateTo(uri, forceLoad);
    }
}
