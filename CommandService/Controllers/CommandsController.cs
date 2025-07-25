using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [ApiController]
    [Route("api/c/[controller]")]
    public class CommandsController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> TestInbound()
        {
            return Ok("Inbound test successful");
        }
    }
}