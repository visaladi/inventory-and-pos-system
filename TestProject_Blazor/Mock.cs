using IMS.CoreBusiness;
using IMS.UseCases.Products.interfaces;

public class MockAddProductUseCase : IAddProductUseCase
{
    public bool IsProductAdded { get; private set; } = false;

    public async Task ExecuteAsync(Product product)
    {
        IsProductAdded = true; // Set a flag to indicate product is added (mock behavior)
    }
}

public class MockViewProductByIdUseCase : IViewProductByIdUseCase
{
    public Product ReturnedProduct { get; set; }

    public async Task<Product> ExecuteAsync(int productId)
    {
        return ReturnedProduct; // Return the pre-set product (mock behavior)
    }
}

// Add similar mock classes for other injected services as needed
