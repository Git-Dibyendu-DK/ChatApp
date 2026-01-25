using Chat.Application;
using Chat.Infrastructure;

namespace Chat.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddApplicationDI()
                   .AddInfraStructureDI(configuration);

            return service;
        }
    }
}
