using GameRent.Application.Queries.Client;
using GameRent.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameRent.Application.Interfaces.Queries
{
    public interface IClientQueryRepository
    {
        Task<List<ClientViewModel>> GetFilteredItemsAsync(ClientQueryRequest request);

        Task<ClientViewModel> GetFilteredItemAsync(ClientQueryRequest request);
    }
}