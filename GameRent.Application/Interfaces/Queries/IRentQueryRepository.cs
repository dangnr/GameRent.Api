using GameRent.Application.Queries.Rent;
using GameRent.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameRent.Application.Interfaces.Queries
{
    public interface IRentQueryRepository
    {
        Task<List<RentViewModel>> FilteredWhereAsync(RentQueryRequest request);

        Task<RentViewModel> GetById(RentQueryRequest request);
    }
}