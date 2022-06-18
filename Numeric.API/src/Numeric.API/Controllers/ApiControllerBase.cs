using Microsoft.AspNetCore.Mvc;
using Numeric.BusinessLogic.Enums;
using Numeric.BusinessLogic.Models;

namespace Numeric.API.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult MapResponse<TBusinessModel, TResponseModel>(ValueServiceResult<TBusinessModel> result, Func<TBusinessModel, TResponseModel> map)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            else if (map is null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            if (result.IsSuccessful)
            {
                return result.IsEmpty
                    ? NoContent()
                    : Ok(map.Invoke(result.Value));
            }
            else
            {
                return MapErrorResponse(result);
            }
        }

        protected IActionResult MapResponse(ServiceResult result)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            if (result.IsSuccessful)
            {
                return Ok();
            }
            else
            {
                return MapErrorResponse(result);
            }
        }

        private IActionResult MapErrorResponse(ServiceResult result)
        {
            return result.Error.Type switch
            {
                ErrorType.BusinessError => BadRequest(result.Error),
                _ => StatusCode(StatusCodes.Status500InternalServerError, result.Error)
            };
        }
    }
}
