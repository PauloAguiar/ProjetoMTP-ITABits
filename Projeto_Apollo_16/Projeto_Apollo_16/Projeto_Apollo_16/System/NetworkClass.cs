using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    public static class NetworkClass
    {
        const int PORT = 14242;
        const int MAX_CONNECTIONS = 5;
        const String NETWORK_NAME = "apollo";
        static Boolean isOn = false;

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

    }
}
