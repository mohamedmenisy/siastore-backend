using e_commerce.Context;
using e_commerce.Interfaces;
using e_commerce.Models;

namespace e_commerce.Repositories
{
    public class PaymentRepo : IPayment
    {
        private readonly StoreContext _db;

        public PaymentRepo(StoreContext db)
        {
            _db = db;
        }
        public void OrderPay(PaymentOrder payment_)
        {
            _db.PaymentOrders.Add(payment_);
            _db.SaveChanges();  

        }
    }
}
