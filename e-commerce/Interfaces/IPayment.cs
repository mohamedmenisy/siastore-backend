using e_commerce.Models;

namespace e_commerce.Interfaces
{
    public interface IPayment
    {
        public void OrderPay(PaymentOrder payment_);
    }
}
