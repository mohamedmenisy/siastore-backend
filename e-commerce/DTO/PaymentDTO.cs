using e_commerce.Models;

namespace e_commerce.DTO
{
    public class PaymentDTO
    {
        public decimal Amount { get; set; }

        public string Method { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public string CartNumber { get; set; }

        public int ZIPCode { get; set; }

        public string PhoneNumber { get; set; }

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }
        public int OrderId { get; set; }
        public string UserID { get; set; }


    }
}
