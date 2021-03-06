﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Projeto_Apollo_16
{
    public class MultipleExplosion : ExplosionClass
    {
        const float MAX_VOLUME = 0.4f;
        const float MIN_VOLUME = 0.0f;

        int i, j;
        const double DELAY = 0.005;
        const int RANGE = 200; // o ideal é que fosse circular
        double time;
        int x;
        int dx; //deslocamento aleatório do centro da explosão em x
        int dy; //deslocamento aleatório do centro da explosão em y
        Random randNum = new Random();
        public MultipleExplosion(Vector2 playerPosition, Vector2 enemyPosition, ContentManager content)
            : base(playerPosition, enemyPosition, content)
        {
            lifeTime = 200;
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
            sound = content.Load<SoundEffect>(@"Sounds\explosion");
        }

        float GetVolume()
        {
            volume = 1 / distance * 1000;
            volume = MathHelper.Clamp(volume, MIN_VOLUME, MAX_VOLUME);
            return volume;
        }

        public override void Update(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
            
            if (x == 16)
            {
                dx = randNum.Next(-RANGE, RANGE);
                dy = randNum.Next(-RANGE, RANGE);
                x = 1;
                i = j = 0;
            }

            if (x == 1)
                sound.Play(GetVolume(), 0, 0);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle r = new Rectangle(i, j, 64, 64);
            spriteBatch.Draw(texture, globalPosition + new Vector2(dx,dy) , r, Color.White);
            if (time > DELAY * x)
            {
                i = (i + 64) % 320;
                j = (x / 4) * 64;
                x++;
            }
        }
    }
}
