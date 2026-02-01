using Chat.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Data
{
    public static class IdentitySeed
    {
        public static async Task SeedAsync(IServiceProvider services, IConfiguration config)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = { config["DefaultRoles:AdminRole"]!, config["DefaultRoles:UserRole"]! };

            foreach(var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }

            await CreateUserAsync(userManager, config["DefaultUsers:AdminUser:Email"]!,
                config["DefaultUsers:AdminUser:Password"]!, config["DefaultRoles:AdminRole"]!);

            await CreateUserAsync(userManager, config["DefaultUsers:NormalUser:Email"]!,
                config["DefaultUsers:NormalUser:Password"]!, config["DefaultRoles:UserRole"]!);
        }

        public static async Task CreateUserAsync(UserManager<ApplicationUser> userManager, string email, string password, string role)
        {
            if (await userManager.FindByEmailAsync(email) != null) return;

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = email,
                UserName = email,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await userManager.CreateAsync(user, password);
            if(result.Succeeded)
                await userManager.AddToRoleAsync(user, role);
        }
    }
}
