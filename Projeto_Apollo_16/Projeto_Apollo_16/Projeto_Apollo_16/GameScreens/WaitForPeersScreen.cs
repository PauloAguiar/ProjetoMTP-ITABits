using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Lidgren.Network;

namespace Projeto_Apollo_16
{
    public class WaitForPeersScreen : BaseGameState
    {
        /* Constructor */
        PictureBox backgroundImage;
        Label waitingMessage;
        Label status;

        DateTime time = DateTime.Now;
        TimeSpan timetopass = new TimeSpan(0, 0, 0, 0, 30);

        public WaitForPeersScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
        }

        /* XNA Methods */
        public override void Initialize()
        {
            base.Initialize();

            try
            {
                systemRef.StartServer();
                waitingMessage.Text = "Servidor Criado. Aguardando Conexao...";
            }
            catch
            {
                waitingMessage.Text = "Erro. Nao foi possivel criar o servidor...";
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            backgroundImage = new PictureBox(content.Load<Texture2D>(@"Menus\Backgrounds\mulher"),systemRef.screenRectangle);
            controlManager.Add(backgroundImage);

            waitingMessage = new Label();
            waitingMessage.Text = "Inicializando Servidor...";
            waitingMessage.Position = new Vector2(0, 0);

            controlManager.Add(waitingMessage);

            status = new Label();
            status.Text = "Status: Nao Conectado!";
            status.Position = new Vector2(0, 30);

            controlManager.Add(status);
            
        }

        public override void Update(GameTime gameTime)
        {
            NetIncomingMessage inc = null;
            status.Text = "Status: Incoming Data..." + systemRef.GetServer().ConnectionsCount;

            if ((inc = systemRef.GetServer().ReadMessage()) != null)
            {
                // Theres few different types of messages. To simplify this process, i left only 2 of em here
                switch (inc.MessageType)
                {
                    // If incoming message is Request for connection approval
                    // This is the very first packet/message that is sent from client
                    // Here you can do new player initialisation stuff
                    case NetIncomingMessageType.ConnectionApproval:

                        // Read the first byte of the packet
                        // ( Enums can be casted to bytes, so it be used to make bytes human readable )
                        if (inc.ReadByte() == (byte)PacketTypes.LOGIN)
                        {
                            // Approve clients connection ( Its sort of agreenment. "You can be my client and i will host you" )
                            inc.SenderConnection.Approve();
                            status.Text = "Status: Aceito...";

                            NetOutgoingMessage outmsg = systemRef.GetServer().CreateMessage();

                            outmsg.Write((byte)PacketTypes.WORLDSTATE);
                            systemRef.GetServer().SendMessage(outmsg, inc.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
                            status.Text = "Status: Conectado!";
                        }

                        break;
                    default:
                        // As i statet previously, theres few other kind of messages also, but i dont cover those in this example
                        // Uncommenting next line, informs you, when ever some other kind of message is received
                        //Console.WriteLine("Not Important Message");
                        break;
                }
            }

            controlManager.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            systemRef.spriteBatch.Begin();

            controlManager.Draw(systemRef.spriteBatch);

            systemRef.spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    // This is good way to handle different kind of packets
    // there has to be some way, to detect, what kind of packet/message is incoming.
    // With this, you can read message in correct order ( ie. Can't read int, if its string etc )

    // Best thing about this method is, that even if you change the order of the entrys in enum, the system won't break up
    // Enum can be casted ( converted ) to byte
    enum PacketTypes
    {
        LOGIN,
        WORLDSTATE
    }
}
