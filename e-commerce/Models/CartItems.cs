using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e_commerce.Models
{
    public class CartItems
    {
        [Key]
        [ForeignKey("UserCart")]
        [Required]
        public int CartId { get; set; }
        [Key]
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]
        public string Img { get; set; }
        [Required]
        public decimal Price { get; set; }

        public string Brand { get; set; }
        public string type { get; set; }

        public int Quantity { get; set; }
        public decimal TotalPrice => Quantity * Price;
        [JsonIgnore]

        public UserCart UserCart { get; set; }
        [JsonIgnore]

        public Product Product { get; set; }

    }
}