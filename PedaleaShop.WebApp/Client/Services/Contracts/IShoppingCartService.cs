using PedaleaShop.Entities.Dtos;

namespace PedaleaShop.WebApp.Client.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<List<ShoppingCartItemDto>> GetItems(int userId);
        Task<ShoppingCartItemDto> AddItem(ShoppingCartItemToAddDto cartItemToAddDto);
        Task<ShoppingCartItemDto> DeleteItem(int id);
        Task<ShoppingCartItemDto> UpdateQuantity(ShoppingCartItemQuantityUpdateDto cartItemQuantityUpdateDto);
        Task<ShoppingCartItemDto> UpdateIsSeparated(ShoppingCartItemIsSeparatedUpdateDto cartItemQuantityUpdateDto);
        event Action<int> OnShoppingCartChanged;
        void RaiseEventOnShoppingCartChanged(int totalQuantity);

    }
}
