using GameRent.Api.Extensions;
using GameRent.Application.Interfaces.Queries;
using GameRent.Domain.Commands.Rent;
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
        private readonly IRentQueries _rentQueries;

        public RentController(IMediator mediator, IRentQueries rentQueries)
        {
            _mediator = mediator;
            _rentQueries = rentQueries;
        }

        [HttpPost]
        [RoleAuthorize(UserRoleType.Admin, UserRoleType.User)]
        public async Task<IActionResult> Post([FromBody] CreateRentCommand createRentCommand)
        {
            var response = await _mediator.Send(createRentCommand);

            return CustomResponse.GetResponse(response);
        }

        [HttpPut]
        [Route("finish/{id}")]
        [RoleAuthorize(UserRoleType.Admin, UserRoleType.User)]
        public async Task<IActionResult> Put(Guid id)
        {
            var response = await _mediator.Send(new FinishRentCommand { Id = id });

            return CustomResponse.GetResponse(response);
        }

        [HttpGet]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _rentQueries.GetAll();

            return CustomResponse.GetResponse(response);
        }

        [HttpGet]
        [Route("finished")]
        [RoleAuthorize(UserRoleType.Admin)]
        public async Task<IActionResult> GetAllFinished()
        {
            var response = await _rentQueries.GetAllFinished();

            return CustomResponse.GetResponse(response);
        }

        [HttpGet("{id}")]
        [RoleAuthorize(UserRoleType.Admin, UserRoleType.User)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _rentQueries.GetById(id);

            return CustomResponse.GetResponse(response);
        }

        [HttpGet]
        [Route("client/{clientId}")]
        [RoleAuthorize(UserRoleType.Admin, UserRoleType.User)]
        public async Task<IActionResult> GetByClientId(Guid clientId)
        {
            var response = await _rentQueries.GetByClientId(clientId);

            return CustomResponse.GetResponse(response);
        }
    }
}