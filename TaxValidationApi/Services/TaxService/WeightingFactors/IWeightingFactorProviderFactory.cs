using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxValidationApi.Services.TaxService.WeightingFactors
{
    public interface IWeightingFactorProviderFactory
    {
        IWeightingFactor GetWeightingFactorProvider(int totalDigits);
    }
}
