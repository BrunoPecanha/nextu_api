using System.ComponentModel;

namespace UFF.Domain.Enum {
    public enum QueueStatusEnum
    {
        [Description("Closed")]
        Closed,
        [Description("Open")]
        Open,
        [Description("Paused")]
        Paused
    }
}
