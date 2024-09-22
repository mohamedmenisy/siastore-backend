using e_commerce.DTO;
using e_commerce.Errors;
using e_commerce.Helper;
using e_commerce.Interfaces;
using e_commerce.Models;
using e_commerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayment paymentRepo;
        private readonly IMailingService mail;
        private readonly IOrder orderRepo;

        public PaymentController(IPayment _paymentRepo, IMailingService _mail,IOrder _orderRepo)
        {
            paymentRepo = _paymentRepo;
            mail = _mail;
            orderRepo = _orderRepo;
        }
        [HttpPost("Pay")]
        public IActionResult Pay(PaymentDTO _dto)
        {
            var order = orderRepo.GetOrderByID(_dto.OrderId);
            if (order !=null)
            {
                var orders = orderRepo.GetUserOrder(order.UserID);

                PaymentOrder payment = new PaymentOrder() { 
                 Amount = _dto.Amount,
                 CartNumber = _dto.CartNumber,
                 CustomerEmail = _dto.CustomerEmail,
                 CustomerName = _dto.CustomerName,
                 Method =_dto.Method,
                OrderId = _dto.OrderId,
                PaymentDate = _dto.PaymentDate,
                 PhoneNumber = _dto.PhoneNumber,
                 ZIPCode = _dto.ZIPCode,
                 UserID = _dto.UserID,
                 };

                paymentRepo.OrderPay(payment);
                order.PaymentID = payment.Id;
                order.OrderStatus = "Your Order Is on The Way";
                var orderDto = orderRepo.UpdateOrder(order);
                var result = new List<string>();
               
                foreach (var item in orderDto.OrderItems)
                {
                    result.Add(item.ProductName);
                }
                
                result.Add("Total Price You Payed = " +orderDto.TotalOrderPrice);

                mail.SendEmailAsync(_dto.CustomerEmail, "Welcome To Sia Store" +" "+payment.CustomerName, result, null);
                return Ok(orders);

            }
            

            return NotFound(new ErrorResponse() { Message = "There is No Order To Pay"});
        }
    }
}
