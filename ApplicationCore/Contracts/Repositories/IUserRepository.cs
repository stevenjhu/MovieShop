using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int userid);
        Task<User> GetUserByEmail(string email);
        Task<User> AddUser(User user);
        Task<Favorite> AddFavorite(Favorite favorite);
        Task<bool> FavoriteExists(Favorite favorite);
        Task RemoveFavorite(Favorite favorite);
        Task<Review> AddReview(Review review);
        Task UpdateReview(Review review);
        Task RemoveReview(Review review);
        Task<bool> ReviewExists(Review review);
        Task<List<Favorite>> GetFavoritesById(int userId);
        Task<List<Purchase>> GetPurchasesById(int userId);
        Task<List<Review>> GetReviewsById(int userId);
        Task<Purchase> GetPurchasesDetailByUserAndMovieId(int userId, int movieId);
        Task<Purchase> PurchaseExists(int userId, int movieId);
        Task AddPurchase(Purchase purchase);
        
    }
}
