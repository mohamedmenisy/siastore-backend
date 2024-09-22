using e_commerce.DTO;
using e_commerce.Models;

namespace e_commerce.Interfaces
{
    public interface IFilters
    {
        public List<Product> Sort(string SortType, List<Product> products);
        public List<ProductDTO> FilterByBrand(int id);
        public List<ProductDTO> FilterByType(int id);
        public List<ProductDTO> SearchByName(string name );

    }
}
