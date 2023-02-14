using Microsoft.AspNetCore.Mvc;
using PricingEngine.Models.Requests;
using PricingEngine.Services;

namespace PricingEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInputController : ControllerBase
    {
        private readonly ICalculationService _service;

        public UserInputController(
            ICalculationService service)
        {
            _service = service;
        }


        [HttpPost("calculate-input")]
        public IActionResult CalculateInput()
        {
            //for demo puproses I'll just use property-defined UserInputEntity
            var userInput = new CalculateLoanRequest();

            var loans = _service.PerformCalculations(userInput);

            return Ok(loans);
        }





    }
}
