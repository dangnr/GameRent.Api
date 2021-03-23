using GameRent.Domain.Enums;
using GameRent.Domain.Shared;
using System;
using System.Threading.Tasks;

namespace GameRent.Application.Interfaces.Queries
{
    public interface IClientQueries
    {
        Task<BaseResponse> GetById(Guid id);
        Task<BaseResponse> GetByUsername(string username);
        Task<BaseResponse> GetByRole(UserRoleType role);
        Task<BaseResponse> GetAll();
    }
}