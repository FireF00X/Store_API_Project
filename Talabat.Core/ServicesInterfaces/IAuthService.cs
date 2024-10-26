using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.IdentityEntities;

namespace Talabat.Core.ServicesInterfaces
{
    public interface IAuthService
    {
        Task<string> CreateTokenAsync(AppUser user,UserManager<AppUser> userManager);
    }
}
