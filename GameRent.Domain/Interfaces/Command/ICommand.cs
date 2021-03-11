using GameRent.Domain.Shared;
using MediatR;

namespace GameRent.Domain.Interfaces.Command
{
    public interface ICommand : IRequest<BaseResponse>
    { }
}