using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Common;

namespace RabbitMQ.Test
{
    class Program
    {

        static void Main(string[] args) 
        {
            Console.WriteLine("this is a sender!");
            var client = new RabbitMQClient();
            var conn = client.GetConnection();
            IModel channel = client.GetChannel(conn, "direct");
            var props = channel.CreateBasicProperties();
            props.SetPersistent(true);

            while (true)
            {
                Console.WriteLine("请输入:");
                var input = Console.ReadLine();
                if (input != null)
                {
                    var msgBody = Encoding.UTF8.GetBytes(input);
                    channel.BasicPublish(client.ExchangeName, routingKey: client.QueueName, basicProperties: props, body: msgBody);
                    Console.WriteLine("发送成功！");
                    Console.WriteLine();
                }
            }
  
            Console.WriteLine("q");
            Console.ReadLine();
            client.Dispose(conn,channel);
            
        }
    }
}
