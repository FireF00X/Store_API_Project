using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.IdentityEntities;

namespace Talabat.Repository.Data.IdentityData
{
    public class StoreIdentityContextSeed
    {
        public async static Task SeedIdentityAsync(UserManager<AppUser> userManger,AppIdentityDbContext dbContext)
        {
            if(userManger.Users.Count() == 0)
            {
                var userSeed = new AppUser()
                {
                    DisplayName = "AboElsayed",
                    Email = "j.ahmed.elsaied@gmail.com",
                    UserName = "j.ahmed.elsaied",
                    PhoneNumber = "01150243983"
                };
                await userManger.CreateAsync(userSeed, "P@$$w0rd");
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
