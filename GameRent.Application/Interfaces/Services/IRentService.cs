using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameRent.Application.Interfaces.Services
{
    public interface IRentService
    {
        Task<bool> CheckIfRentExists(Guid id);
        Task<bool> CheckIfClientExists(Guid id);
        bool CheckIfGamesAreValid(List<Domain.Entities.Game> games);
    }
}