using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PricingEngine.Repositories;
using PricingEngine.Services;

namespace PricingEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInputController : ControllerBase
    {
        private readonly IUserInputRepository _repository;
        private readonly ICalculateUserInput _service;

        public UserInputController(
            IUserInputRepository repository,
            ICalculateUserInput service)
        {
            _repository = repository;
            _service = service;
        }


        [HttpPost("add-user-input")]
        public async Task<IActionResult> AddUserInput()
        {

            return Ok();
        }

        [HttpPost("calculate-input")]
        public async Task<IActionResult> CalculateInput()
        {

            await _service.CalculateInputsAsync();

            await _service.SaveChangesAsync();

            return Ok();
        }





    }
}
