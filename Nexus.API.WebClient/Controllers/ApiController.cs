using Microsoft.AspNetCore.Mvc;
using Nexus.API.Manager.Validations;

namespace Nexus.API.WebClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        [NonAction]
        public IActionResult GetExceptions(Exception ex)
        {
            if (ex is CustomValidation)
            {
                return BadRequest(new
                {
                    result = false,
                    message = ex.Message,
                    error = ((CustomValidation)ex).validations
                });
            }
            else
            {
                //if (ex.InnerException.Message != null)
                //{
                //    //optional
                //    return BadRequest(new
                //    {
                //        result = false,
                //        message = ex.Message,
                //        error = ex.InnerException.Message
                //    });
                //}
                //else
                //{
                    return BadRequest(new
                    {
                        result = false,
                        message = ex.Message,
                        error = ""
                    });
                //}
            }
        }
    }
}
