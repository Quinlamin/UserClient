using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UserClient
{
    static class Server
    {
        public static Socket server;
        
        public static async void ClientSend(Packet packet)
        {
            byte[] data = packet.data;

            _ = await Server.server.SendAsync(data, SocketFlags.None);
            var buffer = new byte[1024];
            var received = await Server.server.ReceiveAsync(buffer, SocketFlags.None);
            var ackPacket = new Packet(buffer);
            if(ackPacket.packetID == 0)
            {
                Console.WriteLine(ackPacket.ReadString(10) + ", " + ackPacket.ReadInt32());
            }
            
        }

        public static async void Connect(string server)
        {
            int port = 26950;
            IPEndPoint iPEndPoint = new(IPAddress.Parse(server), port);

            Server.server = new(iPEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            
            Console.WriteLine("stop");
            await Server.server.ConnectAsync(iPEndPoint);
            while (true)
            {
                try
                {
                    Console.WriteLine("Sending Connection");
                    Packet conPacket = new Packet(0);
                    _ = await Server.server.SendAsync(new ArraySegment<byte>(conPacket.data), SocketFlags.None);
                    
                    var buffer = new byte[1024];
                    var received = await Server.server.ReceiveAsync(buffer, SocketFlags.None);
                    Packet ackPacket = new Packet(buffer);

                    if(ackPacket.packetID == 1)
                    {
                        Int32 pID = ackPacket.ReadInt32();
                        ackPacket.DebugContents();
                        Console.WriteLine("Received: {0}", pID);

                        
                        
                        break;
                    }
                    
                    
                }
                catch (Exception)
                {


                }
            }
            Listener.Start(port);
            Console.WriteLine("Connected");

        }
    }
}
