using Microsoft.AspNetCore.Components;
using PedaleaShop.Entities.Dtos;

namespace PedaleaShop.WebApp.Client.Pages
{
    public class DisplayProductsBase:ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductsDto> Products { get; set; }
    
    }
}
