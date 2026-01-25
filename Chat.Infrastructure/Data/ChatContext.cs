using Chat.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Data
{
    public class ChatContext(DbContextOptions<ChatContext> options): IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
    {
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>(); 
    }
}
