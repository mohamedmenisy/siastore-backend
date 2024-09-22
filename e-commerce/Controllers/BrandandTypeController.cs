using e_commerce.Interfaces;
using e_commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    public class BrandandTypeController : Controller
    {
        private readonly IBrand brand;
        private readonly IType type;

        public BrandandTypeController(IBrand brand , IType type)
        {
            this.brand = brand;
            this.type = type;
        }
        [HttpGet("GetBrands")]
        public IActionResult GetBrands()
        {
            var Brands = brand.GetAllBrands();
            return Ok(Brands);
        }
        [HttpGet("GetTypes")]
        public IActionResult GetTypes()
        {
            var Types = type.GetAllTypes();
            return Ok(Types);
        }




        [HttpPost("AddBrand")]
        public IActionResult AddBrand(ProductBrand Brand)
        {

            var newBrand = brand.AddBrand(Brand);
            return Ok(newBrand);


        }

        [HttpPost("AddType")]
        public IActionResult AddBrand(ProductType Type)
        {

            var newBrand = type.AddType(Type);
            return Ok(Type);


        }

    }
}
