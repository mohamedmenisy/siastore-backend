using e_commerce.DTO;
using e_commerce.Models;

namespace e_commerce.Interfaces
{
    public interface IProduct
    {
        public List<Product> GetProducts(); 
        public Product GetProductsById(int id);
        public Product AddProduct(ProductDTO2 product, string picUrl);
        public List<Product> GetproductByTypeName(string TypeName);

    }
}
