using HappyPaws.Core.Interfaces;
using HappyPaws.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HappyPaws.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("ConnectionStrings:Postgres");

            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));

            services.AddScoped<IAppointmentProcedureRepository, AppointmentProcedureRepository>()
                .AddScoped<IAppointmentRepository, AppointmentRepository>()
                .AddScoped<INoteRepository, NoteRepository>()
                .AddScoped<IPetRepository, PetRepository>()
                .AddScoped<IProcedureRepository, ProcedureRepository>()
                .AddScoped<ITimeSlotRepository, TimeSlotRepository>()
                .AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
