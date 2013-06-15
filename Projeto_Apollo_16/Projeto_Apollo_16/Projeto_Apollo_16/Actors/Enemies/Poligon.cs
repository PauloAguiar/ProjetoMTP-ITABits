using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public sealed class Poligon : EnemyClass
    {
        private const float speed = 0.7f;
        private Vector2 centralPosition;
        private const int side = 300;
        private List<Vector2> Vertex = new List<Vector2>(4);// { new Vector2(side, -side), new Vector2(-side) , new Vector2(-side, side) , new Vector2(side)};
        private const int sides = 4;
        private int i = 0;

        public Poligon(Vector2 position, ContentManager content) : base(position, content)
        {
            centralPosition = position;
            globalPosition = position + new Vector2(side, -side);
            
            Vertex.Add(new Vector2(side, -side) + centralPosition);
            Vertex.Add(new Vector2(-side) + centralPosition);
            Vertex.Add(new Vector2(-side, side) + centralPosition);
            Vertex.Add(new Vector2(side) + centralPosition);
            
            /*
            Vertex.Add(new Vector2(side, -side));
            Vertex.Add(new Vector2(-side));
            Vertex.Add(new Vector2(-side, side));
            Vertex.Add(new Vector2(side));
            for (int i = 0; i < sides; i++)
            {
                Vector2 v = Vertex.ElementAt(i) + centralPosition; 
                Vertex.Add(v);
            }
            for (int i = 0; i < sides; i++)
            {
                Vertex.RemoveAt(i);
            }
            */
        }


        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\Enemies\porygon");
        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }

        public override void LoadSound(ContentManager content)
        {
            //throw new global::System.NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            Vector2 d = Vertex.ElementAt((i + 1)%sides) - Vertex.ElementAt(i);
            Vector2 v = d;
            v.Normalize();
            v *= speed;

            globalPosition += v*(float)dt;

            if ((globalPosition - Vertex.ElementAt(i)).Length() > d.Length())
            {
                i = (i + 1) % sides; 
                globalPosition = Vertex.ElementAt(i);
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, globalPosition, texture.Bounds, Color.White, 0, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.ENEMY_LAYER);
            
            spriteBatch.DrawString(spriteFont, "Pos:" + globalPosition.ToString(), globalPosition - new Vector2((texture.Width / 2) - 1, (texture.Height / 2) - 1), Color.Red);
        }
    }
}
