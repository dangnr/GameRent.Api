using GameRent.Api.Extensions;
using GameRent.Application.Commands.Rent.CreateRent;
using GameRent.Application.Commands.Rent.FinishRent;
using GameRent.Application.Queries.Rent;
using GameRent.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GameRent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [RoleAuthorize(UserRoleType.Admin, UserRoleType.User)]
        public async Task<IActionResult> Post([FromBody] CreateRentCommandRequest createRentCommand)
        {
            var response = await _mediator.Send(createRentCommand);

            return CustomResponse.GetResponse(response);
        }

        [HttpPut]
        [Route("finish/{id}")]
        [RoleAuthorize(UserRoleType.Admin, UserRoleType.User)]
        public async Task<IActionResult> Put(Guid id)
        {
            var response = await _mediator.Send(new FinishRentCommandRequest(id: id));

            return CustomResponse.GetResponse(response);
        }

        [HttpGet]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new RentQueryRequest());

            return CustomResponse.GetResponse(response);
        }

        [HttpGet]
        [Route("finished")]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> GetAllFinished()
        {
            var response = await _mediator.Send(new RentQueryRequest(isActive: false));

            return CustomResponse.GetResponse(response);
        }

        [HttpGet("{id}")]
        [RoleAuthorize(UserRoleType.Admin, UserRoleType.User)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new RentQueryRequest(id: id));

            return CustomResponse.GetResponse(response);
        }

        [HttpGet]
        [Route("client/{clientId}")]
        [RoleAuthorize(UserRoleType.Admin, UserRoleType.User)]
        public async Task<IActionResult> GetByClientId(Guid clientId)
        {
            var response = await _mediator.Send(new RentQueryRequest(id: clientId, isClientId: true));

            return CustomResponse.GetResponse(response);
        }
    }
}