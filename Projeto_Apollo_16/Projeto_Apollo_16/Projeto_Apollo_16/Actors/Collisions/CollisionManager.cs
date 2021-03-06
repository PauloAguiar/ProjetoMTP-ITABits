﻿using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Projeto_Apollo_16
{
    public static class CollisionManager
    {
        public static EnemyClass FindNearest(ActorClass actor, EnemyManager list)
        {
            if (list.Count <= 0)
            {
                return null;
            }

            float d = (actor.GlobalPosition - list.ElementAt(0).GlobalPosition).Length();
            EnemyClass e = list.ElementAt(0);

            foreach (EnemyClass enemy in list)
            {
                if( (actor.GlobalPosition - enemy.GlobalPosition).Length() < d)
                {
                    d = (actor.GlobalPosition - enemy.GlobalPosition).Length();
                    e = enemy;
                }
            }

            return e;

        }

        public static bool CircularCollision(ActorClass a1, ActorClass a2)
        {
            Vector2 r = a2.GlobalPosition - a1.GlobalPosition;

            /*  //se globalPosition for o canto da texture
            float dx,dy;
            dx = a2.Texture.Width - a1.Texture.Width;
            dy = a2.Texture.Height - a1.Texture.Height;
            r.X += dx;
            r.Y += dy;
            */

            float r1, r2;
            r1 = 1/2.0f * Math.Max(a1.Texture.Width, a1.Texture.Height);
            r2 = 1/2.0f * Math.Max(a2.Texture.Width, a2.Texture.Height);


            if (r.Length() < r1 + r2)
            {
                return true;
            }

            return false;

        }


        public static bool CanCreateEnemy(Vector2 enemyPostion, PlayerClass player)
        {
            Vector2 r = enemyPostion - player.GlobalPosition;

            float r1, r2;
            r1 = Globals.ENEMY_RADIUS;
            r2 = Globals.SAFE_RADIUS;


            if (r.Length() < r1 + r2)
            {
                return true;
            }

            return false;
        }

        public static bool ItemCollision(ItemClass item, PlayerClass player)
        {
            Vector2 r = item.GlobalPosition - player.GlobalPosition;

            float r1, r2;
            r1 = 1 / 2.0f * Math.Max(player.Texture.Width, player.Texture.Height);
            r2 = Globals.ITEM_RADIUS;


            if (r.Length() < r1 + r2)
            {
                return true;
            }

            return false;
        }

        
        public static bool LightSaberCollision(LightSaber lightSaber , EnemyClass enemy)
        {
            Vector2 r;

            float r1, r2;
            r1 = lightSaber.Texture.Width / 2;
            r2 = 1 / 2.0f * Math.Max(enemy.Texture.Width, enemy.Texture.Height);

            int n = (int)(lightSaber.Texture.Height / lightSaber.Texture.Width);


            List<Vector2> circles = new List<Vector2>(n);

            for (int i = 0; i < n; i++)
            {
                circles.Add(lightSaber.GlobalPosition - MathFunctions.AngleToVector(lightSaber.Angle) * r1 * (i + 1) * 2);
                r = circles.ElementAt(i) - enemy.GlobalPosition;

                if (r.Length() < r1 + r2)
                {
                    return true;
                }

            }

            //return circles;

            return false;

        }

        //só pra debugar
        /*
        public static void DrawLightSaberCircles(SpriteBatch spriteBatch, LightSaber lightSaber, Texture2D ponto)
        {
            float r1;
            r1 = lightSaber.Texture.Width / 2;
            int n = (int)(lightSaber.Texture.Height / lightSaber.Texture.Width);

            List<Vector2> circles = new List<Vector2>(n);

            for (int i = 0; i < n; i++)
            {
                circles.Add(lightSaber.GlobalPosition - MathFunctions.AngleToVector(lightSaber.Angle) * r1 * (i+1) * 2);
                
            }

            foreach (Vector2 center in circles)
            {
                spriteBatch.Draw(ponto, center, Color.White);
            }

        }
          */



    }
}
