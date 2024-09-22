using e_commerce.DTO;
using e_commerce.Errors;
using e_commerce.Helper;
using e_commerce.Interfaces;
using e_commerce.Models;
using e_commerce.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavorite favrepo;
        private readonly IProduct productrepo;

        public FavoriteController(IFavorite _Favrepo,IProduct _productrepo)
        {
            favrepo = _Favrepo;
            productrepo = _productrepo;
        }
        [HttpPost("AddToFav")]
        public IActionResult AddFavorite(int _productid, string _userid)
        {
            var product = productrepo.GetProductsById(_productid);
            var _productDto = Mapping.MapingFun(product);
            var _favCart = favrepo.GetUserFavorite(_userid);
            var existingFavItem = favrepo.IFExist(_favCart, _productid);
            if (existingFavItem != null)
            {
                return BadRequest(new ErrorResponse() { Message = "Product Existing" });


            }
            else
            {
                FavItem item = new FavItem() {
                    FavoriteId = _favCart.FavoriteID,
                    Img = _productDto.PictureUrl,
                    Name = _productDto.Name,
                    ProductId = _productDto.Id,

                };
                favrepo.Additem(item);
                return Ok(new ErrorResponse() { Message="Success"});
            }


           
        }

        [HttpDelete("DeleteItem")]
        public IActionResult DeleteITem(string _userid, int _productid)
        {
            var product = productrepo.GetProductsById(_productid);
            var _favCart = favrepo.GetUserFavorite(_userid);
            var existingFavItem = favrepo.IFExist(_favCart, _productid);
            if (existingFavItem != null) {

                favrepo.DeleteItem(existingFavItem);
                return Ok(_favCart);

            }
            else
            {

                return BadRequest(new ErrorResponse() { Message = "Faild" });
            }
        }

        [HttpGet("GetFavorite")]
        public IActionResult GetFavorite(string userid)
        {
            var userFav = favrepo.GetUserFavorite(userid);
            if (userFav != null)
            {
                return Ok(userFav);
            }
            return NotFound(new ErrorResponse() { Message = "UserFavorite Not Found" });
        }
    }
}
