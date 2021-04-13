using System;
using System.Threading.Tasks;

namespace GameRent.Application.Interfaces.Services
{
    public interface IClientService
    {
        Task<bool> CheckIfClientCpfIsUnique(string cpf, Guid? id = null);
        Task<bool> CheckIfClientExists(Guid id);
    }
}