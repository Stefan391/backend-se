using Microsoft.AspNetCore.Mvc;

namespace backend_se.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok("ok");
        }
    }
}
