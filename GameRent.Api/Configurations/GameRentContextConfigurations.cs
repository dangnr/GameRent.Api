using GameRent.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameRent.Api.Configurations
{
    public static class GameRentContextConfigurations
    {
        public static IServiceCollection AddCustomDataContexConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped((x) =>
            {
                return new DbContextOptionsBuilder<GameRentContext>()
                .UseSqlServer(connectionString)
                .Options;
            });

            services.AddDbContext<GameRentContext>();

            return services;
        }
    }
}