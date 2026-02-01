using Chat.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Command.Auth.Refresh
{
    public record RefreshTokenCommand (string refreshToken): IRequest<AuthResponse>;
}
