using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.Models
{
    public class Order
    {
        public int Id { get; set; } 
        public string OrderStatus { get; set; }
        public int TotalOrderPrice { get; set; }

        public List<OrderItem> OrderItems { get; set; }= new List<OrderItem>();
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("PaymentOrder")]
        public int? PaymentID { get; set; }
        public PaymentOrder? PaymentOrder { get; set; }

    }
}
