using Microsoft.AspNetCore.Components;
using PedaleaShop.Models.Dtos;

namespace PedaleaShop.WebApp.Client.Pages
{
    public class DisplayProductsBase:ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; }
    
    }
}
