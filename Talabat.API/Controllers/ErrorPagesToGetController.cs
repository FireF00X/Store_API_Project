using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;

namespace Talabat.API.Controllers
{
    [Route("[controller]/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorPagesToGetController : ControllerBase
    {
        public ActionResult ErrorPage(int code)
        {
            if(code == 404)
            {
                return NotFound(new ApiResponse(code));
            }
            else if(code == 401)
            {
                return Unauthorized(new ApiResponse(code));
            }else
            {
                return StatusCode(code);
            }
        }
    }
}
