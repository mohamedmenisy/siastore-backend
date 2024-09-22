using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace e_commerce.Models
{
    public class Favorite
    {
        [Key]
        [JsonIgnore]
        public int FavoriteID { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
   
        public List<FavItem> FavItems { get; set; } = new List<FavItem>(); 
        [JsonIgnore]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
