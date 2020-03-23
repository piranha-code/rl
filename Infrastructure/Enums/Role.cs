using System.ComponentModel;

namespace Infrastructure.Enums
{
    [Description("Roles")]
    public enum Role
    {
        [Description("Guest")]
        Guest = 2,
        [Description("Admin")]
        Admin = 4
    }
}