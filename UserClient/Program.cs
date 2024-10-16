using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UserClient;

namespace ClientApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server.Connect("127.0.0.1");
            Console.Read();
            
            Console.ReadLine();
        }

        
    }
}
