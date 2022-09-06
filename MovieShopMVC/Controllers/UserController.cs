using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Buy(int movieId, int userId)
        {
            var purchaseRequest = new PurchaseRequestModel(movieId, userId);
            var isPurchased = await _userService.IsMoviePurchased(purchaseRequest,userId);
            PurchaseDetailsModel purchaseDetailsModel = new PurchaseDetailsModel();
            if (!isPurchased)
            {
                purchaseDetailsModel = await _userService.PurchaseMovie(purchaseRequest, userId);
                return RedirectToAction(actionName: "Details", controllerName: "Movie", routeValues:movieId);
            }
            else
            {
                return View();
            }
            
        }
        public async Task<IActionResult> Review(int movieId, int userId, decimal rating, string reviewText)
        {
            var reviewRequest = new ReviewRequestModel(movieId, userId, rating, reviewText);
            try
            {
                await _userService.AddMovieReview(reviewRequest);
            }catch(Exception e)
            {
                return View(); //already reviewed
            }
            
            return RedirectToAction(actionName: "Details", controllerName: "Movie", routeValues: movieId);
        }
        public async Task<IActionResult> Purchases(int userId)
        {
            var purchases =  await _userService.GetAllPurchasesForUser(userId);
            return View(); //return all purchased
        }
        public async Task<IActionResult> Favorites(int movieId, int userId)
        {
            var favorites = await _userService.GetAllFavoritesForUser(userId);
            return View(); //return all favorited
            
        }
    }
}
