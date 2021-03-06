﻿using System;
using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    public static partial class GameLogic
    {
        const int MIN_TIME_CHANGE_WEAPON = 300;
        static double timeChangedWeapon = MIN_TIME_CHANGE_WEAPON;
        static ProjectileManager projectilesManager;
        public enum Bullets
        {
            linear = 0,
            circular,
            homing,
            area,
            lightSaber,
        }
        private static int NUMBER_TYPE_BULLETS = Enum.GetNames(typeof(Bullets)).Length;

        public static void CreateBullets()
        {
            if (player.bullets == Bullets.linear )
            {
                LinearProjectile p = new LinearProjectile(player.GlobalPosition, player.Direction, content);
                projectilesManager.CreateBullet(p);
            }
            else if (player.bullets == Bullets.circular && CircularProjectile.ammo > 0 )
            {
                CircularProjectile p = new CircularProjectile(player.GlobalPosition, content, player);
                projectilesManager.CreateBullet(p);
                CircularProjectile.ammo--; 
            }
            else if(player.bullets == Bullets.homing && HomingProjectile.ammo > 0)
            {
                if (enemyManager.Count > 0)
                {
                    HomingProjectile p = new HomingProjectile(player.GlobalPosition, content, CollisionManager.FindNearest(player, enemyManager), player.Direction);
                    projectilesManager.CreateBullet(p);
                    HomingProjectile.ammo--;
                }
            }
            else if (player.bullets == Bullets.area && AreaProjectile.ammo > 0)
            {
                float ang = 0;
                while (ang < 2 * Math.PI)
                {
                    AreaProjectile p = new AreaProjectile(player.GlobalPosition, MathFunctions.RotateVector(player.Direction , ang), content, player);
                    projectilesManager.CreateBullet(p);
                    ang += (float)Math.PI /4.0f;
                }
                AreaProjectile.ammo-=8;
            }
            else if (player.bullets == Bullets.lightSaber)
            {
                LightSaber ls = new LightSaber(player.GlobalPosition, content, player);
                projectilesManager.CreateBullet(ls);
                //AreaProjectile p = new AreaProjectile(player.GlobalPosition, player.Direction * player.Speed, content);
//                projectilesManager.CreateBullet(p);
            }

        }

        public static void ShiftBulletsRight()
        {
            timeChangedWeapon = 0;
            player.bullets = (Bullets)(((int)player.bullets + 1) % NUMBER_TYPE_BULLETS);
        }

        public static void ShiftBulletsLeft()
        {
            timeChangedWeapon = 0;
            player.bullets = (Bullets)(((int)player.bullets - 1 + NUMBER_TYPE_BULLETS) % NUMBER_TYPE_BULLETS);
        }

    }
}
