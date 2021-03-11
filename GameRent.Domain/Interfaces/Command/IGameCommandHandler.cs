using GameRent.Domain.Commands.Game;
using GameRent.Domain.Shared;
using MediatR;

namespace GameRent.Domain.Interfaces.Command
{
    public interface IGameCommandHandler :
        IRequestHandler<CreateGameCommand, BaseResponse>,
        IRequestHandler<UpdateGameCommand, BaseResponse>,
        IRequestHandler<DeleteGameCommand, BaseResponse>
    { }
}