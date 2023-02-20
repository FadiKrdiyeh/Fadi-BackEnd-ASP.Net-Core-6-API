using FadiBackEndApI.DTOs;
using FadiBackEndApI.Utilities;
using FadiBackEndApI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;

namespace FadiBackEndApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel user)
        {
            ResponseApi<LoginViewModel> response = new ResponseApi<LoginViewModel>();

            try
            {
                if (user == null)
                {
                    response = new ResponseApi<LoginViewModel>() { Status = false, Message = "Invalid client request" };
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }

                var loginUser = await _userManager.FindByNameAsync(user.Username);
                //Console.WriteLine("Login User: " + loginUser.Id);
                //Console.WriteLine("----------------------------------");
                if (user != null)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("F@diKrdiyeh@963WebDeveloperForThisAngular15ApplicationForFrontEndAndASP.NetCoreWebAPIForBackEnd"));
                    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokenOptions = new JwtSecurityToken(
                        issuer: "https://localhost:4200",
                        audience: "https://localhost:4200",
                        //claims: new List<Claim>(),
                        claims: new List<Claim>()
                        {
                            new Claim("UserId", loginUser.Id)
                        },
                        expires: DateTime.Now.AddHours(3),
                        signingCredentials: signingCredentials
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                    LoginViewModel loggedinUser = new LoginViewModel() 
                    {
                        Username = loginUser.UserName,
                        FullName = loginUser.NormalizedUserName,
                        Token = tokenString,
                    };

                    response = new ResponseApi<LoginViewModel>() { Status = true, Message = "Ok", Value = loggedinUser };
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                response = new ResponseApi<LoginViewModel>() { Status = false, Message = "Not authorized" };
                return StatusCode(StatusCodes.Status401Unauthorized, response);
            }
            catch (Exception ex)
            {
                response = new ResponseApi<LoginViewModel>() { Status = false, Message = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost, Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            ResponseApi<RegisterViewModel> response = new ResponseApi<RegisterViewModel>();

            try
            {
                if (model.Username == null || model.Password == null || model.FullName == null) 
                {
                    response = new ResponseApi<RegisterViewModel>() { Status = false, Message = "Required fields" };
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }

                var user = new IdentityUser { UserName = model.Username, NormalizedUserName = model.FullName };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("F@diKrdiyeh@963WebDeveloperForThisAngular15ApplicationForFrontEndAndASP.NetCoreWebAPIForBackEnd"));
                    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokenOptions = new JwtSecurityToken(
                        issuer: "https://localhost:4200",
                        audience: "https://localhost:4200",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddHours(3),
                        signingCredentials: signingCredentials
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    //user.NormalizedUserName = tokenString;

                    RegisterViewModel regesterdUser = new RegisterViewModel()
                    {
                        Username = user.UserName,
                        FullName = user.NormalizedUserName,
                        Token = tokenString
                    };

                    response = new ResponseApi<RegisterViewModel>() { Status = true, Message = "Ok", Value = regesterdUser };
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                response = new ResponseApi<RegisterViewModel>() { Status = false, Message = result.Errors.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            catch (Exception ex)
            {
                response = new ResponseApi<RegisterViewModel>() { Status = false, Message = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet, Route("profile")]
        [Authorize]
        public async Task<IActionResult> GetUserData ()
        {
            ResponseApi<UserViewModel> response = new ResponseApi<UserViewModel>();

            try
            {
                string userId = User.Claims.First(claim => claim.Type == "UserId").Value;
                //Console.WriteLine("User Id: " + userId);
                //Console.WriteLine("----------------------------------");
                var user = await _userManager.FindByIdAsync(userId);
                //Console.WriteLine("Profile User: " + user);
                //Console.WriteLine("----------------------------------");

                if (user == null)
                {
                    response = new ResponseApi<UserViewModel>() { Status = false, Message = "User not found" };
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }
                
                UserViewModel returnedUser = new UserViewModel()
                {
                    Id = user.Id,
                    Username = user.UserName,
                    FullName = user.NormalizedEmail
                };

                response = new ResponseApi<UserViewModel>() { Status = true, Message = "Ok", Value = returnedUser };
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseApi<UserViewModel>() { Status = false, Message = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet, Route("users")]
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            ResponseApi<List<LoginViewModel>> response = new ResponseApi<List<LoginViewModel>>();

            try
            {
                List<LoginViewModel> users = await _userManager.Users.Select(user => new LoginViewModel { Id = user.Id, Username = user.UserName }).ToListAsync();
                if (users.Count == 0)
                {
                    response = new ResponseApi<List<LoginViewModel>>() { Status = false, Message = "No users" };
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }

                response = new ResponseApi<List<LoginViewModel>>() { Status = true, Message = "Ok", Value = users };
                return StatusCode(StatusCodes.Status200OK, response);
            } 
            catch (Exception ex)
            {
                response = new ResponseApi<List<LoginViewModel>>() { Status = false, Message = ex.Message};
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet, Route("check-username")]
        public async Task<IActionResult> CheckUsername(string username)
        {
            ResponseApi<bool> response = new ResponseApi<bool>();

            try
            {
                var user = await _userManager.FindByNameAsync(username);

                if (user == null)
                {
                    response = new ResponseApi<bool>() { Status = false, Message = "User not found", Value = false };
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                response = new ResponseApi<bool>() { Status = true, Message = "Ok", Value = true};
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseApi<bool>() { Status = false, Message = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
