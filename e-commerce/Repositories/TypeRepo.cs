using e_commerce.Context;
using e_commerce.Interfaces;
using e_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Repositories
{
    public class TypeRepo : IType
    {

        private readonly StoreContext db;

        public TypeRepo(StoreContext _db)
        {
            db = _db;
        }

        public List<ProductType> GetAllTypes()
        {
            var Types = db.ProductTypes.ToList();
            return Types;
        }

        public ProductType GetType(int id)
        {
            var ProductType = db.ProductTypes.FirstOrDefault(x => x.Id == id);
            return ProductType;
        }
        public ProductType AddType(ProductType Type)
        {
            db.ProductTypes.Add(Type);
            db.SaveChanges();
            return Type;
        }
    }
}
