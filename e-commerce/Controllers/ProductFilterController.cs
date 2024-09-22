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
    public class ProductFilterController : ControllerBase
    {
        private readonly IFilters filter;
        private readonly IBrand brand;
        private readonly IType type;
        private readonly IProduct product;
        public ProductFilterController(IFilters _filter,
            IBrand _brand, IType _Type, IProduct _product)
        {
            filter = _filter;
            brand = _brand;
            product = _product;
            type = _Type;
        }


        [HttpGet("GetProduct")]
        public IActionResult GetProducts(int page, [FromQuery] string? sortValue , [FromQuery] int? typeid, [FromQuery] int? brandid ,[FromQuery] string? searchValue) {

            var AllProducts = product.GetProducts();


            if (typeid.HasValue && brandid == null)
            {
                AllProducts = AllProducts.Where(x => x.ProductTypeId == typeid).ToList();
            }
            if (brandid.HasValue && typeid == null)
            {
                AllProducts = AllProducts.Where(x => x.ProductBrandId == brandid).ToList();

            }
            if (typeid.HasValue && brandid.HasValue)
            {
                AllProducts = AllProducts.Where(x => x.ProductTypeId == typeid).Where(x => x.ProductBrandId == brandid).ToList();

            }
            if (sortValue != null)
            {
                AllProducts = filter.Sort(sortValue,AllProducts);
            }
            if (searchValue !=null)
            {
                AllProducts = AllProducts.Where(p => p.Name.ToLower().Contains(searchValue.ToLower())).ToList();
            }
            if (page < 1)
                page = 1;
            var totalProducts = product.GetProducts().Count;
            var totalPages = (int)Math.Ceiling(totalProducts / (double)6);
            if (page > totalPages)

            {

                return NotFound(new ErrorResponse() { Message = $"No Product Here Back To {totalPages} " });
            }
            var myproducts = AllProducts.Skip((page - 1) * 6).Take(6).ToList();


            var productsDto = Mapping.MapingFun(myproducts);


            return Ok(new { pageNumber = page, TotalPages = totalPages, PageSize = productsDto.Count, AllProducts = totalProducts, products = productsDto });
        }

    }
}
