using Chat.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Common.Interface
{
    public interface IAuthService
    {
        Task<AuthUser?> ValidateUserAsync(string email, string password);
        Task<IList<string>> GetUserRolesAsync(Guid userId);
    }
}
