using Microsoft.AspNetCore.Components;
using PedaleaShop.Entities.Dtos;
using PedaleaShop.WebApp.Client.Services.Contracts;

namespace PedaleaShop.WebApp.Client.Shared
{
    public class ProductSizesNavigationMenuBase<T>: ComponentBase where T : class
    {
        [Inject]
        public IProductService ProductService { get; set; }

        public IEnumerable<T> productSize { get; set; }
        [Parameter]
        public  List<string> SelectedIds { get; set; } //= new List<string>();
        public string ErrorMessage { get; set; }
        [Parameter] 
        public Action SelectedValuesString { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                productSize = (IEnumerable<T>)await ProductService.GetProductsSizes();

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

    }
}
