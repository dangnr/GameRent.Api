using GameRent.Domain.Aggregates;
using GameRent.Domain.Interfaces.Repositories;
using GameRent.Infra.Data.Context;
using System;
using System.Threading.Tasks;

namespace GameRent.Infra.Repositories
{
    public class GameRentCommandRepository : IGameRentedCommandRepository
    {
        private readonly GameRentContext _context;

        public GameRentCommandRepository(GameRentContext context)
        {
            _context = context;
        }

        public async Task Create(Domain.Aggregates.GameRent gameRented)
        {
            _context.GameRent.Add(gameRented);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var dbClient = await _context.GameRent.FindAsync(id);

            _context.Remove(dbClient);

            await _context.SaveChangesAsync();
        }

        public async Task<Domain.Aggregates.GameRent> GetById(Guid id)
        {
            return await _context.GameRent.FindAsync(id);
        }

        public async Task Update(Domain.Aggregates.GameRent gameRented)
        {
            var dbClient = await _context.GameRent.FindAsync(gameRented.Id);

            dbClient.UpdateGameRented(gameRented);

            await _context.SaveChangesAsync();
        }
    }
}