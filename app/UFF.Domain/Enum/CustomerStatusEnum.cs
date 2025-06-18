using System.ComponentModel;

namespace UFF.Domain.Enum
{
    public enum CustomerStatusEnum
    {
        [Description("Waiting")]
        Waiting,
        [Description("Removed")]
        Removed,
        [Description("InService")]
        InService,
        [Description("Done")]
        Done,
        [Description("Absent")]
        Absent,
        [Description("Canceled")]
        Canceled,
        [Description("Pending")]
        Pending,
        [Description("Approved")]
        Approved,
        [Description("Rejected")]
        Rejected
    }
}
