using System.ComponentModel;

namespace GameRent.Domain.Enums
{
    public enum UserRoleType
    {
        [Description("Administrator")]
        Admin = 1,

        [Description("User")]
        User = 2
    }
}