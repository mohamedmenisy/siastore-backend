using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace e_commerce.Models
{
    public class FavItem
    {
        [Key]
        [ForeignKey("Favorite")]
        [Required]
        public int FavoriteId { get; set; }
        [Key]
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]
        public string Img { get; set; }
       
        [JsonIgnore]

        public Favorite Favorite { get; set; }
        [JsonIgnore]

        public Product Product { get; set; }
    }
}
