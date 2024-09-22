using e_commerce.Helper;
using e_commerce.Interfaces;
using e_commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder orderRepo;
        private readonly ICart cartRepo;

        public OrderController(IOrder _orderRepo ,ICart cartRepo)
        {
            orderRepo = _orderRepo;
            this.cartRepo = cartRepo;
        }
        [HttpPost("SetOrder")]
        public IActionResult CreateOrder(string userid)
        {
            var cart = cartRepo.GetUserCart(userid);
            if (cart.CartItems.Count > 0)
            {
                Models.Order order = new Models.Order() {
                    OrderDate = DateTime.Now,
                    OrderStatus = "Pending",
                    TotalOrderPrice = (int)cart.TotalPrice,
                    UserID = cart.UserId,
                    OrderItems = new List<OrderItem>(),
                    

                };
                for (int i = 0; i < cart.CartItems.Count; i++)
                {
                    OrderItem orderItem = new OrderItem() { 
                    Brand= cart.CartItems[i].Brand,
                    Img= cart.CartItems[i].Img,
                    Price= cart.CartItems[i].Price,
                    ProductId= cart.CartItems[i].ProductId,
                        ProductName = cart.CartItems[i].Name,
                        Quantity= cart.CartItems[i].Quantity,
                        type = cart.CartItems[i].type,
                        
                    
                    
                    };
                    order.OrderItems.Add(orderItem);
                }
                orderRepo.SetOrder(order);
                cartRepo.DeleteCartItems(cart.CartId);
                cartRepo.UpdateUserCart(cart);
                var orderDto = Mapping.MapingFun(order);
                return Ok(orderDto);
            }
            else
            {
                return NotFound();
            }
         

        }

        [HttpGet("GetUserOrders")]
        public IActionResult GetUserOrders(string userid) {

            var userOrders = orderRepo.GetUserOrder(userid);
           
            return Ok(userOrders);
        
        }

        [HttpDelete("CancelOrder")]
        public IActionResult CancelOrder(string userid  ,int orderId)
        {
           
            orderRepo.CancelOrder(orderId);
            var userOrders = orderRepo.GetUserOrder(userid);
            return Ok(userOrders);
        }
    }
}
