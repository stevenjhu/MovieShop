using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        [HttpGet]
        [Route("{castId:int}")]
        public async Task<IActionResult> GetCastDetailsById(int castId)
        {
            throw new NotImplementedException();
        }
    }
}
