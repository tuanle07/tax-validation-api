using System;
using System.Linq;

namespace TaxValidationApi.Services
{
    public interface ITaxService
    {
        bool GetTfnValidity(int tfn);
    }

    public class TaxService : ITaxService
    {
        public bool GetTfnValidity(int tfn)
        {
            if (tfn < 0)
            {
                throw new Exception("TFN cannot be a negative");
            };

            //converting tfn to array of digits
            var digits = tfn.ToString()
                .Select(t => int.Parse(t.ToString()))
                .ToArray();

            if (digits.Length > 9)
            {
                throw new Exception("TFN cannot contain more than 9 digits");
            };

            var digitsWithWeightingFactor = digits.Select((t, i) => new
            {
                digit = t,
                weighting = GetWeightingFactor(i, digits.Length)
            });
            var sum = digitsWithWeightingFactor.Aggregate(0, (acc, x) => acc + x.digit * x.weighting);
            return sum % 11 == 0 ? true : false;
        }

        private int GetWeightingFactor(int index, int totalDigits)
        {
            switch (index)
            {
                case 0: return 10;
                case 1: return 7;
                case 2: return 8;
                case 3: return 4;
                case 4: return 6;
                case 5: return 3;
                case 6: return 5;
                case 7: return totalDigits == 8 ? 1 : 2;
                case 8: return 1;
                default: return 0;
            }
        }
    }
}
