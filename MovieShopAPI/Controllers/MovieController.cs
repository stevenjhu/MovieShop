using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMoviesByPage()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{movieId:int}")]
        public async Task<IActionResult> GetMovieDetailsById(int movieId)
        {
            var movie = await _movieService.GetMovieDetails(movieId);
            if (movie == null)
            {
                return NotFound(new { errorMessage = $"No Movie Found for {movieId}" });
            }
            return Ok(movie);
        }

        [HttpGet]
        [Route("top-rated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("top-grossing")] //Attribute Routing
        //MVC_Version http://localhost:port/movie/GetTopGrossingMovies => traditional/convention based routing
        //API_Version http://localhost:port/api/movie/top-grossing
        public async Task<IActionResult> GetTopGrossingMovies()
        {
            //call my service
            var movies = await _movieService.GetTop30GrossingMovies();

            //return
            //return the movie information in JSON format
            //ASP .NET code automatically serializes C# objects to JSON objects
            //System.Text.Json .NET 3 & newer
            //older version of .NET to work with JSON Newtonsoft.JSON
            //return data(json format) along with it return HTTP status code 
            if (movies == null || !movies.Any())
            {
                return NotFound(new { errorMessage = "No Movies Found." });
            }
            return Ok(movies);
        }

        [HttpGet]
        [Route("genre/{genreId:int}")]
        public async Task<IActionResult> GetMoviesByGenre(int genreId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{movieId:int}/reviews")]
        public async Task<IActionResult> GetReviewsByMovieId(int movieId)
        {
            throw new NotImplementedException();
        }
    }
}
