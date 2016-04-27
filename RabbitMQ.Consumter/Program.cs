using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Common;

namespace RabbitMQ.Consumter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("this is s consumter!");
            var client = new RabbitMQClient();
            var conn = client.GetConnection();
            IModel channel = client.GetChannel(conn, "direct");
            var props = channel.CreateBasicProperties();
            props.SetPersistent(true);

            //1.BasicGet+ACK

            while (true)
            {
                Thread.Sleep(1000);

                Console.WriteLine("正在接受消息。。。");

                BasicGetResult msgResponse = channel.BasicGet(client.QueueName, noAck: false);
                if(msgResponse==null)continue;
                var msgBody = Encoding.UTF8.GetString(msgResponse.Body);
               channel.BasicAck(msgResponse.DeliveryTag, multiple: false);
                Console.WriteLine("收到消息:" + msgBody+Environment.NewLine);
            }
     

            //2事件订阅
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
            client.Dispose(conn, channel);
        }
    }
}
