using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.IdentityEntities;

namespace Talabat.API.Helper
{
    public static class UserManagerExtention
    {
        public async static Task<AppUser> FindUseIncludesAddressAsync(this UserManager<AppUser> manager,ClaimsPrincipal User)
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var user = manager.Users.Where(u=>u.Email==email).Include(u=>u.Address).FirstOrDefault();
            return user;
        }
    }
}
