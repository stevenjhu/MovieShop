using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> GetUserProfileById(int userId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("purchase-movie")]
        public async Task<IActionResult> BuyMovie(int movieId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> AddFavorite(int movieId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("un-favorite")]
        public async Task<IActionResult> RemoveFavorite(int movieId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("check-movie-favorite/{movieId:int}")]
        public async Task<IActionResult> FavoriteExists(int movieId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("add-review")]
        public async Task<IActionResult> AddReview(int movieId, decimal rating, string reviewText)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("edit-review")]
        public async Task<IActionResult> EditReveiw(int movieId, decimal rating, string reviewText)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("delete-review/{movieId:int}")]
        public async Task<IActionResult> DeleteReview(int movieId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetMoviesPurchasedByUser()
        {
            //we need to get the userId from the token, using HttpContext
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("purchase-details/{movieId:int}")]
        public async Task<IActionResult> GetPurchaseDetails(int movieId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("check-movie-purchased/{movieId:int}")]
        public async Task<IActionResult> PurchaseExists(int movieId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("favorites")]
        public async Task<IActionResult> GetFavorites()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("movie-reviews")]
        public async Task<IActionResult> GetReview()
        {
            throw new NotImplementedException();
        }

    }
}
