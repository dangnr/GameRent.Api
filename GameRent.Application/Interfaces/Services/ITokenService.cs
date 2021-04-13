using GameRent.Application.Shared;
using GameRent.Domain.Entities;
using System.Threading.Tasks;

namespace GameRent.Application.Interfaces.Services
{
    public interface ITokenService
    {
        Task<BaseResponse> GenerateToken(Client client);
    }
}