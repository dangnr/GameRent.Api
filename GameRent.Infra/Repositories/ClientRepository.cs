using GameRent.Domain.Entities;
using GameRent.Domain.Interfaces.Repositories;
using GameRent.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GameRent.Infra.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly GameRentContext _context;

        public ClientRepository(GameRentContext context)
        {
            _context = context;
        }

        public async Task Create(Client client)
        {
            _context.Client.Add(client);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Client client)
        {
            var dbClient = await _context.Client.FindAsync(client.Id);

            dbClient.UpdateClient(client);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var dbClient = await _context.Client.FindAsync(id);

            _context.Remove(dbClient);

            await _context.SaveChangesAsync();
        }

        public async Task<Client> GetById(Guid id)
        {
            return await _context.Client.FindAsync(id);
        }

        public async Task<Client> GetByUsernameAndPassword(string username, string password)
        {
            return await _context.Client.Where(x => x.Username.Equals(username) && x.Password.Equals(password))
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsUniqueCpf(string cpf)
        {
            return !(await _context.Client.AnyAsync(x => x.Cpf.Equals(cpf)));
        }

        public async Task<bool> IsUniqueCpf(Guid id, string cpf)
        {
            return !(await _context.Client.Where(x => x.Id != id).AnyAsync(x => x.Cpf.Equals(cpf)));
        }

        public async Task<bool> IsUniqueUsername(string username)
        {
            return !(await _context.Client.AnyAsync(x => x.Username.Equals(username)));
        }

        public async Task<bool> IsUniqueUsername(Guid id, string username)
        {
            return !(await _context.Client.Where(x => x.Id != id).AnyAsync(x => x.Username.Equals(username)));
        }
    }
}