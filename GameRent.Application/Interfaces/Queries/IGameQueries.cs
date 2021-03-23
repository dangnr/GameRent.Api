using GameRent.Domain.Shared;
using System;
using System.Threading.Tasks;

namespace GameRent.Application.Interfaces.Queries
{
    public interface IGameQueries
    {
        Task<BaseResponse> GetById(Guid id);
        Task<BaseResponse> GetAll();
        Task<BaseResponse> GetAllAvailable();
        Task<BaseResponse> GetAllRented();
    }
}