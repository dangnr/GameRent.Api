using GameRent.Domain.Aggregates;
using GameRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameRent.Infra.Data.Context
{
    public class GameRentContext : DbContext
    {
        public DbSet<Game> Game { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Rent> Rent { get; set; }
        public DbSet<Domain.Aggregates.GameRent> GameRent { get; set; }

        public GameRentContext(DbContextOptions options) : base(options)
        { }
    }
}