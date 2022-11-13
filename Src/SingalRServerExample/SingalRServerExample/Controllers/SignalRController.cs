using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SingalRServerExample.Business;

namespace SingalRServerExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalRController : ControllerBase
    {
        readonly MyBusiness _myBusiness;
        public SignalRController(MyBusiness myBusiness)
        {
            _myBusiness = myBusiness;

        }

        [HttpGet("{message}")]
        public async Task<IActionResult> Index(string message )
        {

           await _myBusiness.SendMessageAsync(message);

            return Ok();
        }

    }
}
