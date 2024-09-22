using e_commerce.Context;
using e_commerce.DTO;
using e_commerce.Errors;
using e_commerce.Helper;
using e_commerce.Interfaces;
using e_commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProduct productRepo;

        public ProductsController(IProduct _ProductRepo)
        {
            productRepo = _ProductRepo;
        }


       

        [HttpGet("/GetProductById/{id}")]
        public IActionResult GetProduct(int id)
        {
            var myproduct = productRepo.GetProductsById(id);
            if (myproduct == null)
            {
                return NotFound(new ErrorResponse() { Message="Product Not Found"});
            }
            var product = Mapping.MapingFun(myproduct);

            return Ok(product);
            
        }

    



        //  Pagination ----------------------------------------


        [HttpGet("GetProducts/{page}")]
        public IActionResult GetProductsByPage(int page)
        {
            if (page < 1)
                page = 1;
           

            var totalProducts = productRepo.GetProducts().Count;
            var totalPages = (int)Math.Ceiling(totalProducts / (double)6);
            if (page > totalPages)
               
            {
                
                return NotFound(new ErrorResponse() { Message = $"No Product Here Back To {totalPages} " });
            }
          


            var AllProducts = productRepo.GetProducts().Skip((page - 1) * 6).Take(6).ToList();
            var productsDto = Mapping.MapingFun(AllProducts);
            Random random = new Random();
            var randomList= productsDto.OrderBy(x=>random.Next()).ToList();
            
            return Ok(new { pageNumber = page , TotalPages = totalPages, PageSize = AllProducts.Count ,AllProducts=totalProducts, products = randomList });
        }







        //-----------------------------------------------------

        [HttpPost("Add")]
       public IActionResult AddPRoduct(ProductDTO2 product)
        {

            var projectFolder = Directory.GetCurrentDirectory();
            var relativeImagesPath = Path.Combine("wwwroot", "Images");
            var fullImagesPath = Path.Combine(projectFolder, relativeImagesPath);
            var fileName = $"{Guid.NewGuid()}_{product.Img.FileName}";
            var fullImagePath = Path.Combine(fullImagesPath, fileName);
            using (var stream = new FileStream(fullImagePath, FileMode.Create))
            {
                product.Img.CopyTo(stream);
                stream.Flush();
            }
            var url = $"{Request.Scheme}://{Request.Host}/wwwroot/Images/{fileName}";

            var myproduct = productRepo.AddProduct(product,url);




            return Ok(myproduct);
        }

        [HttpGet("RandomProducts")]
        public IActionResult GetRandomProducts()
        {
            var myproduct = productRepo.GetProducts();
            if (myproduct.Count == 0)
            {
                return NotFound(new ErrorResponse() { Message = "Products Not Found" });
            }
            var products = Mapping.MapingFun(myproduct);
            Random rnd = new Random();
            var randomProducts = products.OrderBy(x => rnd.Next()).Take(6).ToList();


            return Ok(randomProducts);
        }


        /*-----------------------*/
            [HttpGet("GetProductByTypename")]
            public IActionResult GetProductsByType(string typename)
            {
                var products = productRepo.GetproductByTypeName(typename);
                var mappingProducts = Mapping.MapingFun(products);
                return Ok(mappingProducts);
            }
    }
}
