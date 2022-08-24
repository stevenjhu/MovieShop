using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        public List<MovieCardModel> GetTop30GrossingMovies()
        {
            var movieRepository = new MovieRepository();
            var movies = movieRepository.GetTop30GrossingMovies().Take(6);

            var movieCards = new List<MovieCardModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardModel { Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl });
            }

            return movieCards;
        }
    }
}
