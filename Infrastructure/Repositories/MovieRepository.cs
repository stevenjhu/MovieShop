using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository:IMovieRepository
    {
        private MovieShopDbContext _movieShopDbContext;
        
        public MovieRepository(MovieShopDbContext dbContext)
        {
            _movieShopDbContext = dbContext;
        }
        public async Task<Movie> GetById(int id)
        {
            //select * from movie where id = 1 join genre, cast, moviegenre, moviecast, rating
            var movieDetails = await _movieShopDbContext.Movies
                .Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre)
                .Include(m => m.CastsOfMovie).ThenInclude(m => m.Cast)
                .Include(m =>m.ReviewsOfMovie)
                .Include(m => m.Trailers)
                .FirstOrDefaultAsync(m=>m.Id==id);
            return movieDetails;
        }

        public async Task<List<Movie>> GetMoviesByGenre(int genreId, int pageSize, int pageNumber)
        {
            var movieGenre = await _movieShopDbContext.MovieGenres.FirstOrDefaultAsync(m => m.GenreId == genreId);
            var movies = await _movieShopDbContext.Movies.Where(m => m.GenresOfMovie.Contains(movieGenre)).Skip(pageSize*(pageNumber-1)).Take(pageSize).ToListAsync();
            return movies;
        }

        public async Task<List<Movie>> GetTop30GrossingMovies()
        {
            //call the database with EF Core and get the data
            //use MovieShopDbContext and Movies DbSet
            //select top 30 * from Movies order by Revenue
            //corresponding LINQ Query
            var movies = await _movieShopDbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }
    }
}
