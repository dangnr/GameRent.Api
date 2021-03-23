using GameRent.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameRent.Api.Extensions
{
    public static class CustomResponse
    {
        public static ObjectResult GetResponse(BaseResponse responseApi)
        {
            ObjectResult result = ((int)responseApi.StatusCode) switch
            {
                (int)HttpStatusCode.NotFound => new NotFoundObjectResult(responseApi),
                (int)HttpStatusCode.InternalServerError => new BadRequestObjectResult(responseApi),
                (int)HttpStatusCode.BadRequest => new BadRequestObjectResult(responseApi),
                    _ => new OkObjectResult(responseApi),
            };

            return result;
        }
    }
}