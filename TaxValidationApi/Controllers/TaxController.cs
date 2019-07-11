using Microsoft.AspNetCore.Mvc;
using TaxValidationApi.Services.TaxService;

namespace TaxValidationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly ITaxService _taxService;

        public TaxController(ITaxService taxService)
        {
            _taxService = taxService;
        }

        // GET api/tax/validity/5
        [HttpGet("validity/{tfn}")]
        public ActionResult<bool> GetTfnValidity(long tfn)
        {
            return _taxService.GetTfnValidity(tfn);
        }
    }
}
