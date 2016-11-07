using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Common;

namespace RabbitMQ.Test
{
    class Program
    {
        public enum MessegePriority : byte
        {
            Low = 1,
            Normal = 2,
            High = 3
        }
        static void Main(string[] args) 
        {
            Console.WriteLine("this is a sender!");
            var client = new RabbitMQClient();
            var conn = client.GetConnection();
            IModel channel = client.GetChannel(conn, "direct");
           
            while (true)
            {
                Console.WriteLine("请输入:");
                var input = Console.ReadLine();
                var props = channel.CreateBasicProperties();
                props.Persistent = true;
                Console.WriteLine("请输入优先级:");
                var input3 = Console.ReadLine();
                switch (input3)
                {
                    case "1":
                        props.Priority = (byte)MessegePriority.Low;
                        break;
                    case "2":
                        props.Priority = (byte)MessegePriority.Normal;
                        break;
                    case "3":
                        props.Priority = (byte)MessegePriority.High;
                        break;
                    default:
                        props.Priority = (byte)MessegePriority.Low;
                        break;
                }
    

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
