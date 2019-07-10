using Microsoft.AspNetCore.Mvc;
using TaxValidationApi.Services;

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
        [HttpGet("Validity/{tfn}")]
        public ActionResult<bool> GetTfnValidity(int tfn)
        {
            return _taxService.GetTfnValidity(tfn);
        }
    }
}
