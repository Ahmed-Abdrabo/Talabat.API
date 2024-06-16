using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;

namespace Talabat.Service
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> GenerateTokenAsync(AppUser User,UserManager<AppUser> userManager)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName,User.UserName),
                new Claim(ClaimTypes.Email,User.Email)
            };
            var userRoles=await userManager.GetRolesAsync(User);
            foreach (var role in userRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtToken:SecretKey"]));

            var token = new JwtSecurityToken(
                    audience:_configuration["JwtToken:Audience"],
                    issuer:_configuration["JwtToken:Issuer"],
                    expires:DateTime.UtcNow.AddDays(double.Parse(_configuration["JwtToken:TokenExpiry"])),
                    claims:authClaims,
                    signingCredentials:new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)
                    
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
