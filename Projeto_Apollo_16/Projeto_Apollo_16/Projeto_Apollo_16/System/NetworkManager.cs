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
        INPUT_DATA,
        RADAR_DATA,
        RADAR_DATA_IMMEDIATE,
    }

    enum ConnectionID
    {
        PILOT,
        RADAR
    }

    public class NetworkManager
    {
        protected SystemClass systemRef; /* This is a reference to our SystemClass*/
        TimeSpan updateRadar = TimeSpan.Zero;

        private int PORT = 14242;
        private int MAX_CONNECTIONS = 5;
        private String NETWORK_NAME = "apollo";

        private NetConnection pilotConnection = null;
        private NetConnection radarConnection = null;

        public String status = "";

        private NetServer networkServer;
        private NetPeerConfiguration networkConfig;


        public NetworkManager(Game game)
        {
            systemRef = (SystemClass)game;
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
            networkConfig.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            //networkConfig.EnableMessageType(NetIncomingMessageType.ErrorMessage);
            //networkConfig.EnableMessageType(NetIncomingMessageType.Data);

            networkServer = new NetServer(networkConfig);

            networkServer.Start();
        }

        public void HandleConnectionPackets(NetIncomingMessage msg)
        {
            
            switch (msg.ReadByte())
            {
                case (byte)PacketTypes.LOGIN:
                    switch(msg.ReadByte())
                    {
                        case (byte)ConnectionID.PILOT:
                            status = "O Piloto conectou-se...";
                            pilotConnection = msg.SenderConnection;
                            pilotConnection.Approve();
                            break;

                        case (byte)ConnectionID.RADAR:
                            status = "O Radar conectou-se...";
                            radarConnection = msg.SenderConnection;
                            radarConnection.Approve();
                            break;
                    }
                    break;
            }
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
                        HandleConnectionPackets(msg);
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

                    case NetIncomingMessageType.DiscoveryRequest:
                        // Create a response and write some example data to it
                        NetOutgoingMessage response = networkServer.CreateMessage();

                        // Send the response to the sender of the request
                        networkServer.SendDiscoveryResponse(response, msg.SenderEndPoint);
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
                    case NetIncomingMessageType.ConnectionApproval:
                        HandleConnectionPackets(msg);
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

                    case NetIncomingMessageType.DiscoveryRequest:
                        // Create a response and write some example data to it
                        NetOutgoingMessage response = networkServer.CreateMessage();

                        // Send the response to the sender of the request
                        networkServer.SendDiscoveryResponse(response, msg.SenderEndPoint);
                        break;

                    /* RECEIVE DATA MESSAGES */
                    case NetIncomingMessageType.Data:
                        switch (msg.ReadByte())
                        {
                            case (byte)PacketTypes.INPUT_DATA:
                                var input = new InputDataClass();
                                input.Decode(msg);
                                systemRef.gamePlayScreen.player.ParseInput(input);
                                break;
                        }
                        break;
                    default:
                        //status = "Unhandled type: " + msg.MessageType;
                        break;
                }
                networkServer.Recycle(msg);

            }
        }

        public void SendPackets(GameTime gameTime, PilotDataClass pilotData, RadarDataClass radarData, RadarDataImmediate radarImmediateData)
        {
            updateRadar += gameTime.ElapsedGameTime;

            if (pilotConnection != null)
            {
                NetOutgoingMessage pilotmsg = networkServer.CreateMessage();
                pilotmsg.Write((byte)PacketTypes.PILOT_DATA);
                pilotData.EncodePilotData(pilotmsg);
                networkServer.SendMessage(pilotmsg, pilotConnection, NetDeliveryMethod.ReliableOrdered);
            }
            
            if (radarConnection != null && updateRadar > TimeSpan.FromMilliseconds(300))
            {
                NetOutgoingMessage radarmsg = networkServer.CreateMessage();
                radarmsg.Write((byte)PacketTypes.RADAR_DATA);
                radarData.EncodeRadarData(radarmsg);
                networkServer.SendMessage(radarmsg, radarConnection, NetDeliveryMethod.ReliableOrdered);
                updateRadar = TimeSpan.Zero;
            }
            
            if (radarConnection != null)
            {
                NetOutgoingMessage radarmsg = networkServer.CreateMessage();
                radarmsg.Write((byte)PacketTypes.RADAR_DATA_IMMEDIATE);
                radarImmediateData.EncodeRadarImmediateData(radarmsg);
                networkServer.SendMessage(radarmsg, radarConnection, NetDeliveryMethod.ReliableOrdered);
            }

        }
    }
}
