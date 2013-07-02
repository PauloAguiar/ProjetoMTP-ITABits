using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Net;
using Lidgren.Network;

namespace Apollo_16_Copiloto
{
    public class NetworkManager
    {
        /* This is a reference to our SystemClass*/
        protected SystemClass systemRef; 

        /* Network Object */
        NetClient networkClient;

        private IPEndPoint serverIP;
        private Boolean isOnline = false;

        public NetworkManager(Game game)
        {
            systemRef = (SystemClass)game;

            NetPeerConfiguration networkConfig;
            networkConfig = new NetPeerConfiguration(Globals.NETWORK_NAME);
            networkConfig.AutoFlushSendQueue = false;

            networkConfig.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);
            networkConfig.EnableMessageType(NetIncomingMessageType.ConnectionApproval);

            networkClient = new NetClient(networkConfig);

            General.Log("NetworkClient Created!");

            networkClient.Start();
            General.Log("NetworkClient Started!");
        }

        public void DiscoverServer()
        {
            networkClient.DiscoverLocalPeers(Globals.PORT);
        }

        public Boolean IsOnline()
        {
            return isOnline;
        }

        public void ConnectToServer()
        {
            NetOutgoingMessage outmsg = networkClient.CreateMessage();
            outmsg.Write((byte)ConnectionID.RADAR);
            networkClient.Connect(serverIP, outmsg);

            General.Log("Connection Requested to " + serverIP.ToString());
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
                    isOnline = false;
                    systemRef.stateManager.ChangeState(systemRef.initGameScreen);
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

            while ((msg = networkClient.ReadMessage()) != null)
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
                        General.Log(msg.MessageType.ToString() + ": " + msg.ReadString());
                        break;

                    /* RECEIVE STATUS CHANGE MESSAGES */
                    case NetIncomingMessageType.StatusChanged:
                        HandleStatusChangePackets(msg);
                        break;

                    case NetIncomingMessageType.DiscoveryResponse:
                        General.Log("Server exists at" + msg.SenderEndPoint);
                        serverIP = msg.SenderEndPoint;
                        isOnline = true;
                        break;

                    /* RECEIVE DATA MESSAGES */
                    case NetIncomingMessageType.Data:
                        if (state is GamePlayScreen)
                        {
                        }
                        break;
                    default:
                        General.Log("Unexpected Message of type " + msg.MessageType);
                        break;
                }
            }
        }
    }
}
