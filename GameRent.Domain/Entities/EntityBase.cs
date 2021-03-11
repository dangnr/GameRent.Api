using System;

namespace GameRent.Domain.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; protected set; }
        public bool IsActive { get; protected set; }
        public DateTime CreatedOn { get; protected set; }
        public DateTime? UpdatedOn { get; protected set; }
    }
}