using Microsoft.AspNetCore.Components;
using PedaleaShop.Models.Dtos;
using PedaleaShop.WebApp.Client.Services.Contracts;

namespace PedaleaShop.WebApp.Client.Pages
{
    public class ProductsBase:ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }
        
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public IManageProductsLocalStorageService ManageProductsLocalStorageService { get; set; }

        [Inject]
        public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await ClearLocalStorage();

                Products =  await ManageProductsLocalStorageService.GetCollection();

                var shoppingCartItems = await ManageCartItemsLocalStorageService.GetCollection();
               
                var totalQuantity = shoppingCartItems.Sum(i => i.Quantity);

                ShoppingCartService.RaiseEventOnShoppingCartChanged(totalQuantity);

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                
            }
            
        }
        protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedProducts(List<int> ColorsIdsToSearch)
        {
            return from product in Products
                   where ColorsIdsToSearch.Contains(product.ColorId)
                   group product by product.CategoryId into prodByCatGroup
                   orderby prodByCatGroup.Key
                   select prodByCatGroup;
        }
        protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedProductsByCategory()
        {  
            return from product in Products
                   group product by product.CategoryId into prodByCatGroup
                   orderby prodByCatGroup.Key
                   select prodByCatGroup;
        }
        protected string GetCategoryName(IGrouping<int, ProductDto> groupedProductDtos)
        {
            return groupedProductDtos.FirstOrDefault(pg => pg.CategoryId == groupedProductDtos.Key).CategoryName;
        }

        private async Task ClearLocalStorage()
        {
            await ManageProductsLocalStorageService.RemoveCollection();
            await ManageCartItemsLocalStorageService.RemoveCollection();
        }

    }
}
