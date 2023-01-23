using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedaleaShop.Entities.Dtos
{
    public class ShoppingCartItemIsSeparatedUpdateDto
    {
        public int CartItemId { get; set; }
        public bool Separated { get; set; }
    }
}
