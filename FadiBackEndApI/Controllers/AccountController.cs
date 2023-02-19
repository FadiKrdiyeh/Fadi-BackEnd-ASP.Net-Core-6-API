using FadiBackEndApI.DTOs;
using FadiBackEndApI.Utilities;
using FadiBackEndApI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly SignInManager<IdentityUser> _signInManager;

        //public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        //{
        //    this._userManager = userManager;
        //    this._signInManager = signInManager;
        //}

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel user)
        {
            ResponseApi<string> response = new ResponseApi<string>();

            try
            {
                if (user == null)
                {
                    response = new ResponseApi<string>() { Status = false, Message = "Invalid client request" };
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }

                if (user.Username == "FadiKrdiyeh" && user.Password == "fadi@123")
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("F@diKrdiyeh@963WebDeveloperForThisAngular15ApplicationForFrontEndAndASP.NetCoreWebAPIForBackEnd"));
                    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokenOptions = new JwtSecurityToken(
                        issuer: "https://localhost:4200",
                        audience: "https://localhost:4200",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddHours(3),
                        //expires: DateTime.Now.AddSeconds(5),
                        signingCredentials: signingCredentials
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                    response = new ResponseApi<string>() { Status = true, Message = "Ok", Value = tokenString };
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                response = new ResponseApi<string>() { Status = false, Message = "Not authorized" };
                return StatusCode(StatusCodes.Status401Unauthorized, response);
            }
            catch (Exception ex)
            {
                response = new ResponseApi<string>() { Status = false, Message = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
