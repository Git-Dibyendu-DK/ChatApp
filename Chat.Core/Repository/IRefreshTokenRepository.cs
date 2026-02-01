using Chat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Core.Repository
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
        Task<RefreshToken?> GetByTokenAsync(string refreshToken);
        Task RevokeAsync(RefreshToken refreshToken);
    }
}
