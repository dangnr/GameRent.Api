using GameRent.Application.Queries.Client;
using GameRent.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameRent.Application.Interfaces.Queries
{
    public interface IClientQueryRepository
    {
        Task<List<ClientViewModel>> FilteredWhereAsync(ClientQueryRequest request);

        Task<ClientViewModel> GetById(ClientQueryRequest request);
    }
}