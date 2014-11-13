using System;
using Lidgren.Network;

namespace Network
{
	public class ClientAdapter : Client
	{
		private NetClient client;
		private NetPeerConfiguration clientConfig;

		public ClientAdapter (string apId)
		{
			clientConfig = new NetPeerConfiguration (apId);
			this.clientConfig.EnableMessageType (NetIncomingMessageType.Data);
			client = new NetClient (clientConfig);
			startClient ();
		}

        public override void sendMessage(NetworkMessage msg)
        {
            base.sendMessage(msg);
            NetOutgoingMessage outmsg = this.client.CreateMessage(msg.type + "," + msg.data);
            client.SendMessage(outmsg, NetDeliveryMethod.UnreliableSequenced);
        }

		private void startClient()
		{
			client.Start ();
		}

		public override void registerReceivedDataCallBack ()
		{
			base.registerReceivedDataCallBack ();
			this.client.RegisterReceivedCallback (new System.Threading.SendOrPostCallback (receivedData));
		}

		private void receivedData(object state)
		{
			NetIncomingMessage incmsg;

			while ((incmsg = client.ReadMessage ()) != null) {

				switch (incmsg.MessageType) {
				case NetIncomingMessageType.Data:
                        this.handleMessage(incmsg.ReadString());
					break;
				}
			}
		}

		public override void connect ()
		{
			base.connect ();
			client.Connect (this.IpToConnectTo, this.PortToConnectTo);
		}
	}
}

