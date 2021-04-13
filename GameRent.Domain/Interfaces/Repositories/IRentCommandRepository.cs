using GameRent.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameRent.Domain.Interfaces.Repositories
{
    public interface IRentCommandRepository
    {
        Task Create(Rent rent);
        Task Finish(Guid id);
        Task<Rent> GetById(Guid id);
        Task<List<GameRent.Domain.Entities.Game>> GetGamesFromIds(List<Guid> gamesIds);
        Task<GameRent.Domain.Entities.Client> GetClientById(Guid id);
    }
}