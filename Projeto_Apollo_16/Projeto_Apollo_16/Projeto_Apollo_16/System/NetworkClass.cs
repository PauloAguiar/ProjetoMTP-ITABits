using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    enum PacketTypes
    {
        CONNECTION_ACCEPTED,
        LOGIN,
    }

    public static class NetworkClass
    {
        const int PORT = 14242;
        const int MAX_CONNECTIONS = 5;
        const String NETWORK_NAME = "apollo";
        static Boolean isOn = false;

        static public String status = "";

        static NetServer networkServer;
        static NetPeerConfiguration networkConfig;

        static NetworkClass()
        {
            // Create new instance of configs. Parameter is "application Id". It has to be same on client and server.
            networkConfig = new NetPeerConfiguration(NETWORK_NAME);
            networkConfig.Port = PORT;
            networkConfig.MaximumConnections = MAX_CONNECTIONS;
            networkConfig.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            networkConfig.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            networkConfig.EnableMessageType(NetIncomingMessageType.ErrorMessage);

            // Create new server based on the configs just defined
            networkServer = new NetServer(networkConfig);
        }

        static public void StartServer()
        {
             networkServer.Start();
             isOn = true;
        }

        static public NetServer GetServer()
        {
            return networkServer;
        }

        static public String GetStatus()
        {
            if (isOn)
                return "Online! Conexoes: " + networkServer.ConnectionsCount.ToString();
            else
                return "Offline!";
        }

        static public void ReadPackets()
        {
            NetIncomingMessage msg;
            while ((msg = networkServer.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.ConnectionApproval:
                        if (msg.ReadByte() == (byte)PacketTypes.LOGIN)
                        {
                            status = "Incoming LOGIN...";
                            msg.SenderConnection.Approve();
                            NetOutgoingMessage outmsg = networkServer.CreateMessage();
                            outmsg.Write((byte)PacketTypes.CONNECTION_ACCEPTED);
                            networkServer.SendMessage(outmsg, msg.SenderConnection, NetDeliveryMethod.ReliableOrdered);
                        }
                        break;
                    case NetIncomingMessageType.ErrorMessage:
                        status = msg.ReadString();
                        break;
                    case NetIncomingMessageType.Data:
                        status = "Data: " + msg.Data;
                        break;
                    default:
                        status = "Unhandled type: " + msg.MessageType;
                        break;
                }
                networkServer.Recycle(msg);

            }
        }

    }
}
