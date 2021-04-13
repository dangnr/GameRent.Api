using GameRent.Domain.Aggregates;
using System;
using System.Threading.Tasks;

namespace GameRent.Domain.Interfaces.Repositories
{
    public interface IGameRentedCommandRepository
    {
        Task Create(Aggregates.GameRent gameRented);
        Task Update(Aggregates.GameRent gameRented);
        Task Delete(Guid id);
        Task<Aggregates.GameRent> GetById(Guid id);
    }
}