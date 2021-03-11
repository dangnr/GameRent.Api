using GameRent.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameRent.Application.Interfaces.Queries
{
    public interface IRentQueries
    {
        Task<RentViewModel> GetById(Guid id);
        Task<List<RentViewModel>> GetAll();
        Task<List<RentViewModel>> GetByClientId(Guid id);
    }
}