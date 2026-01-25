using Chat.Application.Common.Interface;
using Chat.Application.DTOs;
using Chat.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Auth
{
    public class AuthService(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager) : IAuthService
    {
        public async Task<IList<string>> GetUserRolesAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return user == null
                ? new List<string>()
                : await _userManager.GetRolesAsync(user);
        }

        public async Task<AuthUser?> ValidateUserAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if(!result.Succeeded) return null;

            return new AuthUser
            {
                Id = user.Id,
                Email = email!
            };
        }
    }
}
