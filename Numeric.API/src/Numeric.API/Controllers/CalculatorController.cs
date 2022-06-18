using Microsoft.AspNetCore.Mvc;
using Numeric.API.Models.Calculator;
using Numeric.BusinessLogic.Interfaces;

namespace Numeric.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ApiControllerBase
    {
        private readonly ICalculatorService _service;

        public CalculatorController(ICalculatorService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetRandomValue()
        {
            var random = new Random();
            return Ok(random.Next(-1000, 1001));
        }


        [HttpPut]
        public IActionResult Calculate([FromBody] CalculatorRequest request)
        {
            var result = _service.Calculate(request.Expression);
            return MapResponse(result, x => x);
        }
    }
}