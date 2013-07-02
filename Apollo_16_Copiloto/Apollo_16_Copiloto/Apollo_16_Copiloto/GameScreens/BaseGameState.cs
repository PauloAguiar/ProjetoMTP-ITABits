using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Apollo_16_Copiloto
{
    /* BaseGameState inherits from GameState, so it can be handled by GameStateManager, and defines
     * common fields to all GameStates
     * Author: Paulo Henrique
     * Created 21/03/2013
     */
    public abstract class BaseGameState : GameState
    {
        /* Fields */
        protected SystemClass systemRef; /* This is a reference to our SystemClass, used to initialize our contentManager*/

        protected ControlManager controlManager;

        /* Each Game state will have its own gameContent */
        protected ContentManager content;

        public BaseGameState(Game game, GameStateManager manager) 
            : base(game, manager)
        {
            systemRef = (SystemClass)game;

            content = new ContentManager(systemRef.Content.ServiceProvider, systemRef.Content.RootDirectory);
        }
        
        /* Override inherited methods */
        protected override void LoadContent()
        {
            ContentManager Content = Game.Content;

            SpriteFont menuFont = Content.Load<SpriteFont>(@"ControlFont");
            controlManager = new ControlManager(menuFont);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

    }
}
