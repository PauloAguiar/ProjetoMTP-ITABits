using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    /* This class will handle the gameStates and use events to send messages to the listeners gameStates*
     * Author: Paulo Henrique
     * Created 21/03/2013
     */

    public class GameStateManager : GameComponent
    {
        /* Fields */
        public event EventHandler OnStateChange;

        Stack<GameState> gameStates = new Stack<GameState>();
        const int START_DRAW_ORDER = 5000;
        const int DRAW_ORDER_INC = 100;
        int drawOrder;

        /* Getters and Setters */
        public GameState CurrentState
        {
            get { return gameStates.Peek(); }
        }

        /* Constructor */
        public GameStateManager(Game game) 
            : base(game)
        {
            drawOrder = START_DRAW_ORDER;
        }

        /* Override methods from gameCOmponent, so they will be called each loop */
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /* Class Methods */
        public void PopState()
        {
            if (gameStates.Count > 0)
            {
                RemoveState();
                drawOrder -= DRAW_ORDER_INC;

                if (OnStateChange != null)
                {
                    OnStateChange(this, null);
                }
            }
        }

        private void RemoveState()
        {
            GameState State = gameStates.Peek();

            OnStateChange -= State.StateChange;
            Game.Components.Remove(State);
            gameStates.Pop();
        }

        public void PushState(GameState newState)
        {
            drawOrder += DRAW_ORDER_INC;
            newState.DrawOrder = drawOrder;

            AddState(newState);

            if (OnStateChange != null)
            {
                OnStateChange(this, null);
            }
        }

        private void AddState(GameState newState)
        {
            gameStates.Push(newState);

            Game.Components.Add(newState);

            OnStateChange += newState.StateChange;
        }

        public void ChangeState(GameState newState)
        {
            while (gameStates.Count > 0)
            {
                RemoveState();
            }

            newState.DrawOrder = START_DRAW_ORDER;
            drawOrder = START_DRAW_ORDER;

            AddState(newState);

            if (OnStateChange != null)
                OnStateChange(this, null);
        }

    }
}
