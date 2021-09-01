namespace PriceCalculator.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal ToPercentage(this decimal decimalVar)
        {
            return decimalVar/100;
        }

        public static string ToCurrencyWithPence(this decimal decimalVar)
        {
            return decimalVar >= 1 ? $"£{decimalVar:0.00}" : $"{decimalVar * 100:0}p";
        }
    }
}
