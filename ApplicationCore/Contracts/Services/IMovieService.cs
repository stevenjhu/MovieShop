using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IMovieService
    {
        Task<List<MovieCastModel>> GetTop30GrossingMovies();
        Task<MovieDetailsModel> GetMovieDetails(int movieId);
        Task<MovieDetailsModel> GetMoviesByGenre(int genreId, int pageSize, int pageNumber);
    }
}
