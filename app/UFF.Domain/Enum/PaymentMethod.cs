using System.ComponentModel;

namespace UFF.Domain.Enum {
    public enum PaymentMethod
    {
        [Description("Pix")]
        Pix,
        [Description("Cartão")]
        Card,
        [Description("Dinheiro")]
        Cash
    }
}
