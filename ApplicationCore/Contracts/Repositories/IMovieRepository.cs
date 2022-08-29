using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repository
{
    public interface IMovieRepository
    {
        List<Movie> GetTop30GrossingMovies();
        public Movie GetById(int id);
        public Movie GetByGenre(string genre);
    }
}
