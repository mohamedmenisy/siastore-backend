using e_commerce.DTO;
using e_commerce.Models;

namespace e_commerce.Interfaces
{
    public interface IOrder
    {

        public void SetOrder(Order order);

        public void CancelOrder(int orderid);

        public OrderDTO GetOrder(int orderid);
        public Order GetOrderByID(int orderid);

        public List<OrderDTO> GetUserOrder(string userid);
        public void UpdateStock(OrderDTO order);
        public OrderDTO UpdateOrder(Order order);

    }
}
