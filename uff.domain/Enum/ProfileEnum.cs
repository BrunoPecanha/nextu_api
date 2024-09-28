using System.ComponentModel;

namespace uff.Domain.Enum {
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
