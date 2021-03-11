using GameRent.Domain.Entities;
using GameRent.Domain.Shared;
using System.Threading.Tasks;

namespace GameRent.Application.Interfaces.Services
{
    public interface ITokenService
    {
        Task<BaseResponse> GenerateToken(Client client);
    }
}