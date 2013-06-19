﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Projeto_Apollo_16
{
    public class MultipleExplosion : ExplosionClass
    {
        int i, j;
        const double delay = 0.005;
        const int range = 200; // o ideal é que fosse circular
        double time;
        int x;
        int dx; //deslocamento aleatório do centro da explosão em x
        int dy; //deslocamento aleatório do centro da explosão em y
        Random randNum = new Random();
        public MultipleExplosion(Vector2 position, ContentManager content)
            : base(position, content)
        {
            Lifetime = 200;
            i = j = 0;
            time = 0;
            x = 16;
        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\Explosions\multiple");
        }


        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");

        }
        
        public override void LoadSound(ContentManager content)
        { 
            sound = content.Load<SoundEffect>(@"Sounds\BUM");
        }

        public override void Update(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
            
            if (x == 16)
            {
                dx = randNum.Next(-range, range);
                dy = randNum.Next(-range, range);
                x = 1;
                i = j = 0;
            }

            if (x == 1)
                sound.Play(0.5f, 1.0f, 1.0f);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle r = new Rectangle(i, j, 64, 64);
            spriteBatch.Draw(texture, globalPosition + new Vector2(dx,dy) , r, Color.White);
            if (time > delay * x)
            {
                i = (i + 64) % 320;
                j = (x / 4) * 64;
                x++;
            }
        }
    }
}
