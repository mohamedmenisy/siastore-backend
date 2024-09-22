using e_commerce.Context;
using e_commerce.DTO;
using e_commerce.Interfaces;
using e_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Repositories
{
    public class ProductRepo : IProduct
    {
        private readonly StoreContext db;

        public ProductRepo(StoreContext _db)
        {
            db = _db;
        }

       

        public List<Product> GetProducts()
        {
           return db.Products.Include(t=>t.ProductType).Include(b=>b.ProductBrand).ToList();
        }

        public Product GetProductsById(int id)
        {
            return db.Products.Include(t => t.ProductType).Include(b => b.ProductBrand).FirstOrDefault(p => p.Id == id);
        }
      
        public Product AddProduct (ProductDTO2 product ,string picUrl) {
         
          Product myproduct = new Product() {  Name = product.name , Description = product.description , PictureUrl = picUrl, Price = product.price, ProductBrandId = product.ProductBrandId,ProductTypeId=product.ProductTypeId };

            db.Products.Add(myproduct);
            db.SaveChanges();
            return myproduct;
        
        }
        public List<Product> GetproductByTypeName(string TypeName)
        {
            return db.Products.Include(t => t.ProductType).Include(b => b.ProductBrand).Where(p=>p.ProductType.Name.ToLower() == TypeName.ToLower()).ToList();

        }


    }
}
