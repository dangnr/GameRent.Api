using GameRent.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameRent.Api
{
    public class MigrationWorker : IHostedService
    {
        readonly IServiceProvider provider;

        public MigrationWorker(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = provider.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<GameRentContext>();

            await context.Database.MigrateAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}