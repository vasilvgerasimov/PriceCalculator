using System;
using System.Collections.Generic;
using PriceCalculator.Discounts;

namespace PriceCalculator
{
    public interface IOutputWriter
    {
        void ShowReceipt(decimal subTotal, IEnumerable<RelevantDiscount> discounts, decimal totalPrice);
        void ShowErrors(IEnumerable<string> errors);
    }
}
