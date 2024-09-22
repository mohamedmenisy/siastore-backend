using e_commerce.Models;

namespace e_commerce.Interfaces
{
    public interface IBrand
    {
        public ProductBrand GetBrand(int id);
        public List<ProductBrand> GetAllBrands();
        public ProductBrand AddBrand(ProductBrand brand);
    }
}
