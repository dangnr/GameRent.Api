using GameRent.Domain.Interfaces.Repositories;
using GameRent.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GameRent.Api.Configurations
{
    public static class RepositoryConfigurations
    {
        public static IServiceCollection AddRepositoryConfigurations(this IServiceCollection services)
        {
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IRentRepository, RentRepository>();

            return services;
        }
    }
}