using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PedaleaShop.Models.Dtos;
using PedaleaShop.WebApp.Client.Services.Contracts;

namespace PedaleaShop.WebApp.Client.Pages
{
    public class ShoppingCartBase:ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }

        public List<CartItemDto> ShoppingCartItems { get; set; }

        public string ErrorMessage { get; set; }

        protected string TotalPrice { get; set; }
        protected int TotalQuantity { get; set; }
        protected bool SplitPlanOn { get; set; } = false;
        protected int SeparatePlansplits { get; set; } = 3;
        protected decimal SeparatePlansFirstBill { get; set; } = 0;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                SplitPlanOn = false;
                ShoppingCartItems = await ManageCartItemsLocalStorageService.GetCollection();
                CartChanged();
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }
        protected async Task DeleteCartItem_Click(int id)
        {
            var cartItemDto = await ShoppingCartService.DeleteItem(id);

            await RemoveCartItem(id);
            CartChanged();

        }
        protected void PlanSepareEvent(int id)
        {
            SplitPlanOn = !SplitPlanOn;


        }
        protected async Task UpdateSeparatePlanQuantity(int id, int Quantity)
        {
            
        }
            protected async Task UpdateQuantityCartItem_Click(int id, int Quantity)
        {
            try
            {
                if (Quantity > 0)
                {
                    var updateItemDto = new CartItemQuantityUpdateDto
                    {
                        CartItemId = id,
                        Quantity = Quantity
                    };

                    var returnedUpdateItemDto = await this.ShoppingCartService.UpdateQuantity(updateItemDto);

                    await UpdateItemTotalPrice(returnedUpdateItemDto);
                    
                    CartChanged();

                    await MakeUpdateQuantityButtonVisible(id, false);


                }
                else
                {
                    var item = this.ShoppingCartItems.FirstOrDefault(i => i.Id == id);

                    if (item != null)
                    {
                        item.Quantity = 1;
                        item.TotalPrice = item.Price;
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        protected async Task UpdateQuantity_Input(int id)
        {
           await MakeUpdateQuantityButtonVisible(id, true);
        }
        protected async Task UpdateSeparatePlanFirstPayment_Input(int Id)
        {
           var item = GetCartItem(Id);
            
            if (SeparatePlansFirstBill < item.Price * (decimal)0.2)
            {
                SeparatePlansFirstBill = item.Price * (decimal)0.2;
            }
        }
        private async Task MakeUpdateQuantityButtonVisible(int id, bool visible)
        {
            await Js.InvokeVoidAsync("MakeUpdateQuantityButtonVisible", id, visible);
        }

        private async Task UpdateItemTotalPrice(CartItemDto cartItemDto)
        {
            var item = GetCartItem(cartItemDto.Id);

            if (item != null)
            {
                item.TotalPrice = cartItemDto.Price * cartItemDto.Quantity;
            }

            await ManageCartItemsLocalStorageService.SaveCollection(ShoppingCartItems);

        }
        private void CalculateCartSummaryTotals()
        {
            SetTotalPrice();
            SetTotalQuantity();
        }

        private void SetTotalPrice()
        {
            TotalPrice = this.ShoppingCartItems.Sum(p => p.TotalPrice).ToString("C");
        }
        private void SetTotalQuantity()
        {
            TotalQuantity = this.ShoppingCartItems.Sum(p => p.Quantity);
        }

        private CartItemDto GetCartItem(int id)
        {
            return ShoppingCartItems.FirstOrDefault(i => i.Id == id);
        }
        private async Task RemoveCartItem(int id)
        { 
            var cartItemDto = GetCartItem(id);

            ShoppingCartItems.Remove(cartItemDto);

            await ManageCartItemsLocalStorageService.SaveCollection(ShoppingCartItems);

        }
        private void CartChanged()
        { 
            CalculateCartSummaryTotals();
            ShoppingCartService.RaiseEventOnShoppingCartChanged(TotalQuantity);
        }

    }
}
