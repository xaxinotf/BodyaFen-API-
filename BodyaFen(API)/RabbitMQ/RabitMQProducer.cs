using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace BodyaFen_API_.RabbitMQ
{
    public class RabitMQProducer : IRabitMQProducer
    {
        public void SendMessage<T>(T message)
        {
            
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
           
            var connection = factory.CreateConnection();
            using
            var channel = connection.CreateModel();
            
            channel.QueueDeclare("genre", exclusive: false);
            
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            
            channel.BasicPublish(exchange: "", routingKey: "genre", body: body);
        }
    }
}
