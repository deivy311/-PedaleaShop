using Blazored.LocalStorage;
using PedaleaShop.Entities.Dtos;
using PedaleaShop.WebApp.Client.Services.Contracts;

namespace PedaleaShop.WebApp.Client.Services
{
    public class ManageCartItemsLocalStorageService : IManageCartItemsLocalStorageService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IShoppingCartService shoppingCartService;

        const string key = "CartItemCollection";

        public ManageCartItemsLocalStorageService(ILocalStorageService localStorageService,
                                                  IShoppingCartService shoppingCartService)
        {
            this.localStorageService = localStorageService;
            this.shoppingCartService = shoppingCartService;
        }

        public async Task<List<ShoppingCartItemDto>> GetCollection()
        {
            return await this.localStorageService.GetItemAsync<List<ShoppingCartItemDto>>(key)
                    ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
           await this.localStorageService.RemoveItemAsync(key);
        }

        public async Task SaveCollection(List<ShoppingCartItemDto> cartItemDtos)
        {
            await this.localStorageService.SetItemAsync(key,cartItemDtos);
        }

        private async Task<List<ShoppingCartItemDto>> AddCollection()
        {
            var shoppingCartCollection = await this.shoppingCartService.GetItems(HardCoded.UserId);

            if(shoppingCartCollection != null)
            {
                await this.localStorageService.SetItemAsync(key, shoppingCartCollection);
            }
            
            return shoppingCartCollection;

        }

    }
}
