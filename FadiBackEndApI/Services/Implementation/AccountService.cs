﻿using FadiBackEndApI.Services.Contract;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FadiBackEndApI.Services.Implementation
{
    public class AccountService : IAccountService
    {
        public async Task<string> Login(string username, string password)
        {
            try
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

                return tokenString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
