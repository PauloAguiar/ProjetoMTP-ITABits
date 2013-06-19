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
        PILOT_DATA,
        INPUT_DATA
    }

    enum ConnectionID
    {
        PILOT,
    }

    public class NetworkManager
    {
        protected SystemClass systemRef; /* This is a reference to our SystemClass*/

        const String IP = "127.0.0.1";
        const int PORT = 14242;
        const int MAX_CONNECTIONS = 5;
        const String NETWORK_NAME = "apollo";
        public Boolean connected = false;

        public String status = "";

        NetClient networkClient;
        NetPeerConfiguration networkConfig;

        public NetworkManager(Game game)
        {
            systemRef = (SystemClass)game;
        }

        public void ConnectToServer()
        {
            // Create new instance of configs. Parameter is "application Id". It has to be same on client and server.
            networkConfig = new NetPeerConfiguration(NETWORK_NAME);
            networkConfig.AutoFlushSendQueue = false;

            //networkConfig.EnableMessageType(NetIncomingMessageType.WarningMessage);
            //networkConfig.EnableMessageType(NetIncomingMessageType.VerboseDebugMessage);
            //networkConfig.EnableMessageType(NetIncomingMessageType.ErrorMessage);
            //networkConfig.EnableMessageType(NetIncomingMessageType.Error);
            //networkConfig.EnableMessageType(NetIncomingMessageType.DebugMessage);
            networkConfig.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            //networkConfig.EnableMessageType(NetIncomingMessageType.Data);

            // Create new server based on the configs just defined
            networkClient = new NetClient(networkConfig);

            status = "networkClient Created!";

            networkClient.Start();

            status = "Client Started!";

            NetOutgoingMessage outmsg = networkClient.CreateMessage();
            outmsg.Write((byte)PacketTypes.LOGIN);
            outmsg.Write((byte)ConnectionID.PILOT);

            networkClient.Connect(IP, PORT, outmsg);

            status = "Connection Requested!";
        }

        public void DiscoverServer()
        {
            //networkClient.DiscoverLocalServers(PORT);
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
                        //status = msg.ReadString();
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
                        status = st.ToString() + ": " + reason;

                        break;
                    case NetIncomingMessageType.Data:
                        switch (msg.ReadByte())
                        {
                            case (byte)PacketTypes.PILOT_DATA:
                                systemRef.networkScreen.pilot.HandlePilotData(msg);
                                break;
                        }
                        break;

                    default:
                        status = "Unexpected Message of type " + msg.MessageType;
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
