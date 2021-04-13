using GameRent.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace GameRent.Domain.Interfaces.Repositories
{
    public interface IClientCommandRepository
    {
        Task Create(Client client);
        Task Update(Client client);
        Task Delete(Guid id);
        Task<Client> GetById(Guid id);
        Task<Client> GetByUsernameAndPassword(string username, string password);
        Task<bool> IsUniqueCpf(string cpf);
        Task<bool> IsUniqueCpf(Guid id, string cpf);
        Task<bool> IsUniqueUsername(string username);
        Task<bool> IsUniqueUsername(Guid id, string username);
    }
}