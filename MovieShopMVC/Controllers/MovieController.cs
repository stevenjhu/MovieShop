using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MovieController : Controller
    {

        //public MovieController(IMovieService movieService)
        //{
        //    _movieService = movieService;
        //}
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            //go to db and get the movie info by movid id
            //and send the data(Model) to the view
            //ADO.NET
            //Dapper StackOverflow -> Micro ORM
            //Entity Framework Core => Full ORM for Microsoft

            //Select * from Movies where id = 12;
            //Code is Maintenable, Reusable, Readable, Extensible, Testable
            //layers => Layered architecture
            //Onion, Clean Architecture
            var movieDetails = await _movieService.GetMovieDetails(id);
            return View(movieDetails);
        }

        [HttpGet]
        public async Task<IActionResult> MoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            var paginatedResultSet = await _movieService.GetMoviesByGenre(genreId, pageSize, pageNumber);
            return View(paginatedResultSet);
        }
    }
}
