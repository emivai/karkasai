using HappyPaws.Infrastructure;
using HappyPaws.Application;
using HappyPaws.EmailService;
using HappyPaws.API.Auth.Handlers;
using Microsoft.AspNetCore.Authorization;

namespace HappyPaws.API.Capabilities
{
    public static class StartupInjection
    {
        public static IServiceCollection ConfigureInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .ConfigureInfrastructureServices(configuration)
                .ConfigureApplicationServices()
                .ConfigureEmailService(configuration)
                .AddSingleton<IAuthorizationHandler, OwnerAuthorizationHandler>()
                .AddSingleton<IAuthorizationHandler, SameUserAuthorizationHandler>();

            return services;
        }
    }
}
