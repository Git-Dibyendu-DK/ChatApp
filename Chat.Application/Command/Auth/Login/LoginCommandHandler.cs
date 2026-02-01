using Chat.Application.Common.Interface;
using Chat.Application.DTOs;
using Chat.Core.Entities;
using Chat.Core.Repository;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Command.Auth.Login
{
    public class LoginCommandHandler(IAuthService authService, ITokenService tokenService, IRefreshTokenRepository _refreshTokenRepository ,IConfiguration config) : IRequestHandler<LoginCommand, AuthResponse>
    {
        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await authService.ValidateUserAsync(request.email, request.password);

            if(user == null) throw new UnauthorizedAccessException();

            var roles = await authService.GetUserRolesAsync(user.Id);

            var accessToken = tokenService.createAccessToken(user.Id, user.Email, roles);
            var refreshTokenValue = tokenService.CreateRefreshToken();

            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshTokenValue,
                Expires = DateTime.UtcNow.AddDays(int.Parse(config["Jwt:RefreshTokenExpiryDays"]!)),
                IsRevoked = false
            };
            await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue
            };
        }
    }
}
