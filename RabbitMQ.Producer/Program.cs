using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Common;

namespace RabbitMQ.Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("this is a Producer!");
            Console.Title = "RabbitMQ.Producer";
            var client = new RabbitMQClient();
            client.InitDefaultConnectionFactory();
        

            while (true)
            {
                Console.WriteLine("please input message:");
                var input = Console.ReadLine();
                if (input == null) continue;
                var conn = client.GetConnection();
                var channel = client.GetChannel(conn, "direct");
                var props = channel.CreateBasicProperties();
                props.Persistent = true;
                var msgBody = Encoding.UTF8.GetBytes(input);
                channel.BasicPublish(client.ExchangeName, routingKey: client.QueueName, basicProperties: props, body: msgBody);
                Console.WriteLine("send success!"+Environment.NewLine);
                client.Dispose(conn, channel);
            }
        }
    }
}
