namespace PriceCalculator.Services.DTO
{
    public class Product
    {
        public decimal Price { get; set; }
        public string Type { get; set; }

        public Product(decimal price, string type)
        {
            Price = price;
            Type = type;
        }
    }
}
