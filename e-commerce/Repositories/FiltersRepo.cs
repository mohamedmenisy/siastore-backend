using e_commerce.Context;
using e_commerce.DTO;
using e_commerce.Errors;
using e_commerce.Helper;
using e_commerce.Interfaces;
using e_commerce.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Buffers;

namespace e_commerce.Repositories
{
    public class FiltersRepo:IFilters
    {
        private readonly StoreContext db;

        public FiltersRepo(StoreContext _db)
        {
            db = _db;
        }
        //filterBrand
        public List<ProductDTO> FilterByBrand(int id)
        {
            var productsBrand = db.Products.Where(P => P.ProductBrandId == id).Include(b=>b.ProductBrand).Include(T => T.ProductType).ToList();
            var products = Mapping.MapingFun(productsBrand);
            return products;
        }
        //filter-Type
        public List<ProductDTO> FilterByType(int id)
        {
            var productsType = db.Products.Where(P => P.ProductTypeId == id).Include(b => b.ProductBrand).Include(T => T.ProductType).ToList();
            var products = Mapping.MapingFun(productsType);
            return products;
        }

        // --- Search by name
        public List<ProductDTO> SearchByName(string name)
        {
           
            var products =  db.Products.Where(p => p.Name.ToLower().Contains(name.ToLower())).Include(b => b.ProductBrand).Include(T => T.ProductType).ToList();
            var productsDto = Mapping.MapingFun(products);

            return productsDto;

        }
        // Sort Asc or Desc
        public List<Product> Sort(string SortType, List<Product>? products)
        {
            var myProducts = new List<Product>();
            if (SortType == "alpha" && products != null)
            {
                myProducts = products?.OrderBy(p => p.Name).ToList();
                


            }
            else if (SortType == "htl")
            {
                myProducts = products?.OrderByDescending(p => p.Price).ToList();


            }
            else
            {
                myProducts = products?.OrderBy(p => p.Price).ToList();
            }
            

            return myProducts;


        }

    }
}
