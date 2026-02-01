using Chat.Core.Entities;
using Chat.Core.Repository;
using Chat.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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

        public async Task<RefreshToken?> GetByTokenAsync(string refreshToken)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);
        }

        public async Task RevokeAsync(RefreshToken refreshToken)
        {
            refreshToken.IsRevoked = true;
            await _context.SaveChangesAsync();
        }
    }
}
