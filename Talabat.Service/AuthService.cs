using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Talabat.Core.Entities.IdentityEntities;
using Talabat.Core.ServicesInterfaces;

namespace Talabat.Service
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {            
            var userPrivateClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.DisplayName),
                new Claim(ClaimTypes.Email,user.Email)
            };
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                userPrivateClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecurityKey"]??string.Empty));
            var token = new JwtSecurityToken(
                audience: _config["JWT:ValidAud"] ,
                issuer: _config["JWT:ValidIss"] ,
                expires: DateTime.Now.AddDays(double.Parse(_config["JWT:DurationInDayes"])),
                claims: userPrivateClaims,
                signingCredentials: new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
