using GameRent.Api.Extensions;
using GameRent.Application.Commands.Game.CreateGame;
using GameRent.Application.Commands.Game.DeleteGame;
using GameRent.Application.Commands.Game.UpdateGame;
using GameRent.Application.Queries.Game;
using GameRent.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GameRent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> Post([FromBody] CreateGameCommandRequest createGameCommand)
        {
            var response = await _mediator.Send(createGameCommand);

            return CustomResponse.GetResponse(response);
        }

        [HttpPut]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> Put([FromBody] UpdateGameCommandRequest updateGameCommand)
        {
            var response = await _mediator.Send(updateGameCommand);

            return CustomResponse.GetResponse(response);
        }

        [HttpDelete("{id}")]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _mediator.Send(new DeleteGameCommandRequest(id));

            return CustomResponse.GetResponse(response);
        }

        [HttpGet]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GameQueryRequest());

            return CustomResponse.GetResponse(response);
        }

        [HttpGet]
        [Route("available")]
        [RoleAuthorize(UserRoleType.Admin, UserRoleType.User)]
        public async Task<IActionResult> GetAllAvailable()
        {
            var response = await _mediator.Send(new GameQueryRequest(true));

            return CustomResponse.GetResponse(response);
        }

        [HttpGet]
        [Route("rented")]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> GetAllRented()
        {
            var response = await _mediator.Send(new GameQueryRequest(false));

            return CustomResponse.GetResponse(response);
        }

        [HttpGet("{id}")]
        [RoleAuthorize(UserRoleType.Admin, UserRoleType.User)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GameQueryRequest(id));

            return CustomResponse.GetResponse(response);
        }
    }
}