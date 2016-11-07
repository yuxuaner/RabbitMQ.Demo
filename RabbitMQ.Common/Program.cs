using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQ.Common
{
    public class Program
    {
        public static readonly ConnectionFactory RabbitMqFactory = new ConnectionFactory { HostName = "localhost", UserName = "gb", Password = "gb", VirtualHost = "/" };
    }
}
