using GameRent.Domain.Commands.Rent;
using GameRent.Domain.Shared;
using MediatR;

namespace GameRent.Domain.Interfaces.Command
{
    public interface IRentCommandHandler :
        IRequestHandler<CreateRentCommand, BaseResponse>,
        IRequestHandler<FinishRentCommand, BaseResponse>
    { }
}