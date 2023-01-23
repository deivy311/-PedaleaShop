using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedaleaShop.Entities.Dtos
{
    public class ShoppingCartItemToAddDto
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool Separated { get; set; }
        public bool Paid { get; set; }
    }
}
