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
        }
        private static int NUMBER_TYPE_BULLETS = Enum.GetNames(typeof(Bullets)).Length;


        private static void CreateBullets()
        {
            if (player.bullets == Bullets.linear)
            {
                LinearProjectile p = new LinearProjectile(player.GlobalPosition, player.Direction, content);
                projectilesManager.CreateBullet(p);
            }
            else if (player.bullets == Bullets.circular)
            {
                CircularProjectile p = new CircularProjectile(player.GlobalPosition, content, player);
                projectilesManager.CreateBullet(p);
            }
            else if(player.bullets == Bullets.homing)
            {
                if (enemyManager.Count > 0)
                {

                    HomingProjectile p = new HomingProjectile(player.GlobalPosition, content, CollisionManager.findNearest(player, enemyManager), player.Direction);
                    projectilesManager.CreateBullet(p);
                }
            }
            else if (player.bullets == Bullets.area)
            {
                AreaProjectile p = new AreaProjectile(player.GlobalPosition, player.Direction * player.Speed, content);
                projectilesManager.CreateBullet(p);
            }

        }

        private static void ShiftBulletsRight()
        {
            timeChangedWeapon = 0;
            player.bullets = (Bullets)(((int)player.bullets + 1) % NUMBER_TYPE_BULLETS);
        }

        private static void ShiftBulletsLeft()
        {
            timeChangedWeapon = 0;
            player.bullets = (Bullets)(((int)player.bullets - 1 + NUMBER_TYPE_BULLETS) % NUMBER_TYPE_BULLETS);
        }

    }
}
