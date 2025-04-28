using System.ComponentModel;

namespace UFF.Domain.Enum {
    public enum ProfileEnum
    {
        [Description("User")]
        User,
        [Description("Owner")]
        Owner,
        [Description("Admin")]
        Admin
    }
}
