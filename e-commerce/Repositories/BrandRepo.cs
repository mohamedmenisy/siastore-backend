using e_commerce.Context;
using e_commerce.Interfaces;
using e_commerce.Models;

namespace e_commerce.Repositories
{
    public class BrandRepo : IBrand
    {
        private readonly StoreContext db;

        public BrandRepo(StoreContext _db)
        {
            db = _db;
        }
        public List<ProductBrand> GetAllBrands()
        {
            var brands = db.ProductBrands.ToList();
            return brands;
        }

        public ProductBrand GetBrand(int id)
        {
            var productBrand = db.ProductBrands.FirstOrDefault(x => x.Id == id);
            return productBrand;
           
        }
        public ProductBrand AddBrand(ProductBrand brand)
        {
            db.ProductBrands.Add(brand);
            db.SaveChanges();
            return brand;
        }

    }
}
