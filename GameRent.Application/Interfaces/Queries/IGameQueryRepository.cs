using GameRent.Application.Queries.Game;
using GameRent.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameRent.Application.Interfaces.Queries
{
    public interface IGameQueryRepository
    {
        Task<List<GameViewModel>> GetFilteredItemsAsync(GameQueryRequest request);

        Task<GameViewModel> GetFilteredItemAsync(GameQueryRequest request);
    }
}