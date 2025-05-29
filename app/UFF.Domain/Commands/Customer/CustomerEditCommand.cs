using UFF.Domain.Commands.Queue;

namespace UFF.Domain.Commands.Customer
{
    public class CustomerEditServicesPaymentCommand
    {
        public QueueServiceCommand[] SelectedServices { get; set; }
        public string Notes { get; set; }
        public int PaymentMethod { get; set; }
        public int Id { get; set; }
    }
}