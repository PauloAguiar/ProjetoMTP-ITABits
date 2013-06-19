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
        ID_PACKET,
        LOGIN,
        PILOT_DATA,
        INPUT_DATA
    }

    enum ConnectionID
    {
        PILOT,
    }

    public class NetworkManager
    {
        private int PORT = 14242;
        private int MAX_CONNECTIONS = 5;
        private String NETWORK_NAME = "apollo";

        private NetConnection pilotConnection = null;

        public String status = "";

        private NetServer networkServer;
        private NetPeerConfiguration networkConfig;


        public NetworkManager()
        {
        }

        public void StartServer()
        {
            // Create new instance of configs. Parameter is "application Id". It has to be same on client and server.
            networkConfig = new NetPeerConfiguration(NETWORK_NAME);
            networkConfig.Port = PORT;
            networkConfig.MaximumConnections = MAX_CONNECTIONS;

            //networkConfig.EnableMessageType(NetIncomingMessageType.WarningMessage);
            //networkConfig.EnableMessageType(NetIncomingMessageType.VerboseDebugMessage);
            //networkConfig.EnableMessageType(NetIncomingMessageType.Error);
            //networkConfig.EnableMessageType(NetIncomingMessageType.DebugMessage);
            networkConfig.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            //networkConfig.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            //networkConfig.EnableMessageType(NetIncomingMessageType.ErrorMessage);
            //networkConfig.EnableMessageType(NetIncomingMessageType.Data);

            networkServer = new NetServer(networkConfig);

            networkServer.Start();
        }

        public Boolean NetworkIsReady()
        {
            if (pilotConnection != null)
                return true;
            return false;
        }

        public void ReadLobbyPackets()
        {
            NetIncomingMessage msg;

            while ((msg = networkServer.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    /* RECEIVE CONNECTION REQUEST */
                    case NetIncomingMessageType.ConnectionApproval:
                        if (msg.ReadByte() == (byte)PacketTypes.LOGIN)
                        {
                            switch (msg.ReadByte())
                            {
                                case (byte)ConnectionID.PILOT:
                                    status = "O Piloto conectou-se...";
                                    pilotConnection = msg.SenderConnection;
                                    pilotConnection.Approve();
                                    NetOutgoingMessage outmsg = networkServer.CreateMessage();
                                    outmsg.Write((byte)PacketTypes.CONNECTION_ACCEPTED);
                                    networkServer.SendMessage(outmsg, pilotConnection, NetDeliveryMethod.ReliableOrdered);
                                    break;
                            }
                        }
                        break;

                    /* RECEIVE ERROR MESSAGES */
                    case NetIncomingMessageType.ErrorMessage:
                        
                    /* RECEIVE VERBOSE DEBUG MESSAGES */
                    case NetIncomingMessageType.VerboseDebugMessage:

                    /* RECEIVE DEBUG MESSAGES */
                    case NetIncomingMessageType.DebugMessage:
                        
                    /* RECEIVE WARNING MESSAGES */
                    case NetIncomingMessageType.WarningMessage:
                        //status = msg.ReadString();
                        break;

                    /* RECEIVE STATUS CHANGE MESSAGES */
                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus st = (NetConnectionStatus)msg.ReadByte();
						string reason = msg.ReadString();
						status = NetUtility.ToHexString(msg.SenderConnection.RemoteUniqueIdentifier) + " " + st + ": " + reason;
                        break;
                    default:
                        status = "Unexpected Message of type " + msg.MessageType;
                        break;
                }
            }
        }

        public void ReadInGamePackets()
        {
            NetIncomingMessage msg;

            while ((msg = networkServer.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    /* RECEIVE ERROR MESSAGES */
                    case NetIncomingMessageType.ErrorMessage:

                    /* RECEIVE VERBOSE DEBUG MESSAGES */
                    case NetIncomingMessageType.VerboseDebugMessage:

                    /* RECEIVE DEBUG MESSAGES */
                    case NetIncomingMessageType.DebugMessage:

                    /* RECEIVE WARNING MESSAGES */
                    case NetIncomingMessageType.WarningMessage:

                        //status = msg.ReadString();
                        break;

                    /* RECEIVE STATUS CHANGE MESSAGES */
                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus st = (NetConnectionStatus)msg.ReadByte();
						string reason = msg.ReadString();
						status = NetUtility.ToHexString(msg.SenderConnection.RemoteUniqueIdentifier) + " " + st + ": " + reason;
                        break;

                    /* RECEIVE DATA MESSAGES */
                    case NetIncomingMessageType.Data:
                        switch (msg.ReadByte())
                        {
                            case (byte)PacketTypes.INPUT_DATA:
                                if (msg.ReadBoolean())
                                    status = "T";
                                else
                                    status = "F";
                                break;
                        }
                        break;
                    default:
                        status = "Unhandled type: " + msg.MessageType;
                        break;
                }
                networkServer.Recycle(msg);

            }
        }

        public void SendPackets(PilotDataClass pilotData)
        {
            if (pilotConnection != null)
            {
                NetOutgoingMessage pilotmsg = networkServer.CreateMessage();
                pilotmsg.Write((byte)PacketTypes.PILOT_DATA);
                pilotData.EncodePilotData(pilotmsg);
                networkServer.SendMessage(pilotmsg, pilotConnection, NetDeliveryMethod.ReliableOrdered);
            }
        }
    }
}
