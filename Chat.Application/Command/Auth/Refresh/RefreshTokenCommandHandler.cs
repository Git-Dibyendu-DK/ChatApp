using Chat.Application.Common.Interface;
using Chat.Application.DTOs;
using Chat.Core.Entities;
using Chat.Core.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Command.Auth.Refresh
{
    public class RefreshTokenCommandHandler
        (ITokenService tokenService, 
        IRefreshTokenRepository refreshTokenRepository, 
        UserManager<ApplicationUser> userManager) : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await refreshTokenRepository.GetByTokenAsync(request.refreshToken);

            if (refreshToken == null || refreshToken.IsRevoked || refreshToken.Expires < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Invalid Refresh Token");

            var user = await userManager.FindByIdAsync(refreshToken.UserId.ToString());

            if (user == null)
                throw new UnauthorizedAccessException("User Not Found");

            var roles = await userManager.GetRolesAsync(user);

            var accessToken = tokenService.createAccessToken(user.Id, user.Email!, roles);
            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = request.refreshToken
            };
        }
    }
}
