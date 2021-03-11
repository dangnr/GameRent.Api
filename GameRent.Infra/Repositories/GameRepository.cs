using GameRent.Domain.Entities;
using GameRent.Domain.Interfaces.Repositories;
using GameRent.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GameRent.Infra.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly GameRentContext _context;

        public GameRepository(GameRentContext context)
        {
            _context = context;
        }

        public async Task Create(Game game)
        {
            _context.Game.Add(game);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Game game)
        {
            var dbGame = await _context.Game.FindAsync(game.Id);

            dbGame.UpdateGame(game);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var dbGame = await _context.Game.FindAsync(id);

            _context.Remove(dbGame);

            await _context.SaveChangesAsync();
        }

        public async Task<Game> GetById(Guid id)
        {
            return await _context.Game.FindAsync(id);
        }

        public async Task<bool> IsUniqueGameName(string name)
        {
            return !(await _context.Game.AnyAsync(x => x.Name.Equals(name)));
        }

        public async Task<bool> IsUniqueGameName(Guid id, string name)
        {
            return !(await _context.Game.Where(x => x.Id != id).AnyAsync(x => x.Name.Equals(name)));
        }
    }
}