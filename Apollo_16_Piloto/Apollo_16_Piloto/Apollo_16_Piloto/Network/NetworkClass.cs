using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace Apollo_16_Piloto
{
    enum PacketTypes
    {
        CONNECTION_ACCEPTED,
        ID_PACKET,
        LOGIN,
        PILOT_DATA
    }

    enum ConnectionID
    {
        PILOTO,
    }

    public static class NetworkClass
    {
        const String IP = "127.0.0.1";
        const int PORT = 14242;
        const int MAX_CONNECTIONS = 5;
        const String NETWORK_NAME = "apollo";
        static public Boolean connected = false;

        static public String status = "";

        static NetClient networkClient;
        static NetPeerConfiguration networkConfig;

        static NetworkClass()
        {
            // Create new instance of configs. Parameter is "application Id". It has to be same on client and server.
            networkConfig = new NetPeerConfiguration(NETWORK_NAME);

            networkConfig.EnableMessageType(NetIncomingMessageType.WarningMessage);
            networkConfig.EnableMessageType(NetIncomingMessageType.VerboseDebugMessage);
            networkConfig.EnableMessageType(NetIncomingMessageType.ErrorMessage);
            networkConfig.EnableMessageType(NetIncomingMessageType.Error);
            networkConfig.EnableMessageType(NetIncomingMessageType.DebugMessage);
            networkConfig.EnableMessageType(NetIncomingMessageType.ConnectionApproval);

            // Create new server based on the configs just defined
            networkClient = new NetClient(networkConfig);

            status = "NetworkClient Created!";
        }

        static public void StartClient()
        {
             networkClient.Start();
             status = "Client Started!";
        }

        static public void ConnectToServer(String serverIP)
        {
            NetOutgoingMessage outmsg = networkClient.CreateMessage();
            outmsg.Write((byte)PacketTypes.LOGIN);
            outmsg.Write((byte)ConnectionID.PILOTO);
            networkClient.Connect(IP, PORT, outmsg);
            status = "Connection Requested!";
        }

        static public void DiscoverServer()
        {
            //networkClient.DiscoverLocalServers(PORT);
        }

        static public void ReadPackets()
        {
            NetIncomingMessage msg;

            while ((msg = networkClient.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.ErrorMessage:
                        status = msg.ReadString();
                        break;
                    case NetIncomingMessageType.DebugMessage:
                        status = msg.ReadString();
                        break;
                    case NetIncomingMessageType.WarningMessage:
                        status = msg.ReadString();
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        switch ((NetConnectionStatus)msg.ReadByte())
                        {
                            case NetConnectionStatus.Connected:
                                status = "{0} Connected " + msg.SenderEndPoint;
                                connected = true;
                                break;
                            case NetConnectionStatus.Disconnected:
                                status = "{0} Disconnected " + msg.SenderEndPoint;
                                connected = false;
                                break;
                            case NetConnectionStatus.RespondedAwaitingApproval:
                                msg.SenderConnection.Approve();
                                break;
                        }
                        break;
                    case NetIncomingMessageType.Data:
                        switch (msg.ReadByte())
                        {
                            case (byte)PacketTypes.PILOT_DATA:
                                status = "Data:" + msg.ReadString();
                                break;
                        }
                        break;
                    default:
                        status = "Unhandled type: " + msg.MessageType;
                        break;
                }

                networkClient.Recycle(msg);
            }
        }
    }
}
