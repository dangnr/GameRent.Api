using GameRent.Domain.Commands.Login;
using GameRent.Domain.Shared;
using MediatR;

namespace GameRent.Domain.Interfaces.Command
{
    public interface ILoginCommandHandler :
        IRequestHandler<LoginCommand, BaseResponse>
    { }
}