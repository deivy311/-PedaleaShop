

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PedaleaShop.Entities.Dtos;
using PedaleaShop.Entities.Dtos;
using PedaleaShop.WebApp.Client.Services.Contracts;

namespace PedaleaShop.WebApp.Client.Pages
{
    public class CheckoutBase:ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }

        protected IEnumerable<ShoppingCartItemDto> ShoppingCartItems { get; set; }

        protected int TotalQuantity { get; set; }

        protected string PaymentDescription { get; set; }

        protected decimal PaymentAmount { get; set; }
        protected int  TotalPaidProducts { get; set; }
        protected int TotalSeparatedProducts { get; set; }


        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }

        protected async Task CheckOutPaymentButtonEvent(string UserName)
        {
            var updateItemDto = new UserDto
            {
                TotalPaidProducts = TotalPaidProducts,
                TotalSeparatedProducts = TotalSeparatedProducts
            };

            var returnedUpdateItemDto = await this.ShoppingCartService.UpdateUserMetrics(UserName,updateItemDto);
        }
        protected string DisplayButtons { get; set; } = "block";

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ManageCartItemsLocalStorageService.GetCollection();

                if (ShoppingCartItems != null && ShoppingCartItems.Count() > 0)
                {
                    Guid orderGuid = Guid.NewGuid();

                    PaymentAmount = ShoppingCartItems.Sum(p => p.TotalPrice);
                    TotalQuantity = ShoppingCartItems.Sum(p => p.Quantity);
                    TotalSeparatedProducts = ShoppingCartItems.Sum(p => Convert.ToInt32(p.Separated));
                    TotalPaidProducts= TotalQuantity- TotalSeparatedProducts;

                    PaymentDescription = $"O_{HardCoded.UserId}_{orderGuid}";

                }
                else
                {
                    DisplayButtons = "none";
                }

            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    await Js.InvokeVoidAsync("initPayPalButton");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
