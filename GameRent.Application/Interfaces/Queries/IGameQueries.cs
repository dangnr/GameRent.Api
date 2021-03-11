using GameRent.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameRent.Application.Interfaces.Queries
{
    public interface IGameQueries
    {
        Task<GameViewModel> GetById(Guid id);
        Task<List<GameViewModel>> GetAll();
        Task<List<GameViewModel>> GetAllAvailable();
        Task<List<GameViewModel>> GetAllRented();
    }
}