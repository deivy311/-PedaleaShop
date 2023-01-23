using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedaleaShop.Entities.Dtos
{
    public class ShoppingCartItemQuantityUpdateDto
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
    }
}
