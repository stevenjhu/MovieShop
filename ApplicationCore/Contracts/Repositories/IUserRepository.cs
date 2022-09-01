using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repository
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

    }
}
