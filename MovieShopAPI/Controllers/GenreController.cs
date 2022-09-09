using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            throw new NotImplementedException();
        }
    }
}
