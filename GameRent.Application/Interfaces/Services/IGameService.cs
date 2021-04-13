using System;
using System.Threading.Tasks;

namespace GameRent.Application.Interfaces.Services
{
    public interface IGameService
    {
        Task<bool> CheckIfGameNameIsUnique(string name, Guid? id = null);
        Task<bool> CheckIfGameExists(Guid id);
    }
}