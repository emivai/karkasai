using HappyPaws.EmailService.Interfaces;
using HappyPaws.EmailService.Models;
using HappyPaws.EmailService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HappyPaws.EmailService
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();

            services.AddSingleton(emailConfig);

            services.AddScoped<IEmailSender, EmailSender>();

            return services;
        }
    }
}
