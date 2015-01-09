using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteSpace.Network;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server.startServer("test", 1111);
            Server.registerNetworkListenerMethod("Host", test);
            Server.registerNetworkListenerMethod("FindGames", OnFindGamesRequestEnter);
            Console.ReadLine();
        }

        static void test(ReceiveableNetworkMessage msg)
        {
            GameContainer.activeGames.Add(new Lobby(msg.getInformation("GameName")));
            Server.sendMessageToSingleRecipient(new SendableNetworkMessage("Host"), msg.SenderConnection);
        }

        static void OnFindGamesRequestEnter(ReceiveableNetworkMessage msg)
        {
            foreach(Lobby l in GameContainer.activeGames)
            {
                SendableNetworkMessage smsg = new SendableNetworkMessage("FoundGames");
                smsg.addInformation("GameName", l.name);
                Server.sendMessageToSingleRecipient(smsg, msg.SenderConnection);
            }
        }

    }
}
