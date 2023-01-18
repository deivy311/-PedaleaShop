using Microsoft.AspNetCore.Components;
using PedaleaShop.Models.Dtos;
using PedaleaShop.WebApp.Client.Services.Contracts;

namespace PedaleaShop.WebApp.Client.Shared
{
    public class ProductColorsNavigationMenuBase<T>: ComponentBase where T : class
    {
        [Inject]
        public IProductService ProductService { get; set; }

        public IEnumerable<T> productColor { get; set; }
        [Parameter]
        public  List<string> SelectedIds { get; set; } //= new List<string>();
        public string ErrorMessage { get; set; }
        [Parameter] 
        public Action SelectedValuesString { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                productColor = (IEnumerable<T>)await ProductService.GetProductsColors();

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

    }
}
