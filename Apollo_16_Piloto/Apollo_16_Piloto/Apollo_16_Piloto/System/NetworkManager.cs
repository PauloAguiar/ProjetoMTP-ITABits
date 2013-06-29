using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using System.Net;

namespace Apollo_16_Piloto
{
    public class NetworkManager
    {
        /* This is a reference to our SystemClass*/
        protected SystemClass systemRef;
        protected IPEndPoint serverIP;
        /* Network Object */
        NetClient networkClient;
        
        public NetworkManager(Game game)
        {
            systemRef = (SystemClass)game;

            NetPeerConfiguration networkConfig;
            // Create new instance of configs. Parameter is "application Id". It has to be same on client and server.
            networkConfig = new NetPeerConfiguration(Global.NETWORK_NAME);
            networkConfig.AutoFlushSendQueue = false;
            networkConfig.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);

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
            networkClient.Start();

            NetOutgoingMessage outmsg = networkClient.CreateMessage();
            outmsg.Write((byte)PacketTypes.LOGIN);
            outmsg.Write((byte)ConnectionID.PILOT);

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
                        /*switch ((NetConnectionStatus)msg.ReadByte())
                        {
                            case NetConnectionStatus.Connected:
                                status = "Connected: " + msg.SenderEndPoint;
                                break;
                            case NetConnectionStatus.Disconnected:
                                status = "Disconnected" + msg.SenderEndPoint;
                                break;
                            case NetConnectionStatus.RespondedAwaitingApproval:
                                msg.SenderConnection.Approve();
                                break;
                        }*/
                        NetConnectionStatus st = (NetConnectionStatus)msg.ReadByte();
                        string reason = msg.ReadString();
                        General.Log(st.ToString() + ": " + reason);

                        break;

                    case NetIncomingMessageType.DiscoveryResponse:
                        General.Log("Server is Online!");
                        systemRef.networkScreen.networkStatus = true;
                        serverIP = msg.SenderEndPoint;
                        break;

                    case NetIncomingMessageType.Data:
                        switch (msg.ReadByte())
                        {
                            case (byte)PacketTypes.CONNECTION_ACCEPTED:
                                General.Log("Connection Accepted!");
                                break;
                            case (byte)PacketTypes.PILOT_DATA:
                                //systemRef.networkScreen.pilot.HandlePilotData(msg);
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

        public void SendPackets(InputDataClass inputData)
        {
            if (networkClient.ServerConnection != null)
            {
                NetOutgoingMessage inputmsg = networkClient.CreateMessage();
                inputmsg.Write((byte)PacketTypes.INPUT_DATA);
                inputData.Encode(inputmsg);
                networkClient.SendMessage(inputmsg, NetDeliveryMethod.ReliableOrdered);
                networkClient.FlushSendQueue();
            }
        }
    }
}
