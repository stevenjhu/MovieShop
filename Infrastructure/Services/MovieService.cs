using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUserRepository _userRepository;

        public MovieService(IMovieRepository movieRepository, IUserRepository userRepository)
        {
            _movieRepository = movieRepository;
            _userRepository = userRepository;
        }

        public async Task<List<MovieCardModel>> GetTop30GrossingMovies()
        {
            var movies = await _movieRepository.GetTop30GrossingMovies();

            var movieCards = new List<MovieCardModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardModel { Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl });
            }

            return movieCards;
        }

        public async Task<List<MovieCardModel>> GetMoviesByGenre(int genreId, int pageSize, int pageNumber)
        {
            var movies = await _movieRepository.GetMoviesByGenre(genreId,pageSize,pageNumber);
            var movieCards = new List<MovieCardModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardModel { Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl });
            }
            return movieCards;
        }

        public async Task<MovieDetailsModel> GetMovieDetails(int movieId)
        {
            var movieDetails = await _movieRepository.GetById(movieId);
            var purchased = await _userRepository.PurchaseExists(344, movieId);
            
            if(movieDetails == null)
            {
                return null;
            }
            var movieDetailsModel = new MovieDetailsModel
            {
                Id = movieDetails.Id,
                Title = movieDetails.Title,
                PosterUrl = movieDetails.PosterUrl,
                BackdropUrl = movieDetails.BackdropUrl,
                OriginalLanguage = movieDetails.OriginalLanguage,
                Overview = movieDetails.Overview,
                Budget = movieDetails.Budget,
                ReleaseDate = movieDetails.ReleaseDate,
                Revenue = movieDetails.Revenue,
                ImdbUrl = movieDetails.ImdbUrl,
                TmdbUrl = movieDetails.TmdbUrl,
                RunTime = movieDetails.RunTime,
                Tagline = movieDetails.Tagline,
                Price = movieDetails.Price
            };
            foreach (var trailer in movieDetails.Trailers)
            {
                movieDetailsModel.Trailers.Add(new TrailerModel
                {
                    Name = trailer.Name,
                    TrailerUrl = trailer.TrailerUrl
                });
            }
            foreach (var cast in movieDetails.CastsOfMovie)
            {
                movieDetailsModel.Casts.Add(new CastModel 
                {
                    Id = cast.CastId,
                    Name = cast.Cast.Name,
                    Character = cast.Character,
                    ProfilePath = cast.Cast.ProfilePath
                });
            }
            foreach (var genre in movieDetails.GenresOfMovie)
            {
                movieDetailsModel.Genres.Add(new GenreModel
                {
                    Id = genre.GenreId,
                    Name = genre.Genre.Name
                });
            }
            decimal? rating = 0;
            foreach (var review in movieDetails.ReviewsOfMovie)
            {
                movieDetailsModel.Reviews.Add(new ReviewModel
                {
                    MovieId = review.MovieId,
                    UserId = review.UserId,
                    Rating = review.Rating,
                    CreateDate = review.CreateDate,
                    ReviewText = review.ReviewText
                });
                rating += review.Rating;
            }
            try
            {
                rating /= movieDetails.ReviewsOfMovie.Count;
            }
            catch (DivideByZeroException e)
            {
                rating = null;
            }
            movieDetailsModel.Rating = rating != null ? Math.Round(rating.Value, 1):null;

            if (purchased !=null)
            {
                movieDetailsModel.IsMoviePurchased = true;
            }
            return movieDetailsModel;
        }

        
    }
}
