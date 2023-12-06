namespace HappyPaws.API.Capabilities
{
    public static class StartupMvc
    {
        public static IMvcCoreBuilder ConfigureMvc(this IServiceCollection services)
        {
            return services
            .AddMvcCore();
        }
    }
}
