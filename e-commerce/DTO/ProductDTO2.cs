namespace e_commerce.DTO
{
    public class ProductDTO2
    {
        public string name { get; set; }
        public string description { get; set; }

        public decimal price { get; set; }

        public IFormFile Img {  get; set; }
        public int ProductTypeId { get; set; }

        public int ProductBrandId { get; set; }



    }
}
