using HappyPaws.API.Auth.Handlers;
using HappyPaws.API.Auth.Policies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HappyPaws.API.Capabilities
{
    public static class StartupAuth
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters.ValidateLifetime = true;
                o.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
                o.TokenValidationParameters.ValidAudience = configuration["JWT:ValidAudience"];
                o.TokenValidationParameters.ValidIssuer = configuration["JWT:ValidIssuer"];
                o.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            }).Services;
        }

        public static IMvcCoreBuilder ConfigureAuthorization(this IMvcCoreBuilder services)
        {
            return services.AddAuthorization(o =>
            {
                o.AddPolicy(PolicyNames.Owner, policy => policy.Requirements.Add(new OwnerRequirement()));
                o.AddPolicy(PolicyNames.SameUser, policy => policy.Requirements.Add(new SameUserRequirement()));
            });
        }
    }
}
