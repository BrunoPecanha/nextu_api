using System.ComponentModel;

namespace Supply.Domain.Enum {
    public enum StatusEnum {
        [Description("Enabled")]
        Enabled,
        [Description("Disabled")]
        Disabled
    }
}
