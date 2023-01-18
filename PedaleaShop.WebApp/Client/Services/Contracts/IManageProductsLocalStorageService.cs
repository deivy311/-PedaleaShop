using PedaleaShop.Models.Dtos;

namespace PedaleaShop.WebApp.Client.Services.Contracts
{
    public interface IManageProductsLocalStorageService
    {
        Task<IEnumerable<ProductDto>> GetCollection();
        Task RemoveCollection();
    }
}
