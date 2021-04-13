using GameRent.Domain.Aggregates;
using GameRent.Domain.Entities;
using GameRent.Domain.Interfaces.Repositories;
using GameRent.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRent.Infra.Repositories
{
    public class RentCommandRepository : IRentCommandRepository
    {
        private readonly GameRentContext _context;

        public RentCommandRepository(GameRentContext context)
        {
            _context = context;
        }

        public async Task Create(Rent rent)
        {
            await _context.GameRent.AddRangeAsync(rent.GamesRent);

            _context.Client.Attach(rent.Client);

            await _context.Rent.AddAsync(rent);
            await _context.SaveChangesAsync();
        }

        public async Task Finish(Guid id)
        {
            var dbRent = _context.Rent
                .Include(x => x.GamesRent).ThenInclude(x => x.Game)
                .Include(x => x.Client)
                .FirstOrDefault(x => x.Id == id);

            dbRent.FinishRent();

            await _context.SaveChangesAsync();
        }

        public async Task<Rent> GetById(Guid id)
        {
            return await _context.Rent
                .Include(x => x.GamesRent).ThenInclude(x => x.Game)
                .Include(x => x.Client)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<List<Domain.Entities.Game>> GetGamesFromIds(List<Guid> gamesIds)
        {
            return await _context.Game.Where(x => gamesIds.Contains(x.Id)).ToListAsync();
        }

        public async Task<Client> GetClientById(Guid id)
        {
            return await _context.Client.FindAsync(id);
        }
    }
}