using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Data;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity.DataSeed
{
    public static class AppIdentityDbContextSeed
    {
        public async static Task SeedUserAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    DisplayName = "Ahmed Abdrabo",
                    Email = "am3991156@gmail.com",
                    UserName = "admin",
                    PhoneNumber = "123456789"
                };
                await _userManager.CreateAsync(user,"Admin123@");
            }
        }
    }
}
