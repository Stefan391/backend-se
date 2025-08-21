using backend_se.Common.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace backend_se.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
