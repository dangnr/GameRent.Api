using Dapper;
using GameRent.Application.Interfaces.Queries;
using GameRent.Application.ViewModels;
using GameRent.Domain.Enums;
using GameRent.Infra.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRent.Infra.Queries
{
    public class ClientQueries : IClientQueries
    {
        private readonly SqlConnection _sqlConnection;

        public ClientQueries(GameRentContext context)
        {
            _sqlConnection = new SqlConnection(context.Database.GetConnectionString());
        }

        public async Task<List<ClientViewModel>> GetAll()
        {
            return (await _sqlConnection
                .QueryAsync<ClientViewModel>
                    ("Select Id, FirstName, LastName, Cpf, Email, IsActive From Client"))
                .AsList();
        }

        public async Task<ClientViewModel> GetById(Guid id)
        {
            var queryArgs = new DynamicParameters();

            queryArgs.Add("Id", id);

            return (await _sqlConnection
                .QueryAsync<ClientViewModel>
                    ("Select Id, FirstName, LastName, Cpf, Email, IsActive From Client Where Id = @id", queryArgs))
                .FirstOrDefault();
        }

        public async Task<ClientViewModel> GetByUsername(string username)
        {
            return (await _sqlConnection
                .QueryAsync<ClientViewModel>
                    ($"Select Id, FirstName, LastName, Cpf, Email, IsActive From Client Where Username = '{username}'"))
                .FirstOrDefault();
        }

        public async Task<ClientViewModel> GetByRole(UserRoleType role)
        {
            return (await _sqlConnection
                .QueryAsync<ClientViewModel>
                    ($"Select Id, FirstName, LastName, Cpf, Email, IsActive From Client Where Role = {(int)role}"))
                .FirstOrDefault();
        }
    }
}