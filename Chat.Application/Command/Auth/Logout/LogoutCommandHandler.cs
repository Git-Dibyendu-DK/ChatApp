using Chat.Application.Command.Auth.Login;
using Chat.Core.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Command.Auth.Logout
{
    public class LogoutCommandHandler(IRefreshTokenRepository refreshTokenRepository) : IRequestHandler<LogoutCommand>
    {
        public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await refreshTokenRepository.GetByTokenAsync(request.token);
            if (refreshToken == null || refreshToken.IsRevoked)
                throw new UnauthorizedAccessException("Invalid Refreash Token");
            await refreshTokenRepository.RevokeAsync(refreshToken);
        }
    }
}
