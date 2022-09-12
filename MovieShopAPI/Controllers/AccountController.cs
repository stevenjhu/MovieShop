using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IUserRepository userRepository, IConfiguration configuration)
        {
            _accountService = accountService;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterModel model)
        {
            var user = await _accountService.RegisterUser(model);
            return Ok(user);
        }

        [HttpGet]
        [Route("check-email")]
        public async Task<IActionResult> EmailExists ([FromBody] string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
                return NotFound(new { errorMessage = "The email is not associated with any existing account." });
            else
                return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var login = await _accountService.ValidateUser(model);
            //iOS (), Android, Angular, React, Java
            //token JWT Jason Web Token
            //Client will send email/password to API
            //API will validate the email/password and if valid, then API will create the JWT token and send to client
            //It's client's responsibility to store the token somewhere
            //Angular, React (local storage or session storage in browser)
            //iOS/Android, device
            //when client needs some secure information or needs to perform any operations that requires users to be authenticated
            //then client needs to send the token to the API in th HTTP header
            //Once the API receives the token from client it will validate the JWT token and if valid, it will send the data back to the client
            //IF JWT token is invalid or token is expired, then API will send 401 Unauthorized
            if (login == null)
            {
                throw new UnauthorizedAccessException("Please check email and password.");
                return Unauthorized(new { errorMessage = "The credential does not match any record"});
            }
            //create token
            var jwtToken = CreateJWTToken(login);
            return Ok(new { token = jwtToken});
        }

        private string CreateJWTToken(UserLoginSuccessModel user)
        {
            //create the claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim("Country","USA"),
                new Claim("language","english")
            };

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            //specify a secret key
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secretKey"]));
            //specify the algorithm
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            //specify the expiration of the token
            var tokenExpiration = DateTime.UtcNow.AddHours(2);
            //create an object with all the above information to create the token
            var tokenDetails = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = tokenExpiration,
                SigningCredentials = credentials,
                Issuer = "MovieShop, Inc",
                Audience = "MovieShop Clients"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedJwt = tokenHandler.CreateToken(tokenDetails);
            return tokenHandler.WriteToken(encodedJwt);
        }
    }
}
