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
        public async Task<IActionResult> CalculateInput()
        {
            //for demo puproses I'll just use property-defined UserInputEntity
            var userInput = new CalculateLoanRequest();

            _service.PerformCalculations(userInput);

            return Ok();
        }





    }
}
