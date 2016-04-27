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
        /// <summary>
        /// Default ExchangeName
        /// </summary>
        public string ExchangeName = "test.exchange";

        /// <summary>
        /// Default QueueName
        /// </summary>
        public string QueueName = "test.queue";

        /// <summary>
        /// RabbitMqFactory  Reference
        /// </summary>
        public static   ConnectionFactory RabbitMqFactory = null;

        /// <summary>
        /// 初始化默认ConnectionFactory
        /// </summary>
        public void InitDefaultConnectionFactory()
        {
            RabbitMqFactory=new ConnectionFactory { HostName = "localhost", UserName = "gb", Password = "gb", VirtualHost = "/" };
        }

        /// <summary>
        /// 初始化ConnectionFactory
        /// </summary>
        /// <param name="cf">ConnectionFactory instance</param>
        public void InitConnectionFactory(ConnectionFactory cf)
        {
            RabbitMqFactory = cf;
        }

        /// <summary>
        /// 从ConnectionFactory获取一个Connection
        /// </summary>
        /// <returns></returns>
        public IConnection GetConnection()
        {
            return RabbitMqFactory.CreateConnection();
        }

        /// <summary>
        /// 获取Channel
        /// </summary>
        /// <param name="conn">Connection instance</param>
        /// <param name="type">type</param>
        /// <returns></returns>
        public IModel GetChannel(IConnection conn, string type)
        {

            IModel channel = conn.CreateModel();
            channel.ExchangeDeclare(ExchangeName, type, durable: true, autoDelete: false, arguments: null);
            channel.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(QueueName, ExchangeName, routingKey: QueueName);
            return channel;


        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="conn">Connection instance</param>
        /// <param name="channel">Channel instance</param>
        public void Dispose(IConnection conn, IModel channel)
        {
            conn?.Dispose();
            channel?.Dispose();
        }
    }
}
