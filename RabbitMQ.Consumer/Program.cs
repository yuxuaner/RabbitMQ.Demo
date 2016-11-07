using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Common;

namespace RabbitMQ.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("this is a Consumer!");
            Console.Title = "RabbitMQ.Consumer";
            var client = new RabbitMQClient();
            client.InitDefaultConnectionFactory();

            //1.BasicGet+ACK
            while (true)
            {
                Thread.Sleep(1000);

                var conn = client.GetConnection();
                var channel = client.GetChannel(conn, "direct");
                var props = channel.CreateBasicProperties();
                props.Persistent = true;
                Console.WriteLine("receiving...");

                var msgResponse = channel.BasicGet(client.QueueName, noAck: false);
                if (msgResponse == null) continue;
                var msgBody = Encoding.UTF8.GetString(msgResponse.Body);
                channel.BasicAck(msgResponse.DeliveryTag, multiple: false);
                Console.WriteLine("\r\nreceive message:" + msgBody + Environment.NewLine);
                client.Dispose(conn, channel);

            }


            //2.事件订阅
            //var client = new RabbitMQClient();
            //IModel channel = client.GetChannel(conn, "direct");
            //var props = channel.CreateBasicProperties();
            //props.SetPersistent(true);
            //var consumer = new QueueingBasicConsumer(channel);

            //channel.BasicConsume(client.QueueName, noAck: false, consumer: consumer);

            //while (true)
            //{
            //    Console.WriteLine("正在接受消息。。。");

            //    var msgResponse = consumer.Queue.Dequeue(); //blocking

            //    var msgBody = Encoding.UTF8.GetString(msgResponse.Body);

            //    Console.WriteLine("收到消息:" + msgBody);
            //    Thread.Sleep(1000);
            //}
            Console.WriteLine("q");
            Console.ReadLine();
        }
    }
}
