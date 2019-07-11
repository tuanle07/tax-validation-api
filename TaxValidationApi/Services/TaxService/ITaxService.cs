namespace TaxValidationApi.Services.TaxService
{
    public interface ITaxService
    {
        bool GetTfnValidity(long tfn);
    }
}
