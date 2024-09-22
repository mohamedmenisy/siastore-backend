using e_commerce.Context;
using e_commerce.DTO;
using e_commerce.Helper;
using e_commerce.Interfaces;
using e_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Repositories
{
    public class OrderRepo:IOrder
    {
        private readonly StoreContext db;
        private readonly IProduct _product;

        public OrderRepo(StoreContext _db , IProduct product)
        {
            db = _db;
            _product = product;
        }
        public OrderDTO UpdateOrder(Order order)
        {
            db.Orders.Update(order);
            db.SaveChanges();
            return Mapping.MapingFun(order);
        }
        public Order GetOrderByID(int orderid)
        {
            var myorder = db.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == orderid);
            return myorder;
        }

        public void SetOrder(Order order)
        {

            db.Orders.Add(order);
            db.SaveChanges();
        }

        public void CancelOrder(int orderid)
        {
            var myorder= db.Orders.Include(o=>o.OrderItems).FirstOrDefault(o=>o.Id == orderid);
            var orderDto = Mapping.MapingFun(myorder);
            UpdateStock(orderDto);
            db.Orders.Remove(myorder);
            db.SaveChanges();
        }

        public OrderDTO GetOrder(int orderid)
        {
            var myorder = db.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == orderid);
            var OrderDto = Mapping.MapingFun(myorder);
            return OrderDto;
        }

        public List<OrderDTO> GetUserOrder(string userid)
        {
            var userorder = db.Orders.Include(o => o.OrderItems).Where(o => o.UserID == userid).ToList();
            var userorderdto = Mapping.MapingFun(userorder);
            return userorderdto;
        }
        public void UpdateStock(OrderDTO order)
        {
            for (int i = 0; i < order.OrderItems.Count; i++)
            {
                var product = _product.GetProductsById(order.OrderItems[i].ProductId);
                product.Stock += order.OrderItems[i].Quantity;
            }
        }

    }
}
