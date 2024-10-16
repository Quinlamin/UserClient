using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UserClient
{
    static class Listener
    {
        static int port;
        public static int pId;
        
        public static void Start(int _port)
        {
            port = _port;

            MainAsync();
        }

        static async void MainAsync()
        {
            

            Console.WriteLine("Started");
            while (true)
            {





                var buffer = new byte[1024];
                var received = await Server.server.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);

                Packet packet = new Packet(buffer);

                var ackMessage = new Packet(1);
                Console.WriteLine("Received data");
                if (packet.packetID == 0)
                {
                    Console.WriteLine(packet.ReadString(10) + ", " + packet.ReadInt32());
                    packet.AddString("Received Data", 10);
                }


                await Server.server.SendAsync(new ArraySegment<byte>(ackMessage.data), SocketFlags.None);
                Console.WriteLine($"Sent Acknowledgement to Server");

            }
        }
    }
}
