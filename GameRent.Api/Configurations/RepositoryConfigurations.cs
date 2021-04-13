using GameRent.Domain.Interfaces.Repositories;
using GameRent.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GameRent.Api.Configurations
{
    public static class RepositoryConfigurations
    {
        public static IServiceCollection AddRepositoryConfigurations(this IServiceCollection services)
        {
            services.AddScoped<IGameCommandRepository, GameCommandRepository>();
            services.AddScoped<IClientCommandRepository, ClientCommandRepository>();
            services.AddScoped<IRentCommandRepository, RentCommandRepository>();

            return services;
        }
    }
}