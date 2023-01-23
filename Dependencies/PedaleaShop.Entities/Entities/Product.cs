using System.ComponentModel.DataAnnotations.Schema;

namespace PedaleaShop.Entities.Dtos
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [ForeignKey("ColorId")]
        public Colors Color { get; set; }
        [ForeignKey("SizeId")]
        public Size Size { get; set; }

    }
}
