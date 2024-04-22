using System.ComponentModel;

namespace Supply.Domain.Enum {
    public enum StateEnum {
        [Description("Ativo")]
        Ativo = 1,
        [Description("Inativo")]
        Inativo = 2     
    }
}
