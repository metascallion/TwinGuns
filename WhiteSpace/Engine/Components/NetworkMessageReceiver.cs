using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Network;
using WhiteSpace.Composite;

namespace WhiteSpace.Components
{
    public abstract class NetworkMessageReceiver : StandardComponent
    {
        public NetworkMessageReceiver()
        {

        }

        public NetworkMessageReceiver(string header)
        {
            Client.registerNetworkListenerMethod(header, onNetworkMessageEnter);
        }

        protected abstract void onNetworkMessageEnter(ReceiveableNetworkMessage msg);
    }
}
