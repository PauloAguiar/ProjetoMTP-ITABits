using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Apollo16.Model.GameStates
{
    public abstract class GameState : DrawableGameComponent
    {
        List<GameComponent> childComponents;
        GameState actualGameState;
        GameStateManager stateManager;

        public List<GameComponent> Components
        {
            get { return childComponents; }
        }

        public GameState ActualGameState
        {
            get { return actualGameState; }
        }

        /* Constructor */
        public GameState(Game game, GameStateManager manager) 
            : base(game)
        {
            stateManager = manager;
            childComponents = new List<GameComponent>();
            actualGameState = this;
        }

        /* Override methods from DrawableGameComponent, that will be called each loop */
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent component in childComponents)
            {
                if (component.Enabled)
                {
                    component.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent drawComponent;

            foreach (GameComponent component in childComponents)
            {
                if (component is DrawableGameComponent)
                {
                    drawComponent = component as DrawableGameComponent;

                    if (drawComponent.Visible)
                    {
                        drawComponent.Draw(gameTime);
                    }
                }
            }

            base.Draw(gameTime);
        }

        /* Class Methods */
        public virtual void StateChange(object sender, EventArgs e)
        {
            if (stateManager.CurrentState == actualGameState)
                Show();
            else
                Hide();
        }

        protected virtual void Show()
        {
            Visible = true;
            Enabled = true;
            foreach (GameComponent component in childComponents)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = true;
            }
        }

        protected virtual void Hide()
        {
            Visible = false;
            Enabled = false;
            foreach (GameComponent component in childComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = false;
            }
        }
    }
}
