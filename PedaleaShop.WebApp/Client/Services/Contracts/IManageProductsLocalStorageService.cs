using PedaleaShop.Entities.Dtos;

namespace PedaleaShop.WebApp.Client.Services.Contracts
{
    public interface IManageProductsLocalStorageService
    {
        Task<IEnumerable<ProductsDto>> GetCollection();
        Task RemoveCollection();
    }
}
