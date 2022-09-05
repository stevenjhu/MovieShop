using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            var dbFavorite = new Favorite
            {
                MovieId = favoriteRequest.MovieId,
                UserId = favoriteRequest.UserId
            };
            var addedFavorite = await _userRepository.AddFavorite(dbFavorite);
        }
        public async Task<bool> FavoriteExists(int id, int movieId)
        {
            var favorite = new Favorite { UserId = id, MovieId = movieId };
            var result = await _userRepository.FavoriteExists(favorite);
            return result;
        }

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            var favorite = new Favorite { UserId=favoriteRequest.UserId, MovieId=favoriteRequest.MovieId };
            await _userRepository.RemoveFavorite(favorite);
        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            var dbReivew = new Review
            {
                MovieId = reviewRequest.MovieId,
                UserId = reviewRequest.UserId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText
            };
            var exist = await _userRepository.ReviewExists(dbReivew);
            if (!exist)
            {
                var addedReview = await _userRepository.AddReview(dbReivew);
            }
            else
            {
                throw new Exception("Review already created.");
            }
            
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {
            var dbReview = new Review
            {
                UserId = userId,
                MovieId = movieId
            };
            var exist = await _userRepository.ReviewExists(dbReview);
            if (exist)
            {
                await _userRepository.RemoveReview(dbReview);
            }
            else
            {
                throw new Exception("Review does not exist.");
            }
        }
        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            var dbReview = new Review
            {
                MovieId = reviewRequest.MovieId,
                UserId = reviewRequest.UserId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText
            };
            var exist = await _userRepository.ReviewExists(dbReview);
            if (exist)
            {
                await _userRepository.UpdateReview(dbReview);
            }
            else
            {
                throw new Exception("Review does not exist");
            }
        }

        public async Task<List<MovieCardModel>> GetAllFavoritesForUser(int userId)
        {
            var favorites = await _userRepository.GetFavoritesById(userId);
            List<MovieCardModel> movieCards = new List<MovieCardModel>();
            foreach (var favorite in favorites)
            {
                var newCard = new MovieCardModel
                {
                    Id = favorite.MovieId,
                    PosterUrl = favorite.Movie.PosterUrl,
                    Title = favorite.Movie.Title
                };
                movieCards.Add(newCard);
            }
            return movieCards;
        }

        public async Task<List<MovieCardModel>> GetAllPurchasesForUser(int id)
        {
            var purchases = await _userRepository.GetPurchasesById(id);
            List<MovieCardModel> movieCards = new List<MovieCardModel>();
            foreach (var purchase in purchases)
            {
                var newCard = new MovieCardModel
                {
                    Id = purchase.MovieId,
                    PosterUrl = purchase.Movie.PosterUrl,
                    Title = purchase.Movie.Title
                };
                movieCards.Add(newCard);
            }
            return movieCards;
        }

        public async Task<List<ReviewRequestModel>> GetAllReviewsByUser(int id)
        {
            var reviews = await _userRepository.GetReviewsById(id);
            List<ReviewRequestModel> reviewRequestModels = new List<ReviewRequestModel>();
            foreach (var review in reviews)
            {
                reviewRequestModels.Add(new ReviewRequestModel
                {
                    MovieId = review.MovieId,
                    UserId = review.UserId,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText
                });
            }
            return reviewRequestModels;
        }

        public async Task<PurchaseRequestModel> GetPurchasesDetails(int userId, int movieId)
        {
            //var movieDetails = await _userRepository.
            throw new NotImplementedException();
        }

        public async Task IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
