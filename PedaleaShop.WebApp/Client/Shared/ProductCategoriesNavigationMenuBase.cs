using Microsoft.AspNetCore.Components;
using PedaleaShop.Models.Dtos;
using PedaleaShop.WebApp.Client.Services.Contracts;

namespace PedaleaShop.WebApp.Client.Shared
{
    public class ProductCategoriesNavigationMenuBase:ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public IEnumerable<ProductCategoryDto> ProductCategoryDtos { get; set; }
        public int CurrentCategory = 0;
        public List<string> SelectedColorsIds = new List<string>();
        public List<string> SelectedSizesIds = new List<string>();
        public string ErrorMessage { get; set; }
        public string SelectedColorsIdsString { get; set; }
        public string SelectedSizesIdsString { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ProductCategoryDtos = await ProductService.GetProductCategories();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        protected void ShowSelectedValues()
        {
            string[] arraySelectedColorsIds = SelectedColorsIds.ToArray();
            string[] arraySelectedSizesIds = SelectedSizesIds.ToArray();
            SelectedColorsIdsString = arraySelectedColorsIds.Length == 0 ? "0" : string.Join(",", arraySelectedColorsIds);
            SelectedSizesIdsString = arraySelectedSizesIds.Length == 0 ?"0": string.Join(",",  arraySelectedSizesIds);
            NavigationManager.NavigateTo("/ProductsByCategory/" + CurrentCategory + "/" + SelectedColorsIdsString + "/" + SelectedSizesIdsString); 
            
            StateHasChanged();
        }
        protected void UpdateCategoryId(int urrentCategoryId)
        {
            CurrentCategory = urrentCategoryId;
        }
    }
}
