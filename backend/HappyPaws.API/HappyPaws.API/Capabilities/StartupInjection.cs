using HappyPaws.Infrastructure;
using HappyPaws.Application;

namespace HappyPaws.API.Capabilities
{
    public static class StartupInjection
    {
        public static IServiceCollection ConfigureInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .ConfigureInfrastructureServices(configuration)
                .ConfigureApplicationServices();

            return services;
        }
    }
}
