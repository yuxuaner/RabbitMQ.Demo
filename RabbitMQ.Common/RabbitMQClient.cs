using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQ.Common
{
    public class RabbitMQClient
    {
        public string ExchangeName = "test.exchange";
        public string QueueName = "test.queue";

        public IConnection GetConnection()
        {
            return Program.RabbitMqFactory.CreateConnection();
        }
        public IModel GetChannel(IConnection conn,string type)
        {

            IModel channel = conn.CreateModel();
            channel.ExchangeDeclare(ExchangeName, type, durable: true, autoDelete: false, arguments: null);
          channel.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(QueueName, ExchangeName, routingKey: QueueName);
            return channel;


        }

        public void Dispose(IConnection conn, IModel channel)
        {
            conn?.Dispose();
            channel?.Dispose();
        }
    }
}
