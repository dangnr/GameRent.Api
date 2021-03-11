using GameRent.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GameRent.Domain.Entities
{
    public class Client: EntityBase
    {
        [MaxLength(100)]
        public string FirstName { get; private set; }

        [MaxLength(100)]
        public string LastName { get; private set; }

        [MaxLength(11)]
        public string Cpf { get; private set; }

        [MaxLength(100)]
        public string Email { get; private set; }

        [MaxLength(100)]
        public string Username { get; private set; }

        [MaxLength(100)]
        public string Password { get; private set; }

        public UserRoleType Role { get; private set; }

        public Client(string firstName, string lastName, string cpf, string email, string username, string password, UserRoleType role)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Cpf = cpf;
            Email = email;
            Username = username;
            Password = password;
            Role = role;
            IsActive = true;
            CreatedOn = DateTime.Now;
        }

        public Client(Guid id, string firstName, string lastName, string cpf, string email, string username, string password, UserRoleType role, bool isActive)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Cpf = cpf;
            Email = email;
            Username = username;
            Password = password;
            Role = role;
            IsActive = isActive;
        }

        public void UpdateClient(Client client)
        {
            Id = client.Id;
            FirstName = client.FirstName;
            LastName = client.LastName;
            Cpf = client.Cpf;
            Email = client.Email;
            Username = client.Username;
            Password = client.Password;
            Role = client.Role;
            IsActive = client.IsActive;
            UpdatedOn = DateTime.Now;
        }
    }
}