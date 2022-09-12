using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetTop30GrossingMovies();
        Task<Movie> GetById(int id);
        Task<PagedResultSet<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1);
        
    }
}
