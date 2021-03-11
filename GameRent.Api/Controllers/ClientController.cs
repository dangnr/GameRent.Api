using GameRent.Api.Extensions;
using GameRent.Application.Interfaces.Queries;
using GameRent.Domain.Commands.Client;
using GameRent.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GameRent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IClientQueries _clientQueries;

        public ClientController(IMediator mediator, IClientQueries clientQueries)
        {
            _mediator = mediator;
            _clientQueries = clientQueries;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateClientCommand createClientCommand)
        {
            var response = await _mediator.Send(createClientCommand);

            return Ok(response);
        }

        [HttpPut]
        [RoleAuthorize(UserRoleType.Admin, UserRoleType.User)]
        public async Task<IActionResult> Put([FromBody] UpdateClientCommand updateClientCommand)
        {
            var response = await _mediator.Send(updateClientCommand);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _mediator.Send(new DeleteClientCommand { Id = id});

            return Ok(response);
        }

        [HttpGet]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _clientQueries.GetAll();

            return Ok(response);
        }

        [HttpGet("{id}")]
        [RoleAuthorize(UserRoleType.Admin, UserRoleType.User)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _clientQueries.GetById(id);

            return Ok(response);
        }

        [HttpGet]
        [Route("role/{role}")]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> GetByRole(UserRoleType role)
        {
            var response = await _clientQueries.GetByRole(role);

            return Ok(response);
        }
    }
}