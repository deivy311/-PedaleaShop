﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using PedaleaShop.Entities.Dtos;
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

        public List<ShoppingCartItemDto> ShoppingCartItems { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        public string ErrorMessage { get; set; }

        protected string TotalPriceStr { get; set; }
        protected decimal TotalPrice { get; set; }
        protected decimal MinUmbralToSeparate { get; set; } = 100;
        protected decimal TotalDisccount { get; set; } = 0;
        protected int TotalSeparatedItems { get; set; }
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
        protected async void PlanSepareEvent(ShoppingCartItemDto _updateItemDto)
        {
            try
            {
                _updateItemDto.Separated = !_updateItemDto.Separated;

                var updateItemDto = new ShoppingCartItemIsSeparatedUpdateDto
                {
                    CartItemId = _updateItemDto.Id,
                    Separated = _updateItemDto.Separated
                };

                var returnedUpdateItemDto = await this.ShoppingCartService.UpdateIsSeparated(updateItemDto);


                CartChanged();


            }
            catch (Exception)
            {

                throw;
            }


        }

        protected async Task UpdateQuantityCartItem_Click(int id, int Quantity)
        {
            try
            {
                if (Quantity > 0)
                {
                    var updateItemDto = new ShoppingCartItemQuantityUpdateDto
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

        private async Task UpdateItemTotalPrice(ShoppingCartItemDto cartItemDto)
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
            SetTotalSeparatedItems();
            SetTotalPrice();
            SetTotalDisccountAsync();
            SetTotalQuantity();
        }

        private async Task SetTotalDisccountAsync()
        {
            var updateItemDto = new UserDto
            {
                TotalPaidProducts = 0,
                TotalSeparatedProducts = 0
            };
            var _user = (await authenticationStateTask).User;
            var returnedUpdateItemDto = await this.ShoppingCartService.UpdateUserMetrics(_user.Identity.Name, updateItemDto);
            if(returnedUpdateItemDto.TotalPaidProducts>1)
            {
                TotalDisccount = 20;
                StateHasChanged();

            }
        }

        private void SetTotalSeparatedItems()
        {
            TotalSeparatedItems = this.ShoppingCartItems.Sum(p => Convert.ToInt32(p.Separated));
        }
        private void SetTotalPrice()
        {
            TotalPrice = this.ShoppingCartItems.Sum(p => p.TotalPrice)*(100- TotalDisccount)/100;
            TotalPriceStr= TotalPrice.ToString("C");
        }
        private void SetTotalQuantity()
        {
            TotalQuantity = this.ShoppingCartItems.Sum(p => p.Quantity);
        }

        private ShoppingCartItemDto GetCartItem(int id)
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
