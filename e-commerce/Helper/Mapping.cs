using e_commerce.DTO;
using e_commerce.Models;

namespace e_commerce.Helper
{
    public static class Mapping
    {
        public static List<ProductDTO> MapingFun(List<Product> products)
        {
            List<ProductDTO> productsDTo = new List<ProductDTO>();
            for (int i = 0; i < products.Count; i++)
            {
                ProductDTO productdto = new ProductDTO()
                {
                    Id = products[i].Id,
                    Name = products[i].Name,
                    Description = products[i].Description,
                    PictureUrl = products[i].PictureUrl,
                    Price = products[i].Price,
                    productBrand = products[i].ProductBrand.Name,
                    productType = products[i].ProductType.Name,
                    Stock = products[i].Stock,
                };
                productsDTo.Add(productdto);
            }


            return productsDTo;

        }


        public static ProductDTO MapingFun(Product products)
        {


            ProductDTO productdto = new ProductDTO()
            {


                Id = products.Id,
                Name = products.Name,
                Description = products.Description,
                PictureUrl = products.PictureUrl,
                Price = products.Price,
                productBrand = products.ProductBrand.Name,
                productType = products.ProductType.Name
            };


            return productdto;


        }



        public static OrderDTO MapingFun(Order order)
        {
            OrderDTO orderdto = new OrderDTO()
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                OrderStatus = order.OrderStatus,
                TotalOrderPrice = order.TotalOrderPrice,
                UserID = order.UserID,
                OrderItems = new List<OrderItemDTO>()
            };


            for (int i = 0; i < order.OrderItems.Count; i++)
            {
                OrderItemDTO item = new OrderItemDTO()
                {
                    Brand= order.OrderItems[i].Brand,
                    Id = order.OrderItems[i].Id,
                    Img = order.OrderItems[i].Img,
                    Orderid = order.OrderItems[i].Orderid,
                    Price= order.OrderItems[i].Price,
                    ProductId = order.OrderItems[i].ProductId,
                    ProductName= order.OrderItems[i].ProductName,
                    Quantity = order.OrderItems[i].Quantity,
                    type = order.OrderItems[i].type

                };
                orderdto.OrderItems.Add(item);
            }

            return orderdto;
        }


        public static List<OrderDTO> MapingFun(List<Order> orders)
        {

            var ordersDto = new List<OrderDTO>();
            for (int i = 0; i < orders.Count; i++)
            {
                var myorder=Mapping.MapingFun(orders[i]);
                ordersDto.Add(myorder);
            }
            return ordersDto;
        }













    }









}
