using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Apollo16.Model.GameStates;

namespace Apollo16.Model
{
    /* This class will handle the gameStates and use events to send messages to the listeners gameStates*
     * Author: Paulo Henrique
     * Created 21/03/2013
     */

    class GameStateManager : GameComponent
    {
        /* Fields */
        public event EventHandler OnStateChange;

        Stack<GameState> gameStates = new Stack<GameState>();

        /* Getters and Setters */
        public GameState CurrentState
        {
            get { return gameStates.Peek(); }
        }

        /* Constructor */
        public GameStateManager(Game game) 
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void PopState()
        {
            if (gameStates.Count > 0)
            {
                RemoveState();

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

            AddState(newState);

            if (OnStateChange != null)
                OnStateChange(this, null);
        }

    }
}
