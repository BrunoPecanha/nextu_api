using System.ComponentModel;

namespace UFF.Domain.Enum {
    public enum ProfileEnum
    {
        [Description("Customer")]
        Customer,
        [Description("Employee")]
        Employee,
        [Description("Owner")]
        Owner,
        [Description("Admin")]
        Admin
    }
}
