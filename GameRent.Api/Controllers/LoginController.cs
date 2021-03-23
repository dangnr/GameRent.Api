using GameRent.Api.Extensions;
using GameRent.Domain.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameRent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginCommand login)
        {
           var response = await _mediator.Send(login);

            return CustomResponse.GetResponse(response);
        }
    }
}