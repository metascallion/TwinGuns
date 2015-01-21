using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteSpace.Network;

namespace TestServer
{
    public abstract class NetworkPart
    {
        public int Id { get; set; }
        public Server serverToRegisterTo;

        public NetworkPart(Server serverToRegisterTo)
        {
            this.serverToRegisterTo = serverToRegisterTo;
        }

        public abstract void updateInformation(SendableNetworkMessage msg);

        public abstract void OnUpdateMessageEnter(ReceiveableNetworkMessage msg);
    }

    public class Drone : NetworkPart
    {
        int health = 3;

        public Drone(Server serverToRegisterTo) : base(serverToRegisterTo)
        {
        }

        public override void updateInformation(SendableNetworkMessage msg)
        {
            msg.addInformationContent(this.Id.ToString(), health);
        }

        public override void OnUpdateMessageEnter(ReceiveableNetworkMessage msg)
        {
            this.health -= 1;
        }

    }

    public class Registration
    {
        public Dictionary<int, NetworkPart> registeredClasses = new Dictionary<int, NetworkPart>();
        List<int> clearedIds = new List<int>();
        int lastId = 0;
        public int currentId;
        Server registeredServer;

        public Registration(Server registeredServer)
        {
            this.registeredServer = registeredServer;
            registeredServer.registerNetworkListenerMethod("Update", OnUpdateMessageEnter);
        }

        public void enqueueClass(NetworkPart partToEnqueue)
        {
            registeredClasses[getFirstId()] = partToEnqueue;
            partToEnqueue.Id = this.currentId;
        }

        private int getFirstId()
        {
            if (clearedIds.Count > 0)
            {
                currentId = clearedIds[0];
                clearedIds.Remove(clearedIds[0]);
                return currentId;
            }
            int temp = lastId;
            currentId = lastId;
            lastId++;
            return temp;
        }

        public void remove(int id)
        {
            clearedIds.Add(id);
            registeredClasses.Remove(id);
        }

        public void sendUpdate()
        {
            SendableNetworkMessage msg = new SendableNetworkMessage("ObjectUpdate");

            foreach(NetworkPart part in registeredClasses.Values)
            {
                part.updateInformation(msg);
            }

            registeredServer.sendMessage(msg);
        }

        public void OnUpdateMessageEnter(ReceiveableNetworkMessage msg)
        {
            int id = int.Parse(msg.getInformation("Id"));

            registeredClasses[id].OnUpdateMessageEnter(msg);
        }

    }
}
