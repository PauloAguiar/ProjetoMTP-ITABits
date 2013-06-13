using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Lidgren.Network;

namespace Apollo.Pilot
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
    enum PacketTypes
    {
        CONNECTION_ACCEPTED,
        LOGIN,
    }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        string hostip = "127.0.0.1";
        string stringTeste = "Teste";

        static NetClient Client;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Create new instance of configs. Parameter is "application Id". It has to be same on client and server.
            NetPeerConfiguration networkConfig = new NetPeerConfiguration("apollo");

            networkConfig.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);
            networkConfig.EnableMessageType(NetIncomingMessageType.Data);

            Client = new NetClient(networkConfig);

            // Create new outgoing message
            NetOutgoingMessage outmsg = Client.CreateMessage();

            // Start client
            Client.Start();

            // Write byte ( first byte informs server about the message type ) ( This way we know, what kind of variables to read )
            outmsg.Write((byte)PacketTypes.LOGIN);

            try
            {
                // Connect client, to ip previously requested from user 
                Client.Connect(hostip, 14242, outmsg);
                stringTeste = "Conectado";

            }
            catch
            {
                stringTeste = "Erro";
            }   
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            
            base.Initialize();


        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>(@"ActorInfo");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            NetIncomingMessage inc;

            if ((inc = Client.ReadMessage()) != null)
            {
                // Switch based on the message types
                switch (inc.MessageType)
                {

                    // All manually sent messages are type of "Data"
                    case NetIncomingMessageType.Data:

                        // Read the first byte
                        // This way we can separate packets from each others
                        if (inc.ReadByte() == (byte)PacketTypes.CONNECTION_ACCEPTED)
                        {
                            stringTeste = inc.ReadString() + "Bizu";
                        }
                        break;

                    default:
                        // Should not happen and if happens, don't care
                        stringTeste = "Unhandled type: " + inc.MessageType;
                        break;
                }
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.DrawString(font, stringTeste, Vector2.Zero, Color.Black);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
