using GameRent.Api.Extensions;
using GameRent.Application.Interfaces.Queries;
using GameRent.Domain.Commands.Game;
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
        private readonly IGameQueries _gameQueries;

        public GameController(IMediator mediator, IGameQueries gameQueries)
        {
            _mediator = mediator;
            _gameQueries = gameQueries;
        }

        [HttpPost]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> Post([FromBody] CreateGameCommand createGameCommand)
        {
            var response = await _mediator.Send(createGameCommand);

            return Ok(response);
        }

        [HttpPut]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> Put([FromBody] UpdateGameCommand updateGameCommand)
        {
            var response = await _mediator.Send(updateGameCommand);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _mediator.Send(new DeleteGameCommand { Id = id});

            return Ok(response);
        }

        [HttpGet]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _gameQueries.GetAll();

            return Ok(response);
        }

        [HttpGet]
        [Route("available")]
        [RoleAuthorize(UserRoleType.Admin, UserRoleType.User)]
        public async Task<IActionResult> GetAllAvailable()
        {
            var response = await _gameQueries.GetAllAvailable();

            return Ok(response);
        }

        [HttpGet]
        [Route("rented")]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> GetAllRented()
        {
            var response = await _gameQueries.GetAllRented();

            return Ok(response);
        }

        [HttpGet("{id}")]
        [RoleAuthorize(UserRoleType.Admin, UserRoleType.User)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _gameQueries.GetById(id);

            return Ok(response);
        }
    }
}