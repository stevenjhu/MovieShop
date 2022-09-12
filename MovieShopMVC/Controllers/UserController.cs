using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Infra;

namespace MovieShopMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUser _currentUser;

        public UserController(IUserService userService, IUserRepository userRepository, ICurrentUser currentUser)
        {
            _userService = userService;
            _userRepository = userRepository;
            _currentUser = currentUser;
        }

        [HttpPost]
        public async Task<IActionResult> Buy(int movieId)
        {
            var userId = _currentUser.UserId;
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

        [HttpPost]
        public async Task<IActionResult> Review(int movieId, decimal rating, string reviewText)
        {
            var userId = _currentUser.UserId;
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

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            //get all the movies purchased by user, user id
            //httpcontext.user.claims and then call the db and get the info to the view
            var userId = _currentUser.UserId;
            var purchases =  await _userService.GetAllPurchasesForUser(userId);
            return View("_MovieCard",purchases); //return all purchased
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var userId = _currentUser.UserId;
            var favorites = await _userService.GetAllFavoritesForUser(userId);
            return View("_MovieCard", favorites); //return all favorited
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var userId = _currentUser.UserId;
            var user = await _userRepository.GetUserById(userId);
            UserEditModel model = new UserEditModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth == null ? null : user.DateOfBirth.Value
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserEditModel model)
        {
            //var userId = _currentUser.UserId;
            return View(model);
        }
    }
}
