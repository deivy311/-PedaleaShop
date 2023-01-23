using System.ComponentModel.DataAnnotations.Schema;

namespace PedaleaShop.Entities.Dtos
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public AspNetUsers User { get; set; }
    }
}
