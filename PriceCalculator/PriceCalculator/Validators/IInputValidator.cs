using System.Collections.Generic;

namespace PriceCalculator.Validators
{
    public interface IInputValidator
    {
        IEnumerable<string> Validate(string[] products);
    }
}
