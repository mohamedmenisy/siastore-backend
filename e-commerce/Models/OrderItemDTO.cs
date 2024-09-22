
namespace e_commerce.Models
{
    public class OrderItemDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string Img { get; set; }

        public decimal Price { get; set; }

        public string Brand { get; set; }
        public string type { get; set; }

        public int Quantity { get; set; }
        public decimal TotalPrice => Quantity * Price;
        public int Orderid { get; set; }
    }
}
