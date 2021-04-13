using GameRent.Application.Shared;
using GameRent.Domain.Enums;
using MediatR;
using System;

namespace GameRent.Application.Queries.Client
{
    public class ClientQueryRequest : IRequest<BaseResponse>
    {
        public ClientQueryRequest()
        { }

        public ClientQueryRequest(Guid? id)
        {
            Id = id;
        }

        public ClientQueryRequest(UserRoleType? role)
        {
            Role = role;
        }

        public ClientQueryRequest(string username)
        {
            Username = username;
        }

        public Guid? Id { get; set; }
        public UserRoleType? Role { get; set; }
        public string Username { get; set; }
    }
}