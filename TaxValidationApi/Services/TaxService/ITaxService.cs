using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxValidationApi.Services.TaxService
{
    public interface ITaxService
    {
        bool GetTfnValidity(long tfn);
    }
}
