using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Net;
using Lidgren.Network;

namespace Apollo_16_Radar
{
    public class NetworkManager
    {
        /* This is a reference to our SystemClass*/
        protected SystemClass systemRef; 

        /* Network Object */
        NetClient networkClient;

        private IPEndPoint serverIP;
        
        public NetworkManager(Game game)
        {
            systemRef = (SystemClass)game;

            NetPeerConfiguration networkConfig;
            // Create new instance of configs. Parameter is "application Id". It has to be same on client and server.
            networkConfig = new NetPeerConfiguration(Global.NETWORK_NAME);
            networkConfig.AutoFlushSendQueue = false;
            networkConfig.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);
            networkConfig.EnableMessageType(NetIncomingMessageType.ConnectionApproval);

            // Create new server based on the configs just defined
            networkClient = new NetClient(networkConfig);

            General.Log("NetworkClient Created!");

            networkClient.Start();
            General.Log("NetworkClient Started!");
        }

        public void DiscoverServer()
        {
            networkClient.DiscoverLocalPeers(Global.PORT);
        }

        public void ConnectToServer()
        {
            NetOutgoingMessage outmsg = networkClient.CreateMessage();
            outmsg.Write((byte)PacketTypes.LOGIN);
            outmsg.Write((byte)ConnectionID.RADAR);

            networkClient.Connect(serverIP, outmsg);

            General.Log("Connection Requested!");
        }

        public void ReadLobbyPackets()
        {
            NetIncomingMessage msg;

            while ((msg = networkClient.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    /* RECEIVE ERROR MESSAGES */
                    case NetIncomingMessageType.ErrorMessage:

                    /* RECEIVE VERBOSE DEBUG MESSAGES */
                    //case NetIncomingMessageType.VerboseDebugMessage:

                    /* RECEIVE DEBUG MESSAGES */
                    case NetIncomingMessageType.DebugMessage:

                    /* RECEIVE WARNING MESSAGES */
                    case NetIncomingMessageType.WarningMessage:
                        General.Log(msg.ReadString());
                        break;

                    /* RECEIVE STATUS CHANGE MESSAGES */
                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus st = (NetConnectionStatus)msg.ReadByte();
                        switch (st)
                        {
                            case NetConnectionStatus.Connected:
                                systemRef.stateManager.ChangeState(systemRef.gamePlayScreen);
                                break;
                            case NetConnectionStatus.RespondedAwaitingApproval:
                                msg.SenderConnection.Approve();
                                break;
                            default:
                                string reason = msg.ReadString();
                                General.Log(st.ToString() + ": " + reason);
                                break;
                        }
                        break;

                    case NetIncomingMessageType.DiscoveryResponse:
                        General.Log("Server is Online!");
                        serverIP = msg.SenderEndPoint;
                        systemRef.networkScreen.networkStatus = true;
                        break;

                    case NetIncomingMessageType.Data:
                        switch (msg.ReadByte())
                        {
                            case (byte)PacketTypes.CONNECTION_ACCEPTED:
                                General.Log("Connection Accepted!");
                                systemRef.stateManager.ChangeState(systemRef.gamePlayScreen);
                                break;
                        }
                        break;

                    
                    default:
                        General.Log("Unexpected Message of type " + msg.MessageType);
                        break;
                }
                networkClient.Recycle(msg);
            }
        }
        public void ReadInGamePackets()
        {
            NetIncomingMessage msg;

            while ((msg = networkClient.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    /* RECEIVE ERROR MESSAGES */
                    case NetIncomingMessageType.ErrorMessage:

                    /* RECEIVE VERBOSE DEBUG MESSAGES */
                    //case NetIncomingMessageType.VerboseDebugMessage:

                    /* RECEIVE DEBUG MESSAGES */
                    case NetIncomingMessageType.DebugMessage:

                    /* RECEIVE WARNING MESSAGES */
                    case NetIncomingMessageType.WarningMessage:
                        General.Log(msg.ReadString());
                        break;

                    /* RECEIVE STATUS CHANGE MESSAGES */
                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus st = (NetConnectionStatus)msg.ReadByte();
                        string reason = msg.ReadString();
                        General.Log(st.ToString() + ": " + reason);

                        break;

                    case NetIncomingMessageType.Data:
                        switch (msg.ReadByte())
                        {
                            case (byte)PacketTypes.RADAR_DATA:
                                systemRef.gamePlayScreen.data.DecodeRadarData(msg);
                                General.Log("Radar Updated!");
                                break;
                        }
                        break;

                    default:
                        General.Log("Unexpected Message of type " + msg.MessageType);
                        break;
                }
                networkClient.Recycle(msg);
            }
        }
    }
}
