using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public sealed class Poligon : EnemyClass
    {
        private float speed = 0.7f;
        private Vector2 centralPosition;
        private int side;
        private List<Vector2> Vertex = new List<Vector2>(4);
        private const int SIDES = 4;
        private int vertex = 0;
        Random rand = new Random();

        public Poligon(Vector2 position, ContentManager content) : base(position, content)
        {
            centralPosition = position;
            vertex = GameLogic.rand.Next(SIDES);
            side = GameLogic.rand.Next(100, 500);
            speed = GameLogic.rand.Next(6, 20) / 10.0f;
            
            globalPosition = position + new Vector2(side, -side);

            Vertex.Add(new Vector2(side, -side) + centralPosition);
            Vertex.Add(new Vector2(-side) + centralPosition);
            Vertex.Add(new Vector2(-side, side) + centralPosition);
            Vertex.Add(new Vector2(side) + centralPosition);
            
        }


        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\Enemies\porygon");
        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }

        public override void Destroy(Vector2 position, ContentManager content, ExplosionManager explosionManager)
        {
            SimpleExplosion e = new SimpleExplosion(position, content);
            explosionManager.createExplosion(e);
        }

        public override void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            Vector2 d = Vertex.ElementAt((vertex + 1)%SIDES) - Vertex.ElementAt(vertex);
            Vector2 v = d;
            v.Normalize();
            v *= speed;

            globalPosition += v*(float)dt;

            if ((globalPosition - Vertex.ElementAt(vertex)).Length() > d.Length())
            {
                vertex = (vertex + 1) % SIDES; 
                globalPosition = Vertex.ElementAt(vertex);
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, globalPosition, texture.Bounds, Color.White, 0, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.ENEMY_LAYER);
            
            spriteBatch.DrawString(spriteFont, "Pos:" + globalPosition.ToString(), globalPosition - new Vector2((texture.Width / 2) - 1, (texture.Height / 2) - 1), Color.Red);
        }
    }
}
