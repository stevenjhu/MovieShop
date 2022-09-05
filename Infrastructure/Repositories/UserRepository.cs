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
    public class UserRepository : IUserRepository
    {
        private readonly MovieShopDbContext _dbContext;
        public UserRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
        public async Task<User> GetUserById(int userid)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userid);
            return user;
        }

        public async Task<User> AddUser(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
        public async Task<Favorite> AddFavorite(Favorite favorite)
        {
            _dbContext.Favorites.Add(favorite);
            await _dbContext.SaveChangesAsync();
            return favorite;
        }
        public async Task<bool> FavoriteExists(Favorite favorite)
        {
            var dbFavorite = await _dbContext.Favorites.FirstOrDefaultAsync(u => u.MovieId == favorite.MovieId && u.UserId == favorite.UserId);
            if (dbFavorite == null)
            {
                return false;
            }
            return true;
        }
        public async Task RemoveFavorite(Favorite favorite)
        {
            _dbContext.Favorites.Remove(favorite);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ReviewExists(Review review)
        {
            var dbReview = await _dbContext.Reviews.FirstOrDefaultAsync(u => u.UserId == review.UserId && u.MovieId == review.MovieId);
            var exist = dbReview == null ? false : true;
            return exist;
        }

        public async Task<Review> AddReview(Review review)
        {
            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }

        public async Task UpdateReview(Review review)
        {
            var dbReview = await _dbContext.Reviews.FirstOrDefaultAsync(u => u.UserId == review.UserId && u.MovieId == review.MovieId);
            dbReview.Rating = review.Rating;
            dbReview.ReviewText = review.ReviewText;
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveReview(Review review)
        {
            _dbContext.Reviews.Remove(review);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Favorite>> GetFavoritesById(int userid)
        {
            var favorites = await _dbContext.Favorites.Include(u=>u.Movie).Where(u => u.UserId == userid).ToListAsync();
            return favorites;
        }

        public async Task<List<Purchase>> GetPurchasesById(int userId)
        {
            var purchases = await _dbContext.Purchases
                .Include(u=>u.Movie)
                .Where(u => u.UserId == userId)
                .ToListAsync();
            return purchases;
        }

        public async Task<List<Review>> GetReviewsById(int userId)
        {
            var reviews = await _dbContext.Reviews
                .Include(u=>u.Movie)
                .Include(u=>u.User)
                .Where(u => u.UserId == userId)
                .ToListAsync();
            return reviews;
        }
    }
}
