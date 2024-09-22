using e_commerce.Models;

namespace e_commerce.Interfaces
{
    public interface IType
    {
        public ProductType GetType(int id);
        public List<ProductType> GetAllTypes();
        public ProductType AddType(ProductType Type);
    }
}
