using System.Text.Json.Serialization;

namespace e_commerce.Models
{
    public class ProductType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public virtual List<Product>? Products { get; set; }

    }
}
