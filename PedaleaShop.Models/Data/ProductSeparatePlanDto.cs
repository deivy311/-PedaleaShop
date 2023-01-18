namespace PedaleaShop.Models.Dtos
{
    public class ProductSeparatePlanDto
    {
        public int NumberOfPayments { get; set; } = 2;
        public bool AlreadyPaid { get; set; } = false;

        public bool Enabled { get; set; } = false;
        public int ProductId { get; set; }
        public decimal TotalPaid { get; set; }


    }
}