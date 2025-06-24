using System.ComponentModel;

namespace UFF.Domain.Enum
{
    public enum NotificationEnum
    {
        /// <summary>
        /// Quando um cliente é chamado na fila, é enviada uma notificação para todos os clientes aguardando
        /// </summary>
        [Description("AvancoNaFila")]
        AdvanceQueue = 0,
        /// <summary>
        /// Quando um cliente entra na fila, o funcionário responsável pela fila ou todos (quando compartilham fila), são notificados
        /// </summary>
        [Description("NovoNaFila")]
        NewInQueue = 1,
        /// <summary>
        /// Quando esta configurado para usar aprovação de pedido antes de aceitar o cliente, uma notificaçãp é enviada toda vez que um cliente tenta entrar na fila
        /// </summary>
        [Description("SolicitacaoEntradaNaFila")]
        QueueEntryRequest = 2,
        /// <summary>
        /// Promoções
        /// </summary>
        [Description("Promotion")]
        Promotion = 3,
        /// <summary>
        /// Quando um estabelecimento envia um convite de trabalho para alguém, o usuário recebe uma notificação.
        /// </summary>
        [Description("SolicitacaoParaTrabalhar")]
        RequestToWork = 4,
        /// <summary>
        /// Quando alguma solicitação é aceita, o usuário solicitante recebe uma notificação
        /// </summary>
        [Description("SolicitacaoAceita")]
        RequestAccepted = 5,
        /// <summary>
        /// Quando o tempo é recalculado por algum motivo, todos os usuário da fila são avisados.
        /// </summary>
        [Description("EstimativaDeTempoRecalculada")]
        RecalculatedEstimatedTime = 6,
        /// <summary>
        /// Quando a fila é pausada, uma notificação é enviada aos usuários
        /// </summary>
        [Description("FilaPausada")]
        PausedQueue = 7,
        /// <summary>
        /// Quando a fila é retomada, é enviado uma aviso ao usuários.
        /// </summary>
        [Description("FilaRetomada")]
        QueueResumed = 8,
        /// <summary>
        /// Quando um estabelecimento recebe uma avaliação
        /// </summary>
        [Description("AvaliacaoAtendimento")]
        ServiceEvaluation = 9,
        [Description("NovaMensageNaFila")]
        NewMessageInQueue = 10
    }
}