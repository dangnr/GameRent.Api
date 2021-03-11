using GameRent.Application.ViewModels;
using GameRent.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameRent.Application.Interfaces.Queries
{
    public interface IClientQueries
    {
        Task<ClientViewModel> GetById(Guid id);
        Task<ClientViewModel> GetByUsername(string username);
        Task<ClientViewModel> GetByRole(UserRoleType role);
        Task<List<ClientViewModel>> GetAll();
    }
}