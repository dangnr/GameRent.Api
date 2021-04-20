using GameRent.Api.Extensions;
using GameRent.Application.Commands.Client.CreateClient;
using GameRent.Application.Commands.Client.DeleteClient;
using GameRent.Application.Commands.Client.UpdateClient;
using GameRent.Application.Queries.Client;
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

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateClientCommandRequest createClientCommand)
        {
            var response = await _mediator.Send(createClientCommand);

            return CustomResponse.GetResponse(response);
        }

        [HttpPut]
        [RoleAuthorize(UserRoleType.Admin, UserRoleType.User)]
        public async Task<IActionResult> Put([FromBody] UpdateClientCommandRequest updateClientCommand)
        {
            var response = await _mediator.Send(updateClientCommand);

            return CustomResponse.GetResponse(response);
        }

        [HttpDelete("{id}")]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _mediator.Send(new DeleteClientCommandRequest(id: id));

            return CustomResponse.GetResponse(response);
        }

        [HttpGet]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new ClientQueryRequest());

            return CustomResponse.GetResponse(response);
        }

        [HttpGet]
        [Route("username/{username}")]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> GetAll(string username)
        {
            var response = await _mediator.Send(new ClientQueryRequest(username: username));

            return CustomResponse.GetResponse(response);
        }

        [HttpGet("{id}")]
        [RoleAuthorize(UserRoleType.Admin, UserRoleType.User)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new ClientQueryRequest(id: id));

            return CustomResponse.GetResponse(response);
        }

        [HttpGet]
        [Route("role/{role}")]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> GetByRole(UserRoleType role)
        {
            var response = await _mediator.Send(new ClientQueryRequest(role: role));

            return CustomResponse.GetResponse(response);
        }
    }
}