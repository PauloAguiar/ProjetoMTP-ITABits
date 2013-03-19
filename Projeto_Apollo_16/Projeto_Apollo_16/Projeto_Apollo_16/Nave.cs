using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projeto_Apollo_16
{
    class Nave
    {
        /////////////
        //fields/////
        /////////////
        //private float Angulo;
        private Vector2 position;
        private Vector2 velocity;
        private Texture2D textureNormal;
        private Texture2D textureRight;
        private enum Direcoes
        {
            normal, right, left 
        };
        private Direcoes direcao = Direcoes.normal;
        
        //////////
        //ctor////
        /////////
        public Nave(Vector2 posicao, Texture2D texturaNormal, Texture2D texturaDireita)
        {
            position = posicao;
            textureNormal = texturaNormal;
            textureRight = texturaDireita;
            direcao = Direcoes.normal;
        }

        ////////////
        //methods///
        ///////////
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public void Update(GameTime gameTime)
        {
            velocity = new Vector2(0);

            if(Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                velocity+=new Vector2(0,-1);
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                velocity+=new Vector2(0,1);
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity+=new Vector2(-1,0);
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity+=new Vector2(1,0);
            }

            if (velocity.X > 0)
            {
                direcao = Direcoes.right;
            }
            else if (velocity.X < 0)
            {
                direcao = Direcoes.left;
            }
            else
            {
                direcao = Direcoes.normal;
            }

            position += 2*velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (direcao == Direcoes.normal)
            {
                spriteBatch.Draw(textureNormal, position, Color.White);
            }
            else if (direcao == Direcoes.right)
            {
                spriteBatch.Draw(textureRight, position, Color.White);
            }
            else
            {
                spriteBatch.Draw(textureRight, position, null, Color.White, 0, new Vector2(0), 1, SpriteEffects.FlipHorizontally, 0);
            }
        }


    }
}
