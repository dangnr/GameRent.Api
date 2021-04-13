using GameRent.Application.Interfaces.Queries;
using GameRent.Infra.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace GameRent.Api.Configurations
{
    public static class QueryConfigurations
    {
        public static IServiceCollection AddQueryConfigurations(this IServiceCollection services)
        {
            services.AddScoped<IGameQueryRepository, GameQueryRepository>();
            services.AddScoped<IClientQueryRepository, ClientQueryRepository>();
            services.AddScoped<IRentQueryRepository, RentQueryRepository>();

            return services;
        }
    }
}