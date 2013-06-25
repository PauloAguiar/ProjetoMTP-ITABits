using System;
using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    public static partial class GameLogic
    {
        const int MIN_TIME_CHANGE_WEAPON = 300;
        static double timeChangedWeapon = MIN_TIME_CHANGE_WEAPON;
        static ProjectileManager projectilesManager;

        private static void CreateBullets()
        {
            if (player.bullets == PlayerClass.Bullets.linear)
            {
                Vector2 v = new Vector2((float)Math.Sin(player.Angle), -(float)Math.Cos(player.Angle));

                LinearProjectile p = new LinearProjectile(player.GlobalPosition, v, content);
                projectilesManager.CreateBullet(p);
            }
            else if (player.bullets == PlayerClass.Bullets.circular)
            {
                CircularProjectile p = new CircularProjectile(player.GlobalPosition, content, player);
                projectilesManager.CreateBullet(p);
            }
            else //homing
            {
                if (enemyManager.Count > 0)
                {

                    HomingProjectile p = new HomingProjectile(player.GlobalPosition, content, CollisionManager.findNearest(player, enemyManager));
                    projectilesManager.CreateBullet(p);
                }
            }

        }

        private static void ShiftBulletsRight()
        {
            timeChangedWeapon = 0;
            player.bullets = (PlayerClass.Bullets)(((int)player.bullets + 1) % PlayerClass.NUMBER_TYPE_BULLETS);
        }

        private static void ShiftBulletsLeft()
        {
            timeChangedWeapon = 0;
            player.bullets = (PlayerClass.Bullets)(((int)player.bullets - 1 + PlayerClass.NUMBER_TYPE_BULLETS) % PlayerClass.NUMBER_TYPE_BULLETS);
        }

    }
}
