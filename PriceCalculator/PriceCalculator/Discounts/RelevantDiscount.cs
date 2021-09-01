namespace PriceCalculator.Discounts
{public class RelevantDiscount
    {
        public decimal Price { get; }
        public string Text { get; }

        public RelevantDiscount(decimal price, string text)
        {
            Price = price;
            Text = text;
        }
    }
}
