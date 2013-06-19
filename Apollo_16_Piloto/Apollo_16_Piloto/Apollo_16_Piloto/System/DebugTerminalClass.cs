using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

//using DebugTerminal;
using Microsoft.Xna.Framework.Input;

namespace Apollo_16_Piloto
{
    public class DebugTerminalClass : DrawableGameComponent
    {
        protected SystemClass systemRef;
        protected SpriteFont spriteFont;
        protected ContentManager content;

        public DebugTerminalClass(Game game)
            : base(game)
        {
            systemRef = (SystemClass)game;

            content = new ContentManager(systemRef.Content.ServiceProvider, systemRef.Content.RootDirectory);
            
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts/DebugTerminal");
            //Terminal.Init(systemRef, systemRef.spriteBatch, spriteFont, systemRef.GraphicsDevice);
            //Terminal.SetSkin(TerminalThemeType.FIRE);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            //Terminal.CheckOpen(Keys.P, Keyboard.GetState());
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //Terminal.CheckDraw(false);
            base.Draw(gameTime);
        }
    }
}
