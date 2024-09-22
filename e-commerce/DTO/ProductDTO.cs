using e_commerce.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.DTO
{
    public class ProductDTO
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public string PictureUrl { get; set; }

        public decimal Price { get; set; }
        public int Stock { get; set; }


        public string productType { get; set; }
        public string productBrand { get; set; }

    }
}
