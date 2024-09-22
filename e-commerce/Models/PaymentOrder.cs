using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.Models
{
   
    public class PaymentOrder
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

        public string Method { get; set; }
        public DateTime PaymentDate { get; set; }
        public string CartNumber {  get; set; }

        public int ZIPCode {  get; set; }

        public string PhoneNumber { get; set; }

        public string CustomerName {  get; set; }

        public string CustomerEmail { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string UserID { get; set; }
    }
}
