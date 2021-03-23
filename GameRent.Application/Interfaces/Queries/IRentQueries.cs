using GameRent.Domain.Shared;
using System;
using System.Threading.Tasks;

namespace GameRent.Application.Interfaces.Queries
{
    public interface IRentQueries
    {
        Task<BaseResponse> GetById(Guid id);
        Task<BaseResponse> GetAllFinished();
        Task<BaseResponse> GetAll();
        Task<BaseResponse> GetByClientId(Guid id);
    }
}