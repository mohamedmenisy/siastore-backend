using e_commerce.Models;

namespace e_commerce.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string OrderStatus { get; set; }
        public int TotalOrderPrice { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string UserID { get; set; }

    }
}
