using System.ComponentModel;

namespace UFF.Domain.Enum {
    public enum QueueStatusEnum
    {
        [Description("Open")]
        Open,
        [Description("Closed")]
        Closed,       
        [Description("Paused")]
        Paused,
        [Description("All")]
        All
    }
}
