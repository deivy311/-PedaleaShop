using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedaleaShop.Entities.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int TotalPaidProducts { get; set; }
        public int TotalSeparatedProducts { get; set; }

    }
}