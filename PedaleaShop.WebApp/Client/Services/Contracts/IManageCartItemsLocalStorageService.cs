using PedaleaShop.Entities.Dtos;

namespace PedaleaShop.WebApp.Client.Services.Contracts
{
    public interface IManageCartItemsLocalStorageService
    {
        Task<List<ShoppingCartItemDto>> GetCollection();
        Task SaveCollection(List<ShoppingCartItemDto> cartItemDtos);
        Task RemoveCollection();
    }
}
