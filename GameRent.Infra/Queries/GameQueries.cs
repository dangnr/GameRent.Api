using Dapper;
using GameRent.Application.Interfaces.Queries;
using GameRent.Application.ViewModels;
using GameRent.Infra.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRent.Infra.Queries
{
    public class GameQueries : IGameQueries
    {
        private readonly SqlConnection _sqlConnection;

        public GameQueries(GameRentContext context)
        {
            _sqlConnection = new SqlConnection(context.Database.GetConnectionString());
        }

        public async Task<List<GameViewModel>> GetAll()
        {
            return (await _sqlConnection
                .QueryAsync<GameViewModel>
                    ("Select Id, Name, Genre, Synopsis, Platform, LaunchDate, IsAvailable, IsActive From Game"))
                .AsList();
        }

        public async Task<List<GameViewModel>> GetAllAvailable()
        {
            var query = @"SELECT 
                          Id,
                    	  Name,
	                      Genre,
                          Synopsis,
	                      Platform,
	                      LaunchDate,
                          IsAvailable,
	                      IsActive
                      FROM Game
                      WHERE IsAvailable = 'true'
                      ORDER BY Name";

           return (await _sqlConnection.QueryAsync<GameViewModel>(query)).AsList();
        }

        public async Task<List<GameViewModel>> GetAllRented()
        {
            var query = @"SELECT 
                          Id,
                    	  Name,
	                      Genre,
                          Synopsis,
	                      Platform,
	                      LaunchDate,
                          IsAvailable,
	                      IsActive
                      FROM Game
                      WHERE IsAvailable = 'false'
                      ORDER BY Name";

            return (await _sqlConnection.QueryAsync<GameViewModel>(query)).AsList();
        }

        public async Task<GameViewModel> GetById(Guid id)
        {
            var queryArgs = new DynamicParameters();

            queryArgs.Add("Id", id);

            return (await _sqlConnection
                .QueryAsync<GameViewModel>
                    ("Select Id, Name, Genre, Synopsis, Platform, LaunchDate, IsAvailable, IsActive From Game Where Id = @id", queryArgs))
                .FirstOrDefault();
        }
    }
}