namespace BodyaFen_API_.RabbitMQ
{
    public interface IRabitMQProducer
    {
        public void SendMessage<T>(T message);
    }
}
