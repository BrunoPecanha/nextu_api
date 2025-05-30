using System.ComponentModel;

namespace UFF.Domain.Enum
{
    public enum FileEnum
    {
        [Description("Profile")]
        Profile,
        [Description("Product")]
        Product,
        [Description("Service")]
        Service,
        [Description("Logo")]
        Logo,
        [Description("WallPaper")]
        WallPaper
    }
}
