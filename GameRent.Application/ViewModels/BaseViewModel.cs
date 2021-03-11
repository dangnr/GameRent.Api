using System;

namespace GameRent.Application.ViewModels
{
    public abstract class BaseViewModel
    {
        public Guid Id { get; protected set; }
        public bool IsActive { get; protected set; }
    }
}