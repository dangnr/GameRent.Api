using GameRent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameRent.Domain.Interfaces.Repositories
{
    public interface IGameRepository
    {
        Task Create(Game game);
        Task Update(Game game);
        Task Delete(Guid id);
        Task<Game> GetById(Guid id);
        Task<bool> IsUniqueGameName(string name);
        Task<bool> IsUniqueGameName(Guid id, string name);
    }
}