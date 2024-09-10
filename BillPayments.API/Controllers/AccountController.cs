using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillPayments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpGet(Name = "SayHello")]
        public IActionResult GetHelloWorld()
        {
            return Ok("Hello World!");
        }
    }
}
