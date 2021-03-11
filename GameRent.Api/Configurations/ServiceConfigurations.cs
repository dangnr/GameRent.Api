using GameRent.Application.Interfaces.Services;
using GameRent.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameRent.Api.Configurations
{
    public static class ServiceConfigurations
    {
        public static IServiceCollection AddServiceConfigurations(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}