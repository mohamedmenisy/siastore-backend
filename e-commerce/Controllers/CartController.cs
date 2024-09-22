using e_commerce.DTO;
using e_commerce.Errors;
using e_commerce.Helper;
using e_commerce.Interfaces;
using e_commerce.Migrations;
using e_commerce.Models;
using e_commerce.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICart cartRepo;
        private readonly IProduct productRepo;

        public CartController(ICart _cart, IProduct _productRepo)
        {
            cartRepo = _cart;
            productRepo = _productRepo;
        }
        [HttpPost("AddToCart")]
       public IActionResult Addtocart(int _productid ,string _userid)
        {
            var _product = productRepo.GetProductsById(_productid);
            var _productDto = Mapping.MapingFun(_product);
            var _userCart = cartRepo.GetUserCart(_userid);

            var existingCartItem = cartRepo.IFExist(_userCart, _productid);

            if (_product.Stock == 0)
            {
                return NotFound(new ErrorResponse() { Message="Stock is Epity"});
            }
            if (existingCartItem !=null)
            {

                existingCartItem.Quantity += 1;
                _product.Stock--;
                cartRepo.UpdateStock(_product);
                cartRepo.UpdateCartItem(existingCartItem); 

            }
            else
            {
                CartItems cartItems = new CartItems()
                {
                    CartId = _userCart.CartId,
                    Brand = _productDto.productBrand,
                    type = _productDto.productType,
                    Img = _productDto.PictureUrl,
                    Name = _productDto.Name,
                    Price = _productDto.Price,
                    ProductId = _productDto.Id,
                    Quantity = 1,



                };
               
                cartRepo.SetItems(cartItems);
                _product.Stock--;
            }

            cartRepo.UpdateStock(_product);
            cartRepo.UpdateUserCart(_userCart);
            return Ok(_userCart);
        }
        [HttpDelete("DeleteFromCart")]
        public IActionResult DeleteITems(string userid ,int productid)
        {
             var _userCart = cartRepo.GetUserCart(userid);
            var _product = productRepo.GetProductsById(productid);
            var existingCartItem =cartRepo.IFExist(_userCart, productid);

            if (existingCartItem != null)
            {
                cartRepo.DeleteFromCart(existingCartItem);
                _product.Stock++;
                cartRepo.UpdateStock(_product);
            }

            cartRepo.UpdateUserCart(_userCart);

            return Ok(_userCart);
        }

        [HttpPut("UpdateQuantity")]
        public IActionResult UpdateQuantity(UpdateQuantityDTO _dto)
        {
            var _userCart = cartRepo.GetUserCart(_dto.userId);
            var _product = productRepo.GetProductsById(_dto.ProductId);
            var existingCartItem = cartRepo.IFExist(_userCart, _dto.ProductId);
            if (existingCartItem != null)
            {
              
                           /*aaaaaaaaaaaaaaaaaaaah error hna*/
                var quantityDifference = existingCartItem.Quantity - _dto.Quantity;
                if (quantityDifference < 0 && _dto.Quantity > 10)
                {
                    existingCartItem.Quantity = _product.Stock + existingCartItem.Quantity;
                    _product.Stock = 0;
                }
                else
                {
                    _product.Stock += quantityDifference;
                    existingCartItem.Quantity = _dto.Quantity;

                }
                
                cartRepo.UpdateStock(_product);
                cartRepo.UpdateCartItem(existingCartItem);
                cartRepo.UpdateUserCart(_userCart);
                return Ok(_userCart);

            }
            else
            {
                return NotFound(new ErrorResponse() { Message = "Can not update this product"});
            }
         

        }

        [HttpGet("GetUserCart")]
        public IActionResult GetCart(string UserId)
        {
            var _userCart= cartRepo.GetUserCart(UserId);
            if (_userCart !=null)
            {
                return Ok(_userCart);
            }
            return NotFound(new ErrorResponse(){ Message= "Cart Not Found"});
        }

    }
}
