namespace UFF.Domain.Entity
{
    public class QueueCustomer : To
    {
        private QueueCustomer()
        {
        }

        public int QueueId { get; private set; }
        public Queue Queue { get; private set; }
        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; }

    }
}