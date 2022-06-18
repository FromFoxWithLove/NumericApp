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

        [HttpPut]
        public IActionResult Calculate([FromBody] CalculatorRequest request)
        {
            try
            {
                var result = _service.Calculate(request.Expression);
                return MapResponse(result, x => x);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return Ok();
        }
    }
}