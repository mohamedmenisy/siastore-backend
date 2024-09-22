using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e_commerce.Models
{
    public class UserCart
    {
        [Key]
        [JsonIgnore]
        public int CartId { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
        public virtual List<CartItems> CartItems { get; set; } = new List<CartItems>();
         [JsonIgnore]
        public ApplicationUser ApplicationUser { get; set; }

    }
}
