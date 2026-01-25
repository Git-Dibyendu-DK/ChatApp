using Chat.Application.Common.Interface;
using Chat.Core.Entities;
using Chat.Infrastructure.Auth;
using Chat.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraStructureDI(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<ChatContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            service
                    .AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                    .AddEntityFrameworkStores<ChatContext>()
                    .AddDefaultTokenProviders();

            service.AddScoped<ITokenService, TokenService>();
            service.AddScoped<IAuthService, AuthService>();

            return service;
        }
    }
}
