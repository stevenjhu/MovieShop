using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;

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

        public async Task<PagedResultSet<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int page= 1)
        {
            //get total row count
            var totalMovieCountOfGenre = await _movieShopDbContext.MovieGenres
                .Where(g => g.GenreId == genreId)
                .CountAsync();
            if (totalMovieCountOfGenre == 0)
            {
                throw new Exception("No Movies found for this genre");
            }

            //get the actual data
            var movies = await _movieShopDbContext.MovieGenres
                .Where(g => g.GenreId == genreId)
                .Include(g=>g.Movie)
                .OrderByDescending(m=>m.Movie.Revenue)
                .Select(m=> new Movie
                {
                    Id = m.MovieId,
                    PosterUrl = m.Movie.PosterUrl,
                    Title = m.Movie.Title,
                })
                .Skip((page-1)*pageSize)
                .Take(pageSize)
                .ToListAsync();

            var pagedMovies = new PagedResultSet<Movie>(movies, page, pageSize, totalMovieCountOfGenre);
            return pagedMovies;
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
