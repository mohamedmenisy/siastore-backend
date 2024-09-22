namespace e_commerce.DTO
{
    public class CartItemDTO
    {
        public int CartId { get; set; }

        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Img { get; set; }


        public int Price { get; set; }

        public int TotalPrice { get; set; }

        public string Brand { get; set; }

        public string type { get; set; }

        public int Quantity { get; set; }

    }
}
