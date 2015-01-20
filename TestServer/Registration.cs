using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer
{
    public class NetworkPart
    {
        public int Id { get; set; }

        public NetworkPart()
        {
        }
    }

    public class Registration
    {
        public Dictionary<int, NetworkPart> registeredClasses = new Dictionary<int, NetworkPart>();
        List<int> clearedIds = new List<int>();
        int lastId = 0;
        public int currentId;

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
    }


}
