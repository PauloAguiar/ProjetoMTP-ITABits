using System;
using System.Collections.Generic;
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

    public enum ConnectionID
    {
        PILOT,
        RADAR
    }

    public class ClientConnection
    {
        public NetConnection connection;

        public ClientConnection(NetConnection conn)
        {
            this.connection = conn;
        }
    }

    public class NetworkManager
    {
        protected SystemClass systemRef;
        TimeSpan updateRadar = TimeSpan.Zero;

        private int PORT = 14242;
        private int MAX_CONNECTIONS = 5;
        private String NETWORK_NAME = "apollo";

        private Dictionary<ConnectionID, ClientConnection> connections = new Dictionary<ConnectionID, ClientConnection>();

        private NetServer networkServer;
        private NetPeerConfiguration networkConfig;


        public NetworkManager(Game game)
        {
            systemRef = (SystemClass)game;
        }

        public void StartServer()
        {
            networkConfig = new NetPeerConfiguration(NETWORK_NAME);
            networkConfig.Port = PORT;
            networkConfig.MaximumConnections = MAX_CONNECTIONS;

            networkConfig.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            networkConfig.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);

            networkServer = new NetServer(networkConfig);

            networkServer.Start();
        }

        public void StopServer()
        {
            networkServer.Shutdown("Server Manually Shutdown");
            General.Log("Server Manually Shutdown");
        }

        public void HandleConnectionPackets(NetIncomingMessage msg)
        {
            
            switch (msg.ReadByte())
            {
                case (byte)PacketTypes.LOGIN:
                    switch(msg.ReadByte())
                    {
                        case (byte)ConnectionID.PILOT:
                            General.Log("O Piloto conectou-se...");
                            AddConnectionByID(ConnectionID.PILOT, msg.SenderConnection);
                            GetConnectionByID(ConnectionID.PILOT).Approve();
                            break;

                        case (byte)ConnectionID.RADAR:
                            General.Log("O Radar conectou-se...");
                            AddConnectionByID(ConnectionID.RADAR, msg.SenderConnection);
                            GetConnectionByID(ConnectionID.RADAR).Approve();
                            break;
                    }
                    break;
            }
        }

        private void HandleStatusChangePackets(NetIncomingMessage msg)
        {
            NetConnectionStatus st = (NetConnectionStatus)msg.ReadByte();
            switch (st)
            {
                case NetConnectionStatus.Connected:
                    General.Log("Connected to " + msg.SenderEndPoint + " (MAC: " + NetUtility.ToHexString(msg.SenderConnection.RemoteUniqueIdentifier) + ")");
                    break;
                case NetConnectionStatus.Disconnected:
                    General.Log("Disconnected from " + msg.SenderEndPoint);
                    break;
                case NetConnectionStatus.Disconnecting:
                    General.Log("Disconnecting from " + msg.SenderEndPoint);
                    break;
                case NetConnectionStatus.RespondedAwaitingApproval:
                    General.Log("Responded awaiting approval to " + msg.SenderEndPoint);
                    break;
                case NetConnectionStatus.RespondedConnect:
                    General.Log("Responded connect to " + msg.SenderEndPoint);
                    break;
                default:
                    General.Log("Unhandled status change packet");
                    string reason = msg.ReadString();
                    General.Log(st.ToString() + ": " + reason);
                    break;
            }
        }

        public void ReadPackets(GameState state)
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
                        General.Log(msg.MessageType.ToString() + ": " +  msg.ReadString());
                        break;

                    /* RECEIVE STATUS CHANGE MESSAGES */
                    case NetIncomingMessageType.StatusChanged:
                        HandleStatusChangePackets(msg);
                        break;

                    case NetIncomingMessageType.DiscoveryRequest:
                        NetOutgoingMessage response = networkServer.CreateMessage();
                        networkServer.SendDiscoveryResponse(response, msg.SenderEndPoint);
                        General.Log("Discovery response sent to " + msg.SenderEndPoint);
                        break;

                    /* RECEIVE DATA MESSAGES */
                    case NetIncomingMessageType.Data:
                        if (state is GamePlayScreen)
                        {
                            switch (msg.ReadByte())
                            {
                                case (byte)PacketTypes.INPUT_DATA:
                                    var input = new InputDataClass();
                                    input.Decode(msg);
                                    systemRef.gamePlayScreen.player.ParseInput(input);
                                    break;
                            }
                        }
                        break;
                    default:
                        General.Log("Unexpected Message of type " + msg.MessageType);
                        break;
                }
            }
        }

        public void SendPackets(GameTime gameTime, PilotDataClass pilotData, RadarDataClass radarData, RadarDataImmediate radarImmediateData)
        {
            updateRadar += gameTime.ElapsedGameTime;

            if (GetConnectionStatudByID(ConnectionID.PILOT) == NetConnectionStatus.Connected)
            {
                NetOutgoingMessage pilotmsg = networkServer.CreateMessage();
                pilotmsg.Write((byte)PacketTypes.PILOT_DATA);
                pilotData.EncodePilotData(pilotmsg);
                networkServer.SendMessage(pilotmsg, GetConnectionByID(ConnectionID.PILOT), NetDeliveryMethod.ReliableOrdered);
            }
			
            if (GetConnectionStatudByID(ConnectionID.PILOT) == NetConnectionStatus.Connected && updateRadar > TimeSpan.FromMilliseconds(300))
            {
                NetOutgoingMessage radarmsg = networkServer.CreateMessage();
                radarmsg.Write((byte)PacketTypes.RADAR_DATA);
                radarData.EncodeRadarData(radarmsg);
                networkServer.SendMessage(radarmsg, GetConnectionByID(ConnectionID.RADAR), NetDeliveryMethod.ReliableOrdered);
                updateRadar = TimeSpan.Zero;
            }
            
            if (GetConnectionStatudByID(ConnectionID.PILOT) == NetConnectionStatus.Connected)
            {
                NetOutgoingMessage radarmsg = networkServer.CreateMessage();
                radarmsg.Write((byte)PacketTypes.RADAR_DATA_IMMEDIATE);
                radarImmediateData.EncodeRadarImmediateData(radarmsg);
                networkServer.SendMessage(radarmsg, radarConnection, NetDeliveryMethod.ReliableOrdered);
            }

        }

        private void AddConnectionByID(ConnectionID id, NetConnection conn)
        {
            General.Log("Connection Added at ID:" + (int)id);
            connections.Add(id, new ClientConnection(conn));
        }

        public NetConnectionStatus GetConnectionStatudByID(ConnectionID id)
        {
            NetConnection conn = GetConnectionByID(id);
            if (conn != null)
                return conn.Status;
            return NetConnectionStatus.None;
        }

        private NetConnection GetConnectionByID(ConnectionID id)
        {
            if(connections.ContainsKey(id) && connections[id] != null)
                return connections[id].connection;
            return null;
        }

        public NetPeerStatus GetStatus()
        {
            if(networkServer != null)
                return networkServer.Status;
            return NetPeerStatus.NotRunning;
        }
    }
}
