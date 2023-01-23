using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedaleaShop.Entities.Dtos
{
    public class SeparatePlans
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ShoppingCartId { get; set; }

        [ForeignKey("UserId")]
        public AspNetUsers User { get; set; }
        [ForeignKey("ShoppingCartId")]
        public ShoppingCart ShoppingCart { get; set; }
    }
}
