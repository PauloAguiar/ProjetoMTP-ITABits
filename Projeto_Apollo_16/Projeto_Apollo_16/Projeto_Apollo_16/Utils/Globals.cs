using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    static public class Globals
    {
        public const float BACKGROUND_LAYER = 1.0f;
        public const float ENEMY_LAYER = 0.4f;
        public const float PLAYER_LAYER = 0.3f;
        public const float BULLET_LAYER = 0.2f;


        public static Vector2 playerPosition;
        public static Vector2 playerVelocity;

    }
}
