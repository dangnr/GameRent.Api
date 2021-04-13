using GameRent.Application.Shared;
using MediatR;
using System;

namespace GameRent.Application.Queries.Rent
{
    public class RentQueryRequest : IRequest<BaseResponse>
    {
        public RentQueryRequest()
        { }

        public RentQueryRequest(Guid? id, bool? isClientId = false)
        {
            if (isClientId.GetValueOrDefault() is true) ClientId = id;
            else Id = id;
        }

        public RentQueryRequest(bool? isActive)
        {
            IsActive = isActive;
        }

        public Guid? Id { get; set; }
        public Guid? ClientId { get; set; }
        public bool? IsActive { get; set; }
    }
}