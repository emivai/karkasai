using HappyPaws.Application.Interfaces;
using HappyPaws.Application.Utilities;
using HappyPaws.Core.Interfaces;
using HappyPaws.Infrastructure.Jobs;
using HappyPaws.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace HappyPaws.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Postgres");

            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));

            services.AddScoped<IAppointmentProcedureRepository, AppointmentProcedureRepository>()
                .AddScoped<IAppointmentRepository, AppointmentRepository>()
                .AddScoped<INoteRepository, NoteRepository>()
                .AddScoped<IPetRepository, PetRepository>()
                .AddScoped<IProcedureRepository, ProcedureRepository>()
                .AddScoped<ITimeSlotRepository, TimeSlotRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddTransient<ITokenManager, TokenManager>()
                .AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                var jobKey = new JobKey("AppointmentStatusJob");
                q.AddJob<AppointmentStatusJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("AppointmentStatusJob-trigger")
                    .WithCronSchedule("0 */5 * * * ?"));
                    //.WithCronSchedule("0 0 0 * * ?"));

            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            return services;
        }
    }
}
