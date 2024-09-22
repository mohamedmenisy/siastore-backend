using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.Models
{
    public class Product
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; } 

        public ProductType ProductType { get; set; }
        [ForeignKey("ProductType")]
        public int ProductTypeId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        [ForeignKey("ProductBrand")]
        public int ProductBrandId { get; set; }


    }
}
