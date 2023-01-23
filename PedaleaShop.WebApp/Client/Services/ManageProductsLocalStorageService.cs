using Blazored.LocalStorage;
using PedaleaShop.WebApp.Client.Services.Contracts;
using PedaleaShop.Entities.Dtos;

namespace PedaleaShop.WebApp.Client.Services
{
    public class ManageProductsLocalStorageService : IManageProductsLocalStorageService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IProductService productService;

        private const string key = "ProductCollection";

        public ManageProductsLocalStorageService(ILocalStorageService localStorageService,
                                                 IProductService productService)
        {
            this.localStorageService = localStorageService;
            this.productService = productService;
        }

        public async Task<IEnumerable<ProductsDto>> GetCollection()
        {
            return await this.localStorageService.GetItemAsync<IEnumerable<ProductsDto>>(key)
                    ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
           await this.localStorageService.RemoveItemAsync(key);
        }

        private async Task<IEnumerable<ProductsDto>> AddCollection()
        {
            var productCollection = await this.productService.GetItems();

            if (productCollection != null)
            {
                await this.localStorageService.SetItemAsync(key, productCollection);
            }

            return productCollection;

        }

    }
}
