using System;
using System.Collections.Generic;
using PriceCalculator.Discounts;
using PriceCalculator.Extensions;

namespace PriceCalculator
{
    public interface IOutputWriter
    {
        void ShowReceipt(decimal subTotal, IEnumerable<RelevantDiscount> discounts, decimal totalPrice);
        void ShowErrors(string[] errors);
    }

    public class OutputWriter : IOutputWriter
    {
        public void ShowReceipt(decimal subTotal, IEnumerable<RelevantDiscount> discounts, decimal totalPrice)
        {
            Console.WriteLine($"Subtotal: {subTotal.ToCurrencyWithPence()}");
            foreach (var discount in discounts)
            {
                Console.WriteLine(discount.Text);
            }
            Console.WriteLine($"Total UnitPrice : {totalPrice.ToCurrencyWithPence()}");
            Console.ReadLine();
        }
        public void ShowErrors(string[] errors)
        {
            foreach (var error in errors)
            {
                Console.WriteLine(error + Environment.NewLine);
            }

            Console.ReadLine();
        }
    }
}
