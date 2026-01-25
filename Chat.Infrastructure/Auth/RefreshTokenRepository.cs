using Chat.Core.Entities;
using Chat.Core.Repository;
using Chat.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Auth
{
    public class RefreshTokenRepository(ChatContext _context) : IRefreshTokenRepository
    {
        public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
            await _context.SaveChangesAsync();
        }
    }
}
