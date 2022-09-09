using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpPost]
        [Route("movie")]
        public async Task<IActionResult> CreateMovie (Movie movie)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("movie")]
        public async Task<IActionResult> UpdateMovie (Movie movie)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("top-purchased-movies")]
        public async Task<IActionResult> GetTopPurchasedMoviesByDateRange(DateTime fromTime, TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }

    }
}
