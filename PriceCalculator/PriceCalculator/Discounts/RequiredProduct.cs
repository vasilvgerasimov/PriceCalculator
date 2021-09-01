namespace PriceCalculator.Discounts
{
    public class RequiredProduct
    {
        public string Type { get; }
        public int Quantity { get; }

        public RequiredProduct(string type, int quantity)
        {
            Type = type;
            Quantity = quantity;
        }
    }
}
