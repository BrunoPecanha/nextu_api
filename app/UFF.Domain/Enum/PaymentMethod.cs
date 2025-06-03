using System.ComponentModel;

namespace UFF.Domain.Enum
{
    public enum PaymentMethod
    {
        [Description("Pix")]
        Pix = 3,
        [Description("Cartão")]
        Card = 1,
        [Description("Dinheiro")]
        Cash = 2
    }
}
