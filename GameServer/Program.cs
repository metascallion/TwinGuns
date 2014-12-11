using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace MasterServer
{
    class Program
    {
        static void Main(string[] args)
        {
            NetPeerConfiguration config = new NetPeerConfiguration("test");
            config.Port = 1111;

            NetServer server = new NetServer(config);
            server.Start();
            Console.WriteLine("Server started.");

            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);

            
            while(true)
            {
                NetIncomingMessage msg;

                if((msg = server.ReadMessage()) != null)
                {
                    if(msg.MessageType == NetIncomingMessageType.ConnectionApproval)
                    {
                        Console.WriteLine("A Client is trying to connect.");
                        msg.SenderConnection.Approve();
                        Console.Write("Connection approved.");
                    }
                }
            }

        }


    }
}
