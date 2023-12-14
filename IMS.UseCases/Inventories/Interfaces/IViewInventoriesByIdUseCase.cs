using IMS.CoreBusiness;

namespace IMS.UseCases.Inventories.Interfaces
{
    public interface IViewInventoriesByIdUseCase
    {
        Task<Inventory> ExecuteAsync(int inventoryId);
    }
}