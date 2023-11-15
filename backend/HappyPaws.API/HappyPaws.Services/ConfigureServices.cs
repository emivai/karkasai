using HappyPaws.Application.Interfaces;
using HappyPaws.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HappyPaws.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentProcedureService, AppointmentProcedureService>()
                    .AddScoped<IAppointmentService, AppointmentService>()
                    .AddScoped<INoteService, NoteService>()
                    .AddScoped<IPetService, PetService>()
                    .AddScoped<IProcedureService, ProcedureService>()
                    .AddScoped<ITimeSlotService, TimeSlotService>()
                    .AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
