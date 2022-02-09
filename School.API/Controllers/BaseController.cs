using Microsoft.AspNetCore.Mvc;

namespace School.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public async Task<ActionResult> CreateResponse<T>(Func<Task<T>> function)
        {
            try
            {
                var data = await function();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [NonAction]
        public async Task<ActionResult> CreateResponse(Func<Task> function)
        {
            try
            {
                await function();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
