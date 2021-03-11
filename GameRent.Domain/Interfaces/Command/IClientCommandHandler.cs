using GameRent.Domain.Commands.Client;
using GameRent.Domain.Shared;
using MediatR;

namespace GameRent.Domain.Interfaces.Command
{
    public interface IClientCommandHandler:
        IRequestHandler<CreateClientCommand, BaseResponse>,
        IRequestHandler<UpdateClientCommand, BaseResponse>,
        IRequestHandler<DeleteClientCommand, BaseResponse>
    { }
}